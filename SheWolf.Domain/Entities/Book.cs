using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SheWolf.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public Author Author { get; set; }

        public Book(string title, Author author)
        {
            Title = title;
            Author = author;
        }

        public Book() { }
    }
}