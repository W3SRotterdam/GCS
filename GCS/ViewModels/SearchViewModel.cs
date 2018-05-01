using W3S_GCS.Models.API;

namespace W3S_GCS.ViewModels {
    public class SearchViewModel {
        public string Query { get; set; }
        public string StartIndex { get; set; }
        public RootObject JSON { get; set; }
    }
}