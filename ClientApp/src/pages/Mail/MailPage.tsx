import {
  Button,
  Form,
  Input,
  Pagination,
  Space,
  Table,
  Tag,
  message,
} from "antd";
import { ColumnsType } from "antd/es/table";
import React, { FC, useEffect, useState } from "react";
import axios from "axios";
import { Label } from "reactstrap";
import { Link } from "react-router-dom";
import { MailModel } from "../../models/MailModel";

const columns: ColumnsType<MailModel> = [
  {
    title: "Id",
    dataIndex: "id",
  },
  {
    title: "Email",
    dataIndex: "email",
  },
  {
    title: "Subject",
    dataIndex: "subject",
  },
  {
    title: "Created by",
    dataIndex: "createBy",
  },
  {
    title: "Action",
    key: "action",
    render: (_, record) => (
      <Space size="middle">
        <Link to={"/mail/" + record.id}>Detail</Link>
      </Space>
    ),
  },
];

const pageSize = 10;

export default function MailPage() {
  const [data, setData] = useState([]);
  const [page, setPage] = useState(1);
  const [loading, setLoading] = useState(false);

  const fetchData = async () => {
    setLoading(true);
    try {
      const result = await axios.get(
        `/api/Mail/GetMails?page=${page}&pageSize=${pageSize}`
      );
      if (result.status === 200) {
        setData(result.data);
      }
    } catch {
      message.error("Loi");
    }
    setLoading(false);
  };

  useEffect(() => {
    fetchData();
  }, [page, pageSize]);

  return (
    <div>
      {/* <Form
        name="basic"
        labelCol={{ span: 5 }}
        wrapperCol={{ span: 14 }}
        style={{ maxWidth: 600 }}
        initialValues={{ remember: true }}
        autoComplete="off"
        onFinish={onFinish}
        onFinishFailed={onFinishFailed}
      >
        <Form.Item label="Page" name="page">
          <Input />
        </Form.Item>
        <Form.Item label="Page size" name="pageSize">
          <Input />
        </Form.Item>
        <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
          <Button type="primary" htmlType="submit">
            Submit
          </Button>
        </Form.Item>
      </Form> */}
      <Table
        columns={columns}
        dataSource={data}
        scroll={{ x: 1500 }}
        pagination={false}
        loading={loading}
      />
      <Pagination
        current={page}
        total={150}
        pageSize={pageSize}
        onChange={(pg) => setPage(pg)}
      />
    </div>
  );
}
