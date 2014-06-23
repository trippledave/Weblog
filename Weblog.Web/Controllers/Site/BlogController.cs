using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Weblog.Web.Models.Weblog;
using Weblog.Web.Services;
using WebMatrix.WebData;


namespace Weblog.Web.Controllers.Site
{
    public class BlogController : Controller
    {
        private IWeblogService _weblogService = new WeblogService();

        #region Views

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult EditorialDepartment()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AddEntry()
        {
            AddEntryModel model = new AddEntryModel();
            model.CategoriesList = _weblogService.GetCategories();

            return View(model);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddEntry(AddEntryModel model)
        {
            if (ModelState.IsValid)
            {
                this._weblogService.StoreEntry(model);
                return RedirectToAction("AddEntry");
            }
            //model.CategoriesList = _weblogService.GetCategories();
            return View();
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteEntry(int id)
        {
            this._weblogService.DeleteEntry(id);
            return RedirectToAction("Index");
        }

        public ActionResult DisplayEntry(String id)
        {
            int entryId;
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                entryId = Convert.ToInt32(id);
            }
            catch (System.FormatException e)
            {
                return RedirectToAction("Index");
            }

            EntryModel model = _weblogService.GetEntry(entryId);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddCategory(AddCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                if (_weblogService.CategoryExists(model.Name))
                {
                    ModelState.AddModelError("", "Eine Kategorie mit dem gleichen Namen ist bereits vorhanden.");
                }
                else
                {
                    _weblogService.StoreCategory(model);
                    return RedirectToAction("AddCategory");
                }
            }
            return View();
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Administrator")]
        public JsonResult DeleteCategory(int id)
        {
            this._weblogService.DeleteCategory(id);
            return Json(new { success = true });
        }

        #endregion

        #region Partial Views

        public ActionResult DisplayEntries()
        {
            List<EntryModel> entryList = this._weblogService.GetEntries();
            return displayEntriesByX(entryList);
        }

        public ActionResult DisplayEntriesByCategory(int categoryID)
        {
            List<EntryModel> entryList = this._weblogService.GetEntriesByCategory(categoryID);
            return displayEntriesByX(entryList);
        }

        public ActionResult DisplayEntriesByDate(int month, int year)
        {
            List<EntryModel> entryList = this._weblogService.GetEntriesByDate(month, year);
            return displayEntriesByX(entryList);
        }

        private ActionResult displayEntriesByX(List<EntryModel> entryList)
        {
            DisplayEntryPageModel entryPageModel = new DisplayEntryPageModel(entryList);
            DisplayEntriesModel model = new DisplayEntriesModel(entryPageModel);
            return PartialView("DisplayEntries", model);
        }

        public ActionResult DisplayCategories()
        {
            List<CategoryModel> viewModel = this._weblogService.GetCategories();
            return PartialView(viewModel);
        }

        public ActionResult DisplayCommentsForEntry(List<CommentModel> model)
        {
            if (model == null)
            {
                model = new List<CommentModel>();
            }
            return PartialView(model);
        }

        public ActionResult AddComment(int entryId)
        {
            AddCommentModel model = new AddCommentModel();
            return PartialView(model);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public ActionResult AddComment(AddCommentModel model)
        {
            if (ModelState.IsValid)
            {
                this._weblogService.StoreComment(model);
            }
            return RedirectToAction("DisplayEntry", new { id = model.EntryID });
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Administrator")]
        public JsonResult DeleteComment(int id)
        {
            this._weblogService.DeleteComment(id);
            return Json(new { success = true });
        }

        public ActionResult Categories()
        {
            List<CategoryModel> categoryList = _weblogService.GetCategories();
            return PartialView(categoryList);
        }

        public ActionResult Dates()
        {
            List<DateModel> dateList = _weblogService.GetDates();
            return PartialView(dateList);
        }

        public ActionResult About()
        {
            return PartialView();
        }
        #endregion

    }
}
