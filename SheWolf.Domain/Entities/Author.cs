using System.ComponentModel.DataAnnotations;

namespace SheWolf.Domain.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}