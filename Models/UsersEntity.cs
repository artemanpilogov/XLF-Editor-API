using System.ComponentModel.DataAnnotations;

namespace Users.Models
{
    public class UsersEntity
    {
        [Key]
        public string? Email { get; set; }
        
        public byte[] Password { get; set; }

        public DateTime Created_Date { get; set; }

        public DateTime Last_Login_Date { get; set; }
    }
}