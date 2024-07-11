using PeakUI.WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PeakUI.WPF.Themes
{
    /// <summary>
    /// ToastUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class ToastUserControl : UserControl
    {
        private Window owner = null;
        private Popup popup = null;
        public static Dictionary<string, ToastUserControl> UserControlPairs = new Dictionary<string, ToastUserControl>();

        public ToastUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #region 属性

        private static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(ToastUserControl), new PropertyMetadata(string.Empty));
        private string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        private CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        private static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ToastUserControl), new PropertyMetadata(new CornerRadius(5)));

        private new Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }
        private static new readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(ToastUserControl), new PropertyMetadata((Brush)new BrushConverter().ConvertFromString("#2E2929")));

        private new HorizontalAlignment HorizontalContentAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty); }
            set { SetValue(HorizontalContentAlignmentProperty, value); }
        }
        private static new readonly DependencyProperty HorizontalContentAlignmentProperty = DependencyProperty.Register("HorizontalContentAlignment", typeof(HorizontalAlignment), typeof(ToastUserControl), new PropertyMetadata(HorizontalAlignment.Left));

        private new VerticalAlignment VerticalContentAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalContentAlignmentProperty); }
            set { SetValue(VerticalContentAlignmentProperty, value); }
        }
        private static new readonly DependencyProperty VerticalContentAlignmentProperty = DependencyProperty.Register("VerticalContentAlignment", typeof(VerticalAlignment), typeof(ToastUserControl), new PropertyMetadata(VerticalAlignment.Center));

        private double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }
        private static readonly DependencyProperty WidthProperty = DependencyProperty.Register("Width", typeof(double), typeof(ToastUserControl), new PropertyMetadata(0.0));

        private string ContextId
        {
            get { return (string)GetValue(ContextIdProperty); }
            set { SetValue(ContextIdProperty, value); }
        }
        private static readonly DependencyProperty ContextIdProperty = DependencyProperty.Register("ContextId", typeof(string), typeof(ToastUserControl), new PropertyMetadata(""));

        private double Height
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }
        private static readonly DependencyProperty HeightProperty = DependencyProperty.Register("Height", typeof(double), typeof(ToastUserControl), new PropertyMetadata(0.0));

        private object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        private static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(ToastUserControl), new PropertyMetadata(null));

        private Visibility IsHead
        {
            get { return (Visibility)GetValue(IsHeadProperty); }
            set { SetValue(IsHeadProperty, value); }
        }
        private static readonly DependencyProperty IsHeadProperty = DependencyProperty.Register("IsHead", typeof(Visibility), typeof(ToastUserControl), new PropertyMetadata(Visibility.Visible));

        //private ToastLocation Location
        //{
        //    get { return (ToastLocation)GetValue(LocationProperty); }
        //    set { SetValue(LocationProperty, value); }
        //}
        //private static readonly DependencyProperty LocationProperty =
        //    DependencyProperty.Register("Location", typeof(ToastLocation), typeof(Toast), new PropertyMetadata(ToastLocation.Default));

        //public Thickness ToastMargin
        //{
        //    get { return (Thickness)GetValue(ToastMarginProperty); }
        //    set { SetValue(ToastMarginProperty, value); }
        //}
        //public static readonly DependencyProperty ToastMarginProperty =
        //    DependencyProperty.Register("ToastMargin", typeof(Thickness), typeof(Toast), new PropertyMetadata(new Thickness(0)));


        #endregion

        #region 私有方法

        private ToastUserControl(Window owner, ToastUserControlOptions options = null)
        {
            InitializeComponent();
            if (options != null)
            {
                if (options.Width == 0)
                {
                    Width = 300;
                }
                else
                {
                    Width = options.Width;
                }
                if (options.Height == 0)
                {
                    Height = 300;
                }
                else
                {
                    Height = options.Height;
                }
                Background = options.Background;
                Foreground = options.Foreground;
                FontStyle = options.FontStyle;
                FontStretch = options.FontStretch;
                FontSize = options.FontSize;
                FontFamily = options.FontFamily;
                FontWeight = options.FontWeight;
                BorderBrush = options.BorderBrush;
                BorderThickness = options.BorderThickness;
                HorizontalContentAlignment = options.HorizontalContentAlignment;
                VerticalContentAlignment = options.VerticalContentAlignment;
                CornerRadius = options.CornerRadius;
                Content = options.Content;
                Message = options.Title;
                Closed = options.Closed;
                ContextId = options.ContextId;
                IsHead = options.IsHead;
                //Location = options.Location;
                //ToastMargin = options.ToastMargin;
            }
            this.DataContext = this;
            if (owner == null)
            {
                this.owner = Application.Current.MainWindow;
            }
            else
            {
                this.owner = owner;
            }
            this.owner.Topmost = true;
            this.owner.Closed += Owner_Closed;
        }

        private void Owner_Closed(object sender, EventArgs e)
        {
            if (UserControlPairs.ContainsKey(ContextId))
            {
                UserControlPairs.Remove(ContextId);
            }
            this.Close();
        }

        private static void ShowToast(ToastUserControl toast)
        {
            toast.popup = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                toast.popup = new Popup
                {
                    PopupAnimation = PopupAnimation.Fade,
                    AllowsTransparency = true,
                    StaysOpen = true,
                    Placement = PlacementMode.Center,
                    IsOpen = false,
                    Child = toast,
                    MinWidth = toast.MinWidth,
                    MaxWidth = toast.MaxWidth,
                    MinHeight = toast.MinHeight,
                    MaxHeight = toast.MaxHeight,
                };
                if (toast.Width != 0)
                {
                    toast.popup.Width = toast.Width;
                }
                if (toast.Height != 0)
                {
                    toast.popup.Height = toast.Height;
                }
                toast.popup.PlacementTarget = GetPopupPlacementTarget(toast); //为 null 则 Popup 定位相对于屏幕的左上角;
                toast.owner.LocationChanged += toast.UpdatePosition;
                toast.owner.SizeChanged += toast.UpdatePosition;
                toast.popup.Closed += Popup_Closed;
                toast.popup.IsOpen = true;  //先显示出来以确定宽高； 
                SetPopupOffset(toast.popup, toast);
                toast.popup.IsOpen = false; //先关闭再打开来刷新定位；
                toast.popup.IsOpen = true;
            }));
        }

        private void UpdatePosition(object sender, EventArgs e)
        {
            var up = typeof(Popup).GetMethod("UpdatePosition", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (up == null || popup == null)
            {
                return;
            }
            SetPopupOffset(popup, this);
            up.Invoke(popup, null);
        }

        private static void Popup_Closed(object sender, EventArgs e)
        {
            Popup popup = sender as Popup;
            if (popup == null)
            {
                return;
            }
            ToastUserControl toast = popup.Child as ToastUserControl;
            if (toast == null)
            {
                return;
            }
            toast.RaiseClosed(e);
        }

        /// <summary>
        /// 获取定位目标
        /// </summary>
        /// <param name="toast">Toast 对象</param>
        /// <returns>容器或null</returns>
        private static UIElement GetPopupPlacementTarget(ToastUserControl toast)
        {
            return toast.owner;
        }

        private static void SetPopupOffset(Popup popup, ToastUserControl toast)
        {
            double winTitleHeight = SystemParameters.CaptionHeight; //标题高度为22；
            double owner_width = toast.owner.ActualWidth;
            double owner_height = toast.owner.ActualHeight - winTitleHeight;
            if (popup.PlacementTarget == null)
            {
                owner_width = SystemParameters.WorkArea.Size.Width;
                owner_height = SystemParameters.WorkArea.Size.Height;
            }

            double popupWidth = (popup.Child as FrameworkElement)?.ActualWidth ?? 0; //Popup 宽高为其 Child 的宽高；
            double popupHeight = (popup.Child as FrameworkElement)?.ActualHeight ?? 0;
            double x = SystemParameters.WorkArea.X;
            double y = SystemParameters.WorkArea.Y;

            /*[dlgcy] 38 和 16 两个数字的猜测：
             * PlacementTarget 为 Window 时，当 Placement 为 Bottom 时，Popup 上边缘与 Window 的下边缘的距离为 38；
             * 当 Placement 为 Right 时，Popup 左边缘与 Window 的右边缘的距离为 16。
             */
            double bottomDistance = 38;
            double rightDistance = 16;
            //Thickness margin = toast.ToastMargin;
            ////上面创建时 Popup 的 Placement 为 PlacementMode.Left；
            //switch (toast.Location)
            //{
            //    case ToastLocation.OwnerLeft: //容器左中间
            //        popup.HorizontalOffset = popupWidth + margin.Left;
            //        popup.VerticalOffset = (owner_height - popupHeight - winTitleHeight) / 2;
            //        break;
            //    case ToastLocation.ScreenLeft: //屏幕左中间
            //        popup.HorizontalOffset = popupWidth + x + margin.Left;
            //        popup.VerticalOffset = (owner_height - popupHeight) / 2 + y;
            //        break;
            //    case ToastLocation.OwnerRight: //容器右中间
            //        popup.HorizontalOffset = owner_width - rightDistance - margin.Right;
            //        popup.VerticalOffset = (owner_height - popupHeight - winTitleHeight) / 2;
            //        break;
            //    case ToastLocation.ScreenRight: //屏幕右中间
            //        popup.HorizontalOffset = owner_width + x - margin.Right;
            //        popup.VerticalOffset = (owner_height - popupHeight) / 2 + y;
            //        break;
            //    case ToastLocation.OwnerTopLeft: //容器左上角
            //        popup.HorizontalOffset = popupWidth + margin.Left;
            //        popup.VerticalOffset = margin.Top;
            //        break;
            //    case ToastLocation.ScreenTopLeft: //屏幕左上角
            //        popup.HorizontalOffset = popupWidth + x + margin.Left;
            //        popup.VerticalOffset = margin.Top;
            //        break;
            //    case ToastLocation.OwnerTopCenter: //容器上中间
            //        popup.HorizontalOffset = popupWidth + (owner_width - popupWidth - rightDistance) / 2;
            //        popup.VerticalOffset = margin.Top;
            //        break;
            //    case ToastLocation.ScreenTopCenter: //屏幕上中间
            //        popup.HorizontalOffset = popupWidth + (owner_width - popupWidth) / 2 + x;
            //        popup.VerticalOffset = y + margin.Top;
            //        break;
            //    case ToastLocation.OwnerTopRight: //容器右上角
            //        popup.HorizontalOffset = owner_width - rightDistance - margin.Right;
            //        popup.VerticalOffset = margin.Top;
            //        break;
            //    case ToastLocation.ScreenTopRight: //屏幕右上角
            //        popup.HorizontalOffset = owner_width + x - margin.Right;
            //        popup.VerticalOffset = y + margin.Top;
            //        break;
            //    case ToastLocation.OwnerBottomLeft: //容器左下角
            //        //popup.HorizontalOffset = popupWidth;
            //        //popup.VerticalOffset = owner_height - popupHeight - winTitleHeight;
            //        popup.Placement = PlacementMode.Bottom;
            //        popup.HorizontalOffset = margin.Left;
            //        popup.VerticalOffset = -(bottomDistance + popupHeight + margin.Bottom);
            //        break;
            //    case ToastLocation.ScreenBottomLeft: //屏幕左下角
            //        popup.HorizontalOffset = popupWidth + x + margin.Left;
            //        popup.VerticalOffset = owner_height - popupHeight + y - margin.Bottom;
            //        break;
            //    case ToastLocation.OwnerBottomCenter: //容器下中间
            //        //popup.HorizontalOffset = popupWidth + (owner_width - popupWidth - rightDistance) / 2;
            //        //popup.VerticalOffset = owner_height - popupHeight - winTitleHeight;
            //        popup.Placement = PlacementMode.Bottom;
            //        popup.HorizontalOffset = (owner_width - popupWidth - rightDistance) / 2;
            //        popup.VerticalOffset = -(bottomDistance + popupHeight + margin.Bottom);
            //        break;
            //    case ToastLocation.ScreenBottomCenter: //屏幕下中间
            //        popup.HorizontalOffset = popupWidth + (owner_width - popupWidth) / 2 + x;
            //        popup.VerticalOffset = owner_height - popupHeight + y - margin.Bottom;
            //        break;
            //    case ToastLocation.OwnerBottomRight: //容器右下角
            //        //popup.HorizontalOffset = popupWidth + (owner_width - popupWidth - rightDistance);
            //        //popup.VerticalOffset = owner_height - popupHeight - winTitleHeight;
            //        popup.Placement = PlacementMode.Bottom;
            //        popup.HorizontalOffset = owner_width - popupWidth - rightDistance - margin.Right;
            //        popup.VerticalOffset = -(bottomDistance + popupHeight + margin.Bottom);
            //        break;
            //    case ToastLocation.ScreenBottomRight: //屏幕右下角
            //        popup.HorizontalOffset = owner_width + x - margin.Right;
            //        popup.VerticalOffset = owner_height - popupHeight + y - margin.Bottom;
            //        break;
            //    case ToastLocation.ScreenCenter: //屏幕正中间
            //        popup.HorizontalOffset = popupWidth + (owner_width - popupWidth) / 2 + x;
            //        popup.VerticalOffset = (owner_height - popupHeight) / 2 + y;
            //        break;
            //    case ToastLocation.OwnerCenter: //容器正中间
            //    case ToastLocation.Default:
            //        //popup.HorizontalOffset = popupWidth + (owner_width - popupWidth - rightDistance) / 2;
            //        //popup.VerticalOffset = (owner_height - popupHeight - winTitleHeight) / 2;
            //        popup.Placement = PlacementMode.Center;
            //        popup.HorizontalOffset = -rightDistance / 2;
            //        popup.VerticalOffset = -bottomDistance / 2;
            //        break;
            //}
            popup.Placement = PlacementMode.Center;
            popup.HorizontalOffset = -rightDistance / 2;
            popup.VerticalOffset = -bottomDistance / 2;

        }

        private event EventHandler<EventArgs> Closed;
        private void RaiseClosed(EventArgs e)
        {
            Closed?.Invoke(this, e);
        }

        private event EventHandler<EventArgs> Click;
        private void RaiseClick(EventArgs e)
        {
            Click?.Invoke(this, e);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            RaiseClick(e);
        }

        private void Close()
        {
            if (UserControlPairs.ContainsKey(ContextId))
            {
                UserControlPairs.Remove(ContextId);
            }
            popup.IsOpen = false;
            owner.LocationChanged -= UpdatePosition;
            owner.SizeChanged -= UpdatePosition;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 展开弹出框
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="options"></param>
        /// <exception cref="Exception"></exception>
        public static void Show(ToastUserControlOptions options = null)
        {
            if (options.Content == null)
            {
                throw new Exception("无法获取页面加载");
            }
            if (UserControlPairs.ContainsKey(options.ContextId))
            {
                return;
            }
            var toast = new ToastUserControl(null, options);
            UserControlPairs[options.ContextId] = toast;
            ShowToast(toast);
        }

        /// <summary>
        /// 展开弹出框
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="msg"></param>
        /// <param name="options"></param>
        /// <exception cref="Exception"></exception>
        public static void Show(Window owner, ToastUserControlOptions options = null)
        {
            var toast = new ToastUserControl(owner, options);
            if (toast.Content == null)
            {
                throw new Exception("无法获取页面加载");
            }
            if (UserControlPairs.ContainsKey(options.ContextId))
            {
                return;
            }
            UserControlPairs[options.ContextId] = toast;
            ShowToast(toast);
        }

        /// <summary>
        /// 关闭弹出框
        /// </summary>
        public static void ShowClose(string name)
        {
            if (ToastUserControl.UserControlPairs.ContainsKey(name))
            {
                var control = ToastUserControl.UserControlPairs[name];
                control.Close();
            }
        }

        #endregion

    }

    public class ToastUserControlOptions
    {
        public double ToastWidth { get; set; }
        public double ToastHeight { get; set; }
        public double TextWidth { get; set; }
        public double Width { get; set; } = 800;
        public double Height { get; set; } = 500;
        public Brush Foreground { get; set; } = (Brush)new BrushConverter().ConvertFromString("#031D38");
        public FontStyle FontStyle { get; set; } = SystemFonts.MessageFontStyle;
        public FontStretch FontStretch { get; set; } = FontStretches.Normal;
        public double FontSize { get; set; } = 14;
        public double IconSize { get; set; } = 25;
        public IconType Icon { get; set; } = IconType.CheckboxCircleLine;
        public FontFamily FontFamily { get; set; } = SystemFonts.MessageFontFamily;
        public FontWeight FontWeight { get; set; } = SystemFonts.MenuFontWeight;
        public CornerRadius CornerRadius { get; set; } = new CornerRadius(5);
        public Brush IconForeground { get; set; } = (Brush)new BrushConverter().ConvertFromString("#00D91A");
        public Brush BorderBrush { get; set; } = (Brush)new BrushConverter().ConvertFromString("#E5E5E5");
        public Thickness BorderThickness { get; set; } = new Thickness(1);
        public Brush Background { get; set; } = (Brush)new BrushConverter().ConvertFromString("#FFFFFF");
        public HorizontalAlignment HorizontalContentAlignment { get; set; } = HorizontalAlignment.Center;
        public VerticalAlignment VerticalContentAlignment { get; set; } = VerticalAlignment.Center;
        public EventHandler<EventArgs> Closed { get; set; }
        public EventHandler<EventArgs> Click { get; internal set; }
        public object Content { get; set; }
        public string Title { get; set; } = "";
        public string ContextId { get; set; } = "";
        public int Time { get; set; } = 2000;
        public Visibility IsHead { get; set; } = Visibility.Visible;
        public ToastLocation Location { get; set; } = ToastLocation.Default;
        public Thickness ToastMargin { get; set; } = new Thickness(2);
    }
}
