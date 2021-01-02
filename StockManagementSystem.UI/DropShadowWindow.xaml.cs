using System.Windows;

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
