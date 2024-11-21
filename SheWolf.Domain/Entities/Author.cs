using System.ComponentModel.DataAnnotations;

namespace SheWolf.Domain.Entities
{
    public class Author : BaseEntity
    {
        [Required(ErrorMessage = "The author's name is required.")]
        [StringLength(100)]
        [Display(Name = "Author Name")]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}