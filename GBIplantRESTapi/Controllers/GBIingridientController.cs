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
    public class GBIingridientController : ApiController
    {
        private readonly IGBIingridientService _service;

        public GBIingridientController(IGBIingridientService service)
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

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var element = _service.GetGBIingridient(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }

        [HttpPost]
        public void AddElement(GBIingridientBindingModel model)
        {
            _service.AddGBIingridient(model);
        }

        [HttpPost]
        public void UpdElement(GBIingridientBindingModel model)
        {
            _service.UpdGBIingridient(model);
        }

        [HttpPost]
        public void DelElement(GBIingridientBindingModel model)
        {
            _service.DelGBIingridient(model.Id);
        }
    }
}
