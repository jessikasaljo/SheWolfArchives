import React from 'react';

function BookCard({ book }) {
    const imageUrl = book.picture
    ? `/book_covers/${book.picture}`
    : 'https://upload.wikimedia.org/wikipedia/commons/a/ac/No_image_available.svg';
  
  const authorName = book.author?.name || 'Unknown author';

  return (
    <div
      className="book-card"
      style={{
        border: '1px solid #ccc',
        padding: '16px',
        marginBottom: '20px',
        borderRadius: '8px',
        width: '200px',
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
