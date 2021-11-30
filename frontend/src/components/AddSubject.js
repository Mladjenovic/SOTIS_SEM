import React from "react";

function AddSubject() {
  return (
    <div>
      <Form>
        <Form.Group>
          <Form.Control type="text" placeholder="title" required></Form.Control>
        </Form.Group>

        <Button type="submit" variant="success" block>
          Add subject
        </Button>
      </Form>
    </div>
  );
}

export default AddSubject;
