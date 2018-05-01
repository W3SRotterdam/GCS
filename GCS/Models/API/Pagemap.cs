using System.Collections.Generic;

namespace W3S_GCS.Models.API {
    public class Pagemap {
        public List<Metatag> metatags { get; set; }
        public List<CseThumbnail> cse_thumbnail { get; set; }
        public List<CseImage> cse_image { get; set; }
    }
}