using System.ComponentModel.DataAnnotations;
using VSC.Toolsy.Common.Enums;

namespace VSC.Toolsy.Common.DTOs.Requests
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "First name is required")]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters long")]
        [MaxLength(20, ErrorMessage = "First name must be at least 20 characters long")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters long")]
        [MaxLength(50, ErrorMessage = "Last name must be at least 50 characters long")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [StringLength(10)]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "DateOfBirth required")]
        public required DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender required")]
        public required Gender Gender { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
        [DataType(DataType.Password)]
        public required string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Profile Image is required")]
        [Url(ErrorMessage = "Invalid profile image URL")]
        public required string ProfileImageUrl { get; set; }
    }
}
