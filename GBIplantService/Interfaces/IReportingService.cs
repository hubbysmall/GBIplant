using GBIplantService.BindingModels;
using GBIplantService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService.Interfaces
{
    public interface IReportingService
    {
        void SaveGBIpieceOfArtPrice(ReportingBindingModel model);

        List<StorageLoadViewModel> GetStorageLoad();

        void SaveStorageLoad(ReportingBindingModel model);

        List<BuyerZakazesViewModel> GetBuyerZakazes(ReportingBindingModel model);

        void SaveBuyerZakazes(ReportingBindingModel model);
    }
}
