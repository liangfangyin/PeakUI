using PeakUI.WPF.Model;
using System.Windows;

namespace PeakUI.WPF
{
    public class ButtonAttribute
    {

         

        #region 前置图标
        /// <summary>
        /// 前置是否显示
        /// </summary>
        public static readonly DependencyProperty IsPreContentProperty = DependencyProperty.RegisterAttached("IsPreContent", typeof(Visibility), typeof(ButtonAttribute), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// Gets the post content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>An object.</returns>
        public static Visibility GetIsPreContent(DependencyObject obj)
        {
            return (Visibility)obj.GetValue(IsPreContentProperty);
        }

        /// <summary>
        /// Sets the post content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetIsPreContent(DependencyObject obj, Visibility value)
        {
            obj.SetValue(IsPreContentProperty, value);
        }


        /// <summary>
        /// 前置图标
        /// </summary>
        public static readonly DependencyProperty PreIconProperty = DependencyProperty.RegisterAttached("PreIcon", typeof(IconType), typeof(ButtonAttribute), new PropertyMetadata(null));

        /// <summary>
        /// Gets the post content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>An object.</returns>
        public static IconType GetPreIcon(DependencyObject obj)
        {
            return (IconType)obj.GetValue(PreIconProperty);
        }

        /// <summary>
        /// Sets the post content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetPreIcon(DependencyObject obj, IconType value)
        {
            obj.SetValue(PreIconProperty, value);
        }

        #endregion

        #region 后置图标
        /// <summary>
        /// 后置是否显示
        /// </summary>
        public static readonly DependencyProperty IsPostContentProperty = DependencyProperty.RegisterAttached("IsPostContent", typeof(Visibility), typeof(ButtonAttribute), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// Gets the post content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>An object.</returns>
        public static Visibility GetIsPostContent(DependencyObject obj)
        {
            return (Visibility)obj.GetValue(IsPostContentProperty);
        }

        /// <summary>
        /// Sets the post content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetIsPostContent(DependencyObject obj, Visibility value)
        {
            obj.SetValue(IsPostContentProperty, value);
        }


        /// <summary>
        /// 后置图标
        /// </summary>
        public static readonly DependencyProperty PostIconProperty = DependencyProperty.RegisterAttached("PostIcon", typeof(IconType), typeof(ButtonAttribute), new PropertyMetadata(null));

        /// <summary>
        /// Gets the post content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>An object.</returns>
        public static IconType GetPostIcon(DependencyObject obj)
        {
            return (IconType)obj.GetValue(PostIconProperty);
        }

        /// <summary>
        /// Sets the post content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetPostIcon(DependencyObject obj, IconType value)
        {
            obj.SetValue(PostIconProperty, value);
        }

        #endregion


    }
}
