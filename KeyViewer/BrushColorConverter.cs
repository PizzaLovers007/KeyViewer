using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace KeyViewer
{
    [ValueConversion(typeof(Brush), typeof(Color))]
    public class BrushToColorConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            SolidColorBrush brush = (SolidColorBrush)value;
            return brush.Color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            Color color = (Color)value;
            return new SolidColorBrush(color);
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
