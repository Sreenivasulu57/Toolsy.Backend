using System.ComponentModel.DataAnnotations;

namespace VSC.Toolsy.Common.DTOs.Requests
{
    public class RoleUpdateRequestDTO
    {
        [Required(ErrorMessage = "RoleId Field Is Required")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "The Name Field Is Required")]
        [MaxLength(50, ErrorMessage = "The Name Field Should Not Exceed More Than 50")]
        [MinLength(2, ErrorMessage = "The Name Field Should Not Less Than 3")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

    }
}
