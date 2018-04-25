using GCS.Converters;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCS.Models.Dtos {
    public class SearchSettings {
        [Key]
        public Int32 Id { get; set; }
        [NotMapped]
        [JsonIgnore]
        public String CurrentURL { get; set; }
        public String BaseURL { get; set; }
        public String CXKey { get; set; }
        public String APIKey { get; set; }
        public Int32 ItemsPerPage { get; set; }
        [NotMapped]
        [JsonIgnore]
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
        public String DocumentTypeFilter { get; set; }
        public DateTime? DateRestrict { get; set; }
        [JsonConverter(typeof(BooleanConverter))]
        public Boolean ShowThumbnail { get; set; }
        public String ThumbnailFallbackGUID { get; set; }
        public String LoadIconGUID { get; set; }
        public String FilterSetupFileType { get; set; }
        public String FilterSetupDocType { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
        public String DevelopmentURL { get; set; }
    }
}