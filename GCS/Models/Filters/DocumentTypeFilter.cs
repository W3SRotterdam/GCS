using System;
using System.Collections.Generic;

namespace W3S_GCS.Models.Filters {
    public class DocumentTypeFilter {
        public List<DocumentTypeFilterItem> DocumentTypes { get; set; }
    }

    public class DocumentTypeFilterItem {
        public String alias { get; set; }
        public String text { get; set; }
    }
}