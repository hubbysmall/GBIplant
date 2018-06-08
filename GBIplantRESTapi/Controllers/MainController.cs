using GBIplantService.BindingModels;
using GBIplantService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GBIplantRESTapi.Controllers
{
    public class MainController : ApiController
    {
        private readonly IMainService _service;

        public MainController(IMainService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void CreateOrder(ZakazBindingModel model)
        {
            _service.CreateZakaz(model);
        }

        [HttpPost]
        public void TakeOrderInWork(ZakazBindingModel model)
        {
            _service.TakeZakazInWork(model);
        }

        [HttpPost]
        public void FinishOrder(ZakazBindingModel model)
        {
            _service.FinishZakaz(model.Id);
        }

        [HttpPost]
        public void PayOrder(ZakazBindingModel model)
        {
            _service.PayZakaz(model.Id);
        }

        [HttpPost]
        public void PutComponentOnStock(Storage__GBIingridientBindingModel model)
        {
            _service.PutGBIingridientInStorage(model);
        }
    }
}
