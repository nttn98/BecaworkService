import * as React from "react";
import "./custom.css";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Layout from "./components/Layout";
import Home from "./components/Home";
import NotificationPage from "./pages/Home/NotificationPage";
import NotificationDetails from "./pages/Home/NotificationDetails";
import NotificationCreate from "./pages/Home/NotificationCreate";
import NotificationEdit from "./pages/Home/NotificationEdit";

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
]);

export default () => (
  <Layout>
    <RouterProvider router={router} />
  </Layout>
);
