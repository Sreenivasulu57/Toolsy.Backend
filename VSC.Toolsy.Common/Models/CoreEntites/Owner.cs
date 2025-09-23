using System.ComponentModel.DataAnnotations;


namespace VSC.Toolsy.Common.Models.CoreEntites
{
    public class Owner
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        //Foreign key to Profile
        public Guid ProfileId { get; set; }

        [StringLength(200)]
        public string? BusinessName { get; set; }

        [StringLength(500)]
        public string? BusinessDescription { get; set; }

        [StringLength(50)]
        public string? BusinessRegistrationNumber { get; set; }

        public Profile? Profile { get; set; }
    }
}
