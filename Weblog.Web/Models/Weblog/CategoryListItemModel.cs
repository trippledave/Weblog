﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Weblog.Core.DataAccess.Weblog;

namespace Weblog.Web.Models.Weblog
{
    public class CategoryListItemModel
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

        public CategoryListItemModel(Category source)
        {
            UpdateModel(source);
        }

        public CategoryListItemModel()
        {
        }
    }
}