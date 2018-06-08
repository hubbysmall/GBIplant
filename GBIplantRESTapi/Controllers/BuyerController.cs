﻿using GBIplantService.BindingModels;
using GBIplantService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GBIplantRESTapi.Controllers
{
    public class BuyerController : ApiController
    {
        private readonly IBuyerService _service;

        public BuyerController(IBuyerService service)
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
            var element = _service.GetBuyer(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }

        [HttpPost]
        public void AddElement(BuyerBindingModel model)
        {
            _service.AddBuyer(model);
        }

        [HttpPost]
        public void UpdElement(BuyerBindingModel model)
        {
            _service.UpdBuyer(model);
        }

        [HttpPost]
        public void DelElement(BuyerBindingModel model)
        {
            _service.DelBuyer(model.Id);
        }
    }
}
