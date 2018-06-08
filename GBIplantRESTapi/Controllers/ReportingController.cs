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
    public class ReportingController : ApiController
    {
        private readonly IReportingService _service;

        public ReportingController(IReportingService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetStocksLoad()
        {
            var list = _service.GetStorageLoad();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public IHttpActionResult GetClientOrders(ReportingBindingModel model)
        {
            var list = _service.GetBuyerZakazes(model);
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void SaveProductPrice(ReportingBindingModel model)
        {
            _service.SaveGBIpieceOfArtPrice(model);
        }

        [HttpPost]
        public void SaveStocksLoad(ReportingBindingModel model)
        {
            _service.SaveStorageLoad(model);
        }

        [HttpPost]
        public void SaveClientOrders(ReportingBindingModel model)
        {
            _service.SaveBuyerZakazes(model);
        }
    }
}
