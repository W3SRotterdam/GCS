using System;

namespace GCS.Models.Search {
    public class SearchRequest {
        public string Query { get; set; }
        public int StartIndex { get; set; }
        public String FileType { get; set; }
        public String Section { get; set; }
        public int CurrentNodeID { get; set; }
    }
}