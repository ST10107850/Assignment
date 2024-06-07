using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class SurveyFormData
    {
        // Required field for the user's full name
        [Required(ErrorMessage = "Please enter your full name.")]
        public string FullName { get; set; }

        // Required field for the user's email address, with email format validation
        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        // Required field for the user's date of birth, with date format validation
        [Required(ErrorMessage = "Please enter your date of birth.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        // Required field for the user's contact number, with phone number format validation
        [Required(ErrorMessage = "Please enter your contact number.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string ContactNumber { get; set; }

        // Required field for the user's favorite food selection
        [Required(ErrorMessage = "Please select your favorite food.")]
        public string FavoriteFoods { get; set; }

        // Required field for rating the user's liking for movies, with range validation
        [Required(ErrorMessage = "Please rate your liking for movies.")]
        [Range(1, 5, ErrorMessage = "Please select a rating between 1 and 5.")]
        public int movies { get; set; }

        // Required field for rating the user's liking for radio, with range validation
        [Required(ErrorMessage = "Please rate your liking for radio.")]
        [Range(1, 5, ErrorMessage = "Please select a rating between 1 and 5.")]
        public int radio { get; set; }

        // Required field for rating the user's liking for eating out, with range validation
        [Required(ErrorMessage = "Please rate your liking for eating out.")]
        [Range(1, 5, ErrorMessage = "Please select a rating between 1 and 5.")]
        public int eat_out { get; set; }

        // Required field for rating the user's liking for watching TV, with range validation
        [Required(ErrorMessage = "Please rate your liking for watching TV.")]
        [Range(1, 5, ErrorMessage = "Please select a rating between 1 and 5.")]
        public int tv { get; set; }
    }
}
