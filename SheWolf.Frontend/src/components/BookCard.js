import React from 'react';
import { useNavigate } from 'react-router-dom';

function BookCard({ book }) {
  const navigate = useNavigate();
  const imageUrl = book.picture
    ? `/book_covers/${book.picture}`
    : 'https://upload.wikimedia.org/wikipedia/commons/a/ac/No_image_available.svg';
  
  const authorName = book.author?.name || 'Unknown author';

  return (
    <div
      className="book-card"
      onClick={() => navigate(`/book/${book.id}`)}
      style={{
        border: '1px solid #ccc',
        padding: '16px',
        marginBottom: '20px',
        borderRadius: '8px',
        width: '200px',
        cursor: 'pointer',
        transition: 'transform 0.2s',
        ':hover': {
          transform: 'scale(1.02)'
        }
      }}
    >
      <img
        src={imageUrl}
        alt={book.title}
        style={{
          width: '100%',
          height: 'auto',
          objectFit: 'cover',
          borderRadius: '8px',
        }}
      />
      <h3>{book.title}</h3>
      <h2>by {authorName}</h2>
    </div>
  );
}

export default BookCard;
