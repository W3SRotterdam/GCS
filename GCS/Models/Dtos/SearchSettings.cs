using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;
using W3S_GCS.Converters;

namespace W3S_GCS.Models.Dtos {
    [PrimaryKey("Id", autoIncrement = true)]
    [TableName("gcs_searchsettings")]
    public class SearchSettings {
        [Key]
        [PrimaryKeyColumn(AutoIncrement = true)]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id")]
        public Int32 Id { get; set; }
        [NotMapped]
        [JsonIgnore]
        [Umbraco.Core.Persistence.Ignore]
        public String CurrentURL { get; set; }
        public String BaseURL { get; set; }
        public String CXKey { get; set; }
        public String APIKey { get; set; }
        public Int32 ItemsPerPage { get; set; }
        [NotMapped]
        [JsonIgnore]
        [Umbraco.Core.Persistence.Ignore]
        public String RedirectNodeURL { get; set; }
        public String RedirectAlias { get; set; }
        public String LoadMoreSetUp { get; set; }
        public Int32 MaxPaginationPages { get; set; }

        [JsonConverter(typeof(BooleanConverter))]
        public Boolean ShowQuery { get; set; }
        [JsonConverter(typeof(BooleanConverter))]
        public Boolean ShowTiming { get; set; }
        [JsonConverter(typeof(BooleanConverter))]
        public Boolean ShowTotalCount { get; set; }
        [JsonConverter(typeof(BooleanConverter))]
        public Boolean ShowSpelling { get; set; }
        [JsonConverter(typeof(BooleanConverter))]
        public Boolean KeepQuery { get; set; }
        [JsonConverter(typeof(BooleanConverter))]
        public Boolean ShowFilterFileType { get; set; }
        public String ExcludeTerms { get; set; }
        public String ExcludeNodeIds { get; set; }
        public DateTime? DateRestrict { get; set; }
        [JsonConverter(typeof(BooleanConverter))]
        public Boolean ShowThumbnail { get; set; }
        public String ThumbnailFallbackGUID { get; set; }
        public String LoadIconGUID { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
        public String DevelopmentURL { get; set; }
    }
}