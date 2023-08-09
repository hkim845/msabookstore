'import { Navigate } from "react-router-dom";
import "./App.css";
import Home from "./Home";

function App() {
  const isAuthenticated = !!localStorage.getItem("token");

  return (
    <>
      {isAuthenticated && <Home></Home>}
      {!isAuthenticated && <Navigate replace to="/login" />}
    </>
  );
}

export default App;
