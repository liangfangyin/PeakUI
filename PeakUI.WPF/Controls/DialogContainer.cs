using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;
using System.Xml.Linq;
using PeakUI.WPF.Core;

namespace PeakUI.WPF
{
    /// <summary>
    /// 对话框
    /// </summary>
    [TemplatePart(Name = TransitionName, Type = typeof(Transition))]
    [TemplatePart(Name = CloseButtonPartName, Type = typeof(Button))]
    [TemplatePart(Name = BackgroundBorderPartName, Type = typeof(Border))]
    [TemplatePart(Name = ContentPresenterPartName, Type = typeof(ContentPresenter))]
    public class DialogContainer : ContentControl
    {
        /// <summary>
        /// 转换动画名称
        /// </summary>
        public const string TransitionName = "Path_Transition";

        /// <summary>
        /// 关闭按钮名称
        /// </summary>
        public const string CloseButtonPartName = "PART_CloseButton";

        /// <summary>
        /// 背景 Border 名称
        /// </summary>
        public const string BackgroundBorderPartName = "PART_BackgroundBorder";

        /// <summary>
        /// Content 内容名称
        /// </summary>
        public const string ContentPresenterPartName = "PART_ContentPresenter";

        /// <summary>
        /// 对话框结果路由事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void DialogResultRoutedEventHandler(object sender, DialogResultRoutedEventArgs e);

        /// <summary>
        /// 打开对话框前事件处理
        /// </summary>
        public Action<DialogContainer> BeforeOpenHandler;

        /// <summary>
        /// 打开对话框后事件处理
        /// </summary>
        public Action<DialogContainer, object> AfterCloseHandler;

