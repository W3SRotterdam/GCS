using System;
using System.Linq;
using W3S_GCS.Interfaces;
using W3S_GCS.Models;
using W3S_GCS.Models.Dtos;

namespace W3S_GCS.Repositories {
    public class InstancesRepository : IRepository<SearchInstance> {
        public SearchInstance Get() {
            using (DBEntities db = new DBEntities()) {
                return db.SearchInstance.FirstOrDefault();
            }
        }

        public SearchInstance Set(SearchInstance model) {
            using (DBEntities db = new DBEntities()) {
                return null;
            }
        }
        public SearchInstance Create(SearchInstance obj) {
            throw new NotImplementedException();
        }


        public SearchInstance CreateWithSettingsId(int settingsId) {
            using (DBEntities db = new DBEntities()) {
                SearchInstance searchInstance = new SearchInstance() {
                    Name = "GCS 1",
                    DateCreated = DateTime.Now.ToString(),
                    SettingsId = settingsId
                };
                db.SearchInstance.Add(searchInstance);
                db.SaveChanges();
                return searchInstance;
            }
        }
    }
}