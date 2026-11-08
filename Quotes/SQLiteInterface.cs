using System;
using System.Collections.Generic;
using Android.Database.Sqlite;
using Android.Database;
using Android.Content;


namespace Quotes
{
    public class SQLiteInterface
    {

        public const string DB_NAME = "quotes.db", DB_TABLE = "Quotes", DB_COL_ID = "_id", DB_COL_TITLE = "Title",
    DB_COL_TEXT = "Text", DB_COL_DATE = "Date", DB_COL_IMPORTANCE = "Importance", DB_COL_PERSON_QUOTED = "PersonQuoted", DB_COL_PERSON_QUOTED_TO = "PersonQuotedTo";
        public const int DB_VERSION = 1;
        public const string SQLITE_EXCEPTION = "SQLiteException";
        private SQLiteOpener sqlOpener;
        private SQLiteDatabase sqlDatabase;
        public static string[] COLOMNS = new string[]{ DB_COL_ID, DB_COL_TITLE,DB_COL_TEXT, DB_COL_DATE , DB_COL_IMPORTANCE, DB_COL_PERSON_QUOTED, DB_COL_PERSON_QUOTED_TO };

        // use try for this methods and log errors
        public SQLiteInterface(Context context)
        {
            sqlOpener = new SQLiteOpener(context);
        }

        public void Open()
        {
            try
            {
                sqlDatabase = sqlOpener.WritableDatabase;
            }
            catch(SQLiteException ex)
            {
                throw ex;
            }
        }

        public void Close()
        {
            sqlDatabase.Close();
        }

        public Quote CreateNewQuote(string newQuoteTitle, string newQuoteText, DateTime newQuoteDate, short newQuoteImportance, string newQuotePersonQuoted, string newQuotePersonQuotedTo)
        {
            ContentValues cv = new ContentValues();

            cv.Put(DB_COL_TITLE, newQuoteTitle);
            cv.Put(DB_COL_TEXT, newQuoteText);
            cv.Put(DB_COL_DATE, newQuoteDate.ToString());
            cv.Put(DB_COL_IMPORTANCE, newQuoteImportance);
            cv.Put(DB_COL_PERSON_QUOTED, newQuotePersonQuoted);
            cv.Put(DB_COL_PERSON_QUOTED_TO, newQuotePersonQuotedTo);
            
            long id = sqlDatabase.Insert(DB_TABLE, null, cv);
            ICursor cr = sqlDatabase.Query(DB_TABLE, COLOMNS, DB_COL_ID + "=" + id, null, null, null,null);
            cr.MoveToFirst();
            return ConvertCursorToQuote(cr);
        }

        public bool UpdateRow(Quote updatedQuote)
        {
            try
            {
                sqlDatabase.ExecSQL("UPDATE " + DB_TABLE + " SET " + DB_COL_TITLE + "=\'" + updatedQuote.Title + "\' WHERE " + DB_COL_ID + "=" + updatedQuote.ID);
                sqlDatabase.ExecSQL("UPDATE " + DB_TABLE + " SET " + DB_COL_TEXT + "=\'" + updatedQuote.Text + "\' WHERE " + DB_COL_ID + "=" + updatedQuote.ID);
                sqlDatabase.ExecSQL("UPDATE " + DB_TABLE + " SET " + DB_COL_DATE + "=\'" + updatedQuote.Date.ToString() + "\' WHERE " + DB_COL_ID + "=" + updatedQuote.ID);
                sqlDatabase.ExecSQL("UPDATE " + DB_TABLE + " SET " + DB_COL_IMPORTANCE + "=" + updatedQuote.Importance.ToString() + " WHERE " + DB_COL_ID + "=" + updatedQuote.ID);
                sqlDatabase.ExecSQL("UPDATE " + DB_TABLE + " SET " + DB_COL_PERSON_QUOTED + "=\'" + updatedQuote.PersonQuoted + "\' WHERE " + DB_COL_ID + "=" + updatedQuote.ID);
                sqlDatabase.ExecSQL("UPDATE " + DB_TABLE + " SET " + DB_COL_PERSON_QUOTED_TO + "=\'" + updatedQuote.PersonQuotedTo + "\' WHERE " + DB_COL_ID + "=" + updatedQuote.ID);
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public List<Quote> GetQuotes()
        {
            List<Quote> allQuotes = new List<Quote>();
            ICursor cr = sqlDatabase.Query(DB_TABLE, COLOMNS, null, null, null, null, DB_COL_IMPORTANCE + " DESC");
            cr.MoveToFirst();
            while(!cr.IsAfterLast)
            {
                allQuotes.Add(ConvertCursorToQuote(cr));
                cr.MoveToNext();
            }
            cr.Close();
            return allQuotes;
        }

        private Quote ConvertCursorToQuote(ICursor cr)
        {
            return new Quote(cr.GetInt(0), cr.GetString(1), cr.GetString(2), DateTime.Parse(cr.GetString(3)) , cr.GetShort(4) , cr.GetString(5), cr.GetString(6));
        }
    }
}