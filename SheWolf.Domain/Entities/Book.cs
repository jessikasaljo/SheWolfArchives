using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SheWolf.Domain.Entities
{
    public class Book : BaseEntity
    {
        [Required(ErrorMessage = "A title is required.")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "An author is required.")]
        [StringLength(100)]
        public Author Author { get; set; }

        public Book(string title, Author author)
        {
            Title = title;
            Author = author;
        }

        public Book() { }
    }
}