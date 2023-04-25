import { Button, Divider, Form, Input, InputNumber, Select } from "antd";
import { DatePicker } from "antd";
import React from "react";
import { Link } from "react-router-dom";

export default function NotificationEdit() {
  return (
    <div className="container">
      <h1>Edit Notification</h1>
      <Divider />

      <Form
        name="basic"
        labelCol={{
          span: 8,
        }}
        wrapperCol={{
          span: 16,
        }}
        style={{
          maxWidth: 600,
        }}
        initialValues={{
          remember: true,
        }}
        autoComplete="off"
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
          <DatePicker format="DD/MM/YYYY" style={{ width: "100%" }} />
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
        <Form.Item
          wrapperCol={{
            offset: 8,
            span: 16,
          }}
        >
          <Button type="primary" htmlType="submit">
            Create
          </Button>
          <Link to="/notification-page"> Back to list</Link>
        </Form.Item>
      </Form>
    </div>
  );
}
