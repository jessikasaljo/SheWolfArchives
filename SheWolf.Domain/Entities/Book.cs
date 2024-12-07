using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SheWolf.Domain.Entities
{
    public class Book : BaseEntity
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Title can be maximum 50 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [JsonIgnore]
        public Author Author { get; set; }
        public Guid AuthorId { get; set; }

        public Book(string title, Author author)
        {
            Title = title;
            Author = author;
            AuthorId = author.Id;
        }

        public Book() { }
    }
}