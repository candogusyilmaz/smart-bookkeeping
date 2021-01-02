namespace StockManagementSystem.UI.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public string DirectoryPath { get; set; }
        public string Time { get; set; }

        public SettingsViewModel()
        {
            DirectoryPath = AppSettingsHelper.ReadSetting("BackupPath");
            Time = AppSettingsHelper.ReadSetting("BackupTime");
        }
    }
}
