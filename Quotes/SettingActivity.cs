
using Android.App;

using Android.OS;
using Android.Widget;
using System;

namespace Quotes
{
    [Activity(Label = "SettingActivity")]
    public class SettingActivity : Activity
    {
        private ToggleButton tbtnSetTheme, tbtnSetFontsize;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_setting);
            this.Title = GetString(Resource.String.text_setting_activity_title);
            tbtnSetTheme = FindViewById<ToggleButton>(Resource.Id.btn_set_app_theme);
            tbtnSetFontsize = FindViewById<ToggleButton>(Resource.Id.btn_set_app_fontsize);

            // must be changed in future
            Settings.Load();
            tbtnSetTheme.Text = Settings.AppTheme.ToString();
            tbtnSetFontsize.Text = Settings.AppFontSize.ToString();
            
            tbtnSetTheme.Click += new EventHandler(tbtnSetTheme_Touched);
            tbtnSetFontsize.Click += new EventHandler(tbtnSetFontsize_Touched);
        }

        private void tbtnSetFontsize_Touched(object sender, EventArgs e)
        {
            Settings.AppFontSize++;
            tbtnSetFontsize.Text = Settings.AppFontSize.ToString();
            
        }

        private void tbtnSetTheme_Touched(object sender, EventArgs e)
        {
            Settings.AppTheme++;
            tbtnSetTheme.Text = Settings.AppTheme.ToString();

        }
    }
}