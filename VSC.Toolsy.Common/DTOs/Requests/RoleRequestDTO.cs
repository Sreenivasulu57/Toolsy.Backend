using System.ComponentModel.DataAnnotations;


namespace VSC.Toolsy.Common.DTOs.Requests
{
    public class RoleRequestDTO
    {
        [Required(ErrorMessage = "The Name Field Is Required")]
        [MaxLength(50, ErrorMessage = "The Name Field Should Not Exceed More Than 50")]
        [MinLength(2, ErrorMessage = "The Name Field Should Not Less Than 3")]
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
