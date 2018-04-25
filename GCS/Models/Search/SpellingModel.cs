using System;

namespace GCS.Models.Search {
    public class SpellingModel {
        public String CorrectedQuery { get; set; }
        public String SearchURL { get; set; }
        public String FormattedSearchURL {
            get {
                return String.Format("{0}?gcs_q={1}&startIndex=1", SearchURL, CorrectedQuery);
            }
        }
    }
}