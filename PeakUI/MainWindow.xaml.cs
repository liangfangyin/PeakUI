using PeakUI.WPF.Model;
using PeakUI.WPF.Themes;
using System.Windows;
using System.Windows.Media;

namespace PeakUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         

        public MainWindow()
        {
            InitializeComponent();
            Toast.Show("这是一个弹出框", this, new ToastUserControlOptions { Icon = IconType.QuestionLine, Location = ToastLocation.ScreenCenter, IconForeground = (Brush)new BrushConverter().ConvertFromString("#FF6600"), Time = 3000 });
        }
    }

    
    
 

}
