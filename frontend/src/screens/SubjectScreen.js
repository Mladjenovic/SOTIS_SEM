import React, { useState, useEffect } from "react";
import { Row, Col, Button, Modal, Form } from "react-bootstrap";
import axios from "axios";
import Subject from "../components/Subject";

function SubjectScreen() {
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const [subjects, setSubjects] = useState([]);
  const [profesors, setProfesors] = useState([]);

  useEffect(() => {
    async function fetchProfesors() {
      const { data } = await axios.get(
        "https://localhost:44393/api/Profesor/ProfesorsVerbose"
      );
      setProfesors(data);
      // console.log(data);
    }
    fetchProfesors();

    async function fetchSubjects() {
      const { data } = await axios.get("https://localhost:44393/api/Subject");
      setSubjects(data);
      // console.log(data);
    }
    fetchSubjects();
  }, []);

  const onFinish = (event) => {
    const config = {
      headers: {
        "Content-type": "application/json",
      },
    };

    const { data } = axios.post(
      "https://localhost:44393/api/Subject",
      {
        title: event.target.elements.title.value,
        description: event.target.elements.description.value,
        name: event.target.elements.name.value,
        minimumPoints: event.target.elements.minimumPoints.value,
        profesorId: event.target.elements.profesorId.value,
      },
      config
    );

    handleClose();
  };

  return (
    <div>
      <div className="header">
        <h1>subjects</h1>
        <Button style={{ margin: 10 }} onClick={handleShow}>
          Create New Subject
        </Button>
      </div>
      <Row>
        {subjects.map((subject) => (
          <Col key={subject.id} sm={12} md={6} lg={4} xl={3}>
            <Subject subject={subject}></Subject>
          </Col>
        ))}
      </Row>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Create new subject</Modal.Title>
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

            <Form.Group className="mb-3" controlId="formBasicName">
              <Form.Label>Name</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter name"
                name="name"
                required
              />
            </Form.Group>

            <Form.Group className="mb-3" controlId="formBasicMinimumPoints">
              <Form.Label>Minimum points</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter min points"
                name="minimumPoints"
                required
              />
            </Form.Group>

            <Form.Group>
              <Form.Label>Profesor</Form.Label>
              <Form.Select
                aria-label="Default select example"
                name="profesorId"
              >
                {profesors.map((profesor) => (
                  <option value={profesor.id}>{profesor.fullname}</option>
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

export default SubjectScreen;
