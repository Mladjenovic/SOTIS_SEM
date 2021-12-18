import React, { useState, useEffect } from "react";

import { useDispatch, useSelector } from "react-redux";

import { Form, Input, Button, Checkbox } from "antd";
import { UserOutlined, LockOutlined } from "@ant-design/icons";
import { Link, useHistory } from "react-router-dom";
import { login } from "../actions/userActions";

import Loader from "../components/Loader";
import Message from "../components/Message";

const LoginScreen = () => {
  const userLogin = useSelector((state) => state.userLogin);
  const { error, loading, userInfo } = userLogin;

  let history = useHistory();

  const dispatch = useDispatch();

  const onFinish = (values) => {
    console.log("Received values of form: ", values);
    dispatch(login(values.username, values.password));
    // history.push(`/subjects/`);
  };

  return (
    <div>
      {error && <Message variant="danger">{error}</Message>}
      {loading && <Loader />}
      <Form
        name="normal_login"
        className="login-form"
        initialValues={{
          remember: true,
        }}
        onFinish={onFinish}
      >
        <Form.Item
          name="username"
          rules={[
            {
              required: true,
              message: "Please input your Username!",
            },
          ]}
        >
          <Input
            prefix={<UserOutlined className="site-form-item-icon" />}
            placeholder="Username"
          />
        </Form.Item>
        <Form.Item
          name="password"
          rules={[
            {
              required: true,
              message: "Please input your Password!",
            },
          ]}
        >
          <Input
            prefix={<LockOutlined className="site-form-item-icon" />}
            type="password"
            placeholder="Password"
          />
        </Form.Item>

        <Form.Item>
          <Button
            type="primary"
            htmlType="submit"
            className="login-form-button"
          >
            Log in
          </Button>
          Or <Link to="/register">register now!</Link>
        </Form.Item>
      </Form>
    </div>
  );
};

export default LoginScreen;
