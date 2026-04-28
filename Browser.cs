using Microsoft.VisualBasic;
using System;
using System.Security.Policy;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MyBrowser
{
    public partial class Browser : Form
    {
        private int currentUserId;
        private string lastUrl = null;
        private string homepageUrl;

        private BookmarkManager bookmarkManager;
        private HistoryManager historyManager;

        // two stacks: one for forward-nav, one for backwards-nav
        private Stack<string> backStack = new Stack<string>();
        private Stack<string> forwardStack = new Stack<string>();

        public Browser(int userId)
        {
            InitializeComponent();
            InitializeContextMenus();

            // initialise current user by id and get their bookmarks and history
            currentUserId = userId;
            using (var context = new BrowserContext())
            {
                var user = context.Users.FirstOrDefault(u => u.UserId == currentUserId);
                userLabel.Text = $"Current User: {user.UserName}";
            }
            
            bookmarkManager = new BookmarkManager(userId);
            historyManager = new HistoryManager(userId);

            LoadHomepage();
            RefreshBookmarksList();
            RefreshHistoryList();
        }

        // context menu for allowing user to right click bookmarks (edit, delete)
        private void InitializeContextMenus()
        {
            contextMenuStrip1 = new ContextMenuStrip();
            contextMenuStrip1.Items.Add("Delete bookmark", null, DeleteBookmark_Click);
            contextMenuStrip1.Items.Add("Edit bookmark", null, EditBookmark_Click);

            // attach context menu to the bookmarks ListBox
            BookmarksList.ContextMenuStrip = contextMenuStrip1;
            BookmarksList.MouseDown += BookmarksList_MouseDown;
        }

        private async void LoadHomepage()
        {
            using (var context = new BrowserContext())
            {
                // get user using their id and the default home page
                var user = context.Users.FirstOrDefault(u => u.UserId == currentUserId);

                if (user != null && !string.IsNullOrWhiteSpace(user.HomePage))
                {
                    homepageUrl = user.HomePage;
                } 
                else
                {
                    // set default homepage if not already set
                    homepageUrl = "http://hw.ac.uk";
                    if (user != null)
                    {
                        user.HomePage = homepageUrl;
                        context.SaveChanges();
                    }
                }
            }
            textBox1.Text = homepageUrl;
            await LoadPage(homepageUrl, true);
        }

        /// <summary>
        /// Fetches a given webpage using GetWebPage
        /// Updates GUI with returned html
        /// Adds to history based on flag parameter
        /// </summary>
        /// <param name="url"></param>
        /// <param name="addToHistory"></param>
        /// <returns></returns>
        private async Task LoadPage(string url, bool addToHistory = true)
        {
            // ensure valid http scheme
            url = SanitizeUrl(url);

            try
            {
                Text = "Loading...";
                richTextBox1.Text = "loading page...";
                title_label.Text = "";
                
                var result = await Request.GetWebPage(url).ConfigureAwait(true);
                if (result == null)
                {
                    MessageBox.Show("No response received.", "Request Failed");
                    Text = "Request Failed";
                    return;
                }

                richTextBox1.Text = result.HtmlContent ?? "No HTML to display";
                title_label.Text = result.Title.ToString() ?? "";
                Text = $"{result.StatusCode} - {result.Title ?? ""}";

                // call ExtractLinks on the html to harvest first 5 links (the href attribute for <a> tags)
                var links = ExtractLinks(result.HtmlContent, url);
                RefreshLinksList(links);
                if (result.Success)
                {
                    lastUrl = url;
                    if (addToHistory)
                    {
                        historyManager.AddPage(url);
                        RefreshHistoryList();
                    }
                }
                else
                {
                    MessageBox.Show($"Request failed, status: {result.StatusCode}", "Request failed");
                }
            } catch (Exception)
            {
                MessageBox.Show($"Error: Could not connect to requested webpage", "Error");
                Text = "Error";
            }
        }

        // UI REFRESH FUNCTIONS

        // Refresh the history list on GUI and order history to show most recent page on top of list
        private void RefreshHistoryList()
        {
            HistoryList.Items.Clear();
            foreach (var item in historyManager.History.AsEnumerable().Reverse())
            {
                HistoryList.Items.Add(item);
            }
        }

        // Refresh bookmarks list on GUI
        private void RefreshBookmarksList()
        {
            BookmarksList.Items.Clear();
            foreach (var bookmark in bookmarkManager.Bookmarks)
            {
                BookmarksList.Items.Add(bookmark.Name);
            }
        }

        // Refresh the list of 5 links on GUI for the page visited
        private void RefreshLinksList(List<string> links)
        {
            LinksList.Items.Clear();
            foreach (var link in links)
            {
                LinksList.Items.Add(link);
            }
        }

        // SEARCH AND NAVIGATION

        // calls GetWebPage on url in textbox1 and updates richtextbox1 with returned html
        private async void search_btn_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(url) || url == "http://")
            {
                MessageBox.Show("Enter a url to search.");
                return;
            }

            if (lastUrl != null)
            {
                // push current page to stack and clear the forward history since a new search was done
                backStack.Push(lastUrl);
                forwardStack.Clear();
            }

            await LoadPage(url, true);
        }

        private async void reload_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lastUrl))
            {
                return;
            }

            // empty the textbox to visually inform user of reload
            richTextBox1.Text = "";
            Text = "Reloading...";
            await LoadPage(lastUrl, false);
        }

        private async void BackNav_btn_Click(object sender, EventArgs e)
        {
            if (backStack.Count == 0)
            {
                MessageBox.Show("No previous pages.");
                return;
            }

            // get preceding url and push current page to forward stack
            string previousUrl = backStack.Pop();
            if (previousUrl == lastUrl)
            {
                return;
            }
            forwardStack.Push(lastUrl);
            textBox1.Text = previousUrl;
            await LoadPage(previousUrl, true);
        }

        private async void ForwardNav_btn_Click(object sender, EventArgs e)
        {
            if (forwardStack.Count == 0)
            {
                MessageBox.Show("No pages forward.");
                return;
            }

            // push current page to back stack and get succeeding url
            string nextUrl = forwardStack.Pop();
            if (nextUrl == lastUrl)
            {
                return;
            }
            backStack.Push(lastUrl);
            textBox1.Text = nextUrl;
            await LoadPage(nextUrl, true);
        }

        // HISTORY

        // Loads selected web page in history list on double click
        private async void HistoryList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = HistoryList.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches && e.Button == MouseButtons.Left)
            {
                string selectedUrl = HistoryList.Items[index].ToString();
                textBox1.Text = selectedUrl;

                // clear forward stack when user goes back in history
                if (lastUrl != null)
                {
                    backStack.Push(lastUrl);
                    forwardStack.Clear();
                }
                await LoadPage(selectedUrl, true);
            }
        }

        // BOOKMARKS

        // take current url in textbox1 as url to save in bookmark
        private void addBookmark_btn_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text.Trim();
            url = SanitizeUrl(url);
            if (string.IsNullOrEmpty(url))
            {
                MessageBox.Show("Enter a valid URL to save as bookmark!");
                return;
            }

            // dialog box to enable user to give bookmark a name
            string name = Interaction.InputBox("Enter a name for this bookmark", "Add Bookmark", "New Bookmark");

            if (!string.IsNullOrWhiteSpace(name))
            {
                bookmarkManager.AddBookmark(name, url);
                RefreshBookmarksList();
                MessageBox.Show($"Bookmark: {name} added successfully.");
            }
        }

        // use two dialog boxes to allow user to edit the name and url of a selected bookmark 
        private void EditBookmark_Click(object? sender, EventArgs e)
        {
            if (BookmarksList.SelectedItem == null)
            {
                MessageBox.Show("Select a bookmark to edit.");
                return;
            }

            var oldName = BookmarksList.SelectedItem.ToString();
            var bookmark = bookmarkManager.Bookmarks.FirstOrDefault(b => b.Name == oldName);

            if (bookmark != null)
            {
                // begin with prompt for new name
                string newName = Interaction.InputBox("Update name:", "Edit bookmark", bookmark.Name);
                if (string.IsNullOrWhiteSpace(newName)) return;

                // then prompt user for new url
                string newUrl = Interaction.InputBox("Update URL:", "Edit bookmark", bookmark.Url);
                if (string.IsNullOrWhiteSpace(newUrl)) return;
                newUrl = SanitizeUrl(newUrl);

                // update bookmark and refresh list
                bookmark.Url = newUrl;
                bookmarkManager.EditBookmark(oldName, newName, newUrl);
                RefreshBookmarksList();
                MessageBox.Show($"Bookmark {newName} updated.");
            }
        }

        // delete selected bookmark
        private void DeleteBookmark_Click(object? sender, EventArgs e)
        {
            if (BookmarksList.SelectedItem == null)
            {
                MessageBox.Show("Select a bookmark to delete.");
                return;
            }

            var name = BookmarksList.SelectedItem.ToString();

            // dialog box to confirm deletion
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete this bookmark '{name}'?",
                "Delete Bookmark",
                MessageBoxButtons.OKCancel);

            // remove bookmark from db (user's list)
            if (result == DialogResult.OK)
            {
                bookmarkManager.RemoveBookmark(name);
                RefreshBookmarksList();
                MessageBox.Show($"Bookmark {name} deleted.");
            }
        }

        // right click event for selecting bookmarks (edit, delete)
        private void BookmarksList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = BookmarksList.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    BookmarksList.SelectedIndex = index; // select item right clicked under cursor
                }
            }
        }

        // allow user to double click bookmark and load web page using associated url
        private async void BookmarksList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // get mouse click coords and get index of item at that position
            int index = BookmarksList.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches && e.Button == MouseButtons.Left)
            {
                string selectedName = BookmarksList.Items[index].ToString();
                var bookmark = bookmarkManager.Bookmarks.FirstOrDefault(b => b.Name == selectedName);
                if (bookmark != null)
                {
                    textBox1.Text = bookmark.Url;
                    if (lastUrl != null)
                    {
                        // push current page to stack and clear the forward history since a new search was done
                        backStack.Push(lastUrl);
                        forwardStack.Clear();
                    }
                    await LoadPage(bookmark.Url, true);
                }
            }
        }

        // HOMEPAGE

        // button to save the url currently in textbox1 as the default home page
        private void setHomepage_btn_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text.Trim();
            url = SanitizeUrl(url);
            if (!string.IsNullOrEmpty(url))
            {
                // check if already saved as home page
                if (url == homepageUrl)
                {
                    MessageBox.Show($"{url} is already set as the default home page!");
                    return;
                }

                // get user by id and update their home page in db
                using (var context = new BrowserContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.UserId == currentUserId);
                    if (user != null)
                    {
                        user.HomePage = url;
                        context.SaveChanges();
                        homepageUrl = url;
                        MessageBox.Show($"Home page set to: {homepageUrl} for {user.UserName}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Enter a valid URL to save!");
            }
        }
        
        // LINKS

        // allow user to double click one of the five harvested links on the webpage
        private async void LinksList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = LinksList.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches && e.Button == MouseButtons.Left)
            {
                string selectedUrl = LinksList.Items[index].ToString();
                selectedUrl = SanitizeUrl(selectedUrl);
                textBox1.Text = selectedUrl;
                if (lastUrl != null)
                {
                    // push current page to stack and clear the forward history since a new search was done
                    backStack.Push(lastUrl);
                    forwardStack.Clear();
                }
                await LoadPage(selectedUrl, true);
            }
        }

        /// <summary>
        /// Helper function that uses the HTML Agility Pack document parser to 
        /// extract the first 5 URLs found in a given html string
        /// urls pointing to the same page are converted to full urls
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private List<string> ExtractLinks(string html, string baseUrl)
        {
            var links = new List<string>();
            if (string.IsNullOrEmpty(html) || string.IsNullOrEmpty(baseUrl))
                return links;

            // parse html document
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(html);

            // get all <a> tags with href attributes
            var nodes = htmlDoc.DocumentNode.SelectNodes("//a[@href]");
            if (nodes == null) return links;

            Uri baseUri = new Uri(baseUrl);
            foreach (var node in nodes)
            {
                string href = node.GetAttributeValue("href", string.Empty)?.Trim();
                if (string.IsNullOrWhiteSpace(href)) continue;

                // skip html anchors and javascript links
                if (href.StartsWith("#") || href.StartsWith("javascript", StringComparison.OrdinalIgnoreCase))
                    continue;

                try
                {
                    // convert relative urls (on the same web page) to complete urls
                    Uri fullUri = new Uri(baseUri, href);

                    // only allow http, https schemes
                    if (fullUri.Scheme != Uri.UriSchemeHttp && fullUri.Scheme != Uri.UriSchemeHttps)
                        continue;

                    // prevent duplicates
                    string absoluteUrl = fullUri.ToString();
                    if (!links.Contains(absoluteUrl))
                    {
                        links.Add(absoluteUrl);
                        if (links.Count == 5) // stop at 5 links
                            break;
                    }
                } catch
                {
                    continue; // skip broken urls
                }
            }
            return links;
        }

        // helper method used for ensuring that a given URL has the proper scheme (http:// or https://)
        private string SanitizeUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return url;

            url = url.Trim();

            // already has a scheme
            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                return url;
            }

            // just prepend http:// if there is no scheme
            return "http://" + url;
        }
    }
}
