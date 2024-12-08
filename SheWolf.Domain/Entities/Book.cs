using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SheWolf.Domain.Entities
{
    public class Book : BaseEntity
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Title can be maximum 50 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "AuthorId is required")]
        public Guid AuthorId { get; set; }

        public Author Author { get; set; }

        public Book(string title, Guid authorId)
        {
            Title = title;
            AuthorId = authorId;
        }

        public Book() { }
    }
}