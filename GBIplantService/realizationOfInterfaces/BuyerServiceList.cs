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
    public class BuyerServiceList : IBuyerService
    {
        private ALLDataListSingleton source;

        public BuyerServiceList()
        {
            source = ALLDataListSingleton.GetInstance();
        }

        public List<BuyerViewModel> GetList()
        {           
            List<BuyerViewModel> result = source.Buyers
                .Select(rec => new BuyerViewModel
                {
                    Id = rec.Id,
                    BuyerFIO = rec.BuyerFIO
                })
                .ToList();
            return result;
        }

        public BuyerViewModel GetBuyer(int id)
        {           
            Buyer element = source.Buyers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new BuyerViewModel
                {
                    Id = element.Id,
                    BuyerFIO = element.BuyerFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddBuyer(BuyerBindingModel model)
        {
            Buyer element = source.Buyers.FirstOrDefault(rec => rec.BuyerFIO == model.BuyerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            int maxId = source.Buyers.Count > 0 ? source.Buyers.Max(rec => rec.Id) : 0;
            source.Buyers.Add(new Buyer
            {
                Id = maxId + 1,
                BuyerFIO = model.BuyerFIO
            });
        }

        public void UpdBuyer(BuyerBindingModel model)
        {
            Buyer element = source.Buyers.FirstOrDefault(rec =>
                                    rec.BuyerFIO == model.BuyerFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = source.Buyers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.BuyerFIO = model.BuyerFIO;
        }

        public void DelBuyer(int id)
        {
            Buyer element = source.Buyers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Buyers.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        void qvert() {
            List<BuyerViewModel> result = GetList();
            IEnumerable<BuyerViewModel> query = from buyer in result where buyer.BuyerFIO == "client" select buyer;


        }

    }
}
