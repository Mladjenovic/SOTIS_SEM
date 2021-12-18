import React from "react";
import { Card } from "react-bootstrap";
import { BrowserRouter as Router, Route, Link } from "react-router-dom";

function Test({ test }) {
  return (
    <Card className="my-3 p-3 rounded">
      <Link to={`/test-to-take/${test.id}`}>
        <h3>{test.title}</h3>
      </Link>

      <Card.Body>
        <Card.Text as="div">
          <div className="my-3">
            <p>Title: {test.title}</p>
          </div>
          <div className="my-3">
            <p>MinimumPoints: {test.minimumPoints}</p>
          </div>
          <div className="my-3">
            <p>Profesor id: {test.profesorId}</p>
          </div>
          <div>
            <p>Subject id: {test.subjectId}</p>
          </div>
        </Card.Text>
      </Card.Body>
    </Card>
  );
}

export default Test;
