using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Weblog.Core.DataAccess.Weblog;

namespace Weblog.Web.Models.Weblog
{
    public class CategoryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool isElementsCategory { get; set; }

        public void UpdateModel(Category source)
        {
            this.ID = source.CategoryID;
            this.Name = source.Name;
        }

        public void UpdateSource(Category source)
        {
            source.CategoryID = this.ID;
            source.Name = this.Name;
        }

        public CategoryModel(Category source)
        {
            UpdateModel(source);
        }

        public CategoryModel()
        {
        }
    }
}