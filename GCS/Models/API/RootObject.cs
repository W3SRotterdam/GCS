using System.Collections.Generic;

namespace W3S_GCS.Models.API {
    public class RootObject {
        //public string kind { get; set; }
        //public Url url { get; set; }
        //public Queries queries { get; set; }
        //public Context context { get; set; }
        public SearchInformation searchInformation { get; set; }
        public List<Item> items { get; set; }
        public Spelling spelling { get; set; }
    }
}