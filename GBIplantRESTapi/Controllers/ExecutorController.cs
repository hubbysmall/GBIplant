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
    public class ExecutorController : ApiController
    {
        private readonly IExecutorService _service;

        public ExecutorController(IExecutorService service)
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
            var element = _service.GetExecutor(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }

        [HttpPost]
        public void AddElement(ExecutorBindingModel model)
        {
            _service.AddExecutor(model);
        }

        [HttpPost]
        public void UpdElement(ExecutorBindingModel model)
        {
            _service.UpdExecutor(model);
        }

        [HttpPost]
        public void DelElement(ExecutorBindingModel model)
        {
            _service.DelExecutor(model.Id);
        }
    }
}
