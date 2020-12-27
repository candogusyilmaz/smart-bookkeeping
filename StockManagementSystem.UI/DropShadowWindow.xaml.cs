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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StockManagementSystem.UI
{
    /// <summary>
    /// Interaction logic for DropShadowWindow.xaml
    /// </summary>
    public partial class DropShadowWindow : Window
    {
        public Window NextWindow { get; set; }

        public DropShadowWindow(Window window)
        {
            InitializeComponent();

            this.Loaded += delegate
            {
                NextWindow.Show();
            };

            this.MouseLeftButtonDown += delegate 
            {
                NextWindow.Close();
                this.Close();
            };

            this.Owner = Application.Current.MainWindow;

            this.Height = this.Owner.ActualHeight;
            this.Width = this.Owner.ActualWidth;

            NextWindow = window;
            NextWindow.Owner = this.Owner;
            NextWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            NextWindow.Closed += delegate { this.Close(); };
        }
    }
}
