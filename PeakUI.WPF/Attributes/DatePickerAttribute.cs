using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PeakUI.WPF
{
    public class DatePickerAttribute
    {


        /// <summary>
        /// 宽度
        /// </summary>
        public static readonly DependencyProperty RangeWidthProperty = DependencyProperty.RegisterAttached("RangeWidth", typeof(int), typeof(DatePickerAttribute), new PropertyMetadata(default(int)));

        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetRangeWidth(DependencyObject element, int value)
        {
            element.SetValue(RangeWidthProperty, value);
        }

        /// <summary>
        /// 获取宽度
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>A Brush.</returns>
        public static int GetRangeWidth(DependencyObject element)
        {
            return (int)element.GetValue(RangeWidthProperty);
        }




    }
}
