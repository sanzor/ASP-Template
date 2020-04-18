using ASPT.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace ASPT.Models {
    public class User:IIdentifiable {
        [Key]
        public string Id { get; set; }

        object IIdentifiable.Id => this.Id;
    }
}
