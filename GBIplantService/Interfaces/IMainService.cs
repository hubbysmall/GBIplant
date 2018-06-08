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
    [CustomInterface("Интерфейс для работы с заказами")]
    public interface IMainService
    {
        [CustomMethod("Метод получения списка заказов")]
        List<ZakazViewModel> GetList();
        [CustomMethod("Метод создания заказа")]
        void CreateZakaz(ZakazBindingModel model);
        [CustomMethod("Метод передачи заказа в работу")]
        void TakeZakazInWork(ZakazBindingModel model);
        [CustomMethod("Метод передачи заказа на оплату")]
        void FinishZakaz(int id);
        [CustomMethod("Метод фиксирования оплаты по заказу")]
        void PayZakaz(int id);
        [CustomMethod("Метод пополнения компонент на складе")]
        void PutGBIingridientInStorage(Storage__GBIingridientBindingModel model);
    }
}
