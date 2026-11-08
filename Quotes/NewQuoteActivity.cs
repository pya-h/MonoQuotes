using System;
using Android.App;
using Android.OS;
using Android.Widget;


namespace Quotes
{
    [Activity(Label = "NewQuoteActivity")]
    public class NewQuoteActivity : Activity
    {
        private DatePicker dtpQuoteDate;
        private Button btnSave, btnCancel, btnAddPriority, btnDecreasePriority;
        private EditText txtPersonQuoted, txtPersonQuotedTo, txtQuoteTitle, txtQuoteText;
        private ProgressBar progQuoteImportance;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.quote_data_activity);
            if (ActionBar != null) ActionBar.SetDisplayHomeAsUpEnabled(true);
            Title = GetString(Resource.String.activity_new_quote);

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
            dtpQuoteDate.DateTime = DateTime.Now;

            btnAddPriority.Click += new EventHandler(btnChangePriority_click);
            btnDecreasePriority.Click += new EventHandler(btnChangePriority_click);
            btnSave.Click += new EventHandler(btnSave_click);
            btnCancel.Click += delegate { Finish(); };
        }

        private void btnSave_click(object sender, EventArgs e)
        {
            try
            {
                if (txtQuoteText.Text.Replace(" ", "") != "" && txtPersonQuoted.Text.Replace(" ", "") != "" && txtPersonQuotedTo.Text.Replace(" ", "") != "" && txtQuoteTitle.Text.Replace(" ", "") != "")
                { 
                    MainActivity.SQLite.CreateNewQuote(txtQuoteTitle.Text, txtQuoteTitle.Text, dtpQuoteDate.DateTime, (short)progQuoteImportance.Progress, txtPersonQuoted.Text, txtPersonQuotedTo.Text);
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, GetString(Resource.String.some_details_empty) , ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
        }

        private void btnChangePriority_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            bool addPressed = button.Text == "+" ? true : false;
            progQuoteImportance.Progress = addPressed ? (progQuoteImportance.Progress + 10) : progQuoteImportance.Progress - 10;
                
        }

    }
}