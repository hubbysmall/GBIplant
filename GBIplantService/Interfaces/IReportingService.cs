using GBIplantService.Attributes;
using GBIplantService.BindingModels;
using GBIplantService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService.Interfaces
{
    [CustomInterface("Интерфейс для работы с отчетами")]
    public interface IReportingService
    {
        [CustomMethod("Метод сохранения списка изделий в doc-файл")]
        void SaveGBIpieceOfArtPrice(ReportingBindingModel model);
        [CustomMethod("Метод получения списка складов с количество компонент на них")]
        List<StorageLoadViewModel> GetStorageLoad();
        [CustomMethod("Метод сохранения списка списка складов с количество компонент на них в xls-файл")]
        void SaveStorageLoad(ReportingBindingModel model);
        [CustomMethod("Метод получения списка заказов клиентов")]
        List<BuyerZakazesViewModel> GetBuyerZakazes(ReportingBindingModel model);
        [CustomMethod("Метод сохранения списка заказов клиентов в pdf-файл")]
        void SaveBuyerZakazes(ReportingBindingModel model);
    }
}
