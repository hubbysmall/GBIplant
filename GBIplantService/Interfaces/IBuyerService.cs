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
    [CustomInterface("Интерфейс для работы с клиентами")]
    public interface IBuyerService
    {
        [CustomMethod("Метод получения списка клиентов")]
        List<BuyerViewModel> GetList();
        [CustomMethod("Метод получения клиента по id")]
        BuyerViewModel GetBuyer(int id);
        [CustomMethod("Метод добавления клиента")]
        void AddBuyer(BuyerBindingModel model);
        [CustomMethod("Метод изменения данных по клиенту")]
        void UpdBuyer(BuyerBindingModel model);
        [CustomMethod("Метод удаления клиента")]
        void DelBuyer(int id);
    }
}
