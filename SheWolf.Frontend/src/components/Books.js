import React, { useState, useEffect } from 'react';
import BookCard from './BookCard';

function Books() {
  const [books, setBooks] = useState([]);
  const [error, setError] = useState(null);
  const [searchTerm, setSearchTerm] = useState('');
  const [authorFilter, setAuthorFilter] = useState('');
  const [yearFilter, setYearFilter] = useState('');
  const [authors, setAuthors] = useState([]);

  useEffect(() => {
    // Fetch books
    fetch('http://localhost:5252/books')
      .then(response => {
        if (!response.ok) throw new Error('Failed to fetch books');
        return response.json();
      })
      .then(data => {
        setBooks(data.data || []);
      })
      .catch(error => {
        console.error('Error fetching books:', error);
        setError(error.message);
      });

    // Fetch authors for filter
    fetch('http://localhost:5252/authors')
      .then(response => {
        if (!response.ok) throw new Error('Failed to fetch authors');
        return response.json();
      })
      .then(data => {
        setAuthors(data.data || []);
      })
      .catch(error => {
        console.error('Error fetching authors:', error);
      });
  }, []);

  const filteredBooks = books.filter(book => {
    const matchesSearch = book.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         book.author?.name.toLowerCase().includes(searchTerm.toLowerCase());
    const matchesAuthor = !authorFilter || book.author?.id === authorFilter;
    const matchesYear = !yearFilter || book.publicationYear === parseInt(yearFilter);
    return matchesSearch && matchesAuthor && matchesYear;
  });

  const years = [...new Set(books.map(book => book.publicationYear))].filter(Boolean).sort();

  return (
    <div className="books-container">
      <h1>Books in our library</h1>
      
      <div className="search-filter-container">
        <div className="search-bar">
          <input
            type="text"
            placeholder="Search by title or author..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </div>
        
        <div className="filters">
          <select
            value={authorFilter}
            onChange={(e) => setAuthorFilter(e.target.value)}
          >
            <option value="">All Authors</option>
            {authors.map(author => (
              <option key={author.id} value={author.id}>
                {author.name}
              </option>
            ))}
          </select>

          <select
            value={yearFilter}
            onChange={(e) => setYearFilter(e.target.value)}
          >
            <option value="">All Years</option>
            {years.map(year => (
              <option key={year} value={year}>
                {year}
              </option>
            ))}
          </select>
        </div>
      </div>

      {error && <p className="error-message">Error: {error}</p>}
      
      <div className="books-grid">
        {filteredBooks.length > 0 ? (
          filteredBooks.map(book => (
            <BookCard key={book.id} book={book} />
          ))
        ) : (
          <p>No books found matching your criteria</p>
        )}
      </div>
    </div>
  );
}

export default Books;