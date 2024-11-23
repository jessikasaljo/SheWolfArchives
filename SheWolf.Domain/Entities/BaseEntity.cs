using System.ComponentModel.DataAnnotations;

namespace SheWolf.Domain.Entities
{
    public class BaseEntity
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}