using System;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using W3S_GCS.Interfaces;
using W3S_GCS.Models.Dtos;

namespace W3S_GCS.Repositories {
    public class SettingsRepository : IRepository<SearchSettings> {
        static UmbracoDatabase _umDb {
            get {
                return ApplicationContext.Current.DatabaseContext.Database;
            }
        }

        public SearchSettings Get() {
            try {
                return _umDb.Query<SearchSettings>("SELECT * FROM [gcs_searchsettings]").FirstOrDefault();
            } catch (Exception ex) {
                LogHelper.Error(System.Reflection.MethodBase.GetCurrentMethod().GetType(), "GCS Error Get settings", ex);
                return null;
            }

            //using (DBEntities db = new DBEntities()) {
            //    return db.SearchSettings.FirstOrDefault();
            //}
        }

        public SearchSettings Create(SearchSettings obj) {
            throw new NotImplementedException();
            //using (DBEntities db = new DBEntities()) {
            //    SearchSettings SearchSettings = new SearchSettings();
            //    db.SearchSettings.Add(SearchSettings);
            //    SearchSettings.DateCreated = DateTime.Now;
            //    db.SaveChanges();
            //    return SearchSettings;
            //}
        }

        public SearchSettings Set(SearchSettings model) {
            model.LastUpdated = DateTime.Now;
            model.ItemsPerPage = model.ItemsPerPage > 0 ? model.ItemsPerPage : 1;
            _umDb.Update(model);

            return model;
            //using (DBEntities db = new DBEntities()) {
            //    SearchSettings settings = db.SearchSettings.Attach(model);
            //    var entry = db.Entry(model);
            //    entry.Entity.ItemsPerPage = entry.Entity.ItemsPerPage > 0 ? entry.Entity.ItemsPerPage : 1;
            //    entry.Entity.LastUpdated = DateTime.Now;
            //    entry.State = System.Data.Entity.EntityState.Modified;
            //    db.SaveChanges();
            //    return settings;
            //}
        }
    }
}