﻿namespace SheWolf.Application.DTOs
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
    }
}