import React, { useEffect, useState } from 'react';

function Books() {
  const [books, setBooks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetch('https://your-api-endpoint.com/api/books') // Replace with your API URL
      .then((response) => {
        if (!response.ok) {
          throw new Error('Failed to fetch books');
        }
        return response.json();
      })
      .then((data) => {
        setBooks(data);  // Assuming the API returns a list of books
        setLoading(false);
      })
      .catch((err) => {
        setError(err.message);
        setLoading(false);
      });
  }, []); // Empty dependency array ensures this runs once when the component mounts

  if (loading) {
    return <div>Loading books...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }

  return (
    <div>
      <h1>Books</h1>
      <ul>
        {books.map((book) => (
          <li key={book.id}>
            <h2>{book.title}</h2>
            <p>Author: {book.author}</p>
            <p>ISBN: {book.isbn}</p>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default Books;
