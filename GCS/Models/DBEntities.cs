using GCS.Models.Dtos;
using System.Data.Entity;

namespace GCS.Models {
    public class DBEntities : DbContext {
        public DBEntities() : base("name=umbracoDbDSN") {

        }
        public virtual DbSet<SearchInstance> SearchInstance { get; set; }
        public virtual DbSet<SearchSettings> SearchSettings { get; set; }
        public virtual DbSet<SearchEntry> SearchEntries { get; set; }
    }
}