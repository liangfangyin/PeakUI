﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PeakUI.WPF
{
    public class ControlAttribute
    {

        #region 圆角
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius",
            typeof(CornerRadius), typeof(ControlAttribute), new PropertyMetadata(default(CornerRadius)));

        public static void SetCornerRadius(DependencyObject dependency, CornerRadius value)
        {
            dependency.SetValue(CornerRadiusProperty,value);
        }

        public static CornerRadius GetCornerRadius(DependencyObject dependency) 
        {
            return (CornerRadius)dependency.GetValue(CornerRadiusProperty);
        }
        #endregion



        /// <summary>
        /// 聚焦颜色
        /// </summary>
        public static readonly DependencyProperty FocusedBrushProperty = DependencyProperty.RegisterAttached(
            "FocusedBrush", typeof(Brush), typeof(ControlAttribute), new PropertyMetadata(default(Brush)));

        /// <summary>
        /// Sets the focused brush.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetFocusedBrush(DependencyObject element, Brush value)
        {
            element.SetValue(FocusedBrushProperty, value);
        }

        /// <summary>
        /// Gets the focused brush.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>A Brush.</returns>
        public static Brush GetFocusedBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(FocusedBrushProperty);
        }

        /// <summary>
        /// 聚焦前景色
        /// </summary>
        public static readonly DependencyProperty FocusedForegroundBrushProperty = DependencyProperty.RegisterAttached(
            "FocusedForegroundBrush", typeof(Brush), typeof(ControlAttribute), new PropertyMetadata(default(Brush)));

        /// <summary>
        /// Sets the focused foreground brush.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetFocusedForegroundBrush(DependencyObject element, Brush value)
        {
            element.SetValue(FocusedForegroundBrushProperty, value);
        }

        /// <summary>
        /// Gets the focused foreground brush.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>A Brush.</returns>
        public static Brush GetFocusedForegroundBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(FocusedForegroundBrushProperty);
        }

        /// <summary>
        /// 聚焦边框颜色
        /// </summary>
        public static readonly DependencyProperty FocusBorderBrushProperty = DependencyProperty.RegisterAttached(
             "FocusBorderBrush", typeof(Brush), typeof(ControlAttribute), new PropertyMetadata(default(Brush)));

        /// <summary>
        /// Sets the focus border brush.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetFocusBorderBrush(DependencyObject element, Brush value)
        {
            element.SetValue(FocusBorderBrushProperty, value);
        }

        /// <summary>
        /// Gets the focus border brush.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>A Brush.</returns>
        public static Brush GetFocusBorderBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(FocusBorderBrushProperty);
        }

        /// <summary>
        /// 聚焦边框厚度
        /// </summary>
        public static readonly DependencyProperty FocusBorderThicknessProperty = DependencyProperty.RegisterAttached(
            "FocusBorderThickness", typeof(Thickness), typeof(ControlAttribute), new PropertyMetadata(default(Thickness)));

        /// <summary>
        /// Sets the focus border thickness.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetFocusBorderThickness(DependencyObject element, Thickness value)
        {
            element.SetValue(FocusBorderThicknessProperty, value);
        }

        /// <summary>
        /// Gets the focus border thickness.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>A Thickness.</returns>
        public static Thickness GetFocusBorderThickness(DependencyObject element)
        {
            return (Thickness)element.GetValue(FocusBorderThicknessProperty);
        }

        /// <summary>
        /// 鼠标悬停颜色
        /// </summary>
        public static readonly DependencyProperty MouseOverBrushProperty = DependencyProperty.RegisterAttached(
            "MouseOverBrush", typeof(Brush), typeof(ControlAttribute), new PropertyMetadata(default(Brush)));

        /// <summary>
        /// Sets the mouse over brush.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetMouseOverBrush(DependencyObject element, Brush value)
        {
            element.SetValue(MouseOverBrushProperty, value);
        }

        /// <summary>
        /// Gets the mouse over brush.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>A Brush.</returns>
        public static Brush GetMouseOverBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(MouseOverBrushProperty);
        }

        /// <summary>
        /// 按下颜色
        /// </summary>
        public static readonly DependencyProperty PressedBrushProperty = DependencyProperty.RegisterAttached(
           "PressedBrush", typeof(Brush), typeof(ControlAttribute), new PropertyMetadata(default(Brush)));

        /// <summary>
        /// Sets the pressed brush.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetPressedBrush(DependencyObject element, Brush value)
        {
            element.SetValue(PressedBrushProperty, value);
        }

        /// <summary>
        /// Gets the pressed brush.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>A Brush.</returns>
        public static Brush GetPressedBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(PressedBrushProperty);
        }

        /// <summary>
        /// 选中颜色
        /// </summary>
        public static readonly DependencyProperty SelectedBrushProperty = DependencyProperty.RegisterAttached(
           "SelectedBrush", typeof(Brush), typeof(ControlAttribute), new PropertyMetadata(default(Brush)));

        /// <summary>
        /// Sets the Selected brush.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetSelectedBrush(DependencyObject element, Brush value)
        {
            element.SetValue(SelectedBrushProperty, value);
        }

        /// <summary>
        /// Gets the Selected brush.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>A Brush.</returns>
        public static Brush GetSelectedBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(SelectedBrushProperty);
        }

        /// <summary>
        /// 是否聚焦
        /// </summary>
        public static readonly DependencyProperty IsKeyBoardFocusedProperty = DependencyProperty.RegisterAttached(
            "IsKeyBoardFocused", typeof(bool), typeof(ControlAttribute), new PropertyMetadata(false));

        /// <summary>
        /// Gets the is key board focused.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A bool.</returns>
        public static bool GetIsKeyBoardFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsKeyBoardFocusedProperty);
        }

        /// <summary>
        /// Sets the is key board focused.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">If true, value.</param>
        public static void SetIsKeyBoardFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsKeyBoardFocusedProperty, value);
        }

        /// <summary>
        /// 遮罩透明度
        /// </summary>
        public static readonly DependencyProperty MaskOpacityProperty = DependencyProperty.RegisterAttached(
            "MaskOpacity", typeof(double), typeof(ControlAttribute), new PropertyMetadata(0.6));

        /// <summary>
        /// Gets the mask opacity.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A double.</returns>
        public static double GetMaskOpacity(DependencyObject obj)
        {
            return (double)obj.GetValue(MaskOpacityProperty);
        }

        /// <summary>
        /// Sets the mask opacity.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetMaskOpacity(DependencyObject obj, double value)
        {
            obj.SetValue(MaskOpacityProperty, value);
        }



        /// <summary>
        /// 聚焦边框颜色
        /// </summary>
        public static readonly DependencyProperty PrefixColorProperty = DependencyProperty.RegisterAttached(
             "PrefixColor", typeof(Brush), typeof(ControlAttribute), new PropertyMetadata(default(Brush)));
         
        public static void SetPrefixColor(DependencyObject element, Brush value)
        {
            element.SetValue(FocusBorderBrushProperty, value);
        }
         
        public static Brush GetPrefixColor(DependencyObject element)
        {
            return (Brush)element.GetValue(FocusBorderBrushProperty);
        }



    }
}
