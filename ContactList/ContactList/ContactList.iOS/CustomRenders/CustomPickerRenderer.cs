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

[assembly: ExportRenderer(typeof(Picker), typeof(CustomPickerRenderer))]
namespace ContactList.iOS.CustomRenders
{
    public class CustomPickerRenderer : PickerRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
                if (e.NewElement != null)
                e.NewElement.SizeChanged += (obj, args) =>
                {
                    var xamEl = obj as Picker;
                    if (xamEl == null)
                        return;

                    var entry = this.Control;

                    CALayer border = new CALayer();
                    float width = 1.0f;
                    border.BorderColor = UIColor.FromRGB(244, 244, 244).CGColor; 
                    border.Frame = new CGRect(x: 0, y: xamEl.Height - width, width: xamEl.Width, height: 1.0f);
                    border.BorderWidth = width;

                    entry.Layer.AddSublayer(border);

                    entry.Layer.MasksToBounds = true;
                    entry.BorderStyle = UITextBorderStyle.None;
                    entry.BackgroundColor = new UIColor(1, 1, 1, 1); 
                };
        }
    }
}