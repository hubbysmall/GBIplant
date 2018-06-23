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
    [CustomInterface("Интерфейс для работы с работниками")]
    public interface IExecutorService
    {
        [CustomMethod("Метод получения списка работников")]
        List<ExecutorViewModel> GetList();
        [CustomMethod("Метод получения работника по id")]
        ExecutorViewModel GetExecutor(int id);
        [CustomMethod("Метод добавления работника")]
        void AddExecutor(ExecutorBindingModel model);
        [CustomMethod("Метод изменения данных по работнику")]
        void UpdExecutor(ExecutorBindingModel model);
        [CustomMethod("Метод удаления работника")]
        void DelExecutor(int id);
    }
}
