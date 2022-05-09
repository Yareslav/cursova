using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace real
{
    public partial class MainWindow : Window
    {
        private bool isStatisticsShown = false;
        private List<StatisticsClass> statistics = new List<StatisticsClass>();
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(List<StatisticsClass> statics)
        {
            InitializeComponent();
            statistics = statics;
        }
        private void ShowOrHideStatistics(object sender, RoutedEventArgs e)
        {
            var buttton = (Button)sender;
            var color = (Color)ColorConverter.ConvertFromString(!isStatisticsShown ? Colors.yellow : Colors.white);
            buttton.Background = new SolidColorBrush(color);
            if (isStatisticsShown)
            {
                Info.Opacity = 0;
                Scroll.Children.Clear();
            }  else
            {
                Info.Opacity = 1;
                AddPanel();
            }
            isStatisticsShown = !isStatisticsShown;
        }
        private void AddPanel()
        {
            for (var i = 0; i < statistics.Count; i++)
            {
                var panel = new StackPanel();
                panel.Width = 800;
                panel.Height = 40;
                panel.Orientation = Orientation.Horizontal;
                void FastAdd<T>(T elem)
                {
                    var textBox = new TextBox();
                    textBox.Text = elem.ToString();
                    textBox.Width = 160;
                    textBox.TextAlignment = TextAlignment.Center;
                    textBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Colors.blue));
                    panel.Children.Add(textBox);
                }
                FastAdd(i+1);
                FastAdd(statistics[i].wins);
                FastAdd(statistics[i].loses);
                FastAdd(statistics[i].timeSTart);
                FastAdd(statistics[i].totalTime);
                Scroll.Children.Add(panel);
            }
        }
        private void NavigateToTest(object sender, RoutedEventArgs e)
        {
            var window = new Test(statistics);
            window.Show();
            Close();
        }
    }
}
