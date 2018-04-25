using GCS.Models.Dtos;
using GCS.Repositories;
using System;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace GCS.Controllers {
    public class GCSQueriesController : SurfaceController {

        private QueriesRepository QueriesRepository;
        public GCSQueriesController() {
            QueriesRepository = new QueriesRepository();
        }

        public JsonResult Get() {
            return new JsonResult();
        }

        public JsonResult Set(SearchEntry model) {
            model.Date = DateTime.Now;
            QueriesRepository.Create(model);
            return new JsonResult();
        }

        public JsonResult UpdateClick(SearchEntry model) {
            model.Date = DateTime.Now;
            QueriesRepository.UpdateClick(model);
            return new JsonResult();
        }
    }
}