using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSC.Toolsy.Common
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
