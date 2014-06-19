using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weblog.Web.Models.Weblog;

namespace Weblog.Web.Services
{
    public interface IWeblogService
    {
        #region Einträge

        /// <summary>
        /// Liefert die Einträge aus der Datenbank
        /// </summary>
        /// <returns>Eine Liste von EntryModel-Objekten mit den Daten aus der Datenbank</returns>
        List<EntryModel> GetEntries();

        /// <summary>
        /// Liefert alle Einträge für die Kategorie aus der Datenbank
        /// </summary>
        /// <param name="id">Die id der Kategorie.</param>
        /// <returns>Eine Liste von EntryModel-Objekten mit den Daten aus der Datenbank</returns>
        List<EntryModel> GetEntriesForCategory(int id);

        /// <summary>
        /// Liefert den Eintrag mit der angegebenen ID aus der Datenbank oder null falls der Eintrag nicht existiert
        /// </summary>
        /// <param name="id">Die ID des Eintrags, der aus der Datenbank geliefert werden soll</param>
        /// <returns>Den Eintrag mit der angegebenen ID oder null, falls der Eintrag nicht existiert</returns>
        AddEntryModel GetEntry(int id);

        /// <summary>
        /// Speichert einen Eintrag in die Datenbank (UPDATE oder INSERT)
        /// </summary>
        /// <param name="model">Das EntryModel mit den zu speichernden Daten</param>
        void StoreEntry(AddEntryModel model);

        /// <summary>
        /// Löscht einen Eintrag aus der Datenbank 
        /// </summary>
        /// <param name="id">The entry´s id.</param>
        void DeleteEntry(int id);

        #endregion
        #region Kategorien

        /// <summary>
        /// Liefert die Kategorie mit der angegebenen ID oder null, wenn diese nicht in der Datenbank existiert
        /// </summary>
        /// <param name="id">Die ID der Kategorie</param>
        /// <returns>Die Kategorie mit der angegebenen ID oder null falls die Kategorie nicht existiert</returns>
        AddCategoryModel GetCategory(int id);

        /// <summary>
        /// Liefert die Kategorien aus der Datenbank
        /// </summary>
        /// <returns>Die Liste der Kategorien aus der Datenbank</returns>
        List<CategoryModel> GetCategories();

        /// <summary>
        /// Speichert eine Kategorie in der Datenbank. Liefert eine Exception, wenn die Kategorie nicht gespeichert werden
        /// kann (daher muss vorher überprüft werden, ob die Kategorie schon existiert).
        /// Auch wenn diese Schritte zusammengefasst werden könnten, ist eine Trennung sinnvoll, weil es zwei unterschiedliche
        /// Schritte sind - und bereits nach der Kontrolle kann gesagt werden, ob das Speichern möglich sein wird oder nicht.
        /// </summary>
        /// <param name="model">Die Kategorie, die in der Datenbank gespeichert werden soll</param>
        void StoreCategory(AddCategoryModel model);

        /// <summary>
        /// Kontrolliert, ob die eingegebene Kategorie bereits in der Datenbank existiert. 
        /// </summary>
        /// <param name="category">Die Kategorie, die gespeichert werden soll</param>
        /// <returns>true wenn die Kategorie bereits in der Datenbank existiert, false wenn nicht</returns>
        bool CategoryExists(AddCategoryModel category);

        /// <summary>
        /// Kontrolliert, ob die eingegebene Kategorie bereits in der Datenbank existiert. 
        /// </summary>
        /// <param name="name">Der Name der Kategorie, die gespeichert werden soll</param>
        /// <returns>true wenn die Kategorie mit dem angegebenen Namen bereits in der Datenbank existiert, false wenn nicht</returns>
        bool CategoryExists(string name);

        /// <summary>
        /// Löscht eine Kategorie aus der Datenbank 
        /// </summary>
        /// <param name="id">The entry´s id.</param>
        void DeleteCategory(int id);

        #endregion
        #region Kommentare

        /// <summary>
        ///  Liefert alle Kommentare für den gegeben Eintrag aus der Datenbank
        /// </summary>
        /// <param name="id">Die id des Eintrags.</param>
        /// <returns>Eine Liste von CommentModel-Objekten mit den Daten aus der Datenbank</returns>
        List<CommentModel> GetCommentsForEntry(int id);

        /// <summary>
        /// Liefert den Eintrag mit der angegebenen ID aus der Datenbank oder null falls der Eintrag nicht existiert
        /// </summary>
        /// <param name="id">Die ID des Eintrags, der aus der Datenbank geliefert werden soll</param>
        /// <returns>Den Eintrag mit der angegebenen ID oder null, falls der Eintrag nicht existiert</returns>
        AddCommentModel GetComment(int id);

        /// <summary>
        /// Speichert einen Eintrag in die Datenbank (UPDATE oder INSERT)
        /// </summary>
        /// <param name="model">Das EntryModel mit den zu speichernden Daten</param>
        void StoreComment(AddCommentModel model);

        /// <summary>
        /// Löscht einen Eintrag aus der Datenbank 
        /// </summary>
        /// <param name="id">The entry´s id.</param>
        void DeleteComment(int id);

        #endregion
    }
}
