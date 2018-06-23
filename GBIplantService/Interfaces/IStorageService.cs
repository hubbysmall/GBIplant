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
    [CustomInterface("Интерфейс для работы со складами")]
    public interface IStorageService
    {
        [CustomMethod("Метод получения списка складов")]
        List<StorageViewModel> GetList();
        [CustomMethod("Метод получения склада по id")]
        StorageViewModel GetStorage(int id);
        [CustomMethod("Метод добавления склада")]
        void AddStorage(StorageBindingModel model);
        [CustomMethod("Метод изменения данных по складу")]
        void UpdStorage(StorageBindingModel model);
        [CustomMethod("Метод удаления склада")]
        void DelStorage(int id);
    }
}
