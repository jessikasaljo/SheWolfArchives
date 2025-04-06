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

        public string Picture { get; set; } = "https://upload.wikimedia.org/wikipedia/commons/a/ac/No_image_available.svg";

        public Book(string title, Guid authorId, string picture = "https://upload.wikimedia.org/wikipedia/commons/a/ac/No_image_available.svg")
        {
            Title = title;
            AuthorId = authorId;
            Picture = picture;
        }

        public Book() { }
    }
}