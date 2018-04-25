using GCS.Repositories;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace GCS.Controllers {
    public class GCSStatisticsController : SurfaceController {

        QueriesRepository QueriesRepository;

        public GCSStatisticsController() {
            QueriesRepository = new QueriesRepository();
        }

        public JsonResult Get(int year = -1, int month = -1) {
            return new JsonResult() {
                Data = new {
                    topqueries = QueriesRepository.GetTopQueries(year, month),
                    topspelling = QueriesRepository.GetTopSpelling(year, month),
                    topclicks = QueriesRepository.GetTopClicks(year, month),
                    averageload = QueriesRepository.GetAverageLoad(year, month)
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}