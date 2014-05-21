using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblog.Web.Models.Weblog;
using Weblog.Web.Services;


namespace Weblog.Web.Controllers.Site
{
    public class HomeController : Controller
    {
        private IWeblogService _weblogService = new WeblogService();

        #region Views

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEntry()
        {
            return View();
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public ActionResult AddEntry(AddEntryModel model)
        {
            this._weblogService.StoreEntry(model);
            return View();
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public ActionResult AddCategory(AddCategoryModel model)
        {
            this._weblogService.StoreCategory(model);
            return View();
        }

        #endregion

        #region Partial Views

        public ActionResult DisplayEntries()
        {
            // Liefert eine Partial-View mit den bereits existierenden Einträgen
            List<EntryListItemModel> viewModel = this._weblogService.GetEntries();
            return PartialView(viewModel);
        }

        public ActionResult DisplayCategories()
        {
            List<CategoryListItemModel> viewModel = this._weblogService.GetCategories();
            return PartialView(viewModel);
        }

        #endregion

    }
}
