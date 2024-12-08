namespace SheWolf.Application.DTOs
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> BookTitles { get; set; }

    }
}
