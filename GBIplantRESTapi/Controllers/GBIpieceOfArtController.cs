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
    public class GBIpieceOfArtController : ApiController
    {
        private readonly IGBIpieceOfArtService _service;

        public GBIpieceOfArtController(IGBIpieceOfArtService service)
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
            var element = _service.GetGBIpieceOfArt(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }

        [HttpPost]
        public void AddElement(GBIpieceOfArtBindingModel model)
        {
            _service.AddGBIpieceOfArt(model);
        }

        [HttpPost]
        public void UpdElement(GBIpieceOfArtBindingModel model)
        {
            _service.UpdGBIpieceOfArt(model);
        }

        [HttpPost]
        public void DelElement(GBIpieceOfArtBindingModel model)
        {
            _service.DelGBIpieceOfArt(model.Id);
        }
    }
}
