import React from "react";
import Home from "./components/Home";
import './App.css';

function App() {
  return (
    <div className="App">
      <Home />
      <img src={`${process.env.PUBLIC_URL}/wolf.png`} className="App-logo" alt="Female Symbol" />
    </div>
  );
}

export default App;
