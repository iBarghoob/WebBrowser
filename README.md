A functional, multi-user web browser implementation developed in C# using Windows Forms and the .NET framework. 
It is designed to demonstrate core browser functionalities including HTTP communication, asynchronous programming, and persistent database storage.

## Features

- HTML Rendering: Fetches and displays raw HTML content of web pages using asynchronous HTTP GET requests.  

- Navigation: Supports forward/backward navigation using stacks, as well as page reloading.  

- Multi-User Support: Includes a user selection and creation system where each user has separate storage of bookmarks and history.  

- Persistence: Bookmarks, browsing history, and personalized homepages are stored in a local SQLite database and persist across sessions.  

- URL Harvesting: Automatically extracts and displays the first five URLs found on any visited webpage.  

## Prerequisites

- .NET 8.0

## Usage
- Login: Select an existing user account or create a new one from the form.  
- Browse: Enter a URL in the text box and click Search. The raw HTML will appear in the main text area.  
- Bookmarks: Use the right-click context menu in the side panel to add, edit, or delete bookmarks.  
- Settings: Set your current page as your homepage using the dedicated button.
