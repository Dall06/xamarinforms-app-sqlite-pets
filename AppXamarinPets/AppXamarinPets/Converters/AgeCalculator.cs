using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AppXamarinPets.Converters
{
    public class AgeCalculator : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime birthdate = DateTime.Parse(value.ToString());

            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - birthdate.Year;
            // Go back to the year the person was born in case of a leap year
            if (birthdate.Date > today.AddYears(-age)) age--;

            return age;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
