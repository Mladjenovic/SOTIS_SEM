import axios from "axios";
import React, { useState, useEffect } from "react";
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import { Button, Table, Modal, Form } from "react-bootstrap";
import "../Question.css";

function StudentTestDetailScreen({ match }) {
  const [test, setTest] = useState([]);
  const [sectionsRelatedToTest, setSectionsRelatedToTest] = useState([]);
  const [questionsRelatedToSection, setQuestionsRelatedToSection] = useState(
    []
  );

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

    async function fetchQuestionsRelatedToSection(sectionId) {
      const { data } = await axios.get(
        `https://localhost:44393/api/Question/QuestionsRealtedToSection/${sectionId}`
      );
      setQuestionsRelatedToSection(data);
    }

    fetchQuestionsRelatedToSection();
  }, []);

  const questions = [
    {
      questionText: "What is the capital of France?",
      answerOptions: [
        { answerText: "New York", isCorrect: false },
        { answerText: "London", isCorrect: false },
        { answerText: "Paris", isCorrect: true },
        { answerText: "Dublin", isCorrect: false },
      ],
    },
    {
      questionText: "Who is CEO of Tesla?",
      answerOptions: [
        { answerText: "Jeff Bezos", isCorrect: false },
        { answerText: "Elon Musk", isCorrect: true },
        { answerText: "Bill Gates", isCorrect: false },
        { answerText: "Tony Stark", isCorrect: false },
      ],
    },
    {
      questionText: "The iPhone was created by which company?",
      answerOptions: [
        { answerText: "Apple", isCorrect: true },
        { answerText: "Intel", isCorrect: false },
        { answerText: "Amazon", isCorrect: false },
        { answerText: "Microsoft", isCorrect: false },
      ],
    },
    {
      questionText: "How many Harry Potter books are there?",
      answerOptions: [
        { answerText: "1", isCorrect: false },
        { answerText: "4", isCorrect: false },
        { answerText: "6", isCorrect: false },
        { answerText: "7", isCorrect: true },
      ],
    },
  ];

  const [currentQuestion, setCurrentQuestion] = useState(0);

  const [showScore, setShowScore] = useState(false);

  const [score, setScore] = useState(0);

  const handleAnswerButtonClick = (isCorrect) => {
    if (isCorrect === true) {
      setScore(score + 1);
    }

    const nextQuestion = currentQuestion + 1;
    if (nextQuestion < questions.length) {
      setCurrentQuestion(nextQuestion);
    } else {
      setShowScore(true);
    }
  };

  return (
    <div>
      <Link to="/student-tests/">
        <Button>Go back</Button>
      </Link>
      <div className="test">
        <div className="test-app">
          {/* HINT: replace "false" with logic to display the 
      score when the user has answered all the questions */}
          {showScore ? (
            <div className="test-score-section">
              You scored {score} out of {questions.length}
            </div>
          ) : (
            <>
              <div className="test-question-section">
                <div className="test-question-count">
                  <span>Question {currentQuestion + 1}</span>/{questions.length}
                </div>
                <div className="test-question-text">
                  {questions[currentQuestion].questionText}
                </div>
              </div>
              <div className="test-answer-section">
                {questions[currentQuestion].answerOptions.map(
                  (answerOption) => (
                    <button
                      className="test-button"
                      onClick={() =>
                        handleAnswerButtonClick(answerOption.isCorrect)
                      }
                    >
                      {answerOption.answerText}
                    </button>
                  )
                )}
              </div>
            </>
          )}
        </div>
      </div>
    </div>
  );
}

export default StudentTestDetailScreen;
