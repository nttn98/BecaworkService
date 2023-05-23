import {
  Button,
  Form,
  Input,
  Pagination,
  PaginationProps,
  Select,
  Space,
  Table,
  Tag,
  message,
} from "antd";
import { ColumnsType } from "antd/es/table";
import React, { FC, useEffect, useMemo, useState } from "react";
import axios from "axios";
import { Label } from "reactstrap";
import { Link } from "react-router-dom";
import { MailModel } from "../../models/MailModel";
import { parse } from "path";
import { type } from "os";

const columns: ColumnsType<MailModel> = [
  {
    title: "Id",
    dataIndex: "id",
    sorter: (record1, record2) => record1.id - record2.id,
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
    title: "IsSend",
    dataIndex: "isSend",
    render: (text: boolean) => (text ? "Yes" : "No"),
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
        <Link to={"/mail/update/" + record.id}>Update</Link>
      </Space>
    ),
  },
];

export default function MailPage() {
  const [data, setData] = useState([]);
  const [page, setPage] = useState(1);
  const [loading, setLoading] = useState(false);
  const [totalItems, settotalItems] = useState();
  const [pageSize, setPageSize] = useState(10);
  const [searchText, setSearchText] = useState("");
  const [isSend, setisSend] = useState("");

  const onShowSizeChange: PaginationProps["onShowSizeChange"] = (
    current,
    pageSize
  ) => {
    console.log(current, pageSize);
    setPageSize(pageSize);
  };

  const handleInputChange = async (e: string) => {
    setLoading(true);
    setSearchText(e);
    console.log(isSend);
    try {
      const result = await axios.get(
        `/api/Mail/GetMails2?page=${page}&pageSize=${pageSize}&Content=${searchText}&isSend=${isSend}`
      );
      setData(result.data.items);
      settotalItems(result.data.totalItems);
      console.log(result);
    } catch {
      message.error("Loi");
    }
    setLoading(false);
    ``;
  };

  const param = async (value: string) => {};

  const onChange = async (value: string) => {
    setisSend(value);
  };

  const onSearch = (value: string) => {
    console.log("search:", value);
  };

  useEffect(() => {
    handleInputChange(searchText);
  }, [isSend, searchText, page, pageSize]);

  return (
    <div>
      <Input.Search
        allowClear
        size="large"
        style={{ width: 500, margin: 10 }}
        placeholder="Search..."
        value={searchText}
        onChange={(e) => setSearchText(e.target.value)}
        type="text"
      />

      <Select
        showSearch
        style={{ width: 100, margin: 10 }}
        placeholder="Select a person"
        optionFilterProp="children"
        onChange={onChange}
        onSearch={onSearch}
        filterOption={(input, option) =>
          (option?.label ?? "").toLowerCase().includes(input.toLowerCase())
        }
        options={[
          {
            value: "",
            label: "All ",
          },
          {
            value: true,
            label: "Yes",
          },
          {
            value: false,
            label: "No",
          },
        ]}
      />

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
          total={totalItems || 0}
          pageSize={pageSize}
          onShowSizeChange={onShowSizeChange}
          onChange={(page) => setPage(page)}
        />
      </div>
    </div>
  );
}
