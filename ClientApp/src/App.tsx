import * as React from "react";
import "./custom.css";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Layout from "./components/Layout";
import { HomePage } from "./pages/Home/HomePage";

const router = createBrowserRouter([
  {
    path: "/",
    element: <HomePage />,
  },
]);

export default () => (
  <Layout>
    <RouterProvider router={router} />
  </Layout>
);
