using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ContactList.Controls;
using ContactList.Droid.CustomRenders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(UnderlinedEntry), typeof(CustomUnderlinedEntryRenderer))]
namespace ContactList.Droid.CustomRenders
{
    public class CustomUnderlinedEntryRenderer : EntryRenderer
    {
        public CustomUnderlinedEntryRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.LightGray);
            else
                Control.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.SrcAtop);
        }
    }
}