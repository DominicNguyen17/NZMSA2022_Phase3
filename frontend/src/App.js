import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Student from './components/Student';
import Signup from './components/Signup';
import Login from './components/Login';

function App() {
  return (
    <Router>
      <Routes>
          <Route path='/signup' element={<Signup />} />
          <Route path='/student' element={<Student />} />
          <Route path='/' element={<Login />} />
      </Routes>
    </Router>
  );
}

export default App;
