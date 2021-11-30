import axios from "axios";
import React, { useState, useEffect } from "react";
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import { Button, Table } from "react-bootstrap";

function SubjectDetailScreen({ match }) {
  const [subject, setSubject] = useState([]);
  const [profesors, setProfesors] = useState([]);

  useEffect(() => {
    async function fetchSubject() {
      const { data } = await axios.get(
        `https://localhost:44393/api/Subject/${match.params.id}`
      );
      setSubject(data);
      console.log("subject------------", data);
    }
    fetchSubject();

    async function fetchProfesors() {
      const { data } = await axios.get(
        "https://localhost:44393/api/Profesor/ProfesorsVerbose"
      );
      setProfesors(data);
    }
    fetchProfesors();
  }, []);

  return (
    <div>
      <Link to="/subjects/">
        <Button>Go back</Button>
      </Link>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Id</th>
            <th>Title</th>
            <th>Description</th>
            <th>Name</th>
            <th>Professor</th>
            <th>Problems</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>{subject.id}</td>
            <td>{subject.title}</td>
            <td>{subject.description}</td>
            <td>{subject.name}</td>
            <td>{subject.profesorId}</td>
            <td>
              <Button style={{ marginRight: 5 }}>All</Button>
              <Button>New</Button>
            </td>
          </tr>
        </tbody>
      </Table>
    </div>
  );
}

export default SubjectDetailScreen;
