using System;
using System.ComponentModel.DataAnnotations;

namespace ORMIntro.Models
{
    public class Sundae
    {
        [Key]
        public int SundaeId {get;set;}
        public string Name {get;set;}
        public string Flavor {get;set;}
        public int Scoops {get;set;}
        public string Sauce {get;set;}
        public string Topping {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}