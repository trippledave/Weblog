using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblog.Web.Services;

namespace Weblog.Web.Models.Weblog
{
    public class DisplayEntryPageModel
    {
        public int FullEntriesPerSite { get; set; }
        public List<EntryModel> Entries { get; set; }

        public DisplayEntryPageModel(List<EntryModel> entries)
        {
            ISettingsService _settingsService = new SettingsService();
            this.FullEntriesPerSite = _settingsService.GetSiteSettings().FullEntriesPerSite;
            this.Entries = entries;
        }

        public DisplayEntryPageModel()
        {
            ISettingsService _settingsService = new SettingsService();
            this.FullEntriesPerSite = _settingsService.GetSiteSettings().FullEntriesPerSite;
            this.Entries = new List<EntryModel>();
        }

    }
}
