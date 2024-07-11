using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PeakUI.WPF
{
    public class ContainerAttribute
    {

        #region 内容

        /// <summary>
        /// 内容
        /// </summary>
        public static readonly DependencyProperty ContentProperty = DependencyProperty.RegisterAttached("Content", typeof(object), typeof(ContainerAttribute), new PropertyMetadata(null));

        /// <summary>
        /// Gets the pre content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>An object.</returns>
        public static object GetContent(DependencyObject obj)
        {
            return obj.GetValue(ContentProperty);
        }

        /// <summary>
        /// Sets the pre content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetContent(DependencyObject obj, object value)
        {
            obj.SetValue(ContentProperty, value);
        }


        /// <summary>
        /// 内容颜色
        /// </summary>
        public static readonly DependencyProperty ContentBrushProperty = DependencyProperty.RegisterAttached(
            "ContentBrush", typeof(Brush), typeof(ContainerAttribute), new PropertyMetadata(default(Brush)));
         
        public static void SetContentBrush(DependencyObject element, Brush value)
        {
            element.SetValue(ContentBrushProperty, value);
        }
         
        public static Brush GetContentBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(ContentBrushProperty);
        }

        #endregion

        #region 左内容

        /// <summary>
        /// 内容
        /// </summary>
        public static readonly DependencyProperty LeftContentProperty = DependencyProperty.RegisterAttached("LeftContent", typeof(object), typeof(ContainerAttribute), new PropertyMetadata(null));

        /// <summary>
        /// Gets the pre content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>An object.</returns>
        public static object GetLeftContent(DependencyObject obj)
        {
            return obj.GetValue(LeftContentProperty);
        }

        /// <summary>
        /// Sets the pre content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetLeftContent(DependencyObject obj, object value)
        {
            obj.SetValue(LeftContentProperty, value);
        }

        /// <summary>
        /// 左内容颜色
        /// </summary>
        public static readonly DependencyProperty LeftContentBrushProperty = DependencyProperty.RegisterAttached(
            "LeftContentBrush", typeof(Brush), typeof(ContainerAttribute), new PropertyMetadata(default(Brush)));

        public static void SetLeftContentBrush(DependencyObject element, Brush value)
        {
            element.SetValue(LeftContentBrushProperty, value);
        }

        public static Brush GetLeftContentBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(LeftContentBrushProperty);
        }

        #endregion

        #region 右内容

        /// <summary>
        /// 内容
        /// </summary>
        public static readonly DependencyProperty RightContentProperty = DependencyProperty.RegisterAttached("RightContent", typeof(object), typeof(ContainerAttribute), new PropertyMetadata(null));

        /// <summary>
        /// Gets the pre content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>An object.</returns>
        public static object GetRightContent(DependencyObject obj)
        {
            return obj.GetValue(RightContentProperty);
        }

        /// <summary>
        /// Sets the pre content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetRightContent(DependencyObject obj, object value)
        {
            obj.SetValue(RightContentProperty, value);
        }

        /// <summary>
        /// 右内容颜色
        /// </summary>
        public static readonly DependencyProperty RightContentBrushProperty = DependencyProperty.RegisterAttached(
            "RightContentBrush", typeof(Brush), typeof(ContainerAttribute), new PropertyMetadata(default(Brush)));

        public static void SetRightContentBrush(DependencyObject element, Brush value)
        {
            element.SetValue(RightContentBrushProperty, value);
        }

        public static Brush GetRightContentBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(RightContentBrushProperty);
        }

        #endregion

        #region 头内容

        /// <summary>
        /// 内容
        /// </summary>
        public static readonly DependencyProperty HeaderContentProperty = DependencyProperty.RegisterAttached("HeaderContent", typeof(object), typeof(ContainerAttribute), new PropertyMetadata(null));

        /// <summary>
        /// Gets the pre content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>An object.</returns>
        public static object GetHeaderContent(DependencyObject obj)
        {
            return obj.GetValue(HeaderContentProperty);
        }

        /// <summary>
        /// Sets the pre content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetHeaderContent(DependencyObject obj, object value)
        {
            obj.SetValue(HeaderContentProperty, value);
        }

        /// <summary>
        /// 头部内容颜色
        /// </summary>
        public static readonly DependencyProperty HeaderContentBrushProperty = DependencyProperty.RegisterAttached(
            "HeaderContentBrush", typeof(Brush), typeof(ContainerAttribute), new PropertyMetadata(default(Brush)));

        public static void SetHeaderContentBrush(DependencyObject element, Brush value)
        {
            element.SetValue(HeaderContentBrushProperty, value);
        }

        public static Brush GetHeaderContentBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(HeaderContentBrushProperty);
        }

        #endregion

        #region 尾部内容

        /// <summary>
        /// 内容
        /// </summary>
        public static readonly DependencyProperty FoodContentProperty = DependencyProperty.RegisterAttached("FoodContent", typeof(object), typeof(ContainerAttribute), new PropertyMetadata(null));

        /// <summary>
        /// Gets the pre content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>An object.</returns>
        public static object GetFoodContent(DependencyObject obj)
        {
            return obj.GetValue(FoodContentProperty);
        }

        /// <summary>
        /// Sets the pre content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetFoodContent(DependencyObject obj, object value)
        {
            obj.SetValue(FoodContentProperty, value);
        }

        /// <summary>
        /// 尾部内容颜色
        /// </summary>
        public static readonly DependencyProperty FoodContentBrushProperty = DependencyProperty.RegisterAttached(
            "FoodContentBrush", typeof(Brush), typeof(ContainerAttribute), new PropertyMetadata(default(Brush)));

        public static void SetFoodContentBrush(DependencyObject element, Brush value)
        {
            element.SetValue(FoodContentBrushProperty, value);
        }

        public static Brush GetFoodContentBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(FoodContentBrushProperty);
        }

        #endregion



    }
}
