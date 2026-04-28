using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyBrowser
{
    /// <summary>
    /// Small form to allow users to choose account and login to proceed to browser form
    /// </summary>
    public partial class UserSelectionForm : Form
    {
        public int SelectedUserId { get; set; } = -1;

        public UserSelectionForm()
        {
            InitializeComponent();
            LoadUsers();
        }

        // class to store user info in combo bo
        private class UserItem
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
        }

        private void LoadUsers()
        {
            userComboBox.Items.Clear();

            // get users and fill the combo box
            using (var context = new BrowserContext())
            {
                var users = context.Users.OrderBy(u => u.UserName).ToList();
                foreach (var user in users)
                {
                    userComboBox.Items.Add(new UserItem
                    {
                        UserId = user.UserId,
                        UserName = user.UserName
                    });
                }
                if (userComboBox.Items.Count > 0)
                {
                    userComboBox.SelectedIndex = 0;
                }
            }
            userComboBox.DisplayMember = "UserName";
        }

        // select user from drop down menu
        private void selectUserbtn_Click(object sender, EventArgs e)
        {
            if (userComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a user to login.");
                return;
            }

            var selectedUser = (UserItem)userComboBox.SelectedItem;
            SelectedUserId = selectedUser.UserId;
            // proceed to browser form
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // allow user to create a user, enter username via interaction box
        private void createUserbtn_Click(object sender, EventArgs e)
        {
            string username = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter Username:",
                "Create a New Account");

            if (string.IsNullOrWhiteSpace(username))
            {
                return;
            }

            using (var context = new BrowserContext())
            {
                // check if user already exsits
                if (context.Users.Any(u => u.UserName == username))
                {
                    MessageBox.Show("Username already exists.", "Username taken");
                    return;
                }

                // create a new user and add to db
                var newUser = new User
                {
                    UserName = username,
                    HomePage = "http://hw.ac.uk"
                };

                context.Users.Add(newUser);
                context.SaveChanges();

                MessageBox.Show($"User '{username}' created successfully.",
                    "Success");

                LoadUsers();

                // cast items to UserItem and select the created user
                for (int i = 0; i < userComboBox.Items.Count; i++)
                {
                    var item = (UserItem)userComboBox.Items[i];
                    if (item.UserId == newUser.UserId)
                    {
                        userComboBox.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        
        // allow user to delete a selected account and prompt for action confirmation
        private void deleteUserbtn_Click(object sender, EventArgs e)
        {
            if (userComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a user to delete.");
                return;
            }

            var selectedUser = (UserItem)userComboBox.SelectedItem;

            // prompt user confirmation
            var confirm = MessageBox.Show($"Are you sure you want to delete user '{selectedUser.UserName}'?",
                "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                using (var context = new BrowserContext())
                {
                    // get user by selected id and delete from db
                    var userToDel = context.Users.FirstOrDefault(u => u.UserId == selectedUser.UserId);
                    if (userToDel != null)
                    {
                        context.Users.Remove(userToDel);
                        context.SaveChanges();
                        MessageBox.Show($"User {selectedUser.UserName} deleted.", "Success");
                    } else
                    {
                        MessageBox.Show($"User not found in database.", "Error");
                    }
                }
                LoadUsers();
            }
        }
    }
}
