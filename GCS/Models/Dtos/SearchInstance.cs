using NPoco;
using System;
using System.ComponentModel.DataAnnotations;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace W3S_GCS.Models.Dtos {
    [PrimaryKey("Id", AutoIncrement = true)]
    [TableName("gcs_searchinstance")]
    public class SearchInstance {
        [Key]
        [PrimaryKeyColumn(AutoIncrement = true)]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id")]
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String DateCreated { get; set; }
        [ForeignKey(typeof(SearchSettings))]
        public Int32 SettingsId { get; set; }
    }
}