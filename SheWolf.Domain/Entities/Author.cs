using System.ComponentModel.DataAnnotations;

namespace SheWolf.Domain.Entities
{
    public class Author : BaseEntity
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name can be maximum 50 characters")]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }

        public Author(string name, ICollection<Book> books = null)
        {
            Name = name;
            Books = books ?? new List<Book>();
        }

        public Author() { }
    }
}