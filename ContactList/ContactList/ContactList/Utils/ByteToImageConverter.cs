using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace ContactList.Utils
{
	public class ByteToImageConverter : IValueConverter
	{
        public ImageSource ConvertByteArrayToImageSource(byte[] imageByteArray)
        {
            Stream stream = new MemoryStream(imageByteArray);
            return ImageSource.FromStream(() => { return stream; });
        }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource img = null;
            if(value != null)
            {
                img = this.ConvertByteArrayToImageSource(value as byte[]);
            }
            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
