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
        public int EntriesPerSite { get; set; }
        public int LastPage { get; set; }
        public DisplayEntryPageModel EntryPerPageModel { get; set; }

        public DisplayEntriesModel()
        {
            ISettingsService _settingsService = new SettingsService();
            this.EntriesPerSite = _settingsService.GetSiteSettings().EntriesPerSite;
            this.LastPage = 0;
            this.EntryPerPageModel = new DisplayEntryPageModel();
        }

        public DisplayEntriesModel(DisplayEntryPageModel EntryPerPageModel)
        {
            ISettingsService _settingsService = new SettingsService();
            this.EntriesPerSite = _settingsService.GetSiteSettings().EntriesPerSite;
            this.LastPage = (int)Math.Ceiling(((double)EntryPerPageModel.Entries.Count) / ((double)EntriesPerSite)) - 1;
            this.EntryPerPageModel = EntryPerPageModel;
        }
    }
}