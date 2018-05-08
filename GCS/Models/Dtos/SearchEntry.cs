﻿using System;
using System.ComponentModel.DataAnnotations;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace W3S_GCS.Models.Dtos {
    [PrimaryKey("Id", autoIncrement = true)]
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
        public String TopResultURL { get; set; }
        public String ClickURL { get; set; }
        public String ClickTitle { get; set; }
        public String HTMLSnippet { get; set; }
        public String CorrectedQuery { get; set; }
    }
}