        private Border rootBorder;
        private object openParameter;
        private object closeParameter;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogContainer"/> class.
        /// </summary>
        static DialogContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogContainer), new FrameworkPropertyMetadata(typeof(DialogContainer)));
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            CommandBindings.Add(new CommandBinding(CloseDialogCommand, CloseDialogHandler));
            CommandBindings.Add(new CommandBinding(OpenDialogCommand, OpenDialogHandler));

            ButtonBase closeButton = GetTemplateChild(CloseButtonPartName) as ButtonBase;
            closeButton.Click += (sender, args) =>
            {
                closeParameter = null;
                IsShow = false;
            };

            rootBorder = GetTemplateChild(BackgroundBorderPartName) as Border;
            rootBorder.MouseLeftButtonDown += RootBorder_MouseLeftButtonDown;

            if (GetTemplateChild(TransitionName) is Transition transition)
            {
                transition.Showed += (sender, e) => IsClosed = false;
                transition.Closed += Closed; ;
            }

            if (GetTemplateChild(ContentPresenterPartName) is ContentPresenter contentPresenter)
            {
                contentPresenter.PreviewKeyDown += ContentPresenter_PreviewKeyDown;
            }
        }

        #region 命令

        /// <summary>
        /// 关闭对话框命令
        /// </summary>
        public static RoutedCommand CloseDialogCommand = new RoutedCommand();

        /// <summary>
        /// 打开对话框命令
        /// </summary>
        public static RoutedCommand OpenDialogCommand = new RoutedCommand();

        /// <summary>
        /// 打开前命令
        /// </summary>
        public static readonly DependencyProperty BeforeOpenCommandProperty = DependencyProperty.Register(
            "BeforeOpenCommand", typeof(ICommand), typeof(DialogContainer));

        /// <summary>
        /// 打开前命令
        /// </summary>
        public ICommand BeforeOpenCommand
        {
            get { return (ICommand)GetValue(BeforeOpenCommandProperty); }
            set { SetValue(BeforeOpenCommandProperty, value); }
        }

        /// <summary>
        /// 关闭后命令
        /// </summary>
        public static readonly DependencyProperty AfterCloseCommandProperty = DependencyProperty.Register(
            "AfterCloseCommand", typeof(ICommand), typeof(DialogContainer));

        /// <summary>
        /// 关闭后命令
        /// </summary>
        public ICommand AfterCloseCommand
        {
            get { return (ICommand)GetValue(AfterCloseCommandProperty); }
            set { SetValue(AfterCloseCommandProperty, value); }
        }

        #endregion 命令

        #region 事件

        /// <summary>
        /// 打开前事件
        /// </summary>
        public static readonly RoutedEvent BeforeOpenEvent = EventManager.RegisterRoutedEvent(
            "BeforeOpen", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DialogContainer));

        /// <summary>
        /// 打开前事件
        /// </summary>
        public event RoutedEventHandler BeforeOpen
        {
            add { AddHandler(BeforeOpenEvent, value); }
            remove { RemoveHandler(BeforeOpenEvent, value); }
        }

        /// <summary>
        /// 关闭后事件
        /// </summary>
        public static readonly RoutedEvent AfterCloseEvent = EventManager.RegisterRoutedEvent(
            "AfterClose", RoutingStrategy.Bubble, typeof(DialogResultRoutedEventHandler), typeof(DialogContainer));

        /// <summary>
        /// 关闭后事件
        /// </summary>
        public event DialogResultRoutedEventHandler AfterClose
        {
            add { AddHandler(AfterCloseEvent, value); }
            remove { RemoveHandler(AfterCloseEvent, value); }
        }

        #endregion 事件

        #region 依赖属性

        /// <summary>
        /// 标识
        /// </summary>
        public static readonly DependencyProperty IdentifierProperty = DependencyProperty.Register(
            "Identifier", typeof(string), typeof(DialogContainer), new PropertyMetadata(default(string), OnIdentifierChanged));

        private static void OnIdentifierChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DialogContainer dialog = d as DialogContainer;
            string identifier = e.NewValue.ToString();
            Dialog.AddDialogContainer(identifier, dialog);
        }

        /// <summary>
        /// 标识
        /// </summary>
        public string Identifier
        {
            get { return (string)GetValue(IdentifierProperty); }
            set { SetValue(IdentifierProperty, value); }
        }

        /// <summary>
        /// 对话框内容
        /// </summary>
        public static readonly DependencyProperty DialogContentProperty = DependencyProperty.Register(
            "DialogContent", typeof(object), typeof(DialogContainer), new PropertyMetadata(default(object)));

        /// <summary>
        /// 对话框内容
        /// </summary>
        public object DialogContent
        {
            get { return GetValue(DialogContentProperty); }
            set { SetValue(DialogContentProperty, value); }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(DialogContainer), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// 圆角半径
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(DialogContainer), new PropertyMetadata(default(CornerRadius)));

        /// <summary>
        /// 圆角半径
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// 是否显示关闭按钮
        /// </summary>
        public static readonly DependencyProperty IsShowCloseButtonProperty = DependencyProperty.Register(
            "IsShowCloseButton", typeof(bool), typeof(DialogContainer), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 是否显示关闭按钮
        /// </summary>
        public bool IsShowCloseButton
        {
            get { return (bool)GetValue(IsShowCloseButtonProperty); }
            set { SetValue(IsShowCloseButtonProperty, value); }
        }

        /// <summary>
        /// 是否点击背景关闭弹窗
        /// </summary>
        public static readonly DependencyProperty IsClickBackgroundToCloseProperty = DependencyProperty.Register(
            "IsClickBackgroundToClose", typeof(bool), typeof(DialogContainer), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 是否点击背景关闭弹窗
        /// </summary>
        public bool IsClickBackgroundToClose
        {
            get { return (bool)GetValue(IsClickBackgroundToCloseProperty); }
            set { SetValue(IsClickBackgroundToCloseProperty, value); }
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        public static readonly DependencyProperty IsShowProperty = DependencyProperty.Register(
            "IsShow", typeof(bool), typeof(DialogContainer), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsShowChanged));

        private static void OnIsShowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DialogContainer dialog = d as DialogContainer;

            if ((bool)e.NewValue)
            {
                dialog.OpenAnimiation(dialog);
            }
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow
        {
            get { return (bool)GetValue(IsShowProperty); }
            set { SetValue(IsShowProperty, value); }
        }

        /// <summary>
        /// 遮罩背景色
        /// </summary>
        public static readonly DependencyProperty MaskBackgroundProperty = DependencyProperty.Register(
            "MaskBackground", typeof(Brush), typeof(DialogContainer), new PropertyMetadata(default(Brush)));

        /// <summary>
        ///  遮罩背景色
        /// </summary>
        public Brush MaskBackground
        {
            get { return (Brush)GetValue(MaskBackgroundProperty); }
            set { SetValue(MaskBackgroundProperty, value); }
        }

        /// <summary>
        /// 关闭完成
        /// </summary>
        public static readonly DependencyProperty IsClosedProperty = DependencyProperty.Register(
            "IsClosed", typeof(bool), typeof(DialogContainer), new PropertyMetadata(default(bool)));

        /// <summary>
        /// 关闭完成
        /// </summary>
        public bool IsClosed
        {
            get { return (bool)GetValue(IsClosedProperty); }
            set { SetValue(IsClosedProperty, value); }
        }

        #endregion 依赖属性

        private void OpenDialogHandler(object sender, ExecutedRoutedEventArgs e)
        {
            openParameter = e.Parameter;
            IsShow = true;
        }

        private void CloseDialogHandler(object sender, ExecutedRoutedEventArgs e)
        {
            closeParameter = e.Parameter;
            IsShow = false;
        }

        private void RootBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Border border)
            {
                if (IsClickBackgroundToClose && sender.Equals(border))
                {
                    closeParameter = null;
                    IsShow = false;
                }
            }
        }

        /// <summary>
        /// 关闭对话框
        /// </summary>
        /// <param name="parameter">参数</param>
        public void Close(object parameter = null)
        {
            CloseDialogCommand.Execute(parameter, this);
        }

        // 打开对话框动作
        private void OpenAnimiation(DialogContainer dialog)
        {
            RoutedEventArgs args = new RoutedEventArgs(BeforeOpenEvent);
            dialog.RaiseEvent(args);
            dialog.BeforeOpenCommand?.Execute(null);
            dialog.BeforeOpenHandler?.Invoke(dialog);

            _ = dialog.Focus();

            if (dialog.DialogContent is FrameworkElement element && element.DataContext is IDialogViewModel dialogContext)
            {
                dialogContext.OnDialogOpened(dialog.openParameter);
            }
        }

        private void Closed(object sender, RoutedEventArgs e)
        {
            IsClosed = true;

            var args = new DialogResultRoutedEventArgs(AfterCloseEvent, this.closeParameter, this);
            args.RoutedEvent = AfterCloseEvent;
            this.RaiseEvent(args);
            this.AfterCloseCommand?.Execute(this.closeParameter);
            this.AfterCloseHandler?.Invoke(this, this.closeParameter);

            this.closeParameter = null;
            this.BeforeOpenHandler = null;
            this.AfterCloseHandler = null;
        }

        private void ContentPresenter_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = e.Key == Key.System ? e.SystemKey : e.Key;
            if (key == Key.Tab && IsShow)
            {
                e.Handled = true;
            }
        }
    }

    /// <summary>
    /// 对话框结果路由参数
    /// </summary>
    public class DialogResultRoutedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// 结果
        /// </summary>
        public object Result { get; set; }

        /// <summary>
        /// 对话框
        /// </summary>
        public DialogContainer Dialog { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogResultRoutedEventArgs"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="result">The result.</param>
        /// <param name="dialog">The dialog.</param>
        public DialogResultRoutedEventArgs(RoutedEvent routedEvent, object result, DialogContainer dialog)
            : base(routedEvent)
        {
            Result = result;
            Dialog = dialog;
        }
    }


    /// <summary>
    /// 对话框 view model 接口
    /// </summary>
    public interface IDialogViewModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; }

        /// <summary>
        /// 请求关闭委托
        /// </summary>
        event Action<object> RequestClose;

        /// <summary>
        /// 当对话框打开完成方法
        /// </summary>
        /// <param name="parameters">参数</param>
        void OnDialogOpened(object parameters);
    }

    /// <summary>
    /// 转换动画
    /// </summary>
    public class Transition : ContentControl
    {
        static Transition()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Transition), new FrameworkPropertyMetadata(typeof(Transition)));
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Loaded += Transition_Loaded;
        }

        private void Transition_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsShow)
            {
                ShowAnimation(this);
            }
        }

        #region 命令

        /// <summary>
        /// 显示完命令
        /// </summary>
        public static readonly DependencyProperty ShowedCommandProperty =
            DependencyProperty.Register("ShowedCommand", typeof(ICommand), typeof(Transition), new PropertyMetadata(default(ICommand)));

        /// <summary>
        /// 显示完命令
        /// </summary>
        public ICommand ShowedCommand
        {
            get { return (ICommand)GetValue(ShowedCommandProperty); }
            set { SetValue(ShowedCommandProperty, value); }
        }

        /// <summary>
        /// 关闭完命令
        /// </summary>
        public static readonly DependencyProperty ClosedCommandProperty =
            DependencyProperty.Register("ClosedCommand", typeof(ICommand), typeof(Transition), new PropertyMetadata(default(ICommand)));

        /// <summary>
        /// 关闭完命令
        /// </summary>
        public ICommand ClosedCommand
        {
            get { return (ICommand)GetValue(ClosedCommandProperty); }
            set { SetValue(ClosedCommandProperty, value); }
        }

        #endregion 命令

        #region 事件

        /// <summary>
        /// 显示完事件
        /// </summary>
        public static readonly RoutedEvent ShowedEvent =
           EventManager.RegisterRoutedEvent("Showed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Transition));

        /// <summary>
        /// 显示完事件 handler
        /// </summary>
        public event RoutedEventHandler Showed
        {
            add { AddHandler(ShowedEvent, value); }
            remove { RemoveHandler(ShowedEvent, value); }
        }

        /// <summary>
        /// 关闭完事件
        /// </summary>
        public static readonly RoutedEvent ClosedEvent =
          EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Transition));

        /// <summary>
        /// 关闭完事件 handler
        /// </summary>
        public event RoutedEventHandler Closed
        {
            add { AddHandler(ClosedEvent, value); }
            remove { RemoveHandler(ClosedEvent, value); }
        }

        #endregion 事件

        #region 依赖属性

        /// <summary>
        /// 是否显示
        /// </summary>
        public static readonly DependencyProperty IsShowProperty =
            DependencyProperty.Register("IsShow", typeof(bool), typeof(Transition), new PropertyMetadata(default(bool), OnIsShwowChanged));

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow
        {
            get { return (bool)GetValue(IsShowProperty); }
            set { SetValue(IsShowProperty, value); }
        }

        /// <summary>
        /// 转换类型
        /// </summary>
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(TransitionType), typeof(Transition), new PropertyMetadata(default(TransitionType)));

        /// <summary>
        /// 转换类型
        /// </summary>
        public TransitionType Type
        {
            get { return (TransitionType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        /// <summary>
        /// 动画时长
        /// </summary>
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(Transition), new PropertyMetadata(default(Duration)));

        /// <summary>
        /// 动画时长
        /// </summary>
        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        /// <summary>
        /// 动画位置偏移
        /// </summary>
        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.Register("Offset", typeof(double), typeof(Transition), new PropertyMetadata(default(double)));

        /// <summary>
        /// 动画位置偏移
        /// </summary>
        public double Offset
        {
            get { return (double)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        /// <summary>
        /// 动画初始缩放
        /// </summary>
        public static readonly DependencyProperty InitialScaleProperty =
            DependencyProperty.Register("InitialScale", typeof(double), typeof(Transition), new PropertyMetadata(default(double)));

        /// <summary>
        /// 动画初始缩放
        /// </summary>
        public double InitialScale
        {
            get { return (double)GetValue(InitialScaleProperty); }
            set { SetValue(InitialScaleProperty, value); }
        }

        /// <summary>
        /// 显示缓动
        /// </summary>
        public static readonly DependencyProperty ShowEasingFunctionProperty =
            DependencyProperty.Register("ShowEasingFunction", typeof(IEasingFunction), typeof(Transition), new PropertyMetadata(default(IEasingFunction)));

        /// <summary>
        /// 显示缓动
        /// </summary>
        public IEasingFunction ShowEasingFunction
        {
            get { return (IEasingFunction)GetValue(ShowEasingFunctionProperty); }
            set { SetValue(ShowEasingFunctionProperty, value); }
        }

        /// <summary>
        /// 关闭缓动
        /// </summary>
        public static readonly DependencyProperty CloseEasingFunctionProperty =
            DependencyProperty.Register("CloseEasingFunction", typeof(IEasingFunction), typeof(Transition), new PropertyMetadata(default(IEasingFunction)));

        /// <summary>
        /// 关闭缓动
        /// </summary>
        public IEasingFunction CloseEasingFunction
        {
            get { return (IEasingFunction)GetValue(CloseEasingFunctionProperty); }
            set { SetValue(CloseEasingFunctionProperty, value); }
        }

        #endregion 依赖属性

        private static void OnIsShwowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Transition transition && transition.IsLoaded)
            {
                if (transition.IsShow)
                {
                    ShowAnimation(transition);
                }
                else
                {
                    CloseAnimation(transition);
                }
            }
        }

        private static void ShowAnimation(Transition transition)
        {
            Storyboard storyboard = new Storyboard();
            storyboard.Completed += (sender, e) =>
            {
                var args = new RoutedEventArgs();
                args.RoutedEvent = Transition.ShowedEvent;
                transition.RaiseEvent(args);
                transition.ShowedCommand?.Execute(null);
            };

            DoubleAnimation opacityAnimation = GetOpacityAnimation(transition, 0, 1, transition.ShowEasingFunction);

            switch (transition.Type)
            {
                case TransitionType.Fade:
                case TransitionType.CollapseUp:
                case TransitionType.CollapseDown:
                case TransitionType.CollapseLeft:
                case TransitionType.CollapseRight:
                default:
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.FadeLeft:
                    DoubleAnimation translateAnimation = GetTranslateXAnimation(transition, transition.Offset, 0, transition.ShowEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.FadeRight:
                    translateAnimation = GetTranslateXAnimation(transition, -transition.Offset, 0, transition.ShowEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.FadeUp:
                    translateAnimation = GetTranslateYAnimation(transition, transition.Offset, 0, transition.ShowEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.FadeDown:
                    translateAnimation = GetTranslateYAnimation(transition, -transition.Offset, 0, transition.ShowEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.Zoom:
                    DoubleAnimation scaleXAnimation = GetScaleXAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    DoubleAnimation scaleYAnimation = GetScaleYAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomX:
                    scaleXAnimation = GetScaleXAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomY:
                    scaleYAnimation = GetScaleYAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomLeft:
                    transition.RenderTransformOrigin = new Point(0, 0.5);
                    scaleXAnimation = GetScaleXAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomRight:
                    transition.RenderTransformOrigin = new Point(1, 0.5);
                    scaleXAnimation = GetScaleXAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomUp:
                    transition.RenderTransformOrigin = new Point(0.5, 0);
                    scaleYAnimation = GetScaleYAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomDown:
                    transition.RenderTransformOrigin = new Point(0.5, 1);
                    scaleYAnimation = GetScaleYAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;
            }

            storyboard.Begin();
        }

        private static void CloseAnimation(Transition transition)
        {
            Storyboard storyboard = new Storyboard();
            storyboard.Completed += (sender, e) =>
            {
                var args = new RoutedEventArgs();
                args.RoutedEvent = Transition.ClosedEvent;
                transition.RaiseEvent(args);
                transition.ClosedCommand?.Execute(null);
            };

            DoubleAnimation opacityAnimation = GetOpacityAnimation(transition, 1, 0, transition.CloseEasingFunction);

            switch (transition.Type)
            {
                case TransitionType.Fade:
                case TransitionType.CollapseUp:
                case TransitionType.CollapseDown:
                case TransitionType.CollapseLeft:
                case TransitionType.CollapseRight:
                default:
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.FadeLeft:
                    DoubleAnimation translateAnimation = GetTranslateXAnimation(transition, 0, transition.Offset, transition.CloseEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.FadeRight:
                    translateAnimation = GetTranslateXAnimation(transition, 0, -transition.Offset, transition.CloseEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.FadeUp:
                    translateAnimation = GetTranslateYAnimation(transition, 0, transition.Offset, transition.CloseEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.FadeDown:
                    translateAnimation = GetTranslateYAnimation(transition, 0, -transition.Offset, transition.CloseEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.Zoom:
                    DoubleAnimation scaleXAnimation = GetScaleXAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    DoubleAnimation scaleYAnimation = GetScaleYAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomX:
                    scaleXAnimation = GetScaleXAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomY:
                    scaleYAnimation = GetScaleYAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomLeft:
                    transition.RenderTransformOrigin = new Point(0, 0.5);
                    scaleXAnimation = GetScaleXAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomRight:
                    transition.RenderTransformOrigin = new Point(1, 0.5);
                    scaleXAnimation = GetScaleXAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomUp:
                    transition.RenderTransformOrigin = new Point(0.5, 0);
                    scaleYAnimation = GetScaleYAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomDown:
                    transition.RenderTransformOrigin = new Point(0.5, 1);
                    scaleYAnimation = GetScaleYAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;
            }

            storyboard.Begin();
        }

        private static DoubleAnimation GetOpacityAnimation(Transition transition, double from, double to, IEasingFunction easing)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = transition.Duration,
                EasingFunction = easing,
            };

            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(OpacityProperty));
            Storyboard.SetTarget(opacityAnimation, transition);
            return opacityAnimation;
        }

        private static DoubleAnimation GetTranslateXAnimation(Transition transition, double from, double to, IEasingFunction easing)
        {
            DoubleAnimation transformAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = transition.Duration,
                EasingFunction = easing,
            };

            Storyboard.SetTargetProperty(transformAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));
            Storyboard.SetTarget(transformAnimation, transition);
            return transformAnimation;
        }

        private static DoubleAnimation GetTranslateYAnimation(Transition transition, double from, double to, IEasingFunction easing)
        {
            DoubleAnimation transformAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = transition.Duration,
                EasingFunction = easing,
            };

            Storyboard.SetTargetProperty(transformAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));
            Storyboard.SetTarget(transformAnimation, transition);
            return transformAnimation;
        }

        private static DoubleAnimation GetScaleXAnimation(Transition transition, double from, double to, IEasingFunction easing)
        {
            DoubleAnimation transformAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = transition.Duration,
                EasingFunction = easing,
            };

            Storyboard.SetTargetProperty(transformAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            Storyboard.SetTarget(transformAnimation, transition);
            return transformAnimation;
        }

        private static DoubleAnimation GetScaleYAnimation(Transition transition, double from, double to, IEasingFunction easing)
        {
            DoubleAnimation transformAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = transition.Duration,
                EasingFunction = easing,
            };

            Storyboard.SetTargetProperty(transformAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
            Storyboard.SetTarget(transformAnimation, transition);
            return transformAnimation;
        }
    }

    /// <summary>
    /// 转换类型
    /// </summary>
    public enum TransitionType
    {
        /// <summary>
        /// 淡入
        /// </summary>
        Fade = 0,

        /// <summary>
        /// 向左淡入
        /// </summary>
        FadeLeft,

        /// <summary>
        /// 向右淡入
        /// </summary>
        FadeRight,

        /// <summary>
        /// 向上淡入
        /// </summary>
        FadeUp,

        /// <summary>
        /// 向下淡入
        /// </summary>
        FadeDown,

        /// <summary>
        /// 缩放
        /// </summary>
        Zoom,

        /// <summary>
        /// X 轴缩放
        /// </summary>
        ZoomX,

        /// <summary>
        /// Y 轴缩放
        /// </summary>
        ZoomY,

        /// <summary>
        /// 向左缩放
        /// </summary>
        ZoomLeft,

        /// <summary>
        /// 向右缩放
        /// </summary>
        ZoomRight,

        /// <summary>
        /// 向上缩放
        /// </summary>
        ZoomUp,

        /// <summary>
        /// 向下缩放
        /// </summary>
        ZoomDown,

        /// <summary>
        /// 向上折叠
        /// </summary>
        CollapseUp,

        /// <summary>
        /// 向下折叠
        /// </summary>
        CollapseDown,

        /// <summary>
        /// 向左折叠
        /// </summary>
        CollapseLeft,

        /// <summary>
        /// 向右折叠
        /// </summary>
        CollapseRight,
    }

}
