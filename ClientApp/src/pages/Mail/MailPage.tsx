import {
  Button,
  Form,
  Input,
  Pagination,
  PaginationProps,
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

//const pageSize = 10;

export default function MailPage() {
  const [data, setData] = useState([]);
  const [page, setPage] = useState(1);
  const [loading, setLoading] = useState(false);
  const [totalItems, settotalItems] = useState();
  const [pageSize, setPageSize] = useState(10);

  const onShowSizeChange: PaginationProps["onShowSizeChange"] = (
    current,
    pageSize
  ) => {
    console.log(current, pageSize);
    setPageSize(pageSize);
  };

  const fetchData = async () => {
    setLoading(true);
    try {
      const result = await axios.get(
        `/api/Mail/GetMails2?page=${page}&pageSize=${pageSize}`
      );
      console.log(result);
      if (result.status === 200) {
        setData(result.data.items);
        settotalItems(result.data.totalItems);
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
      <Input.Search
        allowClear
        size="large"
        style={{ width: 500 }}
        placeholder="Search..."
      />

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
      <div className="pagination">
        <Pagination
          current={page}
          total={totalItems}
          pageSize={pageSize}
          onShowSizeChange={onShowSizeChange}
          onChange={(page) => setPage(page)}
        />
      </div>
    </div>
  );
}
