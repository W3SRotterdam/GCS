using System;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using W3S_GCS.Interfaces;
using W3S_GCS.Models.Dtos;

namespace W3S_GCS.Repositories {
    public class InstancesRepository : IRepository<SearchInstance> {
        static UmbracoDatabase _umDb {
            get {
                return ApplicationContext.Current.DatabaseContext.Database;
            }
        }

        public SearchInstance Get() {
            return _umDb.Query<SearchInstance>("SELECT * FROM gcs_searchinstances").FirstOrDefault();
            //using (DBEntities db = new DBEntities()) {
            //    return db.SearchInstance.FirstOrDefault();
            //}
        }

        public SearchInstance Set(SearchInstance model) {
            throw new NotImplementedException();
            //using (DBEntities db = new DBEntities()) {
            //    return null;
            //}
        }

        public SearchInstance Create(SearchInstance obj) {
            throw new NotImplementedException();
            //using (DBEntities db = new DBEntities()) {
            //    return null;
            //}
        }
    }
}