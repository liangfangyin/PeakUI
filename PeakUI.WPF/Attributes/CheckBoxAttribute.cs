using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PeakUI.WPF
{
    public class CheckBoxAttribute
    {

        /// <summary>
        /// 文字1
        /// </summary>
        public static readonly DependencyProperty OneTextProperty = DependencyProperty.RegisterAttached("OneText", typeof(string), typeof(CheckBoxAttribute), new PropertyMetadata(default(string)));

        public static void SetOneText(DependencyObject element, string value)
        {
            element.SetValue(OneTextProperty, value);
        }
        public static string GetOneText(DependencyObject element)
        {
            return (string)element.GetValue(OneTextProperty);
        }

        /// <summary>
        /// 文字2
        /// </summary>
        public static readonly DependencyProperty TwoTextProperty = DependencyProperty.RegisterAttached("TwoText", typeof(string), typeof(CheckBoxAttribute), new PropertyMetadata(default(string)));

        public static void SetTwoText(DependencyObject element, string value)
        {
            element.SetValue(TwoTextProperty, value);
        }
        public static string GetTwoText(DependencyObject element)
        {
            return (string)element.GetValue(TwoTextProperty);
        }



    }
}
