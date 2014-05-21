using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weblog.Core.DataAccess.Weblog;
using Weblog.Core.Repositories;
using Weblog.Web.Models.Weblog;

namespace Weblog.Web.Services
{


    /// <summary>
    /// Der Service. In diesem wird die Business-Logik abgebildet.
    /// </summary>
    public class WeblogService : Weblog.Web.Services.IWeblogService
    {
        /// <summary>
        /// Das Weblog-Repository, das für den Datenbankzugriff zuständig ist. Sämtliche Datenbankzugriffe laufen
        /// über das Weblog-Repository (womit die Datenbank selbst austauschbar wird)
        /// </summary>
        private IWeblogRepository _repository = new WeblogRepository();

        public void StoreEntry(AddEntryModel model)
        {
            bool isNewEntry = model.ID == 0;
            Entry newEntry = isNewEntry ? new Entry() : this._repository.GetEntry(model.ID);
            model.UpdateSource(newEntry);
            this._repository.SaveEntry(newEntry, isNewEntry);
        }

        public void DeleteEntry(int id)
        {
            Entry deletedEntry = this._repository.GetEntry(id); ;
            this._repository.RemoveEntry(deletedEntry);
        }

        public List<EntryListItemModel> GetEntries()
        {
            // hole alle Einträge aus der Datenbank
            List<Entry> entries = this._repository.GetAllEntries();

            // Hier geschieht das Mapping - bitte hierzu nochmal bzgl. LINQ nachschauen.
            // Die Select()-Anweisung ermöglicht es, die Elemente aus der Liste nochmals zu verändern oder
            // sogar komplett neue Objekte zu erzeugen. Die Sortierung der Liste bleibt dabei i.d.R. gleich,
            // zur Sicherheit kann aber auch hier nochmals eine OrderBy-Anweisung verwendet werden.
            // Die ToList()-Anweisung am Ende ist notwendig, da LINQ ausschließlich mit Interfaces arbeitet.
            List<EntryListItemModel> result = entries.Select(e => new EntryListItemModel(e)).ToList();
            return result;
        }

        public AddEntryModel GetEntry(int id)
        {
            Entry entry = this._repository.GetEntry(id);
            return entry == null ? null : new AddEntryModel(entry);
        }

        public AddCategoryModel GetCategory(int id)
        {
            Category currentCategory = this._repository.GetCategory(id);
            return currentCategory == null ? null : new AddCategoryModel(currentCategory);
        }

        public List<CategoryListItemModel> GetCategories()
        {
            List<Category> categories = this._repository.GetAllCategories();
            List<CategoryListItemModel> result = categories.Select(c => new CategoryListItemModel(c)).ToList();
            return result;
        }

        public void StoreCategory(AddCategoryModel model)
        {
            bool isNewCategory = model.ID == 0;
            Category newCategory = isNewCategory ? new Category() : this._repository.GetCategory(model.ID);
            model.UpdateSource(newCategory);
            this._repository.SaveCategory(newCategory, isNewCategory);
        }

        public bool CategoryExists(AddCategoryModel category)
        {
            return this._repository.CategoryExists(category.Name);
        }

        public bool CategoryExists(string name)
        {
            return this._repository.CategoryExists(name);
        }



        public List<EntryListItemModel> GetEntriesForCategory(int id)
        {
            throw new NotImplementedException();
        }

        public List<CommentListItemModel> GetCommentsForEntry(int id)
        {
            throw new NotImplementedException();
        }

        public AddCommentModel GetComment(int id)
        {
            throw new NotImplementedException();
        }

        public void StoreComment(AddCommentModel model)
        {
            throw new NotImplementedException();
        }

        public void DeleteComment(int id)
        {
            throw new NotImplementedException();
        }
    }
}