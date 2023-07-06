import {
  Button,
  Divider,
  Form,
  Input,
  Select,
  DatePicker,
  message,
} from "antd";
import React, { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import axios from "axios";
import locale from "antd/lib/date-picker/locale/en_US";
import { NotificationModel } from "../../models/NotificationModel";
import dayjs from "dayjs";

interface Props {
  notificationId?: number;
  refresh: any;
  handleCancel: any;
}

export default function NotificationEdit({
  notificationId,
  refresh,
  handleCancel,
}: Props) {
  const [form] = Form.useForm();
  const [notification, setNotification] = useState(undefined);
  console.log(notificationId);
  useEffect(() => {
    if (notificationId) {
      axios
        .get(
          `https://localhost:5001/api/Notification/GetNotificationByID/` +
            notificationId
        )
        .then((res) => {
          if (res) {
            res.data.createdTime = dayjs(res.data.createdTime);
            res.data.lastModified = dayjs(res.data.lastModified);
            form.setFieldsValue(res.data);
            setNotification(res.data);
          }
        });
    }
  }, []);

  const onFinish = (values: NotificationModel) => {
    axios.put("/api/Notification/UpdateNotifi", values).then(() => {});
    message.success("Update Notification Successful");
    refresh();
    handleCancel();
  };

  return (
    <div className="container">
      <h1>Edit Notification</h1>
      <Divider />

      <Form
        name="basic"
        form={form}
        labelCol={{
          span: 8,
        }}
        wrapperCol={{
          span: 16,
        }}
        initialValues={{
          remember: true,
        }}
        style={{
          maxWidth: 630,
        }}
        autoComplete="off"
        onFinish={onFinish}
      >
        <Form.Item
          label="Created Time"
          name="createdTime"
          rules={[
            {
              required: true,
            },
          ]}
        >
          <DatePicker
            format="DD/MM/YYYY"
            style={{ width: "100%" }}
            locale={locale}
          />
        </Form.Item>

        <Form.Item
          label="Type"
          name="type"
          rules={[
            {
              required: true,
              message: "Please input type!",
            },
          ]}
        >
          <Input />
        </Form.Item>

        <Form.Item
          label="Content"
          name="content"
          rules={[
            {
              required: true,
              message: "Please input content!",
            },
          ]}
        >
          <Input />
        </Form.Item>
        <Form.Item label="Is Read" name="isRead">
          <Select
            defaultValue="No"
            style={{ width: "100%" }}
            options={[
              {
                value: false,
                label: "No",
              },
              {
                value: true,
                label: "Yes",
              },
            ]}
          />
        </Form.Item>
        <Form.Item
          label="Email"
          name="email"
          rules={[
            {
              required: true,
              message: "Please input email",
            },
          ]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          label="Last Modified"
          name="lastModified"
          rules={[
            {
              required: true,
            },
          ]}
        >
          <DatePicker format="DD/MM/YYYY" style={{ width: "100%" }} />
        </Form.Item>
        <Form.Item
          label="From"
          name="from"
          rules={[
            {
              required: true,
              message: "Please input from field",
            },
          ]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          label="URL"
          name="url"
          rules={[
            {
              required: true,
              message: "Please input the url",
            },
          ]}
        >
          <Input />
        </Form.Item>
        <Form.Item label="Is seen" name="isSeen">
          <Select
            defaultValue="No"
            style={{ width: "100%" }}
            options={[
              {
                value: false,
                label: "No",
              },
              {
                value: true,
                label: "Yes",
              },
            ]}
          />
        </Form.Item>
        <div style={{ textAlign: "center" }}>
          <Button type="primary" htmlType="submit">
            Save
          </Button>
        </div>
      </Form>
    </div>
  );
}
