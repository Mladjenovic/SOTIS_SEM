import axios from "axios";
import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Row, Col, Button, Modal, Form } from "react-bootstrap";

import Test from "../components/Test";

function TestScreen() {
  const userLogin = useSelector((state) => state.userLogin);
  const { userInfo } = userLogin;

  const [show, setShow] = useState(false);
  const handleShow = () => setShow(true);

  const [tests, setTests] = useState([]);

  useEffect(() => {
    async function fetchTests() {
      const { data } = await axios.get("https://localhost:44393/api/Test");
      setTests(data);
    }
    fetchTests();
  }, []);

  return (
    <div>
      <div className="header">
        {userInfo && userInfo.UserType == "Profesor" ? (
          <h1>All test for profesor: {userInfo.UserName}</h1>
        ) : (
          <div></div>
        )}

        {userInfo && userInfo.UserType == "Profesor" ? (
          <Button style={{ margin: 10 }} onClick={handleShow}>
            Create New Test
          </Button>
        ) : (
          <div></div>
        )}
      </div>

      <Row>
        {tests.map((test) => (
          <Col key={test.id} sm={12} md={6} lg={4} xl={3}>
            <Test test={test}></Test>
          </Col>
        ))}
      </Row>
    </div>
  );
}

export default TestScreen;
