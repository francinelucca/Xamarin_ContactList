using System;
using ContactList.Controls;
using ContactList.Droid.CustomRenders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(CustomBorderlessEntryRenderer))]
namespace ContactList.Droid.CustomRenders
{
    public class CustomBorderlessEntryRenderer : EntryRenderer
    {
        public CustomBorderlessEntryRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            else
                Control.Background.SetColorFilter(Android.Graphics.Color.Transparent, PorterDuff.Mode.SrcAtop);
        }
    }
}
