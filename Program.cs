namespace MyBrowser
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            ApplicationConfiguration.Initialize();

            InitializeDatabase();

            // create a UserSelectionForm to allow user to choose an account / create account
            using (var userSelectionForm = new UserSelectionForm())
            {
                if (userSelectionForm.ShowDialog() == DialogResult.OK)
                {
                    // proceed to browser form with select account's ID 
                    int selectedUserId = userSelectionForm.SelectedUserId;

                    if (selectedUserId > 0 )
                    {
                        Application.Run(new Browser(selectedUserId));
                    }
                }
            }
        }

        /// <summary>
        /// ensure the db file exists and initialize default user account
        /// </summary>
        private static void InitializeDatabase()
        {
            using (var context = new BrowserContext())
            {
                // initialise db file and tables
                context.Database.EnsureCreated();

                // if no existing users, create a default user account
                if (!context.Users.Any())
                {
                    var defaultUser = new User
                    {
                        UserName = "DefaultUser",
                        HomePage = "http://hw.ac.uk"
                    };

                    context.Users.Add(defaultUser);
                    context.SaveChanges();
                }
            }
        }
    }
}