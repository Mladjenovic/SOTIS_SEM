import axios from "axios";
import React, { useState, useEffect } from "react";
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import { Button, Table, Modal, Form } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";

function TestDetailScreen({ match }) {
  const userLogin = useSelector((state) => state.userLogin);
  const { userInfo } = userLogin;

  const [test, setTest] = useState([]);

  const [show, setShow] = useState(false);
  const [showAddNewSection, setShowAddNewSection] = useState(false);

  // Section handles
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const handleShowAddNewSection = () => setShowAddNewSection(true);
  const handleCloseShowAddNewSection = () => setShowAddNewSection(false);

  const [sectionsRelatedToTest, setSectionsRelatedToTest] = useState([]);

  useEffect(() => {
    async function fetchTest() {
      const { data } = await axios.get(
        `https://localhost:44393/api/Test/${match.params.id}`
      );
      setTest(data);
    }
    fetchTest();

    async function fetchSectionsRealtedToTest(testId) {
      const { data } = await axios.get(
        `https://localhost:44393/api/Section/SectionRelatedToTest/${match.params.id}`
      );
      setSectionsRelatedToTest(data);
      console.log("AAAAAAAAAAAA", data);
    }
    fetchSectionsRealtedToTest();
  }, []);

  return (
    <div>
      <Link to="/tests/">
        <Button>Go back</Button>
      </Link>
      <Table striped bordered hover>
        <tbody>
          <tr>
            <td>Id</td>
            <td>{test.id}</td>
          </tr>
          <tr>
            <td>Title</td>

            <td>{test.title}</td>
          </tr>
          <tr>
            <td>Description</td>

            <td>{test.description}</td>
          </tr>
          <tr>
            <td>Profesor id</td>
            <td>{test.profesorId}</td>
          </tr>

          <tr>
            <td>Sections</td>
            <td>
              <Button style={{ marginRight: 5 }} onClick={handleShow}>
                View All
              </Button>
              {userInfo && userInfo.UserType != "Student" ? (
                <Button onClick={handleShowAddNewSection}>New</Button>
              ) : (
                <div></div>
              )}
            </td>
          </tr>
        </tbody>
      </Table>

      {/* Sections all */}

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>
            All sections for test({test.id}): {test.title}
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Table striped bordered hover>
            <thead>
              <tr>
                <th>Id</th>
                <th>Name</th>
                <th>Test id</th>
              </tr>
            </thead>
            <tbody>
              {sectionsRelatedToTest.map((section) => (
                <tr key={section.id}>
                  <td>{section.id}</td>
                  <td>{section.name}</td>
                  <td>{section.testId}</td>
                </tr>
              ))}
            </tbody>
          </Table>
        </Modal.Body>
      </Modal>

      {/* Section add new  */}
      {/* 
      <Modal show={showAddNewProblem} onHide={handleCloseAddNewProblem}>
        <Modal.Header closeButton>
          <Modal.Title>
            Add new Problem for subject({subject.id}) {subject.title}
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form onSubmit={onFinish}>
            <Form.Group className="mb-3" controlId="formBasicName">
              <Form.Label>Name</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter title"
                name="problemName"
                required
              />
            </Form.Group>

            <Button type="primary" htmlType="submit">
              Submit
            </Button>
          </Form>
        </Modal.Body>
      </Modal> */}
    </div>
  );
}

export default TestDetailScreen;
