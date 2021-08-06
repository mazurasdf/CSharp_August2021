using System;
using System.ComponentModel.DataAnnotations;

namespace CharacterForm.Models
{
    public class Character
    {
        [Required(ErrorMessage = "you absolutely must have a first name!!")]
        [Display(Name ="First Name")]
        [MinLength(2, ErrorMessage ="You need at least 2 characters in a first name")]
        public string FirstName {get;set;}
        [Required]
        [Display(Name = "Last Name")]
        [MinLength(2)]
        public string LastName {get;set;}
        [Required]
        [NoTimeTravel]
        public DateTime Birthday {get;set;}
        [Required]
        public string Hometown {get;set;}
        [Required]
        public string ImageURL {get;set;}
        [Display(Name = "Favorite Hobby")]
        [MinLength(2)]
        public string FaveHobby {get;set;}
    }

    public class NoTimeTravel : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is DateTime)
            {
                DateTime checkMe;
                checkMe = (DateTime)value;

                if(checkMe > DateTime.Now)
                {
                    return new ValidationResult("no time travel is allowed!");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return new ValidationResult("not a date time");
            }
        }
    }
}