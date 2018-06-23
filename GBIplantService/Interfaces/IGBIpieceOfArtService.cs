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
    [CustomInterface("Интерфейс для работы с изделиями")]
    public interface IGBIpieceOfArtService
    {
        [CustomMethod("Метод получения списка изделий")]
        List<GBIpieceOfArtViewModel> GetList();
        [CustomMethod("Метод получения изделия по id")]
        GBIpieceOfArtViewModel GetGBIpieceOfArt(int id);
        [CustomMethod("Метод добавления изделия")]
        void AddGBIpieceOfArt(GBIpieceOfArtBindingModel model);
        [CustomMethod("Метод изменения данных по изделию")]
        void UpdGBIpieceOfArt(GBIpieceOfArtBindingModel model);
        [CustomMethod("Метод удаления изделия")]
        void DelGBIpieceOfArt(int id);
    }
}
