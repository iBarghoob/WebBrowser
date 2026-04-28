using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBrowser
{
    /// <summary>
    /// Handles bookmark operations for specified user, each object instantiated using userId.
    /// Uses LINQ queries for getting, adding, removing, and editing bookmarks from sqlite database.
    /// </summary>
    public class BookmarkManager
    {
        private int userId;
        public BookmarkManager(int userId)
        {
            this.userId = userId;
        }

        /// <summary>
        /// Get bookmarks for current user using LINQ queries
        /// </summary>
        public List<Bookmark> Bookmarks
        {
            get
            {
                using (var context = new BrowserContext())
                {
                    return context.Bookmarks.Where(b => b.UserId == userId)
                        .OrderBy(b => b.Name)
                        .ToList();
                }
            }
        }

        /// <summary>
        /// Add bookmark entry (name, url, userid) for current user
        /// </summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        public void AddBookmark(string name, string url)
        {
            using (var context = new BrowserContext())
            {
                var bookmark = new Bookmark
                {
                    Name = name,
                    Url = url,
                    UserId = userId
                };

                context.Bookmarks.Add(bookmark);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Remove a bookmark using name
        /// </summary>
        /// <param name="name"></param>
        public void RemoveBookmark(string name)
        {
            using (var context = new BrowserContext())
            {
                var bookmark = context.Bookmarks.FirstOrDefault(
                    b => b.Name == name && b.UserId == userId);

                if (bookmark != null)
                {
                    context.Bookmarks.Remove(bookmark);
                    context.SaveChanges();
                }
            }
        }
        
        /// <summary>
        /// Edit existing bookmark, update name and url
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        /// <param name="url"></param>
        public void EditBookmark(string oldName, string newName, string url)
        {
            using (var context = new BrowserContext())
            {
                var bookmark = context.Bookmarks.FirstOrDefault(
                    b => b.Name == oldName && b.UserId == userId);

                if (bookmark != null)
                {
                    bookmark.Name = newName;
                    bookmark.Url = url;
                    context.SaveChanges();
                }
            }
        }
    }
}
