import axios from "axios";
import React, { useState, useEffect } from "react";
import { Row, Col, Button, Modal, Form } from "react-bootstrap";

function SurmiseScreen() {
  const [knowledgeSpaces, setKnowledgeSpaces] = useState([]);
  const [problems, setProblems] = useState([]);

  useEffect(() => {
    async function fetchKnowledgeSpaces() {
      const { data } = await axios.get(
        "https://localhost:44393/api/KnowledgeSpace"
      );
      setKnowledgeSpaces(data);
    }
    fetchKnowledgeSpaces();

    async function fetchProblems() {
      const { data } = await axios.get("https://localhost:44393/api/Problem");
      setProblems(data);
    }
    fetchProblems();
  }, []);

  const onFinish = (event) => {
    const config = {
      headers: {
        "Content-type": "application/json",
      },
    };

    const { data } = axios.post(
      "https://localhost:44393/api/Surmise",
      {
        problemId: event.target.elements.problemId.value,
        knowledgeSpaceId: event.target.elements.knowledgeSpaceId.value,
      },
      config
    );
  };

  return (
    <div>
      <Form onSubmit={onFinish}>
        <Form.Group>
          <Form.Label>Knowledge space</Form.Label>
          <Form.Select
            aria-label="Default select example"
            name="knowledgeSpaceId"
          >
            {knowledgeSpaces.map((knowledgeSpace) => (
              <option value={knowledgeSpace.id}>{knowledgeSpace.name}</option>
            ))}
          </Form.Select>
        </Form.Group>
        <Form.Group>
          <Form.Label>Problem</Form.Label>
          <Form.Select aria-label="Default select example" name="problemId">
            {problems.map((problem) => (
              <option value={problem.id}>{problem.name}</option>
            ))}
          </Form.Select>
        </Form.Group>
        <Button type="primary" htmlType="submit" style={{ marginTop: 20 }}>
          Submit
        </Button>
      </Form>
    </div>
  );
}

export default SurmiseScreen;
