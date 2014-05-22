using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weblog.Core.DataAccess.Weblog;

namespace Weblog.Web.Models.Weblog
{
    public class AddCommentModel
    {
        public int ID { get; set; }
        [DisplayName("Text")]
        [Required]
        public string Body { get; set; }
        public int Author { get; set; }

        private void UpdateModel( Entry source )
        {
            this.ID = source.EntryID;
            this.Body = source.Body;
            this.Author = source.AuthorID;
        }

        public AddCommentModel() {
        }

        public AddCommentModel(Entry source)
        {
            UpdateModel( source );
        }
    }
}