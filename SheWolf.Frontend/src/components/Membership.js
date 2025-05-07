import React from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

function Membership() {
  const { user } = useAuth();
  const navigate = useNavigate();

  if (!user) {
    return (
      <div className="membership-container">
        <h1>Join The She-Wolf Pack</h1>
        <p className="membership-intro">
          Become a member to access our full collection of feminist literature and join our community.
        </p>
        <div className="membership-cta">
          <button onClick={() => navigate('/register')} className="cta-button">
            Sign Up Now
          </button>
          <p>
            Already a member? <button onClick={() => navigate('/login')} className="link-button">Login here</button>
          </p>
        </div>
      </div>
    );
  }

  return (
    <div className="membership-container">
      <h1>Welcome, {user.name}!</h1>
      <div className="membership-status">
        <h2>Your Membership Status</h2>
        <div className="status-card">
          <p><strong>Member Since:</strong> {new Date(user.createdAt).toLocaleDateString()}</p>
          <p><strong>Membership Type:</strong> Standard Member</p>
        </div>
      </div>
      
      <div className="member-benefits">
        <h2>Your Benefits</h2>
        <ul>
          <li>✓ Access to full book catalog</li>
          <li>✓ Exclusive community events</li>
          <li>✓ Monthly feminist book recommendations</li>
          <li>✓ Reading group participation</li>
        </ul>
      </div>

      <div className="upcoming-events">
        <h2>Upcoming Events</h2>
        <div className="event-list">
          <div className="event-card">
            <h3>Monthly Book Club</h3>
            <p>Join us for discussions on feminist literature and theory.</p>
            <p className="event-date">Next meeting: First Thursday of next month</p>
          </div>
          <div className="event-card">
            <h3>Author Talk Series</h3>
            <p>Meet feminist authors and discuss their works.</p>
            <p className="event-date">Check back for upcoming dates</p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Membership;