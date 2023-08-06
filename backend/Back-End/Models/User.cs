using System.ComponentModel.DataAnnotations;

namespace Back_End.Models
{
    public class User
    {
        public string? Id { get ; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }    
}
