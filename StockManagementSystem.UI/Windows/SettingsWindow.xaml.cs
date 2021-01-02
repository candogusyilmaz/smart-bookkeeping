using StockManagementSystem.UI.ViewModels;
using System;
using System.IO;
using System.Windows;

namespace StockManagementSystem.UI.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsViewModel ViewModel { get; set; }

        public SettingsWindow()
        {
            InitializeComponent();

            ViewModel = new SettingsViewModel();
            this.DataContext = ViewModel;
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            TimeSpan.TryParse(ViewModel.Time, out TimeSpan time);

            if (Directory.Exists(ViewModel.DirectoryPath) || time.TotalMinutes < 1 || time.TotalDays > 1)
            {
                MessageBox.Show("Dosya yolu veya saat biçimi hatalı!", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AppSettingsHelper.AddUpdateAppSettings("BackupTime", ViewModel.Time);
            AppSettingsHelper.AddUpdateAppSettings("BackupPath", ViewModel.DirectoryPath);
        }



        private void SaveFilePath_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog ofd = new Microsoft.Win32.SaveFileDialog
            {
                Title = "Yedekleme klasörünü ve dosya adını seçin",
                DefaultExt = "db",
                Filter = "Veritabanı dosyası (*.db)|*.db"

            };

            bool? result = ofd.ShowDialog();

            if (result != true)
                return;

            ViewModel.DirectoryPath = ofd.FileName;
        }
    }
}
