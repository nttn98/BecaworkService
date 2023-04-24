import * as React from "react";
import { FC, useState } from "react";
import { Button, Layout, Menu } from "antd";
import {
  UserOutlined,
  Home1Outlined,
  SidebarLeftOutlined,
  SidebarRightOutlined,
} from "bu2-sax-icons";
import { HomePage } from "../pages/Home/HomePage";

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
                label: "Home",
              },
              {
                key: "2",
                icon: <UserOutlined />,
                label: "User",
              },
            ]}
          />
        </Sider>
        <Layout className="site-layout">
          <Header className="site-layout-background" style={{ padding: 0 }}>
            {collapsed ? (
              <Button
                icon={<SidebarLeftOutlined size={24} onClick={() => {}} />}
              />
            ) : (
              <Button
                icon={<SidebarLeftOutlined size={24} onClick={() => {}} />}
              />
            )}
          </Header>
          <Content
            className="site-layout-background"
            style={{
              margin: "24px 16px",
              padding: 24,
              minHeight: 280,
            }}
          >
            {<HomePage />}
          </Content>
        </Layout>
      </Layout>
    </React.Fragment>
  );
};

export default AppLayout;
