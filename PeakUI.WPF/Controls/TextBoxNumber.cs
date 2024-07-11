using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PeakUI.WPF
{
    public class TextBoxNumber : Control
    { 
        static TextBoxNumber()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxNumber), new FrameworkPropertyMetadata(typeof(TextBoxNumber))); 
        }
 
        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (GetTemplateChild("btnReduce") is Button btnReduce)
            {
                btnReduce.Click += BtnReduce_Click;
            }

            if (GetTemplateChild("btnIncrease") is Button btnIncrease)
            {
                btnIncrease.Click += BtnIncrease_Click;
            }

            if (GetTemplateChild("txtPeakNumber") is TextBox txtPeakNumber)
            {
                txtPeakNumber.Width = txtPeakNumber.Width - 60;
                txtPeakNumber.CaretIndex = txtPeakNumber.Text.Length;
                txtPeakNumber.TextChanged += TxtPeakNumber_TextChanged;
            }
        }

        private void BtnReduce_Click(object sender, RoutedEventArgs e)
        {
            TextBox txtNumber = GetTemplateChild("txtPeakNumber") as TextBox;
            if (Convert.ToDouble(txtNumber.Text) < 1)
            {
                txtNumber.Text = "0";
            }
            else
            {
                txtNumber.Text = (Convert.ToDouble(txtNumber.Text) - 1).ToString();
            } 
        }

        private void BtnIncrease_Click(object sender, RoutedEventArgs e)
        {
            TextBox txtNumber = GetTemplateChild("txtPeakNumber") as TextBox;
            txtNumber.Text = (Convert.ToDouble(txtNumber.Text) + 1).ToString();
        }

        private DateTime beginDate = DateTime.Now;
        private void TxtPeakNumber_TextChanged(object sender, RoutedEventArgs e)
        {
            if ((DateTime.Now - beginDate).TotalMilliseconds < 10)
            {
                return;
            }
            beginDate = DateTime.Now;
            Double Min = (Double)GetValue(MinProperty);
            Double Max = (Double)GetValue(MaxProperty);
            int Precision = (int)GetValue(PrecisionProperty);
            TextBox txtNumber = GetTemplateChild("txtPeakNumber") as TextBox;
            if (double.TryParse(txtNumber.Text, out double numbers))
            {
                if (numbers < Min)
                { 
                    txtNumber.Text = Min.ToString();
                    txtNumber.CaretIndex = txtNumber.Text.Length;
                    return;
                }
                if (numbers > Max)
                {
                    txtNumber.Text = Max.ToString();
                    return; 
                }
                if (txtNumber.Text.StartsWith(".") && txtNumber.Text.Split(".").Length <= 2)
                {
                    txtNumber.Text = "0.";
                    txtNumber.CaretIndex = txtNumber.Text.Length;
                    return;
                }
                else
                {
                    if (Precision > 0)
                    {
                        string[] values = txtNumber.Text.ToString().Split(".");
                        if (values.Length > 1)
                        {
                            if (values[1].Length > Precision)
                            {
                                txtNumber.Text = Convert.ToDecimal(numbers).ToString("0." + "".PadLeft(Precision, '0'));
                                txtNumber.CaretIndex = txtNumber.Text.Length;
                            }
                        }
                    }
                }
            }
            else
            {
                if (Min > 0)
                {
                    txtNumber.Text = Min.ToString();
                }
                else if (Max < 0)
                {
                    txtNumber.Text = Max.ToString();
                }
                else
                {
                    txtNumber.Text = "0";
                }
                txtNumber.CaretIndex = txtNumber.Text.Length;
            }
        }

        public static readonly RoutedEvent TextBoxNumberEvent = EventManager.RegisterRoutedEvent("TextChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TextBoxNumber));

        /// <summary>
        /// 控件点击的操作.
        /// </summary>
        public event RoutedEventHandler TextChanged
        {
            add
            {
                AddHandler(TextBoxNumberEvent, value);
            }
            remove
            {
                RemoveHandler(TextBoxNumberEvent, value);
            }
        }


        #region 属性


        /// <summary>
        /// 按钮颜色
        /// </summary>
        public static readonly DependencyProperty NumberBackgroundProperty = DependencyProperty.RegisterAttached("NumberBackground", typeof(Brush), typeof(TextBoxNumber), new PropertyMetadata(default(Brush)));

        /// <summary>
        /// 设置按钮颜色
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetNumberBackground(DependencyObject element, Brush value)
        {
            element.SetValue(NumberBackgroundProperty, value);
        }

        /// <summary>
        ///获取按钮颜色.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>A Brush.</returns>
        public static Brush GetNumberBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(NumberBackgroundProperty);
        }


        /// <summary>
        /// 按钮文字颜色
        /// </summary>
        public static readonly DependencyProperty NumberForegroundProperty = DependencyProperty.RegisterAttached("NumberForeground", typeof(Brush), typeof(TextBoxNumber), new PropertyMetadata(default(Brush)));

        /// <summary>
        /// 设置按钮文字颜色
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetNumberForeground(DependencyObject element, Brush value)
        {
            element.SetValue(NumberForegroundProperty, value);
        }

        /// <summary>
        /// 获取按钮文字颜色
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>A Brush.</returns>
        public static Brush GetNumberForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(NumberForegroundProperty);
        }


        /// <summary>
        /// 最小值
        /// </summary>
        public static readonly DependencyProperty MinProperty = DependencyProperty.RegisterAttached("Min", typeof(Double), typeof(TextBoxNumber), new PropertyMetadata(default(Double)));

        /// <summary>
        /// 设置最小值
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetMin(DependencyObject element, Double value)
        {
            element.SetValue(MinProperty, value);
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>A Brush.</returns>
        public static Double GetMin(DependencyObject element)
        {
            return (Double)element.GetValue(MinProperty);
        }


        /// <summary>
        /// 最大值
        /// </summary>
        public static readonly DependencyProperty MaxProperty = DependencyProperty.RegisterAttached("Max", typeof(Double), typeof(TextBoxNumber), new PropertyMetadata(default(Double)));

        /// <summary>
        /// 设置最大值
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetMax(DependencyObject element, Double value)
        {
            element.SetValue(MaxProperty, value);
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>A Brush.</returns>
        public static Double GetMax(DependencyObject element)
        {
            return (Double)element.GetValue(MaxProperty);
        }


        /// <summary>
        /// 默认值
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached("Text", typeof(Double), typeof(TextBoxNumber), new PropertyMetadata(default(Double)));

        /// <summary>
        /// 设置默认值.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetText(DependencyObject element, Double value)
        {
            element.SetValue(TextProperty, value);
        }

        /// <summary>
        /// 获取默认值.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>A Brush.</returns>
        public static Double GetText(DependencyObject element)
        {
            return (Double)element.GetValue(TextProperty);
        }


        /// <summary>
        /// 精确度
        /// </summary>
        public static readonly DependencyProperty PrecisionProperty = DependencyProperty.RegisterAttached("Precision", typeof(int), typeof(TextBoxNumber), new PropertyMetadata(default(int)));

        /// <summary>
        /// 设置精确度.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetPrecision(DependencyObject element, int value)
        {
            element.SetValue(PrecisionProperty, value);
        }

        /// <summary>
        /// 获取精确度.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>A Brush.</returns>
        public static int GetPrecision(DependencyObject element)
        {
            return (int)element.GetValue(PrecisionProperty);
        }

        /// <summary>
        /// 前置是否显示
        /// </summary>
        public static readonly DependencyProperty BtnVisibilityProperty = DependencyProperty.RegisterAttached("BtnVisibility", typeof(Visibility), typeof(TextBoxNumber), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// Gets the post content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>An object.</returns>
        public static Visibility GetBtnVisibility(DependencyObject obj)
        {
            return (Visibility)obj.GetValue(BtnVisibilityProperty);
        }

        /// <summary>
        /// Sets the post content.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetBtnVisibility(DependencyObject obj, Visibility value)
        {
            obj.SetValue(BtnVisibilityProperty, value);
        }

        #endregion


    }
}
