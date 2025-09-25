using System.ComponentModel.DataAnnotations;

namespace VSC.Toolsy.Common.DTOs.Requests
{
    public class ToolImageRequestDto
    {
        [Required(ErrorMessage = "Image URL is required.")]
        [Url(ErrorMessage = "Invalid image URL format.")]
        public string ImageUrl { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Alt text can't exceed 200 characters.")]
        public string? Name { get; set; }

        public bool IsPrimary { get; set; } = false;

    }
}
