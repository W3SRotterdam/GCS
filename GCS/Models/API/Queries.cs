using System.Collections.Generic;

namespace GCS.Models.API {
    public class Queries {
        public List<Request> request { get; set; }
        public List<NextPage> nextPage { get; set; }
    }
}