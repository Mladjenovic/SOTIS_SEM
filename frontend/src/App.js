import { BrowserRouter as Router, Route } from "react-router-dom";

import "antd/dist/antd.css";

import CustomLayout from "./containers/Layout";

function App() {
  return (
    <Router>
      <CustomLayout></CustomLayout>
    </Router>
  );
}

export default App;
