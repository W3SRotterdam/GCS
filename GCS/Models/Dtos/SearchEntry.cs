using NPoco;
using System;
using System.ComponentModel.DataAnnotations;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace W3S_GCS.Models.Dtos {
    [PrimaryKey("Id", AutoIncrement = true)]
    [TableName("gcs_searchentry")]
    public class SearchEntry {
        [Key]
        [PrimaryKeyColumn(AutoIncrement = true)]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id")]
        public Int32 Id { get; set; }
        public String Query { get; set; }
        public DateTime Date { get; set; }
        public Int32 TotalCount { get; set; }
        public double Timing { get; set; }
        [NullSetting(NullSetting = NullSettings.Null)]
        public String TopResultURL { get; set; }
        [NullSetting(NullSetting = NullSettings.Null)]
        public String ClickURL { get; set; }
        [NullSetting(NullSetting = NullSettings.Null)]
        public String ClickTitle { get; set; }
        [NullSetting(NullSetting = NullSettings.Null)]
        public String HTMLSnippet { get; set; }
        [NullSetting(NullSetting = NullSettings.Null)]
        public String CorrectedQuery { get; set; }
        [Ignore]
        public int Count { get; set; }
    }
}