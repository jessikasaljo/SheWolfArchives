import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

function BookDetails() {
  const { id } = useParams();
  const [book, setBook] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetch(`http://localhost:5252/books/${id}`)
      .then(response => {
        if (!response.ok) {
          throw new Error('Failed to fetch book details');
        }
        return response.json();
      })
      .then(data => {
        setBook(data.data);
      })
      .catch(error => {
        console.error('Error fetching book details:', error);
        setError(error.message);
      });
  }, [id]);

  if (error) return <div>Error: {error}</div>;
  if (!book) return <div>Loading...</div>;

  const imageUrl = book.picture
    ? `/book_covers/${book.picture}`
    : 'https://upload.wikimedia.org/wikipedia/commons/a/ac/No_image_available.svg';

  return (
    <div className="book-details">
      <div className="book-details-content">
        <div className="book-image">
          <img src={imageUrl} alt={book.title} />
        </div>
        <div className="book-info">
          <h1>{book.title}</h1>
          <h2>by {book.author?.name || 'Unknown author'}</h2>
          <p>{book.description || 'No description available.'}</p>
          <div className="book-metadata">
            <p><strong>ISBN:</strong> {book.isbn || 'N/A'}</p>
            <p><strong>Publication Year:</strong> {book.publicationYear || 'N/A'}</p>
            <p><strong>Pages:</strong> {book.pages || 'N/A'}</p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default BookDetails;