using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Weblog.Core.Helpers;
using Weblog.Web.Models.Weblog;
using Weblog.Web.Services;
using WebMatrix.WebData;


namespace Weblog.Web.Controllers.Site
{
    public class BlogController : Controller
    {
        private IWeblogService _weblogService = new WeblogService();
        private ISettingsService _settingsService = new SettingsService();

        #region Views

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult EditorialDepartment()
        {
            return View();
        }

        #region Entry
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
        [ValidateInput(false)] //html tags, will be removed,  newline will be <br/>
        public ActionResult AddEntry(AddEntryModel model)
        {
            if (ModelState.IsValid)
            {
                model.Text = InputFilterHelper.sanitizeInput(model.Text);
                this._weblogService.StoreEntry(model);
                return RedirectToAction("Index");
            }
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

        public ActionResult DisplayEntry(String entryID)
        {
            int intEntry;
            if (entryID == null)
            {
                return RedirectToAction("Index", "Errors", null);
            }
            try
            {
                intEntry = Convert.ToInt32(entryID);
            }
            catch (System.FormatException)
            {
                return RedirectToAction("Index", "Errors", null);
            }

            EntryModel model = _weblogService.GetEntry(intEntry);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        #endregion

        #region Category
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

        #endregion

        #region Partial Views

        #region Entry
        public ActionResult DisplayEntries()
        {
            List<EntryModel> entryList = this._weblogService.GetEntries();
            return displayEntriesByX(entryList);
        }

        public ActionResult DisplayEntriesByCategory(String id)
        {
            int categoryID;
            if (id == null)
            {
                return RedirectToAction("Index", "Errors", null);
            }
            try
            {
                categoryID = Convert.ToInt32(id);
            }
            catch (System.FormatException)
            {
                return RedirectToAction("Index", "Errors", null);
            }
            List<EntryModel> entryList = this._weblogService.GetEntriesByCategory(categoryID);
            return displayEntriesByX(entryList);
        }

        public ActionResult DisplayEntriesByDate(String month, String year)
        {
            List<EntryModel> entryList = new List<EntryModel>();
            int intMonth, intYear;
            if (month == null || year == null)
            {
                return RedirectToAction("Index", "Errors", null);
            }
            try
            {
                intMonth = Convert.ToInt32(month);
                intYear = Convert.ToInt32(year);
            }
            catch (System.FormatException)
            {
                return RedirectToAction("Index", "Errors", null);
            }

            try
            {
                entryList = this._weblogService.GetEntriesByDate(intMonth, intYear);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return RedirectToAction("Index", "Errors", null);
            }
            return displayEntriesByX(entryList);
        }

        private ActionResult displayEntriesByX(List<EntryModel> entryList)
        {
            DisplayEntriesPageModel entryPageModel = new DisplayEntriesPageModel(entryList);
            DisplayEntriesModel model = new DisplayEntriesModel(entryPageModel);
            return PartialView("DisplayEntries", model);
        }

        #endregion

        #region Category
        public ActionResult DisplayCategories()
        {
            List<CategoryModel> viewModel = this._weblogService.GetCategories();
            return PartialView("PartialViews/DisplayCategories", viewModel);
        }
        #endregion

        #region Comments

        public ActionResult DisplayCommentsForEntry(String id)
        {
            if (_settingsService.GetSiteSettings().AllowComments)
            {
                List<CommentModel> commentList;
                int entryID;
                if (id == null)
                {
                    return RedirectToAction("Index", "Errors", null);
                }
                try
                {
                    entryID = Convert.ToInt32(id);
                }
                catch (System.FormatException)
                {
                    return RedirectToAction("Index", "Errors", null);
                }

                commentList = _weblogService.GetCommentsForEntry(entryID);


                if (commentList.Count == 0)
                {
                    commentList = new List<CommentModel>();
                }
                return PartialView("PartialViews/DisplayCommentsForEntry", commentList);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult AddComment(String entryString)
        {
            if (_settingsService.GetSiteSettings().AllowComments)
            {
                int entryID;
                if (entryString == null)
                {
                    return RedirectToAction("Index", "Errors", null);
                }
                try
                {
                    entryID = Convert.ToInt32(entryString);
                }
                catch (System.FormatException)
                {
                    return RedirectToAction("Index", "Errors", null);
                }
                EntryModel entry = _weblogService.GetEntry(entryID);
                if (entry != null)
                {
                    AddCommentModel model = new AddCommentModel(entry);
                    return PartialView("PartialViews/AddComment", model);
                }
            }
            return RedirectToAction("Index");

        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        [ValidateInput(false)] //html tags, will be removed,  newline will be <br/>
        public ActionResult AddComment(AddCommentModel model)
        {
            if (ModelState.IsValid)
            {
                if (!WebSecurity.IsAuthenticated)
                {
                    if (CaptchaHelper.CheckCaptcha(model.CaptchaResult))
                    {
                        model.Text = InputFilterHelper.sanitizeInput(model.Text);
                        this._weblogService.StoreComment(model);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Das eingegebene Captcha ist leider falsch.");
                        return PartialView("PartialViews/AddComment", model);
                    }
                }
                else
                {
                    model.Text = InputFilterHelper.sanitizeInput(model.Text);
                    this._weblogService.StoreComment(model);
                }
            }
            return PartialView("PartialViews/AddComment", model);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Administrator")]
        public JsonResult DeleteComment(int id)
        {
            this._weblogService.DeleteComment(id);
            return Json(new { success = true });
        }

        #endregion

        public ActionResult Categories()
        {
            List<CategoryModel> categoryList = _weblogService.GetCategories();
            return PartialView("PartialViews/Categories", categoryList);
        }

        public ActionResult Dates()
        {
            List<DateTime> dateList = _weblogService.GetDates();
            return PartialView("PartialViews/Dates", dateList);
        }

        public ActionResult About()
        {
            return PartialView("PartialViews/About");
        }
        #endregion

    }
}
