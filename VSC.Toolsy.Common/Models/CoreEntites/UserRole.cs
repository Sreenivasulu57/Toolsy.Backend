using System.ComponentModel.DataAnnotations;
using VSC.Toolsy.Common.Models.BaseEntites;

namespace VSC.Toolsy.Common.Models.CoreEntites
{
    public class UserRole : BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        public  User User { get; set; } = null!;

        [Required]
        public int RoleId { get; set; }
        [Required]
        public Role Role { get; set; } = null!;

        public bool IsActive { get; set; } = true;


    }
}
