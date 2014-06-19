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
    [Authorize]
    public class BlogController : Controller
    {
        private IWeblogService _weblogService = new WeblogService();

        #region Views

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditorialDepartment()
        {
            return View();
        }

        public ActionResult AddEntry()
        {
            AddEntryModel model = new AddEntryModel();
            model.CategoriesList = _weblogService.GetCategories();

            return View(model);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public ActionResult AddEntry(AddEntryModel model)
        {
            if (ModelState.IsValid)
            {
                this._weblogService.StoreEntry(model);
                return RedirectToAction("AddEntry");
            }
           model.CategoriesList = _weblogService.GetCategories();
            return View(model);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public JsonResult DeleteEntry(int id)
        {
            this._weblogService.DeleteEntry(id);
            return Json( new { success = true });
        }

        public ActionResult ShowEntry(int entryId)
        {
            _weblogService.GetEntry(entryId);
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

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public JsonResult DeleteCategory(int id)
        {
            this._weblogService.DeleteCategory(id);
            return Json(new { success = true });
        }

        #endregion

        #region Partial Views

        public ActionResult DisplayEntries()
        {
            List<EntryModel> viewModel = this._weblogService.GetEntries();
            return PartialView(viewModel);
        }

        public ActionResult DisplayCategories()
        {
            List<CategoryModel> viewModel = this._weblogService.GetCategories();
            return PartialView(viewModel);
        }

        public ActionResult DisplayCommentsForEntry(EntryModel entry)
        {
            List<CommentModel> viewModel = this._weblogService.GetCommentsForEntry(entry.ID);
            return PartialView(viewModel);
        }

        public ActionResult AddComment()
        {
            return PartialView();
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public ActionResult AddComment(AddCommentModel model)
        {
            this._weblogService.StoreComment(model);
            return PartialView();
        }
        #endregion

    }
}
