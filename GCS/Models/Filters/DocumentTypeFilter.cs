using System;
using System.Collections.Generic;

namespace GCS.Models.Filters {
    public class DocumentTypeFilter {
        public List<DocumentTypeFilterItem> DocumentTypes { get; set; }
    }

    public class DocumentTypeFilterItem {
        public String alias { get; set; }
        public String text { get; set; }
    }
}