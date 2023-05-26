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
} from "antd";
import { Link } from "react-router-dom";
import { useEffect } from "react";
import axios from "axios";
import moment from "moment";
import { info } from "console";

export const MailUpdatePage = () => {
  const { id } = useParams();
  const [data, setData] = useState<MailModel | undefined>();
  const [showAlert, setShowAlert] = useState(false);

  const onFinish = (values: any) => {
    console.log(values);
    axios.put("/api/mail/UpdateMail", values).then((res) => {
      console.log(res.data);
      message.success("Update Successfully");
    });

    setShowAlert(true);
  };

  let navigate = useNavigate();

  useEffect(() => {
    axios.get("/api/Mail/GetMailByID/" + id).then((res) => {
      console.log(res.data);
      if (res) {
        setData(res.data);
      }
    });
  }, []);

  return (
    <div className="detailPage">
      {showAlert && <Alert message="Success Text" type="success" closable />}
      <h1>Mail Details</h1>

      {data ? (
        <div className="showDetails">
          <Form
            name="basic"
            labelCol={{ span: 4 }}
            wrapperCol={{ span: 16 }}
            style={{ maxWidth: 600 }}
            onFinish={onFinish}
            initialValues={{
              id: id,
              email: data.email,
              emailContent: data.emailContent,
              subject: data.subject,
              createby: data.createby,
              createtime: data.createTime,
              sendtime: data.sendTime,
              isSend: data.isSend ? "Yes" : "No",
              sendStatus: data.sentStatus,
              mailType: data.mailType,
            }}
            autoComplete="off"
          >
            <Form.Item label="ID" name="id">
              <Input value={id} disabled />
            </Form.Item>
            <Form.Item label="Email" name="email">
              <Input />
            </Form.Item>
            <Form.Item label="Subject" name="subject">
              <Input />
            </Form.Item>
            <Form.Item label="Create by" name="createby">
              <Input />
            </Form.Item>
            <Form.Item label="Create time" name="createtime">
              <Input />
            </Form.Item>
            <Form.Item label="Send time" name="sendtime">
              <Input />
            </Form.Item>
            <Form.Item name="isSend" label="Is Send">
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
      ) : (
        <p>Not found</p>
      )}
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
