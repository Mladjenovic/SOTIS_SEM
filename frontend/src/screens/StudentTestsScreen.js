import React, { useState, useEffect } from "react";
import axios from "axios";
import { Row, Col } from "react-bootstrap";
import StudentTest from "../components/StudentTest";

function StudentTestsScreen() {
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
      <div>
        <Row>
          {tests.map((test) => (
            <Col key={test.id} sm={12} md={6} lg={4} xl={3}>
              <StudentTest test={test}></StudentTest>
            </Col>
          ))}
        </Row>
      </div>
    </div>
  );
}

export default StudentTestsScreen;
