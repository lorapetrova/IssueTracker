using IssueTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IssueTracker.Entities
{
    public class Issue
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [ForeignKey("AuthorId")]
        public virtual ApplicationUser Author { get; set; }
        public string AuthorId { get; set; }
        public IssueState State { get; set; }
        public DateTime SubmitionDate { get; set; }
    }
}