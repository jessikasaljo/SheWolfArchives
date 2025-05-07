import React from "react";
import { BrowserRouter as Router, Route, Link, Routes } from "react-router-dom";
import { AuthProvider, useAuth } from "./contexts/AuthContext";
import Home from "./components/Home";
import Books from "./components/Books";
import BookDetails from "./components/BookDetails";
import Login from "./components/Login";
import Register from "./components/Register";
import './App.css';

function Membership() {
  return <h2>Membership Page</h2>;
}

function About() {
  return <h2>About Page</h2>;
}

function Navigation() {
  const { user, logout } = useAuth();

  return (
    <nav>
      <ul>
        <li><Link to="/">Home</Link></li>
        <li><Link to="/books">Books</Link></li>
        <li><Link to="/membership">Membership</Link></li>
        <li><Link to="/about">About</Link></li>
        {user ? (
          <li><button onClick={logout}>Logout</button></li>
        ) : (
          <>
            <li><Link to="/login">Login</Link></li>
            <li><Link to="/register">Register</Link></li>
          </>
        )}
      </ul>
    </nav>
  );
}

function AppContent() {
  return (
    <div className="App">
      <Navigation />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/books" element={<Books />} />
        <Route path="/book/:id" element={<BookDetails />} />
        <Route path="/membership" element={<Membership />} />
        <Route path="/about" element={<About />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
      </Routes>
    </div>
  );
}

function App() {
  return (
    <AuthProvider>
      <Router>
        <AppContent />
      </Router>
    </AuthProvider>
  );
}

export default App;