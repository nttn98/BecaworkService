import * as React from "react";
import "./custom.css";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Layout from "./components/Layout";
import Home from "./components/Home";
import NotificationPage from "./pages/Home/NotificationPage";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Home />,
  },
  {
    path: "notification-page",
    element: <NotificationPage />,
  },
]);

export default () => (
  <Layout>
    <RouterProvider router={router} />
  </Layout>
);
