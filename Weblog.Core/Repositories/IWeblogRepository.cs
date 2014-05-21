using System;
using System.Collections.Generic;
using Weblog.Core.DataAccess.Weblog;

namespace Weblog.Core.Repositories
{
    public interface IWeblogRepository
    {
        #region Einträge

        /// <summary>
        /// Liefert alle Einträge aus der Datenbank, absteigend nach Erstellungsdatum sortiert
        /// </summary>
        /// <returns>Eine Liste aller Einträge aus der Datenbank, sortiert nach Datum absteigend</returns>
        List<Entry> GetAllEntries();

        /// <summary>
        /// Liefert alle Einträge aus der Datenbank, die die gegebne Kategorie haben. Absteigend nach Erstellungsdatum sortiert
        /// </summary>
        /// <param name="category">Die Kategorie deren Einträge geliefert werden sollen</param>
        /// <returns>Eine Liste aller Einträge aus der Datenbank zu der Kategorie, sortiert nach Datum absteigend</returns>
        List<Entry> GetEntriesForCategory(Category category);

        /// <summary>
        /// Liefert den Eintrag mit der angegebenen ID oder null, wenn kein solcher Eintrag existiert
        /// </summary>
        /// <param name="id">Die ID des Eintrags der geliefert werden soll</param>
        /// <returns>Den Eintrag als Objekt vom Typ "Entry" oder null, wenn der Eintag nicht in der Datenbank existiert</returns>
        Entry GetEntry(int id);

        /// <summary>
        /// Speichern des Eintrags. Der zusätzliche Parameter "isNewEntry" gibt an, ob es sich um einen neuen Eintrag handelt.
        /// Diese Information stammt aus dem Service, der die Mapping-Arbeit macht und auch für die Entscheidung zuständig ist,
        /// ob ein Eintrag als neuer Eintrag gespeichert wird oder als bestehender
        /// </summary>
        /// <param name="entry">Der Eintrag, der gespeichert werden soll</param>
        /// <param name="isNewEntry">true, wenn es sich um einen neuen Eintrag handelt, false wenn nicht</param>
        void SaveEntry(Entry entry, bool isNewEntry = false);

        /// <summary>
        /// Löscht den übergebenen Eintrag aus der Datenbank
        /// </summary>
        /// <param name="entry">Der Eintrag, der gelöscht werden soll</param>
        void RemoveEntry(Entry entry);

        #endregion

        #region Kategorien

        /// <summary>
        /// Liefert alle Kategorien aus der Datenbank, sortiert nach Name aufsteigend
        /// </summary>
        /// <returns>Eine Liste von Category-Elementen, sortiert nach Name aufsteigend</returns>
        List<Category> GetAllCategories();

        /// <summary>
        /// Liefert die Kategorie mit der angegebenen ID oder null wenn diese Kategorie nicht existiert
        /// </summary>
        /// <param name="id">Die ID der Kategorie</param>
        /// <returns>Die Kategorie mit der angegebenen ID oder null wenn diese Kategorie nicht existiert</returns>
        Category GetCategory(int id);

        /// <summary>
        /// Liefert true wenn die Kategorie mit dem angegebenen Namen existiert, ansonsten false
        /// </summary>
        /// <param name="name">Der Name der Kategorie</param>
        /// <returns>true wenn die Kategorie mit dem angegebenen Namen existiert, ansonsten false</returns>
        bool CategoryExists(string name);

        /// <summary>
        /// Speichert eine Kategorie in der Datenbank. Auch hier gibt der Parameter isNewEntry an, ob es sich um eine neue Kategorie handelt
        /// </summary>
        /// <param name="category">Die Kategorie, die gespeichert werden soll</param>
        /// <param name="isNewCategory">true, wenn es sich um eine neue Kategorie handelt, false wenn nicht</param>
        void SaveCategory(Category category, bool isNewCategory);


        /// <summary>
        /// Löscht die übergebenen Kategorie aus der Datenbank
        /// </summary>
        /// <param name="entry">Die Kategorie die gelöscht werden soll</param>
        void RemoveCategory(Category entry);

        #endregion

        #region Kommentare


        /// <summary>
        /// Liefert alle Kommentare aus der Datenbank, für den jeweiligen Eintrag. Absteigend nach Erstellungsdatum sortiert.
        /// </summary>
        /// <param name="id">Der Eintrag von dem die Kommentare geliefert werden sollen</param>
        /// <returns> Liste mit Kommentaren oder leere Liste</returns>
        List<Comment> GetCommentsForEntry(int id);

        /// <summary>
        /// Liefert den Kommentar der zu der ID passt.
        /// </summary>
        /// <param name="id">Die ID des Kommentars</param>
        /// <returns> Kommentar mit der angegebenen ID oder null wenn diese Kategorie nicht existiert</returns>
        Comment GetComment(int id);

        /// <summary>
        /// Speichern des Kommentars. Der zusätzliche Parameter "isNewEntry" gibt an, ob es sich um einen neuen Kommentar handelt.
        /// </summary>
        /// <param name="comment">Der Kommentar, der gespeichert werden soll</param>
        /// <param name="isNewEntry">true, wenn es sich um einen neuen Kommentar handelt, false wenn nicht</param>
        void SaveComment(Comment comment, bool isNewComment);

        /// <summary>
        /// Löscht die übergebenen Kategorie aus der Datenbank
        /// </summary>
        /// <param name="entry">Die Kategorie die gelöscht werden soll</param>
        void RemoveComment(Comment comment);

        #endregion
    }
}
