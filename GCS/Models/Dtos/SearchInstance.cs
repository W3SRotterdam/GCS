using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace W3S_GCS.Models.Dtos {
    public class SearchInstance {
        [Key]
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String DateCreated { get; set; }
        public SearchSettings SearchSettings { get; set; }
        [ForeignKey(name: "SearchSettings")]
        public Int32 SettingsId { get; set; }
    }
}