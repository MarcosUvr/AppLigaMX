using AppLigaMX.Services;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace AppLigaMX.Converters
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Se despliega una imagen de notFound si no se encuentra una imagen
            if (value == null || string.IsNullOrEmpty(value.ToString())) return "notfound.png";

            //Se convierte la imagen en Base64 a ImageSource
            return new ImageService().ConvertImageFromBase64ToImageSource(value.ToString());
        }

        //Se convierte de vuelta
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
