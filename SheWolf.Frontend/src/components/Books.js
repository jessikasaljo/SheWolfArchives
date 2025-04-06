import { useEffect, useState } from 'react';
import BookCard from './BookCard';

function BookList() {
  const [books, setBooks] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    console.log('Fetching books...');
    fetch('http://localhost:5252/books')
      .then(response => {
        if (!response.ok) {
          throw new Error('Failed to fetch books');
        }
        return response.json();
      })
      .then(data => {
        setBooks(data.data || []);
      })
      .catch(error => {
        console.error('Error fetching books:', error);
        setError(error.message);
      });
  }, []);

  return (
    <div>
      <h1>Books in our library</h1>
      {error && <p>Error: {error}</p>}
      <div style={{ display: 'flex', flexWrap: 'wrap', gap: '20px' }}>
        {books && books.length > 0 ? (
          books.map(book => (
            <BookCard key={book.id} book={book} />
          ))
        ) : (
          <p>No books available</p>
        )}
      </div>
    </div>
  );
}

export default BookList;