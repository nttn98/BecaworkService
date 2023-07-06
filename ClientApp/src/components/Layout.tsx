import * as React from "react";
import { FC, useState } from "react";
import { Button, Layout, Menu } from "antd";
import {
  UserOutlined,
  Home1Outlined,
  SidebarLeftOutlined,
  SidebarRightOutlined,
} from "bu2-sax-icons";
import Home from "./Home";
import NotificationPage from "../pages/Home/NotificationPage";
import { Link } from "react-router-dom";

const { Header, Sider, Content } = Layout;

interface Props {
  children: React.ReactNode;
}

const AppLayout: FC<Props> = (props) => {
  const [collapsed, setCollapsed] = useState(false);

  return (
    <React.Fragment>
      <Layout>
        <Sider trigger={null} collapsible collapsed={collapsed}>
          <div className="logo" />
          <Menu
            theme="dark"
            mode="inline"
            defaultSelectedKeys={["1"]}
            items={[
              {
                key: "1",
                icon: <Home1Outlined />,
                label: <a href="/">Home</a>,
              },
              {
                label: <a href="/notification-page">Notification</a>,
                key: "2",
                icon: <UserOutlined />,
              },
              {
                label: <a href="/mail">Mail</a>,
                key: "3",
                icon: <UserOutlined />,
              },
            ]}
          />
        </Sider>
        <Layout className="site-layout">
<<<<<<< Updated upstream
          <Content className="site-layout-background">{props.children}</Content>
=======
          <Content
            className="site-layout-background"
            style={{
              minHeight: 280,
              marginLeft: "20px",
            }}
          >
            {props.children}
          </Content>
>>>>>>> Stashed changes
        </Layout>
      </Layout>
    </React.Fragment>
  );
};

export default AppLayout;
