using System.ComponentModel.DataAnnotations;

namespace Hangman.Models
{
    public class Guess
    {
        [Required(ErrorMessage ="you must include a guess! a man's life is at stake!")]
        [MaxLength(1)]
        [MinLength(1)]
        public string Letter {get;set;}
    }
}