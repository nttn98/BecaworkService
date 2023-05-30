import React, { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Table, { ColumnsType } from "antd/es/table";
import { MailModel } from "../../models/MailModel";
import {
  Form,
  Button,
  Col,
  Divider,
  Input,
  Row,
  Space,
  Select,
  Alert,
  message,
  DatePicker,
} from "antd";
import { Link } from "react-router-dom";
import { useEffect } from "react";
import axios from "axios";
import moment from "moment";
import { error, info } from "console";
import Moment from "react-moment";
export const MailCreate = () => {
  const { id } = useParams();
  const [showAlert, setShowAlert] = useState(false);
  const onFinish = (values: any) => {
    console.log(values);
    try {
      axios.post("/api/Mail/AddMail", values).then((res) => {
        console.log(res.data);
        message.success("Create Successfully");
      });
    } catch {
      message.error("Create fail");
    }
    // setShowAlert(true);
  };

  let navigate = useNavigate();

  return (
    <div className="detailPage">
      {/* {showAlert && (
        <Alert message="Update Mail Successfully" type="success" closable />
      )} */}
      <h1>Mail Create</h1>
      <div className="showDetails">
        <Form
          name="basic"
          labelCol={{ span: 4 }}
          wrapperCol={{ span: 16 }}
          style={{ maxWidth: 600 }}
          onFinish={onFinish}
          autoComplete="off"
          initialValues={{
            email: "",
            emailContent: "",
            subject: "",
            createBy: "",
            createTime: "",
            sendtime: "",
            isSend: false,
            sendStatus: "",
            mailType: "",
            sendTime: null,
          }}
        >
          <Form.Item label="Email" name="email">
            <Input />
          </Form.Item>
          <Form.Item label="Subject" name="subject">
            <Input />
          </Form.Item>
          <Form.Item label="Create by" name="createBy">
            <Input />
          </Form.Item>
          <Form.Item label="Create time" name="createTime">
            <Input />
          </Form.Item>
          <Form.Item label="Send time" name="sendTime">
            <Input />
          </Form.Item>
          <Form.Item label="Is Send" name="isSend">
            <Input disabled />
          </Form.Item>
          <Form.Item label="Send status" name="sendStatus">
            <Input disabled />
          </Form.Item>
          <Form.Item label="Mail type" name="mailType">
            <Input />
          </Form.Item>
          <Form.Item label="Content" name="emailContent">
            <Input />
          </Form.Item>

          <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
            <Button type="primary" htmlType="submit">
              Submit
            </Button>
          </Form.Item>
        </Form>
      </div>

      <Button
        className="detailPage-btn"
        type="primary"
        onClick={() => navigate(-1)}
      >
        Back
      </Button>
    </div>
  );
};
