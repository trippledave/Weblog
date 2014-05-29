using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Weblog.Core.DataAccess.Weblog;
using Weblog.Web.Models.Account;

namespace Weblog.Web.Models.Weblog
{
    public class EntryListItemModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }

        public string DateString
        {
            get
            {
                return Date.ToShortDateString() + ", um " + Date.ToShortTimeString();
            }
        }

        private void UpdateModel( Entry source )
        {
            this.ID = source.EntryID;
            this.Title = source.Header;
            this.Text = source.Body;
            this.Date = source.DateCreated;
            if (source.Users != null)
                this.Author = source.Users.UserName;
            //this.Author = source.AuthorID; works
        }

        public EntryListItemModel()
        {
        }

        public EntryListItemModel( Entry source )
        {
            UpdateModel( source );
        }

    }
}