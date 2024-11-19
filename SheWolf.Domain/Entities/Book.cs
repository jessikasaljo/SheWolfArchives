using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SheWolf.Domain.Entities
{
    public class Book : BaseEntity
    {
        [Required(ErrorMessage = "A title is required.")]
        [StringLength(100)]
        public string Title { get; set; }

        [ForeignKey("AuthorId")]
        public Guid AuthorId { get; set; }

        [Required(ErrorMessage = "An author is required.")]
        [StringLength(100)]
        public Author Author { get; set; }

        public Book(string title, Guid authorId, Author author)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("A title is required.", nameof(title));

            if (author == null)
                throw new ArgumentNullException(nameof(author), "An author is required.");

            Title = title;
            AuthorId = authorId;
            Author = author;
        }

        public Book() { }
    }
}