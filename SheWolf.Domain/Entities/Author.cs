using System.ComponentModel.DataAnnotations;

namespace SheWolf.Domain.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }

        public Author(string name, ICollection<Book> books)
        {
            Name = name;
            Books = books;
        }

        public Author() { }
    }
}