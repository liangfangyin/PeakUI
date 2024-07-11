using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;

namespace PeakUI.WPF
{
    /// <summary>
    ///  PasswordBox 帮助类
    /// </summary>
    public class PasswordBoxAttribute
    {
        /// <summary>
        /// 聚焦边框颜色
        /// </summary>
        public static readonly DependencyProperty PrefixColorProperty = DependencyProperty.RegisterAttached(
             "PrefixColor", typeof(Brush), typeof(PasswordBoxAttribute), new PropertyMetadata(default(Brush)));

        public static void SetPrefixColor(DependencyObject element, Brush value)
        {
            element.SetValue(PrefixColorProperty, value);
        }

        public static Brush GetPrefixColor(DependencyObject element)
        {
            return (Brush)element.GetValue(PrefixColorProperty);
        }

        /// <summary>
        /// 是否可以绑定密码
        /// </summary>
        public static readonly DependencyProperty IsBindableProperty = DependencyProperty.RegisterAttached(
            "IsBindable", typeof(bool), typeof(PasswordBoxAttribute), new PropertyMetadata(default(bool), OnIsBindableChanaged));

        /// <summary>
        /// Gets the is bindable.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A bool.</returns>
        public static bool GetIsBindable(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsBindableProperty);
        }

        /// <summary>
        /// Sets the is bindable.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">If true, value.</param>
        public static void SetIsBindable(DependencyObject obj, bool value)
        {
            obj.SetValue(IsBindableProperty, value);
        }

        /// <summary>
        /// 可绑定的密码
        /// </summary>
        public static readonly DependencyProperty BindablePasswordProperty = DependencyProperty.RegisterAttached(
            "BindablePassword", typeof(string), typeof(PasswordBoxAttribute), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBindablePasswordChanged));

        /// <summary>
        /// Gets the bindable password.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A string.</returns>
        public static string GetBindablePassword(DependencyObject obj)
        {
            return (string)obj.GetValue(BindablePasswordProperty);
        }

        /// <summary>
        /// Sets the bindable password.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetBindablePassword(DependencyObject obj, string value)
        {
            obj.SetValue(BindablePasswordProperty, value);
        }

        /// <summary>
        /// 是否显示切换密码可见按钮
        /// </summary>
        public static readonly DependencyProperty ShowSwitchButtonProperty = DependencyProperty.RegisterAttached(
            "ShowSwitchButton", typeof(bool), typeof(PasswordBoxAttribute), new PropertyMetadata(false, SwitchPasswordShow));

        /// <summary>
        /// Gets the show switch button.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A bool.</returns>
        public static bool GetShowSwitchButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowSwitchButtonProperty);
        }

        /// <summary>
        /// Sets the show switch button.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">If true, value.</param>
        public static void SetShowSwitchButton(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowSwitchButtonProperty, value);
        }

        /// <summary>
        /// 显示密码
        /// </summary>
        public static readonly DependencyProperty ShowPasswordProperty = DependencyProperty.RegisterAttached(
            "ShowPassword", typeof(bool), typeof(PasswordBoxAttribute));

        /// <summary>
        /// Gets the show password.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A bool.</returns>
        public static bool GetShowPassword(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowPasswordProperty);
        }

        /// <summary>
        /// Sets the show password.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">If true, value.</param>
        public static void SetShowPassword(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowPasswordProperty, value);
        }

        private static void OnIsBindableChanaged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                RoutedEventHandler handler = (a, b) => SetBindablePassword(passwordBox, passwordBox.Password);
                if (GetIsBindable(passwordBox))
                {
                    passwordBox.PasswordChanged += handler;
                }
                else
                {
                    passwordBox.PasswordChanged -= handler;
                }
            }
        }

        private static void OnBindablePasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                if (GetIsBindable(passwordBox))
                {
                    if (passwordBox.Password.Equals(GetBindablePassword(passwordBox)))
                    {
                        return;
                    }

                    passwordBox.Password = GetBindablePassword(passwordBox);
                    if (passwordBox.Password.Length > 0)
                    {
                        _ = passwordBox.GetType()
                                       .GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic)
                                       .Invoke(passwordBox, new object[] { passwordBox.Password.Length, 0 });
                    }
                }
            }
        }

        private static void SwitchPasswordShow(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                MouseButtonEventHandler handleDown = (sender, args) => SetShowPassword(passwordBox, true);
                MouseButtonEventHandler handleUp = (sender, args) => SetShowPassword(passwordBox, false);

                passwordBox.Loaded += (sender, arg) =>
                {
                    if (passwordBox.Template.FindName("switchVisibilityButton", passwordBox) is Button switchButton)
                    {
                        switchButton.AddHandler(UIElement.MouseDownEvent, handleDown, true);
                        switchButton.AddHandler(UIElement.MouseUpEvent, handleUp, true);
                    }
                };

                passwordBox.Unloaded += (sender, arg) =>
                {
                    if (passwordBox.Template.FindName("switchVisibilityButton", passwordBox) is Button switchButton)
                    {
                        switchButton.RemoveHandler(UIElement.MouseDownEvent, handleDown);
                        switchButton.RemoveHandler(UIElement.MouseUpEvent, handleUp);
                    }
                };
            }
        }
    }
}
