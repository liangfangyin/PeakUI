using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace PeakUI.WPF
{
    /// <summary>
    /// 标记
    /// </summary>
    public class Badge : ContentControl
    {
        static Badge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Badge), new FrameworkPropertyMetadata(typeof(Badge)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

        }

        #region 属性

        /// <summary>
        /// 显示文本
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(Badge), new PropertyMetadata(default(string), OnTextChanged));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register("Max", typeof(int), typeof(Badge), new PropertyMetadata(default(int), OnTextChanged));

        public int Max
        {
            get { return (int)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        /// <summary>
        /// 只显示点
        /// </summary>
        public static readonly DependencyProperty IsShowLittleDotProperty = DependencyProperty.Register("IsShowLittleDot", typeof(bool), typeof(Badge), new PropertyMetadata(default(bool)));

        public bool IsShowLittleDot
        {
            get { return (bool)GetValue(IsShowLittleDotProperty); }
            set { SetValue(IsShowLittleDotProperty, value); }
        }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public static readonly DependencyProperty IsHiddenProperty = DependencyProperty.Register("IsHidden", typeof(bool), typeof(Badge), new PropertyMetadata(default(bool)));

        public bool IsHidden
        {
            get { return (bool)GetValue(IsHiddenProperty); }
            set { SetValue(IsHiddenProperty, value); }
        }

        #endregion 属性

        #region 事件

        /// <summary>
        /// 文本改变事件
        /// </summary>
        public static readonly RoutedEvent TextChangedEvent = EventManager.RegisterRoutedEvent("TextChanged", RoutingStrategy.Direct, typeof(RoutedEventArgs), typeof(Badge));

        /// <summary>
        /// 文本改变事件
        /// </summary>处理
        public event RoutedEventHandler TextChanged
        {
            add { AddHandler(TextChangedEvent, value); }
            remove { RemoveHandler(TextChangedEvent, value); }
        }

        #endregion 事件

        #region 方法

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == null)
            {
                return;
            }
            Badge badge = d as Badge;
            RoutedEventArgs args = new RoutedEventArgs();
            args.RoutedEvent = TextChangedEvent;
            //badge.RaiseEvent(args);
        }

        #endregion 方法
    }
}
