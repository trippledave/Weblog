using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weblog.Core.DataAccess.Weblog;

namespace Weblog.Web.Models.Weblog
{
    public class CommentListItemModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }

        public string DateString
        {
            get
            {
                string result = String.Format( "{0:D}, um {0:t} Uhr", Date.Date );
                return result;
            }
        }

        private void UpdateModel( Comment source )
        {
            this.ID = source.EntryID;
           // this.Title = source.Header;
            this.Date = source.DateCreated;

        }

        public CommentListItemModel()
        {
        }

        public CommentListItemModel(Comment source)
        {
            UpdateModel( source );
        }

    }
}