using System;
using Android.Graphics;

namespace Quotes
{
    public class Quote
    {
        private const long NOT_SELECTED = -1;
        private long id;
        private string personQuoted, personQuotedTo, title, text;
        private DateTime date;
        private short importance;
        private static long sSelectedId = Quote.NOT_SELECTED;
        public const short DEFAULT_IMPORTANCE = 50;
        public const float IMPORTANCE_RATE = 2.55f;

        public long ID
        {
            get => id;
            set => id = value;
        }

        public string PersonQuoted
        {
            get => personQuoted;
            set => personQuoted = value;
        }

        public string PersonQuotedTo
        {
            get => personQuotedTo;
            set => personQuotedTo = value;
        }

        public string Title
        {
            get => title;
            set => title = value;
        }

        public string Text
        {
            get => text;
            set => text = value;
        }

        public DateTime Date
        {
            get => date;
            set => date = value;
        }

        public short Importance
        {
            get => importance;
            set => importance = value;
        }

        public Quote(long pId, string pTitle,string pText, DateTime pDate,short pImportance, string pPersonQuoted , string pPersonQuotedTo )
        {
            ID = pId;
            PersonQuotedTo = pPersonQuotedTo;
            Title = pTitle;
            PersonQuoted = pPersonQuoted;
            Text = pText;
            Date = pDate;
            Importance = pImportance;
        }

        public Quote()
        {
            PersonQuoted = PersonQuotedTo = Title = Text = "";
            importance = DEFAULT_IMPORTANCE;
            date = new DateTime();
        }

        public override string ToString()
        {
            return Importance.ToString() + "% | " + Title;
        }

        public string ShortDetail => PersonQuoted + " >> " + PersonQuotedTo + "  :\n" + Text;

        public static Color GetPriorityColor(int importanceValue)
        {
            return Color.Rgb((short)(IMPORTANCE_RATE * importanceValue), 0, (short)((100 - importanceValue) * IMPORTANCE_RATE));
        }

        public void SelectQuote()
        {
            Quote.sSelectedId = this.id;
        }

        public static long GetSelectedId()
        {
            return Quote.sSelectedId;
        }

        public static void DeSelectQuotes()
        {
            Quote.sSelectedId = Quote.NOT_SELECTED;
        }
    }
}