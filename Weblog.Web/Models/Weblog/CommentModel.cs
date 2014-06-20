using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weblog.Core.DataAccess.Weblog;

namespace Weblog.Web.Models.Weblog
{
    public class CommentModel
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public String Author { get; set; }

        public string DateString
        {
            get
            {
                string result = Date.ToShortDateString() + ", um " + Date.ToShortTimeString();
                return result;
            }
        }

        private void UpdateModel( Comment source )
        {
            this.ID = source.CommentID;
            this.Text = source.Text;
            this.Date = source.DateCreated;
            if (source.User.DisplayName != null)
            {
                this.Author = source.User.DisplayName;
            }
            else
            {
                this.Author = source.User.UserName;
            }

        }

        public CommentModel()
        {
        }

        public CommentModel(Comment source)
        {
            UpdateModel( source );
        }

    }
}