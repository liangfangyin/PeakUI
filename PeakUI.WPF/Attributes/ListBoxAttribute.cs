using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace PeakUI.WPF
{ 
    public class ListBoxAttribute
    {


        /// <summary>
        /// item margin
        /// </summary>
        public static readonly DependencyProperty ItemMarginProperty = DependencyProperty.RegisterAttached(
            "ItemMargin", typeof(Thickness), typeof(ListBoxAttribute), new PropertyMetadata(default(Thickness)));

        /// <summary>
        /// Gets the item margin.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A Thickness.</returns>
        public static Thickness GetItemMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(ItemMarginProperty);
        }

        /// <summary>
        /// Sets the item margin.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetItemMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(ItemMarginProperty, value);
        }

        /// <summary>
        /// item padding
        /// </summary>
        public static readonly DependencyProperty ItemPaddingProperty = DependencyProperty.RegisterAttached(
            "ItemPadding", typeof(Thickness), typeof(ListBoxAttribute), new PropertyMetadata(default(Thickness)));

        /// <summary>
        /// Gets the item padding.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A Thickness.</returns>
        public static Thickness GetItemPadding(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(ItemPaddingProperty);
        }

        /// <summary>
        /// Sets the item padding.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetItemPadding(DependencyObject obj, Thickness value)
        {
            obj.SetValue(ItemPaddingProperty, value);
        }

        /// <summary>
        /// item padding
        /// </summary>
        public static readonly DependencyProperty SelectBackgroundProperty = DependencyProperty.RegisterAttached(
            "SelectBackground", typeof(Brush), typeof(ListBoxAttribute), new PropertyMetadata(default(Brush)));

        public static Brush GetSelectBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SelectBackgroundProperty);
        }

        public static void SetSelectBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(SelectBackgroundProperty, value);
        }

        /// <summary>
        /// item BorderBrush
        /// </summary>
        public static readonly DependencyProperty SelectBorderBrushProperty = DependencyProperty.RegisterAttached(
            "SelectBorderBrush", typeof(Brush), typeof(ListBoxAttribute), new PropertyMetadata(default(Brush)));

        public static Brush GetSelectBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SelectBorderBrushProperty);
        }

        public static void SetSelectBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(SelectBorderBrushProperty, value);
        }


    }

}
