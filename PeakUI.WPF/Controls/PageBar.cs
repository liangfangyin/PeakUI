using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Xml.Linq;

namespace PeakUI.WPF
{
    /// <summary>
    /// 页码条子项
    /// </summary>
    public class PageBarItem : ContentControl
    {
        static PageBarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PageBarItem), new FrameworkPropertyMetadata(typeof(PageBarItem)));
        }

        #region 命令

        /// <summary>
        /// 点击页码命令
        /// </summary>
        public static readonly DependencyProperty PageNumberCommandProperty =
            DependencyProperty.Register("PageNumberCommand", typeof(ICommand), typeof(PageBarItem));

        /// <summary>
        /// 点击页码命令
        /// </summary>
        public ICommand PageNumberCommand
        {
            get { return (ICommand)GetValue(PageNumberCommandProperty); }
            set { SetValue(PageNumberCommandProperty, value); }
        }

        #endregion 命令

        #region 依赖属性

        /// <summary>
        /// 值
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(PageBarItem), new PropertyMetadata(default(int)));

        /// <summary>
        /// 值
        /// </summary>
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// 当前颜色
        /// </summary>
        public static readonly DependencyProperty CurrentBrushProperty =
            DependencyProperty.Register("CurrentBrush", typeof(Brush), typeof(PageBarItem), new PropertyMetadata(default(Brush)));

        /// <summary>
        /// 当前颜色
        /// </summary>
        public Brush CurrentBrush
        {
            get { return (Brush)GetValue(CurrentBrushProperty); }
            set { SetValue(CurrentBrushProperty, value); }
        }

         

        #endregion 依赖属性
    }

    /// <summary>
    /// 页码条
    /// </summary>
    [TemplatePart(Name = PageSizePartName, Type = typeof(TextBox))]
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(PageBarItem))]
    public class PageBar : ItemsControl
    {
        /// <summary>
        /// 每页条数文本框
        /// </summary>
        public const string PageSizePartName = "PART_PageSizeTextBox";

        static PageBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PageBar), new FrameworkPropertyMetadata(typeof(PageBar)));
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (GetTemplateChild(PageSizePartName) is TextBox pageSizeTextBox)
            {
                pageSizeTextBox.KeyDown += (sender, e) =>
                {
                    if (e.Key == Key.Enter)
                    {
                        this.Focus();
                    }
                };
            }
        }

        /// <inheritdoc/>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is PageBarItem;
        }

        /// <inheritdoc/>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new PageBarItem();
        }

        /// <inheritdoc/>
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
        }

        #region 命令

        /// <summary>
        /// 每页数量改变命令
        /// </summary>
        public static readonly DependencyProperty PageSizeChangedCommandProperty =
            DependencyProperty.Register("PageSizeChangedCommand", typeof(ICommand), typeof(PageBar), new PropertyMetadata(default(ICommand)));

        /// <summary>
        /// 每页数量改变命令
        /// </summary>
        public ICommand PageSizeChangedCommand
        {
            get { return (ICommand)GetValue(PageSizeChangedCommandProperty); }
            set { SetValue(PageSizeChangedCommandProperty, value); }
        }

        /// <summary>
        /// 当前页改变命令
        /// </summary>
        public static readonly DependencyProperty PageIndexChangedCommandProperty =
            DependencyProperty.Register("PageIndexChangedCommand", typeof(ICommand), typeof(PageBar), new PropertyMetadata(default(ICommand)));

        /// <summary>
        /// 当前页改变命令
        /// </summary>
        public ICommand PageIndexChangedCommand
        {
            get { return (ICommand)GetValue(PageIndexChangedCommandProperty); }
            set { SetValue(PageIndexChangedCommandProperty, value); }
        }

        #endregion 命令

        #region 事件

        /// <summary>
        /// 每页数量改变事件
        /// </summary>
        public static readonly RoutedEvent PageSizeChangedEvent =
            EventManager.RegisterRoutedEvent("PageSizeChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<int>), typeof(PageBar));

        /// <summary>
        /// 每页数量改变事件
        /// </summary>
        public event RoutedPropertyChangedEventHandler<int> PageSizeChanged
        {
            add { AddHandler(PageSizeChangedEvent, value); }
            remove { RemoveHandler(PageSizeChangedEvent, value); }
        }

        /// <summary>
        /// 当前页改变事件
        /// </summary>
        public static readonly RoutedEvent PageIndexChangedEvent =
            EventManager.RegisterRoutedEvent("PageIndexChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<int>), typeof(PageBar));

        /// <summary>
        /// 当前页改变事件
        /// </summary>
        public event RoutedPropertyChangedEventHandler<int> PageIndexChanged
        {
            add { AddHandler(PageIndexChangedEvent, value); }
            remove { RemoveHandler(PageIndexChangedEvent, value); }
        }

        #endregion 事件

        #region 依赖属性

        /// <summary>
        /// 当前页背景色
        /// </summary>
        public static readonly DependencyProperty CurrentBackgroundProperty =
            DependencyProperty.Register("CurrentBackground", typeof(Brush), typeof(PageBar), new PropertyMetadata(default(Brush)));

        /// <summary>
        /// 当前页背景色
        /// </summary>
        public Brush CurrentBackground
        {
            get { return (Brush)GetValue(CurrentBackgroundProperty); }
            set { SetValue(CurrentBackgroundProperty, value); }
        }

        /// <summary>
        /// 当前页前景色
        /// </summary>
        public static readonly DependencyProperty CurrentForegroundProperty =
            DependencyProperty.Register("CurrentForeground", typeof(Brush), typeof(PageBar), new PropertyMetadata(default(Brush)));

        /// <summary>
        /// 当前页前景色
        /// </summary>
        public Brush CurrentForeground
        {
            get { return (Brush)GetValue(CurrentForegroundProperty); }
            set { SetValue(CurrentForegroundProperty, value); }
        }

        /// <summary>
        /// 每页数量
        /// </summary>
        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(int), typeof(PageBar), new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnPageSizeChanged)));

        private static void OnPageSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PageBar pageBar = (PageBar)d;
            pageBar.PageIndex = 1;
            pageBar.ReFreshPageBar();

            int oldValue = (int)e.OldValue;
            int newValue = (int)e.NewValue;
            RoutedPropertyChangedEventArgs<int> args = new RoutedPropertyChangedEventArgs<int>(oldValue, newValue);
            args.RoutedEvent = PageBar.PageSizeChangedEvent;
            pageBar.RaiseEvent(args);
            pageBar.PageSizeChangedCommand?.Execute(newValue);
        }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register("PageIndex", typeof(int), typeof(PageBar), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnPageIndexChanged)));

        private static void OnPageIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PageBar pageBar = (PageBar)d;
            pageBar.ReFreshPageBar();

            int oldValue = (int)e.OldValue;
            int newValue = (int)e.NewValue;
            RoutedPropertyChangedEventArgs<int> args = new RoutedPropertyChangedEventArgs<int>(oldValue, newValue);
            args.RoutedEvent = PageBar.PageIndexChangedEvent;
            pageBar.RaiseEvent(args);
            pageBar.PageIndexChangedCommand?.Execute(newValue);
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }

        /// <summary>
        /// 总数量
        /// </summary>
        public static readonly DependencyProperty TotalProperty =
            DependencyProperty.Register("Total", typeof(int), typeof(PageBar), new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnTotalChanged)));

        private static void OnTotalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PageBar pageBar = (PageBar)d;
            pageBar.ReFreshPageBar();
        }

        /// <summary>
        /// 总数量
        /// </summary>
        public int Total
        {
            get { return (int)GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }

        /// <summary>
        /// 是否显示总数量
        /// </summary>
        public static readonly DependencyProperty IsShowTotalProperty =
            DependencyProperty.Register("IsShowTotal", typeof(bool), typeof(PageBar), new PropertyMetadata(default(bool)));

        /// <summary>
        /// 是否显示总数量
        /// </summary>
        public bool IsShowTotal
        {
            get { return (bool)GetValue(IsShowTotalProperty); }
            set { SetValue(IsShowTotalProperty, value); }
        }

        /// <summary>
        /// 是否显示每页数量
        /// </summary>
        public static readonly DependencyProperty IsShowPageSizeProperty =
            DependencyProperty.Register("IsShowPageSize", typeof(bool), typeof(PageBar), new PropertyMetadata(default(bool)));

        /// <summary>
        /// 是否显示每页数量
        /// </summary>
        public bool IsShowPageSize
        {
            get { return (bool)GetValue(IsShowPageSizeProperty); }
            set { SetValue(IsShowPageSizeProperty, value); }
        }

        #endregion 依赖属性

        #region 方法

        // 刷新页码条
        private void ReFreshPageBar()
        {
            this.Items.Clear();

            if (PageSize == 0 || Total == 0)
            {
                return;
            }

            int pageCount = (int)Math.Ceiling(Total / (PageSize * 1.0));    // 总共多少页

            var previousPage = new PageBarItem
            {
                Content = "<",
                Value = PageIndex - 1,
                IsEnabled = PageIndex != 1 && pageCount != 1,
                PageNumberCommand = new RubyerCommand(PageNumberChanged)
            };

            previousPage.SetResourceReference(ToolTipProperty, "I18N_PageBar_PreviousPage");
            this.Items.Add(previousPage);

            this.Items.Add(new PageBarItem
            {
                Content = "1",
                Value = 1,
                IsEnabled = true,
                PageNumberCommand = new RubyerCommand(PageNumberChanged)
            });

            int begin;
            int end;
            begin = PageIndex >= 6 ? PageIndex - 3 : 2;                         // index 大于等于 6 页就从 index-3 开始，否则 2 开始
            end = PageIndex + 3 >= pageCount ? pageCount - 1 : PageIndex + 3;   // index+3 大于 total 就 total-1 结束，否则 index + 3

            // 补够按键数量
            if (end - begin < 6)
            {
                if (PageIndex - begin < 3)
                {
                    end += 3 - (PageIndex - begin);
                    end = end > pageCount - 1 ? pageCount - 1 : end;
                }
                else if (end - PageIndex < 3)
                {
                    begin -= 3 - (end - PageIndex);
                    begin = begin < 2 ? 2 : begin;
                }
            }

            for (int i = begin; i <= end; i++)
            {
                PageBarItem info = new PageBarItem()
                {
                    Value = i,
                    Content = i.ToString(),
                    IsEnabled = true,
                    PageNumberCommand = new RubyerCommand(PageNumberChanged)
                };

                if (pageCount > 9)
                {
                    if (i == begin && PageIndex - begin >= 3 && PageIndex > 5)
                    {
                        info.Value = PageIndex - 5;
                        info.Content = "...";
                        info.SetResourceReference(ToolTipProperty, "I18N_PageBar_Forward5Pages");
                    }
                    else if (i == end && end - PageIndex >= 3 && pageCount - PageIndex >= 5)
                    {
                        info.Value = PageIndex + 5;
                        info.Content = "...";
                        info.SetResourceReference(ToolTipProperty, "I18N_PageBar_Backwards5Pages");
                    }
                }

                this.Items.Add(info);
            }

            // 最后一页
            if (pageCount > 1)
            {
                this.Items.Add(new PageBarItem()
                {
                    Content = pageCount.ToString(),
                    Value = pageCount,
                    IsEnabled = true,
                    PageNumberCommand = new RubyerCommand(PageNumberChanged)
                });
            }

            // 下一页
            var nextPage = new PageBarItem()
            {
                Content = ">",
                Value = PageIndex + 1,
                IsEnabled = PageIndex != pageCount && pageCount != 1,
                PageNumberCommand = new RubyerCommand(PageNumberChanged)
            };

            nextPage.SetResourceReference(ToolTipProperty, "I18N_PageBar_NextPage");
            this.Items.Add(nextPage);
        }

        private void PageNumberChanged(object obj)
        {
            int num = (int)obj;
            this.PageIndex = num;
        }

        #endregion 方法
    }

    /// <summary>
    /// Command
    /// </summary>
    public class RubyerCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RubyerCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public RubyerCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RubyerCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public RubyerCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (x => true);
        }

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// Refreshes the.
        /// </summary>
        public void Refresh()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
