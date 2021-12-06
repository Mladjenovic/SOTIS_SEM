import axios from "axios";
import React, { useState, useEffect } from "react";
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import { Button, Table, Modal, Form } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";

function SubjectDetailScreen({ match }) {
  const userLogin = useSelector((state) => state.userLogin);
  const { userInfo } = userLogin;

  const [subject, setSubject] = useState([]);
  const [selectedProblem, setSelectedProblem] = useState([]);
  const [profesors, setProfesors] = useState([]);
  const [surmises, setSurmises] = useState([]);
  const [problemsRelatedToSubject, setProblemsRelatedToSubject] = useState([]);
  const [knowledgeSpacesRelatedToSubject, setKnowledgeSpacesRelatedToSubject] =
    useState([]);

  const [show, setShow] = useState(false);
  const [showAddNewProblem, setShowAddNewProblem] = useState(false);

  const [showAllKnowledgeSpaces, setShowAllKnowledgeSpaces] = useState(false);
  const [showAddNewKnowledgeSpace, setShowAddNewKnowledgeSpace] =
    useState(false);

  const [showSurmises, setShowSurmises] = useState(false);

  // Problem handles
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const handleShowAddNewProblem = () => setShowAddNewProblem(true);
  const handleCloseAddNewProblem = () => setShowAddNewProblem(false);

  // Knowledge Spaces handles
  const handleCloseAllKnowledgeSpaces = () => setShowAllKnowledgeSpaces(false);
  const handleShowAllKnowledgeSpaces = () => setShowAllKnowledgeSpaces(true);

  const handleCloseAddNewKnowledgeSpace = () =>
    setShowAllKnowledgeSpaces(false);
  const handleShowAddNewKnowledgeSpace = () =>
    setShowAddNewKnowledgeSpace(true);

  // Surmises
  const handleShowSurmises = (problemId) => {
    setSelectedProblem(problemId);
    setShowSurmises(true);
  };
  const handleCloseShowSurmises = () => setShowSurmises(false);

  useEffect(() => {
    async function fetchSubject() {
      const { data } = await axios.get(
        `https://localhost:44393/api/Subject/${match.params.id}`
      );
      setSubject(data);
    }
    fetchSubject();

    async function fetchProfesors() {
      const { data } = await axios.get(
        "https://localhost:44393/api/Profesor/ProfesorsVerbose"
      );
      setProfesors(data);
    }
    fetchProfesors();

    async function fetchProblemsRelatedToSubject(subjectId) {
      const { data } = await axios.get(
        `https://localhost:44393/api/Problem/ProblemRelatedToSubject/${match.params.id}`
      );
      setProblemsRelatedToSubject(data);
    }
    fetchProblemsRelatedToSubject();

    async function fetchKnowledgeSpacesRelatedToSubject(subjectId) {
      const { data } = await axios.get(
        `https://localhost:44393/api/KnowledgeSpace/KnowledgeSpaceRelatedToSubject/${match.params.id}`
      );
      setKnowledgeSpacesRelatedToSubject(data);
    }
    fetchKnowledgeSpacesRelatedToSubject();

    async function fetchSurmises() {
      const { data } = await axios.get(`https://localhost:44393/api/Surmise`);
      setSurmises(data);
    }
    fetchSurmises();
  }, []);

  const onFinish = (event) => {
    const config = {
      headers: {
        "Content-type": "application/json",
      },
    };

    const { data } = axios.post(
      "https://localhost:44393/api/Problem",
      {
        name: event.target.elements.problemName.value,
        subjectId: subject.id,
      },
      config
    );

    handleCloseAddNewProblem();
  };

  const onFinishAddNewKnowledgeSpace = (event) => {
    const config = {
      headers: {
        "Content-type": "application/json",
      },
    };

    const { data } = axios.post(
      "https://localhost:44393/api/KnowledgeSpace",
      {
        name: event.target.elements.knowledgeSpaceName.value,
        subjectId: subject.id,
      },
      config
    );

    handleCloseAddNewKnowledgeSpace();
  };

  return (
    <div>
      <Link to="/subjects/">
        <Button>Go back</Button>
      </Link>
      <Table striped bordered hover>
        <tbody>
          <tr>
            <td>Id</td>
            <td>{subject.id}</td>
          </tr>
          <tr>
            <td>Title</td>

            <td>{subject.title}</td>
          </tr>
          <tr>
            <td>Description</td>

            <td>{subject.description}</td>
          </tr>
          <tr>
            <td>Name</td>

            <td>{subject.name}</td>
          </tr>
          <tr>
            <td>Profesor</td>

            <td>{subject.profesorId}</td>
          </tr>
          <tr>
            <td>Problems</td>
            <td>
              <Button style={{ marginRight: 5 }} onClick={handleShow}>
                View All
              </Button>
              {userInfo && userInfo.UserType != "Student" ? (
                <Button onClick={handleShowAddNewProblem}>New</Button>
              ) : (
                <div></div>
              )}
            </td>
          </tr>
          <tr>
            <td>Knowledge Spaces</td>
            <td>
              <Button
                style={{ marginRight: 5 }}
                onClick={handleShowAllKnowledgeSpaces}
              >
                View All
              </Button>
              {userInfo && userInfo.UserType != "Student" ? (
                <Button onClick={handleShowAddNewKnowledgeSpace}>New</Button>
              ) : (
                <div></div>
              )}
            </td>
          </tr>
        </tbody>
      </Table>

      {/* Problems all */}

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>
            All problems for subject({subject.id}): {subject.title}
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Table striped bordered hover>
            <thead>
              <tr>
                <th>Id</th>
                <th>Name</th>
                <th>SubjectId</th>
                <th>Surmises</th>
              </tr>
            </thead>
            <tbody>
              {problemsRelatedToSubject.map((problem) => (
                <tr key={problem.id}>
                  <td>{problem.id}</td>
                  <td>{problem.name}</td>
                  <td>{problem.subjectId}</td>
                  <td>
                    <Button onClick={() => handleShowSurmises(problem.id)}>
                      Surmises
                    </Button>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        </Modal.Body>
      </Modal>

      {/* Problem add new  */}

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
      </Modal>

      {/* Knowledge Spaces all */}

      <Modal
        show={showAllKnowledgeSpaces}
        onHide={handleCloseAllKnowledgeSpaces}
      >
        <Modal.Header closeButton>
          <Modal.Title>
            All Knowledge spaces for subject({subject.id}): {subject.title}
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Table striped bordered hover>
            <thead>
              <tr>
                <th>Id</th>
                <th>Name</th>
                <th>SubjectId</th>
              </tr>
            </thead>
            <tbody>
              {knowledgeSpacesRelatedToSubject.map((knowledgeSpace) => (
                <tr key={knowledgeSpace.id}>
                  <td>{knowledgeSpace.id}</td>
                  <td>{knowledgeSpace.name}</td>
                  <td>{knowledgeSpace.subjectId}</td>
                </tr>
              ))}
            </tbody>
          </Table>
        </Modal.Body>
      </Modal>

      {/* Knowledge spaces add new */}

      <Modal show={showAddNewKnowledgeSpace} onHide={handleCloseAddNewProblem}>
        <Modal.Header closeButton>
          <Modal.Title>
            Add new Knowledge space for subject({subject.id}) {subject.title}
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form onSubmit={onFinishAddNewKnowledgeSpace}>
            <Form.Group className="mb-3" controlId="formBasicName">
              <Form.Label>Name</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter name"
                name="knowledgeSpaceName"
                required
              />
            </Form.Group>

            <Button type="primary" htmlType="submit">
              Submit
            </Button>
          </Form>
        </Modal.Body>
      </Modal>

      {/* Knowledge spaces add new */}

      <Modal show={showSurmises} onHide={handleCloseShowSurmises}>
        <Modal.Header closeButton>
          <Modal.Title>Surmises (Needs further implementation)</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Table striped bordered hover size="sm" variant="dark">
            <thead>
              <tr>
                <th>Id</th>
                <th>problem id</th>
                <th>knowledge space id</th>
                <th>Problems</th>
              </tr>
            </thead>
            <tbody>
              {surmises.map((surmise) => (
                <tr key={surmise.id}>
                  <td>{surmise.id}</td>
                  <td>{surmise.problemId}</td>
                  <td>{surmise.knowledgeSpaceId}</td>
                  <tr>
                    <Button type="primary" style={{ marginTop: 20 }}>
                      Problems
                    </Button>
                  </tr>
                </tr>
              ))}
            </tbody>
          </Table>
        </Modal.Body>
      </Modal>
    </div>
  );
}

export default SubjectDetailScreen;
