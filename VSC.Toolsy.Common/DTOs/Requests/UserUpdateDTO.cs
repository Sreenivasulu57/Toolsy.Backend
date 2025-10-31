using System.ComponentModel.DataAnnotations;
using VSC.Toolsy.Common.Enums;

namespace VSC.Toolsy.Common.DTOs.Requests
{
    public class UserUpdateDTO
    {
        [Required(ErrorMessage = "First name is required")]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters long")]
        [MaxLength(20, ErrorMessage = "First name must be at least 20 characters long")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters long")]
        [MaxLength(50, ErrorMessage = "Last name must be at least 50 characters long")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [MinLength(10, ErrorMessage = "Phone number must be 10")]
        [MaxLength(10, ErrorMessage = "Phone number must be 10")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "DateOfBirth required")]
        public required DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender required")]
        public required Gender Gender { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Url(ErrorMessage = "Invalid profile image URL")]
        public string? ProfileImageUrl { get; set; }
    }
}
