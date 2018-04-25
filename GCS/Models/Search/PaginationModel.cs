using System;
using System.Collections.Generic;

namespace GCS.Models.Search {
    public class PaginationModel {
        public List<PagingItemModel> PagingItems { get; set; }
        public int MaxPages { get; set; }
        public List<PagingItemModel> PagingRange {
            get {
                int trailAmount = MaxPages / 2;
                int startIndex = 0;

                if (CurrentPage - trailAmount < 1) {
                    startIndex = 0;
                } else if (CurrentPage == TotalPages) {
                    startIndex = TotalPages - MaxPages > -1 ? TotalPages - MaxPages : 0;
                } else if (CurrentPage + trailAmount > TotalPages) {
                    startIndex = CurrentPage - MaxPages + (TotalPages - CurrentPage);
                } else {
                    startIndex = (CurrentPage - trailAmount) - 1;
                }

                MaxPages = TotalPages < MaxPages ? TotalPages : MaxPages;

                return PagingItems.GetRange(startIndex, MaxPages);
            }
        }
        public int CurrentPage { get; set; }
        public Boolean AtFirstPage { get; set; }
        public Boolean AtLastPage { get; set; }
        public String NextPageurl { get; set; }
        public String PreviousPageUrl { get; set; }
        public String FirstItemUrl { get; set; }
        public String LastItemUrl { get; set; }
        public int TotalItems { get; set; }
        public int Limit { get; set; }
        public int TotalPages { get; set; }
    }
}