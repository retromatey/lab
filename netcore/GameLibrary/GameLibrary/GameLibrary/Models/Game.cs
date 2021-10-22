using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameLibrary.Models
{
    public class Game : IValidatableObject
    {
        public int GameId { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public Genre Genre { get; set; }
        
        [Required]
        [Display(Name = "Publish Year")]
        public int PublishYear { get; set; }
        
        [Required]
        public string Publisher { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();

            if (Genre == Genre.Unknown)
            {
                result.Add(new ValidationResult("Genre cannot be Unknown", new string[] { "Genre" }));
            }

            var currentYear = DateTime.Now.Year;

            if (PublishYear > currentYear)
            {
                result.Add(new ValidationResult($"The publish year cannot be after {currentYear}", new string[] { "PublishYear" }));
            }

            if (PublishYear < 1970)
            {
                result.Add(new ValidationResult("The publish year cannot be before 1970", new string[] { "PublishYear" }));
            }
            
            return result;
        }
    }
}