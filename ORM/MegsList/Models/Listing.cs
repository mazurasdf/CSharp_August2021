using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegsList.Models
{
    public class Listing
    {
        [Key]
        public int ListingId {get;set;}
        //item name, asking price, location, description, isSold
        [Required]
        [MinLength(3)]
        public string Name {get;set;}
        [Required]
        [Range(0, 10000)]
        public int Price {get;set;}
        [Required]
        public string Location {get;set;}
        [Required]
        public string Description {get;set;}
        [Required]
        public bool IsSold {get;set;} = false;
        [Required]
        public int UserId {get;set;}
        public User Seller {get;set;}
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}