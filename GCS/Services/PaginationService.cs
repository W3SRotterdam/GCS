using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using W3S_GCS.Models.API;
using W3S_GCS.Models.Search;

namespace W3S_GCS.Services {
    public class PaginationService {

        private QueryParamsServices QueryParamsService;

        public PaginationService() {
            QueryParamsService = new QueryParamsServices();
        }

        public PaginationModel GetPaginationModel(HttpRequestBase request, RootObject rootobj, int itemsPerPage, int startIndex = 1, String query = "", String fileType = "", String section = "", int maxPages = 6) {
            PaginationModel PagingItemsModel = new PaginationModel();
            List<PagingItemModel> PagingItems = new List<PagingItemModel>();
            int TotalPages = 0;
            int TotalItems = int.Parse(rootobj.searchInformation.totalResults);
            String URL = request.UrlReferrer.LocalPath;

            int PageNumber = 1;
            TotalPages = (TotalItems / itemsPerPage) + 1;

            int j = 0;
            bool AtFirstPage = startIndex == 1 ? true : false;
            bool AtLastPage = (startIndex + itemsPerPage) >= TotalItems ? true : false;

            String queryParameters = QueryParamsService.FormatQueryParameters(URL, new String[] { "gcs_q", "gcs_ftype", "gcs_sect" }, new String[] { query, fileType, section });

            while (PageNumber <= TotalPages) {
                PagingItems.Add(
                new PagingItemModel {
                    PageNumber = PageNumber,
                    Url = String.Format(URL + "{0}", "?startIndex=" + ((itemsPerPage * j) + 1) + queryParameters),
                    Active = ((itemsPerPage * j) + 1) == startIndex
                });

                PageNumber++;
                j++;
            }

            PagingItemsModel.CurrentPage = PagingItems != null && PagingItems.FirstOrDefault(p => p.Active) != null ? PagingItems.FirstOrDefault(p => p.Active).PageNumber : 1;
            PagingItemsModel.PagingItems = PagingItems;
            PagingItemsModel.NextPageurl = String.Format(URL + "{0}", "?startIndex=" + (startIndex + itemsPerPage) + queryParameters);
            PagingItemsModel.PreviousPageUrl = String.Format(URL + "{0}", "?startIndex=" + (startIndex - itemsPerPage) + queryParameters);
            PagingItemsModel.FirstItemUrl = String.Format(URL + "{0}", "?startIndex=1" + queryParameters);
            PagingItemsModel.LastItemUrl = String.Format(URL + "{0}", "?startIndex=" + ((itemsPerPage * TotalPages) - itemsPerPage) + queryParameters);
            PagingItemsModel.AtFirstPage = AtFirstPage;
            PagingItemsModel.AtLastPage = AtLastPage;
            PagingItemsModel.TotalItems = TotalItems;
            PagingItemsModel.TotalPages = TotalPages;
            PagingItemsModel.MaxPages = maxPages;

            return PagingItemsModel;
        }
    }
}