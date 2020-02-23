using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactList.iOS.CustomRenders;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace ContactList.iOS.CustomRenders
{
    public class CustomEntryRenderer : EntryRenderer
    {
        private CALayer _line;


        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            _line = null;

            if (Control == null || e.NewElement == null)
                return;

            Control.BorderStyle = UITextBorderStyle.None;

            _line = new CALayer
            {
                BorderColor = UIColor.FromRGB(244, 244, 244).CGColor,
                BackgroundColor = UIColor.FromRGB(244, 244, 244).CGColor,
                Frame = new CGRect(0, Frame.Height / 2, Frame.Width + 100, 1f)
            };

            Control.Layer.AddSublayer(_line);
        }
    }
}