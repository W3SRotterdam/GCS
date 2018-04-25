using System.Linq;

namespace GCS.Models.API {
    public class Item {
        //public string kind { get; set; }
        public string title { get; set; }
        //public string htmlTitle { get; set; }
        public string link { get; set; }
        //public string displayLink { get; set; }
        public string snippet { get; set; }
        public string htmlSnippet { get; set; }
        //public string cacheId { get; set; }
        public string formattedUrl { get; set; }
        //public string htmlFormattedUrl { get; set; }
        public Pagemap pagemap { get; set; }
        public string fileFormat { get; set; }
        public string GetImage {
            get {
                return pagemap != null && pagemap.cse_thumbnail != null && pagemap.cse_thumbnail.Count > 0 ? pagemap.cse_thumbnail.First().src : "";
            }
        }
    }
}