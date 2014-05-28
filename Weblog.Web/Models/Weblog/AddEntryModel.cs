using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weblog.Core.DataAccess.Weblog;
using System.Diagnostics;
using Weblog.Web.Models.Account;
using WebMatrix.WebData;
using Weblog.Web.Services;

namespace Weblog.Web.Models.Weblog
{
    public class AddEntryModel
    {
        public int ID { get; set; }
        [DisplayName("Titel")]
        [Required]
        public string Header { get; set; }
        [DisplayName("Text")]
        [Required]
        public string Body { get; set; }
        public List<CategoryListItemModel> CategoriesList { get; set; }


        private void UpdateModel( Entry source )
        {
            this.ID = source.EntryID;
            this.Header = source.Header;
            this.Body = source.Body;
        }

        public void UpdateSource( Entry source )
        {
            source.Header = this.Header;
            source.Body = this.Body;    
        }

        public AddEntryModel() {
        }

        public AddEntryModel( Entry source )
        {
            UpdateModel( source );
        }
    }
}