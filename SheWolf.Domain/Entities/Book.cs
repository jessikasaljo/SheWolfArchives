using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SheWolf.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string? Title { get; set; }

        [JsonIgnore]
        public Author? Author { get; set; }

        public Book(string title)
        {
            Title = title;
        }

        public Book() { }
    }
}