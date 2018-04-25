using GCS.Interfaces;
using GCS.Models;
using GCS.Models.Dtos;
using System;
using System.Linq;

namespace GCS.Repositories {
    public class SettingsRepository : IRepository<SearchSettings> {
        public SearchSettings Get() {
            using (DBEntities db = new DBEntities()) {
                return db.SearchSettings.FirstOrDefault();
            }
        }

        public SearchSettings Create() {
            using (DBEntities db = new DBEntities()) {
                SearchSettings SearchSettings = new SearchSettings();
                db.SearchSettings.Add(SearchSettings);
                SearchSettings.DateCreated = DateTime.Now;
                db.SaveChanges();
                return SearchSettings;
            }
        }

        public SearchSettings Set(SearchSettings model) {
            using (DBEntities db = new DBEntities()) {
                SearchSettings settings = db.SearchSettings.Attach(model);
                var entry = db.Entry(model);
                entry.Entity.ItemsPerPage = entry.Entity.ItemsPerPage > 0 ? entry.Entity.ItemsPerPage : 1;
                entry.Entity.LastUpdated = DateTime.Now;
                entry.State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return settings;
            }
        }

        SearchSettings IRepository<SearchSettings>.Set(SearchSettings obj) {
            throw new NotImplementedException();
        }

        public SearchSettings Create(SearchSettings obj) {
            throw new NotImplementedException();
        }
    }
}