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
  const handleClose = () => setShow(false);

  const [tests, setTests] = useState([]);
  const [profesors, setProfesors] = useState([]);
  const [subjects, setSubjects] = useState([]);
  const [currentProfesor, setCurrentProfesor] = useState([]);

  useEffect(() => {
    async function fetchTests() {
      const { data } = await axios.get("https://localhost:44393/api/Test");
      setTests(data);
    }
    fetchTests();

    async function fetchProfesors() {
      const { data } = await axios.get(
        "https://localhost:44393/api/Profesor/ProfesorsVerbose"
      );
      setProfesors(data);
    }
    fetchProfesors();

    async function fetchSubjects() {
      const { data } = await axios.get("https://localhost:44393/api/Subject");
      setSubjects(data);
    }
    fetchSubjects();

    setCurrentProfesor(profesors.find((x) => x.username == userInfo.UserName));
  }, []);

  const onFinish = (event) => {
    const config = {
      headers: {
        "Content-type": "application/json",
      },
    };

    const { data } = axios.post(
      "https://localhost:44393/api/Test",
      {
        title: event.target.elements.title.value,
        description: event.target.elements.description.value,
        minimumPoints: event.target.elements.minimumPoints.value,
        profesorId: currentProfesor.id,
        subjectId: event.target.subjectId.value,
      },
      config
    );
    handleClose();
  };

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

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Create new test</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form onSubmit={onFinish}>
            <Form.Group className="mb-3" controlId="formBasicTitle">
              <Form.Label>Title</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter title"
                name="title"
                required
              />
            </Form.Group>

            <Form.Group className="mb-3" controlId="formBasicDescription">
              <Form.Label>Description</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter description"
                name="description"
                required
              />
            </Form.Group>

            <Form.Group className="mb-3" controlId="formBasicMinimumPoints">
              <Form.Label>Minimum points</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter minimum number of passing the test"
                name="minimumPoints"
                required
              />
            </Form.Group>

            <Form.Group>
              <Form.Label>Subject ID</Form.Label>
              <Form.Select aria-label="Default select example" name="subjectId">
                {subjects.map((subject) => (
                  <option value={subject.id}>{subject.name}</option>
                ))}
              </Form.Select>
            </Form.Group>
            <Button type="primary" htmlType="submit">
              Submit
            </Button>
          </Form>
        </Modal.Body>
      </Modal>
    </div>
  );
}

export default TestScreen;
