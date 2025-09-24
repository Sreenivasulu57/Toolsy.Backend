using System.ComponentModel.DataAnnotations;

namespace VSC.Toolsy.Common.Models.CoreEntites
{
    public class Owner
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [StringLength(200)]
        public string? BusinessName { get; set; }

        [StringLength(500)]
        public string? BusinessDescription { get; set; }

        [StringLength(50)]
        public string? BusinessRegistrationNumber { get; set; }


        public Profile? Profile { get; set; }

        public required Guid ProfileId { get; set; }
    }
}
