using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyBrowser
{
    /// <summary>
    /// Handles history for a specific user, each object instantiated with userId.
    /// Uses LINQ queries to get user history and to add entries to history list.
    /// </summary>
    public class HistoryManager
    {
        private int userId;

        public HistoryManager(int userId)
        {
            this.userId = userId;
        }

        /// <summary>
        /// LINQ query to get user's history of URLs ordered by oldest first
        /// </summary>
        public List<string> History
        {
            get
            {
                using (var context = new BrowserContext())
                {
                    return context.History.Where(h => h.UserId == userId)
                        .OrderBy(h => h.VisitedAt)
                        .Select(h => h.Url) 
                        .ToList();
                }
            }
        }

        /// <summary>
        /// Add page entry to history, prevent consecutive duplicates by checking if last entry is same URL
        /// </summary>
        /// <param name="url"></param>
        public void AddPage(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return;

            using (var context = new BrowserContext())
            {
                var lastEntry = context.History.Where(h => h.UserId == userId)
                    .OrderByDescending(h => h.VisitedAt)
                    .FirstOrDefault();

                if (lastEntry == null || lastEntry.Url != url)
                {
                    var historyEntry = new HistoryEntry
                    {
                        Url = url,
                        UserId = userId,
                        VisitedAt = DateTime.Now
                    };
                    context.History.Add(historyEntry);
                    context.SaveChanges();
                }
            }
        }
    }
}
