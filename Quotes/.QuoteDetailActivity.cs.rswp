
using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace Quotes
{
    [Activity(Label = "QuoteDetailActivity")]
    public class QuoteDetailActivity : Activity
    {
        private const short ID_NOT_INITIALIZED = -1;
        private long thisQuoteId = ID_NOT_INITIALIZED;
        private EditText txtPersonQuoted, txtPersonQuotedTo, txtQuoteTitle, txtQuoteText;
        private ProgressBar progQuoteImportance;
        private DatePicker dtpQuoteDate;
        private Button btnSave, btnCancel, btnAddPriority, btnDecreasePriority;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.quote_data_activity);
            if (ActionBar != null) ActionBar.SetDisplayHomeAsUpEnabled(true);
            txtPersonQuoted = FindViewById<EditText>(Resource.Id.txt_person_quoted);
            txtPersonQuotedTo = FindViewById<EditText>(Resource.Id.txt_person_quoted_to);
            txtQuoteTitle = FindViewById<EditText>(Resource.Id.txt_quote_title);
            txtQuoteText = FindViewById<EditText>(Resource.Id.txt_quote_text);
            progQuoteImportance = FindViewById<ProgressBar>(Resource.Id.prg_importance);
            btnCancel = FindViewById<Button>(Resource.Id.btn_cancel);
            btnSave = FindViewById<Button>(Resource.Id.btn_save);
            btnAddPriority = FindViewById<Button>(Resource.Id.btn_add_priority);
            btnDecreasePriority = FindViewById<Button>(Resource.Id.btn_decrease_priority);

            dtpQuoteDate = FindViewById<DatePicker>(Resource.Id.dtp_quote_date);

            btnAddPriority.Click += new EventHandler(btnChangePriority_click);
            btnDecreasePriority.Click += new EventHandler(btnChangePriority_click);

            Intent intent = Intent;
            thisQuoteId = intent.GetLongExtra(SQLiteInterface.DB_COL_ID, ID_NOT_INITIALIZED);
            txtQuoteTitle.Text = intent.GetStringExtra(SQLiteInterface.DB_COL_TITLE);
            Title = txtQuoteTitle.Text;
            txtPersonQuoted.Text = intent.GetStringExtra(SQLiteInterface.DB_COL_PERSON_QUOTED);
            txtPersonQuotedTo.Text = intent.GetStringExtra(SQLiteInterface.DB_COL_PERSON_QUOTED_TO);
            txtQuoteText.Text = intent.GetStringExtra(SQLiteInterface.DB_COL_TEXT);
            dtpQuoteDate.DateTime = Convert.ToDateTime(intent.GetStringExtra(SQLiteInterface.DB_COL_DATE));

            progQuoteImportance.Progress = intent.GetShortExtra(SQLiteInterface.DB_COL_IMPORTANCE, Quote.DEFAULT_IMPORTANCE);
            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            // Create your application here
            
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            Quote editedQuote = new Quote(thisQuoteId, txtQuoteTitle.Text, txtQuoteText.Text, dtpQuoteDate.DateTime , (short)(progQuoteImportance.Progress) , txtPersonQuoted.Text, txtPersonQuotedTo.Text);

            if(editedQuote.Text.Replace(" ", "") != "" && editedQuote.PersonQuoted.Replace(" ", "") != "" && editedQuote.PersonQuotedTo.Replace(" ", "") != "" && editedQuote.Title.Replace(" ", "") != "")
            {
                // complete this part whith showing the exact error
                if(MainActivity.SQLite.UpdateRow( editedQuote ) )
                {
                    Toast.MakeText(this, Resource.String.updated_successfully, ToastLength.Short).Show();
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, Resource.String.wrong_update, ToastLength.Long).Show();
                }
                
            }
            else
            {
                Toast.MakeText(this, GetString(Resource.String.some_details_empty),  ToastLength.Short).Show();
            }
        }
        void btnCancel_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void btnChangePriority_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            bool addPressed = button.Text == "+" ? true : false;
            progQuoteImportance.Progress = addPressed ? (progQuoteImportance.Progress + 10) : progQuoteImportance.Progress  - 10;

        }

    }

}