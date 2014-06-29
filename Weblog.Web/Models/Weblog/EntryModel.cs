using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Weblog.Core.DataAccess.Weblog;
using Weblog.Web.Models.Account;
using Weblog.Web.Services;

namespace Weblog.Web.Models.Weblog
{
    public class EntryModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public bool AllowComments { get; set; }

        private IWeblogService _weblogService = new WeblogService();

        public string DateString
        {
            get
            {
                string result = Date.ToShortDateString() + ", um " + Date.ToShortTimeString();
                return result;
            }
        }

        public string TextShort
        {
            get
            {
                string shortString;
                int textLength = 2;
                if (Text.Length > textLength)
                    shortString = Text.Substring(0, textLength) + "...";
                else
                    shortString = Text;

                return shortString;
            }
        }

        private void UpdateModel(Entry source)
        {
            this.ID = source.EntryID;
            this.Title = source.Title;
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
            this.Categories= _weblogService.GetCategoriesForEntry(source.EntryID);
            ISettingsService settingsService = new SettingsService();
            this.AllowComments = settingsService.GetSiteSettings().AllowComments;
        }

        public EntryModel()
        {
        }

        public EntryModel(Entry source)
        {
            UpdateModel(source);
        }

    }
}