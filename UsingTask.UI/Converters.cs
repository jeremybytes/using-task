using System;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace UsingTask.UI
{
    public class DecadeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int year = ((DateTime)value).Year;
            return string.Format("{0}0s", year.ToString().Substring(0, 3));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int rating = (int)value;
            return string.Format("{0}/10 Stars", rating.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RatingStarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int rating = (int)value;
            string output = string.Empty;
            return output.PadLeft(rating, '*');
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string input = (string)value;
            //int rating = 0;

            //foreach (var ch in input)
            //    if (ch == '*')
            //        rating++;

            //return rating;

            return input.Count(c => c == '*');
        }
    }

    public class DecadeBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int decade = (((DateTime)value).Year / 10) * 10;

            switch (decade)
            {
                case 1970:
                    return new SolidColorBrush(Colors.Maroon);
                case 1980:
                    return new SolidColorBrush(Colors.DarkGreen);
                case 1990:
                    return new SolidColorBrush(Colors.DarkSlateBlue);
                case 2000:
                    return new SolidColorBrush(Colors.CadetBlue);
                default:
                    return new SolidColorBrush(Colors.DarkSlateGray);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
