﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IssueTracker.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        [ForeignKey("IssueId")]
        public virtual Issue Issue { get; set; }
        public int IssueId { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
    }
}