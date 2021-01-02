using StockManagementSystem.Library;
using System.Collections.ObjectModel;
using System.Linq;

namespace StockManagementSystem.UI
{
    public class CompanyListViewModel : BaseViewModel
    {
        public ObservableCollection<CompanyModel> Companies { get; set; }

        public CompanyListViewModel()
        {
            Companies = new ObservableCollection<CompanyModel>();

            foreach (var item in CommonService.GetAll<CompanyModel>(DbTables.Companies))
                Companies.Add(item);
        }

        public void InsertLastRecord()
        {
            Companies.Add(CommonService.GetLastInsertedRow<CompanyModel>(DbTables.Companies).First());
        }
    }
}
