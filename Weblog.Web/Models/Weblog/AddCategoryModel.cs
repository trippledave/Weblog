using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Weblog.Core.DataAccess.Weblog;

namespace Weblog.Web.Models.Weblog
{
    public class AddCategoryModel
    {

        public int ID { get; set; }
        [DisplayName("Name")]
        [Required]
        public string Name { get; set; }

        private void UpdateModel( Category source )
        {
            this.ID = source.CategoryID;
            this.Name = source.Name;
        }

        public void UpdateSource( Category source )
        {
            source.Name = this.Name;
        }

        public AddCategoryModel() { }

        public AddCategoryModel( Category source )
        {
            UpdateModel( source );
        }

    }
}