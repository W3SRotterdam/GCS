using System;
using System.ComponentModel.DataAnnotations;

namespace W3S_GCS.Models.Dtos {
    public class SearchEntry {
        [Key]
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