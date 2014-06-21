using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblog.Web.Services;

namespace Weblog.Web.Models.Weblog
{
    public class DisplayEntriesModel
    {
        public int CurrentPage { get; set; }
        public int EntriesPerSite { get; set; }
        public List<EntryModel> EntryList;

        public DisplayEntriesModel()
        {
            ISettingsService _settingsService = new SettingsService();
            this.EntriesPerSite = _settingsService.GetSiteSettings().EntriesPerSite;
            EntryList = new List<EntryModel>();
            CurrentPage = 1;
        }

        public DisplayEntriesModel(List<EntryModel> entryList)
        {
            ISettingsService _settingsService = new SettingsService();
            this.EntriesPerSite = _settingsService.GetSiteSettings().EntriesPerSite;
            this.EntryList = entryList;
            this.CurrentPage = 2;
        }

    }
}
