using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System;
using Android.Util;
using Android.Content;
using Android.Runtime;
using Android.Views;

namespace Quotes
{
    [Activity(Label = "Quotes", MainLauncher = true)]
    public class MainActivity : ListActivity
    {
        private Button btnAdd;
        private TextView txtShortDetail;
        private static SQLiteInterface sqlite;
        public const string UPDATE_LIST_EXCEPTION = "UpdateQuotesListException";
        public const byte CODE_ADD_NEW_QUOTE_ACTIVITY = 1;
        public const byte CODE_SETTING_ACTIVITY = 0;
        private List<Quote> allQuotes;
        private QuotesListAdapter lstAdapter;
        public const string TOTAL_ERROR = "TotalError";
        Quote selectedQuote = null;

        public static SQLiteInterface SQLite
        {
            get => sqlite;
            set => sqlite = value;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.app_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Resource.Id.action_setting:
                    Intent settingActivityIntent = new Intent( this, typeof(SettingActivity));
                    StartActivityForResult(settingActivityIntent, CODE_SETTING_ACTIVITY);
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);
            selectedQuote = (Quote)lstAdapter.GetItem(position);
            TextView txtSelectedItem = (TextView)v;
            if (selectedQuote.ID != Quote.GetSelectedId())
            {
                txtShortDetail.Text = selectedQuote.ShortDetail;
                selectedQuote.SelectQuote();
                UpdateQuotesList();
            }
            else
            {
                OpenQuoteDetailActivity(selectedQuote);
            }

        }
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //  1. add SNACKBAR
            //  2. change DetailActivity PriorityManager just like NewQuoteActivity
            //  3.  update listview ui and change its item widget
            //  5.  space for empty ? => happens in newquote
            //  7. add delete option
            try
            {
                SetContentView(Resource.Layout.main_activity);
                sqlite = new SQLiteInterface(this);
                selectedQuote = null;
                txtShortDetail = FindViewById<TextView>(Resource.Id.txt_short_detail);
                btnAdd = FindViewById<Button>(Resource.Id.btn_add);
                sqlite.Open();

                txtShortDetail.Click += new EventHandler(txtShortDetail_Click);
                btnAdd.Click += new EventHandler(btnAddOrDetail_click);
                UpdateQuotesList();

            }
            catch (Android.Database.Sqlite.SQLiteException ex)
            {
                Log.WriteLine(LogPriority.Error, SQLiteInterface.SQLITE_EXCEPTION, ex.Message);
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            catch (Exception totalEx)
            {
                Log.WriteLine(LogPriority.Error, TOTAL_ERROR, totalEx.Message);
                Toast.MakeText(this, totalEx.Message, ToastLength.Long).Show();
            }

        }

        protected override void OnResume()
        {
            base.OnResume();
            UpdateQuotesList();
        }

        protected override void OnPause()
        {
            base.OnPause();
            Quote.DeSelectQuotes();
            txtShortDetail.Text = "";
        }

        private void UpdateQuotesList()
        {
            try
            {
                allQuotes = sqlite.GetQuotes();
                lstAdapter = new QuotesListAdapter(this, Resource.Layout.listview_item, allQuotes);
                ListAdapter = lstAdapter;
            }
            catch (Exception ex)
            {
                Log.WriteLine(LogPriority.Error, UPDATE_LIST_EXCEPTION, ex.Message);
            }
        }

        private void OpenQuoteDetailActivity(Quote quote)
        {
            Intent QuoteDetailIntent = new Intent(this, typeof(QuoteDetailActivity));

            QuoteDetailIntent.PutExtra(SQLiteInterface.DB_COL_ID, quote.ID);
            QuoteDetailIntent.PutExtra(SQLiteInterface.DB_COL_PERSON_QUOTED, quote.PersonQuoted);
            QuoteDetailIntent.PutExtra(SQLiteInterface.DB_COL_PERSON_QUOTED_TO, quote.PersonQuotedTo);
            QuoteDetailIntent.PutExtra(SQLiteInterface.DB_COL_TITLE, quote.Title);
            QuoteDetailIntent.PutExtra(SQLiteInterface.DB_COL_TEXT, quote.Text);
            QuoteDetailIntent.PutExtra(SQLiteInterface.DB_COL_DATE, quote.Date.ToLongDateString());
            QuoteDetailIntent.PutExtra(SQLiteInterface.DB_COL_IMPORTANCE, quote.Importance);

            StartActivity(QuoteDetailIntent);
        }

        private void btnAddOrDetail_click(object sender, EventArgs e)
        {
            StartActivityForResult(typeof(NewQuoteActivity), CODE_ADD_NEW_QUOTE_ACTIVITY);

        }

        private void txtShortDetail_Click(object sender, EventArgs e)
        {
            if (txtShortDetail.Text != "" && selectedQuote != null)
                OpenQuoteDetailActivity(selectedQuote);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == CODE_ADD_NEW_QUOTE_ACTIVITY)
                UpdateQuotesList();
        }

    }
    public class QuotesListAdapter : ArrayAdapter<Quote>
    {

        private int resourceLayout;
        private Context mContext;

        public QuotesListAdapter(Context context, int resource, List<Quote> items) : base(context, resource, items)
        {
            resourceLayout = resource;
            mContext = context;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            View view = convertView;

            if (view == null)
            {
                LayoutInflater lytInf = LayoutInflater.From(mContext);
                view = lytInf.Inflate(resourceLayout, null);
            }

            Quote thisQuote = GetItem(position);

            if (thisQuote != null)
            {
                TextView mainTextView = view.FindViewById<TextView>(Android.Resource.Id.Text1);

                if (mainTextView != null)
                {
                    mainTextView.Text = thisQuote.ToString();
                    if (Quote.GetSelectedId() == thisQuote.ID)
                        mainTextView.SetTextColor(Android.Graphics.Color.Green);
                    else
                        mainTextView.SetTextColor(Quote.GetPriorityColor(thisQuote.Importance));
                }

            }

            return view;
        }

    }
}

