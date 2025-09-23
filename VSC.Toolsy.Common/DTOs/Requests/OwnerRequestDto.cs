using System.ComponentModel.DataAnnotations;

namespace VSC.Toolsy.Common.DTOs.Requests
{
    public class OwnerRequestDto : RegisterUserDto
    {
        [MaxLength(200, ErrorMessage = "The business name cannot exceed 200 characters.")]
        public string? BusinessName { get; set; }

        [MaxLength(500, ErrorMessage = "The business description cannot exceed 500 characters.")]
        public string? BusinessDescription { get; set; }

        [MaxLength(50, ErrorMessage = "The business registration number cannot exceed 50 characters.")]
        public string? BusinessRegistrationNumber { get; set; }
    }
}
