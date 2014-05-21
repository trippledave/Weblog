using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weblog.Core.DataAccess.Weblog;

namespace Weblog.Core.Repositories
{
    public class WeblogRepository : Weblog.Core.Repositories.IWeblogRepository
    {

        #region Einträge

        public void SaveEntry( Entry entry, bool isNewEntry=false )
        {
            if ( isNewEntry )
            {
                entry.DateCreated = DateTime.Now;
                WeblogDataContext.Current.Entries.Add( entry );
            }
            WeblogDataContext.Current.SaveChanges();
        }
        public List<Entry> GetAllEntries()
        {
            return WeblogDataContext.Current.Entries.OrderByDescending(e=>e.DateCreated).ToList();
        }

        public Entry GetEntry( int id )
        {
            return WeblogDataContext.Current.Entries.FirstOrDefault( e => e.EntryID == id );
        }

        #endregion

        #region Kategorien

        public void SaveCategory( Category category, bool isNewCategory )
        {
            if ( isNewCategory )
            {
                if ( !CategoryExists( category.Name ) )
                {
                    WeblogDataContext.Current.Categories.Add( category );
                }
            }
            WeblogDataContext.Current.SaveChanges();
        }

        public List<Category> GetAllCategories()
        {
            return WeblogDataContext.Current.Categories.OrderBy( c => c.Name ).ToList();
        }

        public Category GetCategory(int id)
        {
            return WeblogDataContext.Current.Categories.FirstOrDefault( c => c.CategoryID == id );
        }

        public bool CategoryExists( string name )
        {
            string lowercaseName = name.ToLower();
            return WeblogDataContext.Current.Categories.Any( c => c.Name.ToLower() == lowercaseName );
        }

        #endregion

        #region Kommentare
        #endregion


        public List<Entry> GetEntriesForCategory(Category category)
        {
            return WeblogDataContext.Current.Entries.Where(e => e.Categories == category).OrderByDescending(e => e.DateCreated).ToList();

                //OrderByDescending(e => e.DateCreated).ToList();
        }

        public void RemoveEntry(Entry entry)
        {
            throw new NotImplementedException();
        }

        public void RemoveCategory(Category entry)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetCommentsForEntry(int id)
        {
            throw new NotImplementedException();
        }

        public Comment GetComment(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveComment(Comment comment, bool isNewComment)
        {
            throw new NotImplementedException();
        }

        public void RemoveComment(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
