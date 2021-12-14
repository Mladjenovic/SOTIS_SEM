import axios from "axios";
import React, { useState, useEffect } from "react";
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import { Button, Table, Modal, Form } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";

function TestDetailScreen({ match }) {
  const userLogin = useSelector((state) => state.userLogin);
  const { userInfo } = userLogin;

  const [test, setTest] = useState([]);
  const [checked, setChecked] = useState(false);
  const [selectedSection, setSelectedSection] = useState([]);
  const [selectedQuestion, setSelectedQuestion] = useState([]);

  const [show, setShow] = useState(false);
  const [showAddNewSection, setShowAddNewSection] = useState(false);
  const [sectionsRelatedToTest, setSectionsRelatedToTest] = useState([]);
  const [questionsRelatedToSection, setQuestionsRelatedToSection] = useState(
    []
  );

  const [answersRelatedToQuestion, setAnswersRelatedToQuestion] = useState([]);

  const [showAllQuestions, setShowAllQuestion] = useState(false);
  const [showAddNewQuestion, setShowAddNewQuestion] = useState(false);

  // Helper functions
  async function fetchQuestionsRelatedToSection(sectionId) {
    const { data } = await axios.get(
      `https://localhost:44393/api/Question/QuestionsRealtedToSection/${sectionId}`
    );
    setQuestionsRelatedToSection(data);
  }

  async function fetchAnswersRelatedToQuestion(questionId) {
    const { data } = await axios.get(
      `https://localhost:44393/api/Answer/AnswersRelatedToQuestion/${questionId}`
    );
    setAnswersRelatedToQuestion(data);
    console.log(data);
  }

  // Section handles
  const handleClose = () => setShow(false);
  const handleShow = () => {
    setShow(true);
  };

  const handleShowAddNewSection = () => setShowAddNewSection(true);
  const handleCloseAddNewSection = () => setShowAddNewSection(false);

  // Question handles
  const handleShowAllQuestions = (sectionId) => {
    setSelectedSection(sectionId);
    fetchQuestionsRelatedToSection(sectionId);
    setShowAllQuestion(true);
  };
  const handleCloseAllQuestions = () => setShowAllQuestion(false);

  const handleShowAddNewQuestion = (sectionId) => {
    setShowAddNewQuestion(true);
    setSelectedSection(sectionId);
  };
  const handleCloseAddNewQuestion = () => setShowAddNewQuestion(false);

  // Answers

  const [showAllAnswers, setShowAllAnswers] = useState(false);
  const [showAddNewAnswer, setShowAddNewAnswer] = useState(false);

  const handleShowAddNewAnswer = (questionId) => {
    setSelectedQuestion(questionId);
    setShowAddNewAnswer(true);
  };
  const handleCloseAddNewAnswer = () => setShowAddNewAnswer(false);
  const handleShowAllAnswers = (questionId) => {
    fetchAnswersRelatedToQuestion(questionId);
    setSelectedQuestion(questionId);
    setShowAllAnswers(true);
  };
  const handleCloseAllAnswers = () => setShowAllAnswers(false);

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
    }
    fetchSectionsRealtedToTest();
  }, []);

  const onFinish = (event) => {
    const config = {
      headers: {
        "Content-type": "application/json",
      },
    };

    const { data } = axios.post(
      "https://localhost:44393/api/Section",
      {
        name: event.target.elements.sectionName.value,
        testId: test.id,
      },
      config
    );

    handleCloseAddNewSection();
  };

  const onFinishAddNewQuestion = (event) => {
    const config = {
      headers: {
        "Content-type": "application/json",
      },
    };

    const { data } = axios.post(
      "https://localhost:44393/api/Question",
      {
        text: event.target.elements.text.value,
        pointsPerQuestion: event.target.elements.pointsPerQuestion.value,
        sectionId: selectedSection,
      },
      config
    );

    handleCloseAddNewSection();
  };

  const onFinishAddNewAnswer = (event) => {
    event.preventDefault();
    const config = {
      headers: {
        "Content-type": "application/json",
      },
    };

    const { data } = axios.post(
      "https://localhost:44393/api/Answer",
      {
        text: event.target.elements.text.value,
        correct: checked,
        questionId: selectedQuestion,
      },
      config
    );

    handleCloseAddNewAnswer();
  };

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
      <Modal show={show} onHide={handleClose} size="lg">
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
                <th>Questions</th>
              </tr>
            </thead>
            <tbody>
              {sectionsRelatedToTest.map((section) => (
                <tr key={section.id}>
                  <td>{section.id}</td>
                  <td>{section.name}</td>
                  <td>{section.testId}</td>
                  <td style={{ width: 220 }}>
                    <span>
                      <Button
                        style={{ margin: 10 }}
                        size="sm"
                        onClick={() => handleShowAllQuestions(section.id)}
                      >
                        all
                      </Button>
                      <Button
                        size="sm"
                        onClick={() => handleShowAddNewQuestion(section.id)}
                      >
                        new
                      </Button>
                    </span>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        </Modal.Body>
      </Modal>

      {/* Section add new  */}
      <Modal show={showAddNewSection} onHide={handleCloseAddNewSection}>
        <Modal.Header closeButton>
          <Modal.Title>
            Add Section for test({test.id}): {test.title})
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form onSubmit={onFinish}>
            <Form.Group className="mb-3" controlId="formBasicName">
              <Form.Label>Name</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter name"
                name="sectionName"
                required
              />
            </Form.Group>

            <Button type="primary" htmltype="submit">
              Submit
            </Button>
          </Form>
        </Modal.Body>
      </Modal>

      {/* Questions all */}
      <Modal show={showAllQuestions} onHide={handleCloseAllQuestions} size="lg">
        <Modal.Header closeButton>
          <Modal.Title>
            All questions for section: ({selectedSection})
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Table striped bordered hover>
            <thead>
              <tr>
                <th>Id</th>
                <th>Text</th>
                <th>Points per question</th>
                <th>Section id</th>
                <th style={{ width: 220 }}>Problem id</th>
                <th>Answers</th>
              </tr>
            </thead>
            <tbody>
              {questionsRelatedToSection.map((question) => (
                <tr key={question.id}>
                  <td>{question.id}</td>
                  <td>{question.text}</td>
                  <td>{question.pointsPerQuestion}</td>
                  <td>{question.sectionId}</td>
                  <td>{question.problemId}</td>
                  <td style={{ width: 250 }}>
                    <span>
                      <Button
                        style={{ margin: 10 }}
                        size="sm"
                        onClick={() => handleShowAllAnswers(question.id)}
                      >
                        all
                      </Button>
                      <Button
                        size="sm"
                        onClick={() => handleShowAddNewAnswer(question.id)}
                      >
                        new
                      </Button>
                    </span>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        </Modal.Body>
      </Modal>

      {/* Questions add new  */}
      <Modal
        show={showAddNewQuestion}
        onHide={handleCloseAddNewQuestion}
        className="special_modal"
      >
        <Modal.Header closeButton>
          <Modal.Title>
            Add Question for section: ({selectedSection})
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form onSubmit={onFinishAddNewQuestion}>
            <Form.Group className="mb-3" controlId="formBasicQuestionText">
              <Form.Label>Question Text</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter the question"
                name="text"
                required
              />
            </Form.Group>

            <Form.Group className="mb-3" controlId="formBasicPointsPerQuestion">
              <Form.Label>Points for question</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter how much points this question is worth"
                name="pointsPerQuestion"
                required
              />
            </Form.Group>

            <Button type="primary" htmltype="submit">
              Submit
            </Button>
          </Form>
        </Modal.Body>
      </Modal>

      {/* Answers all */}
      <Modal show={showAllAnswers} onHide={handleCloseAllAnswers} size="lg">
        <Modal.Header closeButton>
          <Modal.Title>
            All answers for question: ({selectedSection})
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Table striped bordered hover>
            <thead>
              <tr>
                <th>Id</th>
                <th>Text</th>
                <th>Correct</th>
                <th>Question Id</th>
              </tr>
            </thead>
            <tbody>
              {answersRelatedToQuestion.map((answer) => (
                <tr key={answer.id}>
                  <td>{answer.id}</td>
                  <td>{answer.text}</td>
                  <td>{answer.correct.toString()}</td>
                  <td>{answer.questionId}</td>
                </tr>
              ))}
            </tbody>
          </Table>
        </Modal.Body>
      </Modal>

      {/* Answers add new  */}
      <Modal
        show={showAddNewAnswer}
        onHide={handleCloseAddNewAnswer}
        className="special_modal"
      >
        <Modal.Header closeButton>
          <Modal.Title style={{ color: "white" }}>
            Add answer for question: ({selectedQuestion})
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form onSubmit={onFinishAddNewAnswer}>
            <Form.Group className="mb-3" controlId="formBasicAnswerText">
              <Form.Label>Answer Text</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter the answer"
                name="text"
                required
              />
            </Form.Group>

            <Form.Group className="mb-3" controlId="formBasicCheckbox">
              <input
                name="correct"
                type="checkbox"
                checked={checked}
                onChange={(e) => setChecked(e.target.checked)}
              />
              <Form.Label style={{ marginLeft: 10 }}>Correct</Form.Label>
            </Form.Group>

            <Button type="primary" htmltype="submit">
              Submit
            </Button>
          </Form>
        </Modal.Body>
      </Modal>
    </div>
  );
}

export default TestDetailScreen;
