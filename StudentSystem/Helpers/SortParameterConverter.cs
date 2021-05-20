using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Microsoft.Toolkit.Uwp.UI.Controls;

namespace StudentSystem.Helpers
{
    
    public class SortParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DataGridColumn c = null;
            if(value is DataGridColumnEventArgs args)
            {
                c = args.Column;
            }
            return c;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
