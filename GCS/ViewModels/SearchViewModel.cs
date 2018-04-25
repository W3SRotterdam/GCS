using GCS.Models.API;

namespace GCS.ViewModels {
    public class SearchViewModel {
        public string Query { get; set; }
        public string StartIndex { get; set; }
        public RootObject JSON { get; set; }
    }
}