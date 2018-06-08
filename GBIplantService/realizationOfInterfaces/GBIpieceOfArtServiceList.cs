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
    public class GBIpieceOfArtServiceList : IGBIpieceOfArtService
    {
        private ALLDataListSingleton source;

        public GBIpieceOfArtServiceList()
        {
            source = ALLDataListSingleton.GetInstance();
        }

         public List<GBIpieceOfArtViewModel> GetList()
        {
            List<GBIpieceOfArtViewModel> result = source.GBIpieceOfArts
                .Select(rec => new GBIpieceOfArtViewModel
                {
                    Id = rec.Id,
                    GBIpieceOfArtName = rec.GBIpieceOfArtNAme,
                    Price = rec.Price,
                    GBIpieceofArt__ingridients = source.GBIpieceofArt__ingridients
                            .Where(recPC => recPC.GBIpieceOfArtId == rec.Id)
                            .Select(recPC => new GBIpieceofArt__ingridientViewModel
                            {
                                Id = recPC.Id,
                                GBIpieceofArtId = recPC.GBIpieceOfArtId,
                                GBIingridientId = recPC.GBIindgridientId,
                                GBIingridientName = source.GBIindgridients
                                    .FirstOrDefault(recC => recC.Id == recPC.GBIindgridientId).GBIindgridientName,   //?.GBIindgridientName
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

      public GBIpieceOfArtViewModel GetGBIpieceOfArt(int id)
        {
            GBIpieceOfArt element = source.GBIpieceOfArts.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new GBIpieceOfArtViewModel
                {
                    Id = element.Id,
                    GBIpieceOfArtName = element.GBIpieceOfArtNAme,
                    Price = element.Price,
                    GBIpieceofArt__ingridients = source.GBIpieceofArt__ingridients
                            .Where(recPC => recPC.GBIpieceOfArtId == element.Id)
                            .Select(recPC => new GBIpieceofArt__ingridientViewModel
                            {
                                Id = recPC.Id,
                                GBIpieceofArtId = recPC.GBIpieceOfArtId,
                                GBIingridientId = recPC.GBIindgridientId,
                                GBIingridientName = source.GBIindgridients
                                        .FirstOrDefault(recC => recC.Id == recPC.GBIindgridientId).GBIindgridientName, //?.GBIindgridientName
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

      public void AddGBIpieceOfArt(GBIpieceOfArtBindingModel model)
      {
          GBIpieceOfArt element = source.GBIpieceOfArts.FirstOrDefault(rec => rec.GBIpieceOfArtNAme == model.GBIpieceOfArtName);
          if (element != null)
          {
              throw new Exception("Уже есть изделие с таким названием");
          }
          int maxId = source.GBIpieceOfArts.Count > 0 ? source.GBIpieceOfArts.Max(rec => rec.Id) : 0;
          source.GBIpieceOfArts.Add(new GBIpieceOfArt
          {
              Id = maxId + 1,
              GBIpieceOfArtNAme = model.GBIpieceOfArtName,
              Price = model.Price
          });
          // компоненты для изделия
          int maxPCId = source.GBIpieceofArt__ingridients.Count > 0 ?
                                  source.GBIpieceofArt__ingridients.Max(rec => rec.Id) : 0;
          // убираем дубли по компонентам
          var groupComponents = model.GBIpieceofArt__ingridients
                                      .GroupBy(rec => rec.GBIingridientId)
                                      .Select(rec => new
                                      {
                                          ComponentId = rec.Key,
                                          Count = rec.Sum(r => r.Count)
                                      });
          // добавляем компоненты
          foreach (var groupComponent in groupComponents)
          {
              source.GBIpieceofArt__ingridients.Add(new GBIpieceofArt__ingridient
              {
                  Id = ++maxPCId,
                  GBIpieceOfArtId = maxId + 1,
                  GBIindgridientId = groupComponent.ComponentId,
                  Count = groupComponent.Count
              });
          }
      }

      public void UpdGBIpieceOfArt(GBIpieceOfArtBindingModel model)
      {
          GBIpieceOfArt element = source.GBIpieceOfArts.FirstOrDefault(rec =>
                                      rec.GBIpieceOfArtNAme == model.GBIpieceOfArtName && rec.Id != model.Id);
          if (element != null)
          {
              throw new Exception("Уже есть изделие с таким названием");
          }
          element = source.GBIpieceOfArts.FirstOrDefault(rec => rec.Id == model.Id);
          if (element == null)
          {
              throw new Exception("Элемент не найден");
          }
          element.GBIpieceOfArtNAme = model.GBIpieceOfArtName;
          element.Price = model.Price;

          int maxPCId = source.GBIpieceofArt__ingridients.Count > 0 ? source.GBIpieceofArt__ingridients.Max(rec => rec.Id) : 0;
          // обновляем существуюущие компоненты
          var compIds = model.GBIpieceofArt__ingridients.Select(rec => rec.GBIingridientId).Distinct();
          var updateComponents = source.GBIpieceofArt__ingridients
                                          .Where(rec => rec.GBIpieceOfArtId == model.Id &&
                                         compIds.Contains(rec.GBIindgridientId));
          foreach (var updateComponent in updateComponents)
          {
              updateComponent.Count = model.GBIpieceofArt__ingridients
                                              .FirstOrDefault(rec => rec.Id == updateComponent.Id).Count;
          }
          source.GBIpieceofArt__ingridients.RemoveAll(rec => rec.GBIpieceOfArtId == model.Id &&
                                     !compIds.Contains(rec.GBIindgridientId));
          // новые записи
          var groupComponents = model.GBIpieceofArt__ingridients
                                      .Where(rec => rec.Id == 0)
                                      .GroupBy(rec => rec.GBIingridientId)
                                      .Select(rec => new
                                      {
                                          ComponentId = rec.Key,
                                          Count = rec.Sum(r => r.Count)
                                      });
          foreach (var groupComponent in groupComponents)
          {
              GBIpieceofArt__ingridient elementPC = source.GBIpieceofArt__ingridients
                                      .FirstOrDefault(rec => rec.GBIpieceOfArtId == model.Id &&
                                                      rec.GBIindgridientId == groupComponent.ComponentId);
              if (elementPC != null)
              {
                  elementPC.Count += groupComponent.Count;
              }
              else
              {
                  source.GBIpieceofArt__ingridients.Add(new GBIpieceofArt__ingridient
                  {
                      Id = ++maxPCId,
                      GBIpieceOfArtId = model.Id,
                      GBIindgridientId = groupComponent.ComponentId,
                      Count = groupComponent.Count
                  });
              }
          }
      }

      public void DelGBIpieceOfArt(int id)
      {
          GBIpieceOfArt element = source.GBIpieceOfArts.FirstOrDefault(rec => rec.Id == id);
          if (element != null)
          {
              // удаяем записи по компонентам при удалении изделия
              source.GBIpieceofArt__ingridients.RemoveAll(rec => rec.GBIpieceOfArtId == id);
              source.GBIpieceOfArts.Remove(element);
          }
          else
          {
              throw new Exception("Элемент не найден");
          }
      }     
    }
}
