using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblog.Web.Services;

namespace Weblog.Web.Models.Weblog
{
    public class DisplayEntriesPageModel
    {
        public int EntriesPerSite { get; set; }
        public int FullEntriesPerSite { get; set; }
        public List<EntryModel> Entries { get; set; }

        private ISettingsService _settingsService = new SettingsService();

        public DisplayEntriesPageModel(List<EntryModel> entries)
        {
            this.EntriesPerSite = _settingsService.GetSiteSettings().EntriesPerSite;
            this.FullEntriesPerSite = _settingsService.GetSiteSettings().FullEntriesPerSite;
            this.Entries = entries;
        }

        public DisplayEntriesPageModel()
        {
            this.EntriesPerSite = _settingsService.GetSiteSettings().EntriesPerSite;
            this.FullEntriesPerSite = _settingsService.GetSiteSettings().FullEntriesPerSite;
            this.Entries = new List<EntryModel>();
        }

    }
}
