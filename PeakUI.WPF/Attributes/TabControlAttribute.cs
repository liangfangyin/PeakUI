using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PeakUI.WPF
{
    /// <summary>
    /// 标题帮助类
    /// </summary>
    public class TabControlAttribute
    {
        /// <summary>
        /// 背景色
        /// </summary>
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.RegisterAttached("Background", typeof(Brush), typeof(TabControlAttribute), new PropertyMetadata(default));

        /// <summary>
        /// Gets the background.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A Brush.</returns>
        public static Brush GetBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BackgroundProperty);
        }

        /// <summary>
        /// Sets the background.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(BackgroundProperty, value);
        }


        /// <summary>
        /// 选中颜色
        /// </summary>
        public static readonly DependencyProperty SelectedBrushProperty = DependencyProperty.RegisterAttached(
           "SelectedBrush", typeof(Brush), typeof(TabControlAttribute), new PropertyMetadata(default(Brush)));

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
        /// 前景色
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.RegisterAttached("Foreground", typeof(Brush), typeof(TabControlAttribute), new PropertyMetadata(default));

        /// <summary>
        /// Gets the foreground.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A Brush.</returns>
        public static Brush GetForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(ForegroundProperty);
        }

        /// <summary>
        /// Sets the foreground.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(ForegroundProperty, value);
        }

        /// <summary>
        /// 字体
        /// </summary>
        public static readonly DependencyProperty FontFamilyProperty =
            DependencyProperty.RegisterAttached("FontFamily", typeof(FontFamily), typeof(TabControlAttribute), new PropertyMetadata(SystemFonts.CaptionFontFamily));

        /// <summary>
        /// Gets the font family.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A FontFamily.</returns>
        public static FontFamily GetFontFamily(DependencyObject obj)
        {
            return (FontFamily)obj.GetValue(FontFamilyProperty);
        }

        /// <summary>
        /// Sets the font family.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetFontFamily(DependencyObject obj, FontFamily value)
        {
            obj.SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        /// 字体大小
        /// </summary>
        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.RegisterAttached("FontSize", typeof(double), typeof(TabControlAttribute), new PropertyMetadata(SystemFonts.CaptionFontSize));

        /// <summary>
        /// Gets the font size.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A double.</returns>
        public static double GetFontSize(DependencyObject obj)
        {
            return (double)obj.GetValue(FontSizeProperty);
        }

        /// <summary>
        /// Sets the font size.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetFontSize(DependencyObject obj, double value)
        {
            obj.SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// 字体加粗
        /// </summary>
        public static readonly DependencyProperty FontWeightProperty =
            DependencyProperty.RegisterAttached("FontWeight", typeof(FontWeight), typeof(TabControlAttribute), new PropertyMetadata(SystemFonts.CaptionFontWeight));

        /// <summary>
        /// Gets the font weight.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A FontWeight.</returns>
        public static FontWeight GetFontWeight(DependencyObject obj)
        {
            return (FontWeight)obj.GetValue(FontWeightProperty);
        }

        /// <summary>
        /// Sets the font weight.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetFontWeight(DependencyObject obj, FontWeight value)
        {
            obj.SetValue(FontWeightProperty, value);
        }

        /// <summary>
        /// padding
        /// </summary>
        public static readonly DependencyProperty PaddingProperty =
            DependencyProperty.RegisterAttached("Padding", typeof(Thickness), typeof(TabControlAttribute), new PropertyMetadata(default));

        /// <summary>
        /// Gets the padding.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A Thickness.</returns>
        public static Thickness GetPadding(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(PaddingProperty);
        }

        /// <summary>
        /// Sets the padding.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetPadding(DependencyObject obj, Thickness value)
        {
            obj.SetValue(PaddingProperty, value);
        }

        /// <summary>
        /// margin
        /// </summary>
        public static readonly DependencyProperty MarginProperty =
            DependencyProperty.RegisterAttached("Margin", typeof(Thickness), typeof(TabControlAttribute), new PropertyMetadata(default));

        /// <summary>
        /// Gets the margin.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A Thickness.</returns>
        public static Thickness GetMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MarginProperty);
        }

        /// <summary>
        /// Sets the margin.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MarginProperty, value);
        }

        /// <summary>
        /// 水平对齐
        /// </summary>
        public static readonly DependencyProperty HorizontalAlignmentProperty =
            DependencyProperty.RegisterAttached("HorizontalAlignment", typeof(HorizontalAlignment), typeof(TabControlAttribute), new PropertyMetadata(default));

        /// <summary>
        /// Gets the horizontal alignment.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A HorizontalAlignment.</returns>
        public static HorizontalAlignment GetHorizontalAlignment(DependencyObject obj)
        {
            return (HorizontalAlignment)obj.GetValue(HorizontalAlignmentProperty);
        }

        /// <summary>
        /// Sets the horizontal alignment.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetHorizontalAlignment(DependencyObject obj, HorizontalAlignment value)
        {
            obj.SetValue(HorizontalAlignmentProperty, value);
        }

        /// <summary>
        /// 垂直对齐
        /// </summary>
        public static readonly DependencyProperty VerticalAlignmentProperty =
            DependencyProperty.RegisterAttached("VerticalAlignment", typeof(VerticalAlignment), typeof(TabControlAttribute), new PropertyMetadata(default));

        /// <summary>
        /// Gets the vertical alignment.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A VerticalAlignment.</returns>
        public static VerticalAlignment GetVerticalAlignment(DependencyObject obj)
        {
            return (VerticalAlignment)obj.GetValue(VerticalAlignmentProperty);
        }

        /// <summary>
        /// Sets the vertical alignment.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetVerticalAlignment(DependencyObject obj, VerticalAlignment value)
        {
            obj.SetValue(VerticalAlignmentProperty, value);
        }

        /// <summary>
        /// 圆角半径
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached(
            "CornerRadius", typeof(CornerRadius), typeof(TabControlAttribute), new PropertyMetadata(default(CornerRadius)));

        /// <summary>
        /// Sets the corner radius.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetCornerRadius(DependencyObject element, CornerRadius value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// Gets the corner radius.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>A CornerRadius.</returns>
        public static CornerRadius GetCornerRadius(DependencyObject element)
        {
            return (CornerRadius)element.GetValue(CornerRadiusProperty);
        }




        /// <summary>
        /// 是否显示清除按钮
        /// </summary>
        public static readonly DependencyProperty IsClearableProperty =
            DependencyProperty.RegisterAttached("IsClearable", typeof(bool), typeof(TabControlAttribute), new PropertyMetadata(false, OnIsClearbleChanged));

        /// <summary>
        /// Gets the is clearable.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A bool.</returns>
        public static bool GetIsClearable(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsClearableProperty);
        }

        /// <summary>
        /// Sets the is clearable.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">If true, value.</param>
        public static void SetIsClearable(DependencyObject obj, bool value)
        {
            obj.SetValue(IsClearableProperty, value);
        }

        private static void OnIsClearbleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TabItem tabItem)
            {
                RoutedEventHandler handle = (sender, args) =>
                {
                    TabControl tabControl = FindTabControl(tabItem);
                    IEditableCollectionView items = tabControl.Items;

                    if (items.CanRemove)
                    {
                        items.Remove(tabItem.DataContext);      // Binding 移除方式
                    }
                    else
                    {
                        tabControl.Items.Remove(tabItem);       // TabItem 移除方式
                    }

                    if (GetIsAnimation(tabControl))
                    {
                        ScrollViewer scrollViewer = tabControl.Template.FindName("headerPanel", tabControl) as ScrollViewer;
                        if (tabControl.TabStripPlacement == Dock.Top || tabControl.TabStripPlacement == Dock.Bottom)
                        {
                            StartRowAnimation(tabControl, scrollViewer);
                        }
                        else
                        {
                            StartColAnimation(tabControl, scrollViewer);
                        }
                    }
                };

                if (tabItem.IsLoaded)
                {
                    if (tabItem.Template.FindName("clearButton", tabItem) is Button clearButton)
                    {
                        if (GetIsClearable(tabItem))
                        {
                            clearButton.Click += handle;
                        }
                        else
                        {
                            clearButton.Click -= handle;
                        }
                    }
                }

                tabItem.Loaded += (sender, arg) =>
                {
                    if (tabItem.Template.FindName("clearButton", tabItem) is Button clearButton)
                    {
                        if (GetIsClearable(tabItem))
                        {
                            clearButton.Click += handle;
                        }
                        else
                        {
                            clearButton.Click -= handle;
                        }
                    }
                };

                tabItem.Unloaded += (sender, arg) =>
                {
                    if (tabItem.Template.FindName("clearButton", tabItem) is Button clearButton)
                    {
                        if (GetIsClearable(tabItem))
                        {
                            clearButton.Click -= handle;
                        }
                    }
                };
            }
        }

        private static TabControl FindTabControl(DependencyObject dependencyObject)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent != null && !(parent is TabControl))
            {
                return FindTabControl(parent);
            }

            return parent != null ? (TabControl)parent : null;
        }

        /// <summary>
        /// 选中动画
        /// </summary>
        public static readonly DependencyProperty IsAnimationProperty =
            DependencyProperty.RegisterAttached("IsAnimation", typeof(bool), typeof(TabControlAttribute), new PropertyMetadata(false, OnIsAnimationChanged));

        /// <summary>
        /// Gets the is animation.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A bool.</returns>
        public static bool GetIsAnimation(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsAnimationProperty);
        }

        /// <summary>
        /// Sets the is animation.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">If true, value.</param>
        public static void SetIsAnimation(DependencyObject obj, bool value)
        {
            obj.SetValue(IsAnimationProperty, value);
        }

        private static void OnIsAnimationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TabControl tabControl)
            {
                tabControl.Loaded += (sender, arg) =>
                {
                    ScrollViewer scrollViewer = tabControl.Template.FindName("headerPanel", tabControl) as ScrollViewer;

                    if (tabControl.TabStripPlacement == Dock.Top || tabControl.TabStripPlacement == Dock.Bottom)
                    {
                        if (tabControl.Template.FindName("selectedRectRow", tabControl) is Rectangle rectangleRow)
                        {
                            if (GetIsAnimation(tabControl))
                            {
                                tabControl.SelectionChanged += (a, b) =>
                                {
                                    StartRowAnimation(tabControl, scrollViewer);
                                };

                                scrollViewer.ScrollChanged += (a, b) =>
                                {
                                    StartRowAnimation(tabControl, scrollViewer, durationMilliseconds: 100);
                                };

                                if (tabControl.SelectedIndex >= 0)
                                {
                                    int index = tabControl.SelectedIndex;
                                    TabItem item = tabControl.ItemContainerGenerator.ContainerFromItem(tabControl.Items[index]) as TabItem;
                                    var bounds = item.TransformToAncestor(scrollViewer).TransformBounds(new Rect(new Point(0, 0), item.RenderSize));
                                    rectangleRow.Width = item.ActualWidth;
                                    rectangleRow.SetValue(Canvas.LeftProperty, bounds.X);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (tabControl.Template.FindName("selectedRectCol", tabControl) is Rectangle rectangleCol)
                        {
                            if (GetIsAnimation(tabControl))
                            {
                                tabControl.SelectionChanged += (a, b) =>
                                {
                                    StartColAnimation(tabControl, scrollViewer);
                                };

                                scrollViewer.ScrollChanged += (a, b) =>
                                {
                                    StartColAnimation(tabControl, scrollViewer, durationMilliseconds: 0);
                                };

                                if (tabControl.SelectedIndex >= 0)
                                {
                                    int index = tabControl.SelectedIndex;
                                    TabItem item = tabControl.ItemContainerGenerator.ContainerFromItem(tabControl.Items[index]) as TabItem;
                                    var bounds = item.TransformToAncestor(scrollViewer).TransformBounds(new Rect(new Point(0, 0), item.RenderSize));
                                    rectangleCol.Height = item.ActualHeight;
                                    rectangleCol.SetValue(Canvas.TopProperty, bounds.Y);
                                }
                            }
                        }
                    }
                };
            }
        }

        private static TabItem GetItem(TabControl tabControl, int index, IEditableCollectionView items)
        {
            TabItem item;
            if (items.CanRemove)
            {
                item = tabControl.ItemContainerGenerator.ContainerFromItem(tabControl.Items[index]) as TabItem;
            }
            else
            {
                item = tabControl.Items[index] as TabItem;
            }

            return item;
        }

        private static void StartColAnimation(TabControl tabControl, ScrollViewer scrollViewer, int durationMilliseconds = 400)
        {
            int index = tabControl.SelectedIndex;
            if (tabControl.Items.Count == 0 || index < 0 || index >= tabControl.Items.Count)
            {
                return;
            }

            Rectangle rectangleCol = tabControl.Template.FindName("selectedRectCol", tabControl) as Rectangle;
            IEditableCollectionView items = tabControl.Items;
            TabItem item = GetItem(tabControl, index, items);
            rectangleCol.Height = item.ActualHeight;
            var bounds = item.TransformToAncestor(scrollViewer).TransformBounds(new Rect(new Point(0, 0), item.RenderSize));

            DoubleAnimation canvasAnimation = new DoubleAnimation
            {
                To = bounds.Y,
                Duration = TimeSpan.FromMilliseconds(durationMilliseconds),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation sizeAnimation = new DoubleAnimation
            {
                To = item.ActualHeight,
                Duration = TimeSpan.FromMilliseconds(durationMilliseconds),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            rectangleCol.BeginAnimation(Canvas.TopProperty, canvasAnimation);
            rectangleCol.BeginAnimation(FrameworkElement.HeightProperty, sizeAnimation);
        }

        private static void StartRowAnimation(TabControl tabControl, ScrollViewer scrollViewer, int durationMilliseconds = 400)
        {
            int index = tabControl.SelectedIndex;
            if (tabControl.Items.Count == 0 || index < 0 || index >= tabControl.Items.Count)
            {
                return;
            }

            if (tabControl.Template.FindName("selectedRectRow", tabControl) is Rectangle rectangleRow)
            {
                IEditableCollectionView items = tabControl.Items;
                TabItem item = GetItem(tabControl, index, items);
                rectangleRow.Width = item.ActualWidth;

                var bounds = item.TransformToAncestor(scrollViewer).TransformBounds(new Rect(new Point(0, 0), item.RenderSize));

                DoubleAnimation canvasAnimation = new DoubleAnimation
                {
                    To = bounds.X,
                    Duration = TimeSpan.FromMilliseconds(durationMilliseconds),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                DoubleAnimation sizeAnimation = new DoubleAnimation
                {
                    To = item.ActualWidth,
                    Duration = TimeSpan.FromMilliseconds(durationMilliseconds),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                rectangleRow.BeginAnimation(Canvas.LeftProperty, canvasAnimation);
                rectangleRow.BeginAnimation(FrameworkElement.WidthProperty, sizeAnimation);
            }
        }





      





    }

}
