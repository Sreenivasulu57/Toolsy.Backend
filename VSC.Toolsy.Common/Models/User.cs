
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VSC.Toolsy.Common.Models
{
    [Table(name: "users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
