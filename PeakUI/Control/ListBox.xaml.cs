using System.Collections.Generic;
using System.Windows;

namespace PeakUI.Control
{
    /// <summary>
    /// ListBox.xaml 的交互逻辑
    /// </summary>
    public partial class ListBox : Window
    {
        List<MyData> MyDataCollection { get; set; } = new List<MyData>
            {
                new MyData { Name = "Item 1" },
                new MyData { Name = "Item 2" },
                new MyData { Name = "Item 3" }
            };
        public ListBox()
        {
            InitializeComponent();
            myListBox.ItemsSource = MyDataCollection;
        }
    }

    public class MyData
    {
        public string Name { get; set; }
    }

}
