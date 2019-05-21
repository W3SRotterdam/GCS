using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Core.Scoping;
using W3S_GCS.Interfaces;
using W3S_GCS.Models.Dtos;

namespace W3S_GCS.Repositories {
    public class QueriesRepository : IRepository<SearchEntry> {
        
        private IScopeProvider _scopeProvider;

        public SearchEntry Get() {
            throw new NotImplementedException();
            //using (DBEntities db = new DBEntities()) {
            //    return null;
            //}
        }

        public SearchEntry Create(SearchEntry entry) {
            using (var scope = _scopeProvider.CreateScope()) {
                if (!String.IsNullOrEmpty(entry.Query)) {
                    scope.Database.Save<SearchEntry>(entry);
                }
            }

            return entry;
            //using (DBEntities db = new DBEntities()) {
            //    SearchEntry SearchEntry = db.SearchEntries.Add(entry);
            //    db.SaveChanges();
            //    return SearchEntry;
            //}
        }

        public void UpdateClick(SearchEntry model) {
            using (var scope = _scopeProvider.CreateScope()) {
                scope.Database.Execute("UPDATE gcs_searchentry SET ClickTitle = @0, ClickURL = @1 WHERE Id = @2", model.ClickTitle, model.ClickURL, model.Id);
            }

            //using (DBEntities db = new DBEntities()) {
            //    db.SearchEntries.Attach(model);
            //    var entry = db.Entry(model);
            //    entry.Property(p => p.ClickTitle).IsModified = true;
            //    entry.Property(p => p.ClickURL).IsModified = true;
            //    db.SaveChanges();
            //}
        }

        public SearchEntry Set(SearchEntry obj) {
            throw new NotImplementedException();
            //using (DBEntities db = new DBEntities()) {
            //    return null;
            //}
        }

        public Dictionary<String, Int32> GetTopQueries(int year = -1, int month = -1) {
            List<SearchEntryStat> entries = new List<SearchEntryStat>();

            using (var scope = _scopeProvider.CreateScope()) {
                if (year != -1 && month != -1) {
                    entries = scope.Database.Query<SearchEntryStat>(@"SELECT Query AS Name, COUNT(*) as count FROM gcs_searchentry WHERE Query != '' AND DATEPART(year, Date) = @0 AND DATEPART(month, Date) = @1 GROUP BY Query ORDER BY count DESC", year, month).Take(10).ToList();
                }
            }

            return entries != null && entries.Count > 0 ? entries.ToDictionary(e => e.Name, e => e.Count) : null;

            //using (DBEntities db = new DBEntities()) {
            //    //return GetTopValues("Query", 10);
            //    return year != -1 && month != -1 && db.SearchEntries != null && db.SearchEntries.Where(e => !String.IsNullOrEmpty(e.Query)) != null ?
            //         db.SearchEntries
            //        .Where(e => !String.IsNullOrEmpty(e.Query))
            //        .FilterByDate(year, month)
            //        .GroupBy(e => e.Query)
            //        .OrderByDescending(g => g.Count())
            //        .Take(10)
            //        .ToDictionary(e => e.First().Query, e => e.Count()) : null;
            //}
        }

        public Dictionary<String, Int32> GetTopSpelling(int year = -1, int month = -1) {
            List<SearchEntryStat> entries = new List<SearchEntryStat>();

            if (year != -1 && month != -1) {
                using (var scope = _scopeProvider.CreateScope()) {
                    entries = scope.Database.Query<SearchEntryStat>(@"SELECT CorrectedQuery AS Name, COUNT(*) as count FROM gcs_searchentry WHERE CorrectedQuery != '' AND DATEPART(year, Date) = @0 AND DATEPART(month, Date) = @1 GROUP BY CorrectedQuery ORDER BY count DESC", year, month).Take(10).ToList();
                }
            }

            return entries != null && entries.Count > 0 ? entries.ToDictionary(e => e.Name, e => e.Count) : null;

            //using (DBEntities db = new DBEntities()) {
            //    //return GetTopValues("CorrectedQuery", 10);
            //    return year != -1 && month != -1 && db.SearchEntries != null && db.SearchEntries.Where(e => !String.IsNullOrEmpty(e.CorrectedQuery)) != null ?
            //        db.SearchEntries
            //       .Where(e => !String.IsNullOrEmpty(e.CorrectedQuery))
            //       .FilterByDate(year, month)
            //       .GroupBy(e => e.Query)
            //       .OrderByDescending(g => g.Count())
            //       .Take(10)
            //       .ToDictionary(e => e.First().Query, e => e.Count()) : null;
            //}
        }

        public Dictionary<String, Int32> GetTopClicks(int year = -1, int month = -1) {
            List<SearchEntryStat> entries = new List<SearchEntryStat>();

            if (year != -1 && month != -1) {
                using (var scope = _scopeProvider.CreateScope()) {
                    entries = scope.Database.Query<SearchEntryStat>(@"SELECT ClickURL AS Name, COUNT(*) as count FROM gcs_searchentry WHERE ClickURL != '' AND DATEPART(year, Date) = @0 AND DATEPART(month, Date) = @1 GROUP BY ClickURL ORDER BY count DESC", year, month).Take(10).ToList();
                }
            }

            return entries != null && entries.Count > 0 ? entries.ToDictionary(e => e.Name, e => e.Count) : null;

            //using (DBEntities db = new DBEntities()) {
            //    //return GetTopValues("ClickURL", 10);
            //    return year != -1 && month != -1 && db.SearchEntries != null && db.SearchEntries.Where(e => !String.IsNullOrEmpty(e.ClickURL)) != null ?
            //        db.SearchEntries
            //       .Where(e => !String.IsNullOrEmpty(e.ClickURL))
            //       .FilterByDate(year, month)
            //       .GroupBy(e => e.ClickURL)
            //       .OrderByDescending(g => g.Count())
            //       .Take(10)
            //       .ToDictionary(e => e.First().ClickURL, e => e != null ? e.Count() : 0) : null;
            //}

        }

        public String GetAverageLoad(int year = -1, int month = -1) {
            //using (DBEntities db = new DBEntities()) {
            //    var entries = db.SearchEntries
            //        .Where(e => e.Date == null ? true : year > -1 ? e.Date.Year == year : true && month > -1 ? e.Date.Month == month : true);
            //    var sumTiming = entries != null && entries.Count() > 0 ? entries.Select(e => e.Timing).Sum() : 0;
            //    return sumTiming != 0 ? Math.Round((sumTiming / db.SearchEntries.Count()), 2).ToString() : "*";
            //}
            return null;

        }

        private String GetTopValues(String alias, Int32 limit) {
            //using (DBEntities db = new DBEntities()) {
            //    var obj = new SearchEntry();
            //    object property = obj.GetType().GetProperty(alias).GetValue(obj, null);

            //    return String.Join(",", db.SearchEntries
            //        .GroupBy(e => property)
            //        .OrderByDescending(g => g.Count())
            //        .Select(e => e.FirstOrDefault().Query)
            //        .Take(limit));
            //}
            return null;

        }
    }
}