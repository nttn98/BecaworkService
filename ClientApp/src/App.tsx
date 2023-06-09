import * as React from "react";
import "./custom.css";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Layout from "./components/Layout";
import Home from "./components/Home";
import NotificationPage from "./pages/Home/NotificationPage";
import NotificationDetails from "./pages/Home/NotificationDetails";
import NotificationCreate from "./pages/Home/NotificationCreate";
import NotificationEdit from "./pages/Home/NotificationEdit";
import MailPage from "./pages/Mail/MailPage";
import { MailDetailPage } from "./pages/Mail/MailDetailPage";
import { MailUpdatePage } from "./pages/Mail/MailUpdatePage";
import { MailCreate } from "./pages/Mail/MailCreate";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Home />,
  },
  {
    path: "notification-page",
    element: <NotificationPage />,
  },
  {
    path: "/notification/details/:id",
    element: <NotificationDetails />,
  },
  {
    path: "/notification/create",
    element: <NotificationCreate />,
  },
  {
    path: "/notification/edit/:id",
    element: <NotificationEdit />,
  },
  {
    path: "mail",
    element: <MailPage />,
  },
  {
    path: "mail/:id",
    element: <MailDetailPage />,
  },
  {
    path: "mail/update/:id",
    element: <MailUpdatePage />,
  },
  {
    path: "mail/create",
    element: <MailCreate />,
  },
  {
    path: "mail/resend/:id",
  },
]);

export default () => (
  <Layout>
    <RouterProvider router={router} />
  </Layout>
);
