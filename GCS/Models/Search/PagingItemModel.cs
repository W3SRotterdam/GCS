using System;

namespace W3S_GCS.Models.Search {
    public class PagingItemModel {
        public int PageNumber { get; set; }
        public bool Active { get; set; }
        public String Url { get; set; }
    }
}