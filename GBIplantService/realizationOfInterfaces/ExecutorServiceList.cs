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
    public class ExecutorServiceList : IExecutorService
    {
        private ALLDataListSingleton source;

        public ExecutorServiceList()
        {
            source = ALLDataListSingleton.GetInstance();
        }

        public List<ExecutorViewModel> GetList()
        {
            List<ExecutorViewModel> result = source.Executors
                .Select(rec => new ExecutorViewModel
                {
                    Id = rec.Id,
                    ExecutorFIO = rec.ExecutorFIO
                })
                .ToList();
            return result;
        }

        public ExecutorViewModel GetExecutor(int id)
        {
            Executor element = source.Executors.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ExecutorViewModel
                {
                    Id = element.Id,
                    ExecutorFIO = element.ExecutorFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddExecutor(ExecutorBindingModel model)
        {
            Executor element = source.Executors.FirstOrDefault(rec => rec.ExecutorFIO == model.ExecutorFIO);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            int maxId = source.Executors.Count > 0 ? source.Executors.Max(rec => rec.Id) : 0;
            source.Executors.Add(new Executor
            {
                Id = maxId + 1,
                ExecutorFIO = model.ExecutorFIO
            });
        }

        public void UpdExecutor(ExecutorBindingModel model)
        {
            Executor element = source.Executors.FirstOrDefault(rec =>
                                        rec.ExecutorFIO == model.ExecutorFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            element = source.Executors.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ExecutorFIO = model.ExecutorFIO;
        }

        public void DelExecutor(int id)
        {
            Executor element = source.Executors.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Executors.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
