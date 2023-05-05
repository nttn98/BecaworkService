import { Button, Input, Space, Table, Tag } from "antd";
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

export default function MailPage() {
  const [data, setData] = useState([]);
  const [page, setPage] = useState(1);

  useEffect(() => {
    axios.get("https://localhost:5001/api/Mail/GetMails").then((res) => {
      console.log(res.data);
      if (res) {
        setData(res.data);
      }
    });
  }, []);

  return (
    <div>
      <Table columns={columns} dataSource={data} scroll={{ x: 1500 }} />
    </div>
  );
}
