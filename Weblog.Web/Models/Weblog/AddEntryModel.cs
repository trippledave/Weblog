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
using Weblog.Core.Helpers;

namespace Weblog.Web.Models.Weblog
{
    public class AddEntryModel
    {
        public int ID { get; set; }
        [DisplayName("Titel")]
        [Required]
        [RegularExpression(InputFilterHelper.FilterHtmlTagsRegex, ErrorMessage = "Der Text enthält Zeichen, die nicht eingegeben werden dürfen.")]
        [StringLength(50)]
        public string Title { get; set; }
        [DisplayName("Text")]
        [Required]
        public string Text { get; set; }
        public List<CategoryModel> CategoriesList { get; set; }

        private void UpdateModel(Entry source)
        {
            this.ID = source.EntryID;
            this.Title = source.Title;
            this.Text = source.Text;
        }

        public void UpdateSource(Entry source)
        {
            source.Title = this.Title;
            source.Text = this.Text;
        }

        public AddEntryModel()
        {
        }

        public AddEntryModel(Entry source)
        {
            UpdateModel(source);
        }
    }
}