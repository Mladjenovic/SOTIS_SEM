import React from "react";
import { Card } from "react-bootstrap";
import { BrowserRouter as Router, Route, Link } from "react-router-dom";

function Subject({ subject }) {
  return (
    <Card className="my-3 p-3 rounded">
      <Link to={`/subject/${subject.id}`}>
        <h3>{subject.title}</h3>
      </Link>

      <Card.Body>
        <Card.Text as="div">
          <div className="my-3">
            <p>Description: {subject.description}</p>
          </div>
          <div className="my-3">
            <p>Name: {subject.name}</p>
          </div>
          <div className="my-3">
            <p>Minimum points: {subject.minimumPoints}</p>
          </div>
        </Card.Text>
      </Card.Body>
    </Card>
  );
}

export default Subject;
