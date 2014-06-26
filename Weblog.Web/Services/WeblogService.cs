using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Weblog.Core.DataAccess.Weblog;
using Weblog.Core.Repositories;
using Weblog.Web.Models.Weblog;
using WebMatrix.WebData;

namespace Weblog.Web.Services
{


    /// <summary>
    /// Der Service. In diesem wird die Business-Logik abgebildet.
    /// </summary>
    public class WeblogService : IWeblogService
    {
        /// <summary>
        /// Das Weblog-Repository, das für den Datenbankzugriff zuständig ist. Sämtliche Datenbankzugriffe laufen
        /// über das Weblog-Repository (womit die Datenbank selbst austauschbar wird)
        /// </summary>
        private IWeblogRepository _repository = new WeblogRepository();

        #region Einträge

        public void StoreEntry(AddEntryModel model)
        {
            bool isNewEntry = model.ID == 0;
            Entry entry;
            if (isNewEntry)
            {
                entry = new Entry();
                //Author wird nur einmal gesetzt, änderungen vom admin ändern autor nicht.
                entry.AuthorID = WebSecurity.CurrentUserId;
                model.UpdateSource(entry);
            }
            else
            {
                entry = this._repository.GetEntry(model.ID);
                model.UpdateSource(entry);
            }

            if (model.CategoriesList != null)
            {
                foreach (var item in model.CategoriesList)
                {
                    if (item.isElementsCategory)
                    {
                        entry.Categories.Add(this._repository.GetCategory(item.ID));
                    }
                }
            }
            this._repository.SaveEntry(entry, isNewEntry);
        }


        public List<EntryModel> GetEntries()
        {
            // hole alle Einträge aus der Datenbank
            List<Entry> entries = this._repository.GetAllEntries();

            // Hier geschieht das Mapping - bitte hierzu nochmal bzgl. LINQ nachschauen.
            // Die Select()-Anweisung ermöglicht es, die Elemente aus der Liste nochmals zu verändern oder
            // sogar komplett neue Objekte zu erzeugen. Die Sortierung der Liste bleibt dabei i.d.R. gleich,
            // zur Sicherheit kann aber auch hier nochmals eine OrderBy-Anweisung verwendet werden.
            // Die ToList()-Anweisung am Ende ist notwendig, da LINQ ausschließlich mit Interfaces arbeitet.
            List<EntryModel> result = entries.Select(e => new EntryModel(e)).ToList();
            return result;
        }

        public EntryModel GetEntry(int id)
        {
            Entry entry = this._repository.GetEntry(id);
            return entry == null ? null : new EntryModel(entry);
        }

        public List<EntryModel> GetEntriesByCategory(int id)
        {
            Category category = _repository.GetCategory(id);
            if (category != null)
            {
                List<Entry> entries = _repository.GetEntriesByCategory(category);
                List<EntryModel> result = entries.Select(e => new EntryModel(e)).ToList();
                return result;
            }
            else
            {
                return new List<EntryModel>();
            }
        }

        public List<EntryModel> GetEntriesByDate(int month, int year)
        {
            DateTime date = new DateTime(year, month, 1);
            List<Entry> entries = _repository.GetEntriesByDate(date);
            List<EntryModel> result = entries.Select(e => new EntryModel(e)).ToList();
            return result;
        }

        public void DeleteEntry(int id)
        {
            Entry entryToDelete = this._repository.GetEntry(id);
            if (entryToDelete != null)
            {
                if (entryToDelete.Categories.Count > 0)
                {
                    entryToDelete.Categories.Clear();
                }
                List<CommentModel> commentList = GetCommentsForEntry(entryToDelete.EntryID);
                if (commentList.Count > 0)
                {
                    foreach (CommentModel item in commentList)
                    {
                        DeleteComment(item.ID);
                    }
                }
                this._repository.RemoveEntry(entryToDelete);
            }
        }


        #endregion
        #region Kategorien

        public AddCategoryModel GetCategory(int id)
        {
            Category currentCategory = this._repository.GetCategory(id);
            return currentCategory == null ? null : new AddCategoryModel(currentCategory);
        }

        public List<CategoryModel> GetCategoriesForEntry(int entryID)
        {
            List<Category> categories = _repository.GetCategoryForEntry(entryID);
            List<CategoryModel> result = categories.Select(c => new CategoryModel(c)).ToList();
            return result;

        }

        public List<CategoryModel> GetCategories()
        {
            List<Category> categories = this._repository.GetAllCategories();
            List<CategoryModel> result = categories.Select(c => new CategoryModel(c)).ToList();
            return result;
        }

        public void StoreCategory(AddCategoryModel model)
        {
            bool isNewCategory = model.ID == 0;
            Category newCategory = isNewCategory ? new Category() : this._repository.GetCategory(model.ID);
            model.UpdateSource(newCategory);
            _repository.SaveCategory(newCategory, isNewCategory);


        }

        public bool CategoryExists(AddCategoryModel category)
        {
            return this._repository.CategoryExists(category.Name);
        }

        public bool CategoryExists(string name)
        {
            return this._repository.CategoryExists(name);
        }

        public void DeleteCategory(int id)
        {
            Category categoryToDelete = this._repository.GetCategory(id);
            if (categoryToDelete != null)
            {
                if (categoryToDelete.Entries.Count > 0)
                {
                    categoryToDelete.Entries.Clear();
                }
                this._repository.RemoveCategory(categoryToDelete);
            }
        }

        #endregion
        #region Kommentare

        public List<CommentModel> GetCommentsForEntry(int id)
        {
            Entry entry = _repository.GetEntry(id);
            List<CommentModel> result;
            if (entry != null)
            {
                List<Comment> entries = _repository.GetCommentsForEntry(entry);
                result = entries.Select(e => new CommentModel(e)).ToList();
            }
            else
            {
                result = new List<CommentModel>();
            }

            return result;
        }

        public AddCommentModel GetComment(int id)
        {
            Comment comment = this._repository.GetComment(id);
            return comment == null ? null : new AddCommentModel(comment);
        }

        public void StoreComment(AddCommentModel model)
        {
            bool isNewEntry = model.ID == 0;
            Comment comment;
            if (isNewEntry)
            {
                comment = new Comment();
                comment.AuthorID = WebSecurity.CurrentUserId;
                //Anonyme Kommentare
                if (comment.AuthorID == -1)
                {
                    comment.AuthorID = _repository.GetUser("Anonym").UserID;
                }
                model.UpdateSource(comment);
            }
            else
            {
                comment = this._repository.GetComment(model.ID);
                model.UpdateSource(comment);
            }

            _repository.SaveComment(comment, isNewEntry);
        }

        public void DeleteComment(int id)
        {
            Comment commentToDelete = this._repository.GetComment(id);
            if (commentToDelete != null)
            {
                this._repository.RemoveComment(commentToDelete);
            }
        }
        #endregion
        #region Datumsangaben
        public List<DateTime> GetDates()
        {
            List<DateTime> dateList = new List<DateTime>();
            List<Entry> entryList = _repository.GetAllEntries();
            DateTime currentItemsDate, previousItemsDate = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, 1);
            foreach (Entry item in entryList)
            {
                currentItemsDate = new DateTime(item.DateCreated.Year, item.DateCreated.Month, 1);
                if (currentItemsDate != previousItemsDate)
                {
                    DateTime date = new DateTime(currentItemsDate.Year, currentItemsDate.Month, 1);
                    dateList.Add(date);

                }
                previousItemsDate = currentItemsDate;

            }

            return dateList;
        }
        #endregion
    }
}