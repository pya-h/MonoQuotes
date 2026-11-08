
using Android.Database.Sqlite;
using Android.Content;

namespace Quotes
{

    class SQLiteOpener : SQLiteOpenHelper
    {

        private const string cmdCreate = "CREATE TABLE " + SQLiteInterface.DB_TABLE + " ( " + SQLiteInterface.DB_COL_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
            SQLiteInterface.DB_COL_TITLE + " TEXT NOT NULL, " + SQLiteInterface.DB_COL_TEXT + " TEXT NOT NULL, " + SQLiteInterface.DB_COL_DATE + " TEXT NOT NULL, " +
            SQLiteInterface.DB_COL_IMPORTANCE + " INTEGER, " + SQLiteInterface.DB_COL_PERSON_QUOTED + " TEXT NOT NULL, " + SQLiteInterface.DB_COL_PERSON_QUOTED_TO + " TEXT NOT NULL);";

        private const string cmdDrop = "DROP TABLE IF EXIST " + SQLiteInterface.DB_TABLE;
        private const string DATABASE_UPDATE_TAG = "DatabaseUpdated";
        public const string DATABASE_UPDATE_WARN = "Updating database from version {0} to {1} ";
        public SQLiteOpener(Context context) : base(context,SQLiteInterface.DB_NAME,null, SQLiteInterface.DB_VERSION)
        {
           
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL(cmdCreate);
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            
            db.ExecSQL(cmdDrop);
            OnCreate(db);
            Android.Util.Log.WriteLine(Android.Util.LogPriority.Warn,DATABASE_UPDATE_TAG, string.Format(DATABASE_UPDATE_WARN, oldVersion, newVersion) );
        }
    }
}