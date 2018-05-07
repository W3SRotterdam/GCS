using System.Data.Entity;
using W3S_GCS.Models.Dtos;

namespace W3S_GCS.Models {
    public class DBEntities : DbContext {
        public DBEntities() : base("name=umbracoDbDSN") {

        }
        public virtual DbSet<SearchInstance> SearchInstance { get; set; }
        public virtual DbSet<SearchSettings> SearchSettings { get; set; }
        public virtual DbSet<SearchEntry> SearchEntries { get; set; }
    }
}