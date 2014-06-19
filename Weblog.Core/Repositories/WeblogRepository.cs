using System;
using System.Collections.Generic;
using System.Linq;
using Weblog.Core.DataAccess.Weblog;

namespace Weblog.Core.Repositories
{
    public class WeblogRepository : Weblog.Core.Repositories.IWeblogRepository
    {

        #region Einträge

        public void SaveEntry(Entry entry, bool isNewEntry = false)
        {
            if (isNewEntry)
            {
                entry.DateCreated = DateTime.Now;
                WeblogDataContext.Current.Entries.Add(entry);
            }
            WeblogDataContext.Current.SaveChanges();
        }
        public List<Entry> GetAllEntries()
        {
            return WeblogDataContext.Current.Entries.OrderByDescending(e => e.DateCreated).ToList();
        }

        public Entry GetEntry(int id)
        {
            return WeblogDataContext.Current.Entries.FirstOrDefault(e => e.EntryID == id);
        }

        public List<Entry> GetEntriesForCategory(Category category)
        {
            return WeblogDataContext.Current.Entries.Where(e => e.Categories == category).OrderByDescending(e => e.DateCreated).ToList();
        }

        public void RemoveEntry(Entry entry)
        {
            WeblogDataContext.Current.Entries.Remove(entry);
            WeblogDataContext.Current.SaveChanges();
        }
        #endregion

        #region Kategorien

        public void SaveCategory(Category category, bool isNewCategory)
        {
            if (isNewCategory)
            {
                WeblogDataContext.Current.Categories.Add(category);

            }
            WeblogDataContext.Current.SaveChanges();
        }

        public List<Category> GetAllCategories()
        {
            return WeblogDataContext.Current.Categories.OrderBy(c => c.Name).ToList();
        }

        public Category GetCategory(int id)
        {
            return WeblogDataContext.Current.Categories.FirstOrDefault(c => c.CategoryID == id);
        }

        public bool CategoryExists(string name)
        {
            if (name == null) return false;
            string lowercaseName = name.ToLower();
            return WeblogDataContext.Current.Categories.Any(c => c.Name.ToLower() == lowercaseName);
        }

        public void RemoveCategory(Category category)
        {
            WeblogDataContext.Current.Categories.Remove(category);
            WeblogDataContext.Current.SaveChanges();
        }
        #endregion

        #region Kommentare
        public List<Comment> GetCommentsForEntry(Entry entry)
        {
            return WeblogDataContext.Current.Comments.Where(e => e.Entry == entry).OrderByDescending(e => e.DateCreated).ToList();
        }

        public Comment GetComment(int id)
        {
            return WeblogDataContext.Current.Comments.FirstOrDefault(e => e.CommentID == id);
        }

        public void SaveComment(Comment comment, bool isNewComment)
        {
            if (isNewComment)
            {
                comment.DateCreated = DateTime.Now;
                WeblogDataContext.Current.Comments.Add(comment);
            }
            WeblogDataContext.Current.SaveChanges();
        }

        public void RemoveComment(Comment comment)
        {
            WeblogDataContext.Current.Comments.Remove(comment);
            WeblogDataContext.Current.SaveChanges();
        }
        #endregion

        #region User

        public User GetUser(string userName)
        {
            return WeblogDataContext.Current.Users.FirstOrDefault(u => u.UserNameLowercase.Equals(userName.ToLower()));
        }

        public User GetUserByEmail(string email)
        {
            return WeblogDataContext.Current.Users.FirstOrDefault(u => u.EmailLowercase.Equals(email.ToLower()));
        }

        public void UpdateUserSettings(User user)
        {
            WeblogDataContext.Current.SaveChanges();
        }

        #endregion

        #region Settings

        private Guid _settingsID = Guid.Empty;

        public AdministratorSettings GetAdministratorSettings()
        {
            Guid settingsID = Guid.Empty;
            return WeblogDataContext.Current.AdministratorSettings.FirstOrDefault(a => a.ID == _settingsID);
        }

        public void UpdateAdministratorSettings(AdministratorSettings adminSettings)
        {
            WeblogDataContext.Current.SaveChanges();
        }

        public void SetAdministratorSettings(AdministratorSettings settings)
        {
            if (GetAdministratorSettings() == null)
            {
                settings.ID = Guid.Empty;
                WeblogDataContext.Current.AdministratorSettings.Add(settings);
                WeblogDataContext.Current.SaveChanges();
            }

        }

        #endregion
    }
}
