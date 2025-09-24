using System.ComponentModel.DataAnnotations;

namespace VSC.Toolsy.Common.Models.CoreEntites
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "AddressLine1 cannot exceed than 200 chars")]
        public required string AddressLine1 { get; set; }

        [MaxLength(200, ErrorMessage = "AddressLine1 cannot exceed than 200 chars")]
        public string? AddressLine2 { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "City cannot exceed than 50 chars")]
        public required string City { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Area cannot exceed than 200 chars")]
        public required string Area { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Mandal cannot exceed than 50 chars")]
        public required string Mandal { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "District cannot exceed than 50 chars")]
        public required string District { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "State cannot exceed than 50 chars")]
        public required string State { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "PostalCode cannot exceed than 10 ")]
        public required string PostalCode { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Country cannot exceed than 50 chars")]
        public required string Country { get; set; }


        [Required]
        public Guid ProfileId { get; set; }

    }
}
