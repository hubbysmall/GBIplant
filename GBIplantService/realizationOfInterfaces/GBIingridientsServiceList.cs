using GBIplantModel;
using GBIplantService.BindingModels;
using GBIplantService.Interfaces;
using GBIplantService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService.realizationOfInterfaces
{
    public class GBIingridientsServiceList : IGBIingridientService
    {
        private ALLDataListSingleton source;

        public GBIingridientsServiceList()
        {
            source = ALLDataListSingleton.GetInstance();
        }

        public List<GBIingridientViewModel> GetList()
        {
            List<GBIingridientViewModel> result = source.GBIindgridients
                .Select(rec => new GBIingridientViewModel
                {
                    Id = rec.Id,
                    GBIingridientName = rec.GBIindgridientName
                })
                .ToList();
            return result;
        }

        public GBIingridientViewModel GetGBIingridient(int id)
        {
            GBIindgridient element = source.GBIindgridients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new GBIingridientViewModel
                {
                    Id = element.Id,
                    GBIingridientName = element.GBIindgridientName
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddGBIingridient(GBIingridientBindingModel model)
        {
            GBIindgridient element = source.GBIindgridients.FirstOrDefault(rec => rec.GBIindgridientName == model.GBIingridient);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            int maxId = source.GBIindgridients.Count > 0 ? source.GBIindgridients.Max(rec => rec.Id) : 0;
            source.GBIindgridients.Add(new GBIindgridient
            {
                Id = maxId + 1,
                GBIindgridientName = model.GBIingridient
            });
        }

        public void UpdGBIingridient(GBIingridientBindingModel model)
        {
            GBIindgridient element = source.GBIindgridients.FirstOrDefault(rec =>
                                        rec.GBIindgridientName == model.GBIingridient && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            element = source.GBIindgridients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.GBIindgridientName = model.GBIingridient;
        }

        public void DelGBIingridient(int id)
        {
            GBIindgridient element = source.GBIindgridients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.GBIindgridients.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
