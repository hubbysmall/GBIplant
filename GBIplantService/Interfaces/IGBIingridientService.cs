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
    [CustomInterface("Интерфейс для работы с компонентами")]
    public interface IGBIingridientService
    {
        [CustomMethod("Метод получения списка компонент")]
        List<GBIingridientViewModel> GetList();
        [CustomMethod("Метод получения компонента по id")]
        GBIingridientViewModel GetGBIingridient(int id);
        [CustomMethod("Метод добавления компонента")]
        void AddGBIingridient(GBIingridientBindingModel model);
        [CustomMethod("Метод изменения данных по компоненту")]
        void UpdGBIingridient(GBIingridientBindingModel model);
        [CustomMethod("Метод удаления компонента")]
        void DelGBIingridient(int id);
    }
}
