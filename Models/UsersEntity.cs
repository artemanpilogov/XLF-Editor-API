using System.ComponentModel.DataAnnotations;

namespace Users.Models
{
    public class UsersEntity
    {
        [Key]
        public string? Email { get; set; }
        public byte[] Password { get; set; }
    }
}