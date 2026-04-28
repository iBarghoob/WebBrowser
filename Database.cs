using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBrowser
{
    /// <summary>
    /// This class defines the database models
    /// </summary>

    // A user entity with associated bookmarks and history
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string HomePage { get; set; }

        public virtual List<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
        public virtual List<HistoryEntry> History { get; set; } = new List<HistoryEntry>();
    }

    // A bookmark entity consisting of name, url and the associated User
    public class Bookmark
    {
        [Key]
        public int BookmarkId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }

        public int UserId { get; set; }
        // nav property, refering user object
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }

    // A history entity (entry) consisting of a url, timestamp and associated User
    public class HistoryEntry
    {
        [Key]
        public int HistoryId { get; set; }
        [Required]
        public string Url { get; set; } = string.Empty;
        public DateTime VisitedAt { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
