using System.ComponentModel.DataAnnotations;

namespace SheWolf.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Password must be at least 3 characters")]
        public string Password { get; set; } = string.Empty;
    }
}
