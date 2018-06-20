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
    public class MainServiceList : IMainService
    {
        private ALLDataListSingleton source;

        public MainServiceList()
        {
            source = ALLDataListSingleton.GetInstance();
        }

        public List<ZakazViewModel> GetList()
        {
            List<ZakazViewModel> result = source.Zakazes
                .Select(rec => new ZakazViewModel
                {
                    Id = rec.Id,
                    BuyerId = rec.BuyerId,
                    GBIpieceOfArtId = rec.GBIpieceofArtId,
                    ExecutorId = rec.ExecutorId,
                    DateCreate = rec.DateCreate.ToLongDateString(),
                    DateExecute = rec.DateCreate.ToLongDateString(),
                    Status = rec.Status.ToString(),
                    Count = rec.Count,
                    Sum = rec.Sum,
                    BuyerFIO = source.Buyers
                                    .FirstOrDefault(recC => recC.Id == rec.BuyerId).BuyerFIO, //?.BuyerFIO,
                    GBIpieceOfArtName = source.GBIpieceOfArts
                                    .FirstOrDefault(recP => recP.Id == rec.GBIpieceofArtId).GBIpieceOfArtNAme, //?.GBIpieceOfArtNAme,
                    ExecutorName = source.Executors
                                    .FirstOrDefault(recI => recI.Id == rec.ExecutorId).ExecutorFIO //?.ExecutorFIO
                })
                .ToList();
            return result;
        }

        public void CreateZakaz(ZakazBindingModel model)
        {
            int maxId = source.Zakazes.Count > 0 ? source.Zakazes.Max(rec => rec.Id) : 0;
            source.Zakazes.Add(new Zakaz
            {
                Id = maxId + 1,
                BuyerId = model.BuyerId,
                GBIpieceofArtId = model.GBIpieceOfArtId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = ZakazStatus.taken
            });
        }

        public void TakeZakazInWork(ZakazBindingModel model)
        {
            Zakaz element = source.Zakazes.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            // смотрим по количеству компонентов на складах
            var productComponents = source.GBIpieceofArt__ingridients.Where(rec => rec.GBIpieceOfArtId == element.GBIpieceofArtId);
            foreach(var productComponent in productComponents)
            {
                int countOnStocks = source.Storage__GBIingridients
                                            .Where(rec => rec.GBIingridientId == productComponent.GBIindgridientId)
                                            .Sum(rec => rec.Count);
                if (countOnStocks < productComponent.Count * element.Count)
                {
                    var componentName = source.GBIindgridients
                                    .FirstOrDefault(rec => rec.Id == productComponent.GBIindgridientId);
                    throw new Exception("Не достаточно компонента " + componentName.GBIindgridientName +  // + componentName?.GBIindgridientName +
                        " требуется " + productComponent.Count + ", в наличии " + countOnStocks);
                }
            }
            // списываем
            foreach (var productComponent in productComponents)
            {
                int countOnStocks = productComponent.Count * element.Count;
                var stockComponents = source.Storage__GBIingridients
                                            .Where(rec => rec.GBIingridientId == productComponent.GBIindgridientId);
                foreach (var stockComponent in stockComponents)
                {
                    // компонентов на одном слкаде может не хватать
                    if (stockComponent.Count >= countOnStocks)
                    {
                        stockComponent.Count -= countOnStocks;
                        break;
                    }
                    else
                    {
                        countOnStocks -= stockComponent.Count;
                        stockComponent.Count = 0;
                    }
                }
            }
            element.ExecutorId = model.ExecutorId;
            element.DateCreate = DateTime.Now;
            element.Status = ZakazStatus.inProcess;
        }

        public void FinishZakaz(int id)
        {
            Zakaz element = source.Zakazes.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = ZakazStatus.ready;
        }

        public void PayZakaz(int id)
        {
            Zakaz element = source.Zakazes.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = ZakazStatus.paid;
        }

        public void PutGBIingridientInStorage(Storage__GBIingridientBindingModel model)
        {
            Storage__GBIingridient element = source.Storage__GBIingridients
                                                .FirstOrDefault(rec => rec.StorageId == model.StorageId &&
                                                                    rec.GBIingridientId == model.GBIingridientId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                int maxId = source.Storage__GBIingridients.Count > 0 ? source.Storage__GBIingridients.Max(rec => rec.Id) : 0;
                source.Storage__GBIingridients.Add(new Storage__GBIingridient
                {
                    Id = ++maxId,
                    StorageId = model.StorageId,
                    GBIingridientId = model.GBIingridientId,
                    Count = model.Count
                });
            }
        }
    }
}
