import React, { useState } from "react";
import { Layout, Menu, Breadcrumb, Button } from "antd";
import { Nav, Navbar, Container, Row } from "react-bootstrap";
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";

import { logout } from "../actions/userActions";

import HomeScreen from "../screens/HomeScreen";
import LoginScreen from "../screens/LoginScreen";
import RegisterScreen from "../screens/RegisterScreen";

import {
  LoginOutlined,
  UserAddOutlined,
  HomeOutlined,
  UserOutlined,
} from "@ant-design/icons";

const { Header, Content, Footer, Sider } = Layout;
const { SubMenu } = Menu;

function CustomLayout() {
  const userLogin = useSelector((state) => state.userLogin);
  const { userInfo } = userLogin;

  console.log("user info log: ", userInfo);

  const dispatch = useDispatch();

  const [collapsed, setCollapsed] = useState(false);

  const onCollapse = (collapsed) => {
    setCollapsed(collapsed);
  };

  const logoutHandler = () => {
    dispatch(logout());
  };

  return (
    <Layout style={{ minHeight: "100vh" }}>
      <Sider collapsible collapsed={collapsed} onCollapse={onCollapse}>
        <div className="logo" />
        <Menu theme="dark" defaultSelectedKeys={["1"]} mode="inline">
          <Menu.Item key="1" icon={<HomeOutlined />}>
            <Link to="/">Home</Link>
          </Menu.Item>

          <SubMenu key="sub1" icon={<UserOutlined />} title="User">
            <Menu.Item key="2">Tom</Menu.Item>
            <Menu.Item key="3">Bill</Menu.Item>
            <Menu.Item key="4">Alex</Menu.Item>
          </SubMenu>
          <Menu.Item key="5" icon={<LoginOutlined />}>
            <Link to="/login/">Login</Link>
          </Menu.Item>
          <Menu.Item key="6" icon={<UserAddOutlined />}>
            <Link to="/register/">Register</Link>
          </Menu.Item>
          {userInfo ? (
            <Menu.Item key="7" icon={<LoginOutlined />}>
              <Button
                type="primary"
                htmlType="submit"
                className="login-form-button"
                onClick={logoutHandler}
              >
                Logout
              </Button>
            </Menu.Item>
          ) : (
            <Menu.Item key="7" disabled>
              <p></p>
            </Menu.Item>
          )}
        </Menu>
      </Sider>
      <Layout className="site-layout">
        <Header className="site-layout-background" style={{ padding: 0 }} />
        <Content style={{ margin: "0 16px" }}>
          <Breadcrumb style={{ margin: "16px 0" }}>
            <Breadcrumb.Item>User</Breadcrumb.Item>
            <Breadcrumb.Item>Bill</Breadcrumb.Item>
          </Breadcrumb>
          <div
            className="site-layout-background"
            style={{ padding: 24, minHeight: 360 }}
          >
            <Container>
              <Route path="/" component={HomeScreen} exact></Route>
              <Route path="/login/" component={LoginScreen} exact></Route>
              <Route path="/register/" component={RegisterScreen} exact></Route>
            </Container>
          </div>
        </Content>
        <Footer style={{ textAlign: "center" }}>
          Ant Design ©2018 Created by Ant UED
        </Footer>
      </Layout>
    </Layout>
  );
}

export default CustomLayout;
