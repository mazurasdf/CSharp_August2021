using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegsList.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [Display(Name="First name:")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name="Last name:")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string SpiritAnimal {get;set;}
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "must be 8 characters!!!")]
        public string Password { get; set; }
        [NotMapped]
        [Required]
        [Display(Name="Verify password:")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "passwords need to match!!!")]
        public string Confirm { get; set; }
        public List<Listing> Listings {get;set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}