using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactList.Controls;
using ContactList.iOS.CustomRenders;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(CustomBorderlessEntryRenderer))]
namespace ContactList.iOS.CustomRenders
{
    public class CustomBorderlessEntryRenderer : EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null)
                return;

            Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}