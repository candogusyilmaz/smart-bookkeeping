using StockManagementSystem.Library;
using System;
using System.Timers;
using System.Windows;

namespace StockManagementSystem.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        TimeSpan time;
        string directory;
        bool backedUp = false;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                time = TimeSpan.Parse(AppSettingsHelper.ReadSetting("BackupTime"));
                directory = AppSettingsHelper.ReadSetting("BackupPath");

            }
            catch { }


            var timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;


        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (time == null) return;

            if ((int)time.TotalMinutes != (int)DateTime.Now.TimeOfDay.TotalMinutes || backedUp) return;

            if (directory != null)
                SqliteDataAccess.Backup(directory);

            backedUp = true;
        }
    }
}
