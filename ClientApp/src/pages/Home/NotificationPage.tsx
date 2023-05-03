import { Button, Input, InputRef, Space, Table, Tag } from "antd";
import { ColumnsType } from "antd/es/table";
import React, { FC, useEffect, useRef, useState } from "react";
import axios from "axios";
import { Link } from "react-router-dom";
import { NotificationModel } from "../../models/NotificationModel";

type DataIndex = keyof NotificationModel;
export default function NotificationPage() {
  const [data, setData] = useState([]);
  const [searchText, setSearchText] = useState("");

  const loadData = () => {
    axios
      .get("https://localhost:5001/api/Notification/GetNotifications")
      .then((res) => {
        if (res) {
          setData(res.data);
        }
      });
  };

  useEffect(() => {
    loadData();
  }, []);

  const columns: ColumnsType<NotificationModel> = [
    {
      title: "Id",
      width: 50,
      dataIndex: "id",
      key: "1",
      fixed: "left",
    },
    {
      title: "Created Time",
      width: 150,
      dataIndex: "createdTime",
      key: "2",
    },
    {
      title: "Type",
      dataIndex: "type",
      key: "3",
    },
    {
      title: "Content",
      dataIndex: "content",
      key: "4",
    },
    {
      title: "Is Read",
      dataIndex: "isRead",
      key: "5",
      width: 100,
      render: (text: boolean) => (text ? "Yes" : "No"),
    },
    {
      title: "Email",
      dataIndex: "email",
      key: "6",
      width: 250,
    },
    {
      title: "Last Modified",
      dataIndex: "lastModified",
      key: "5",
    },
    {
      title: "From",
      dataIndex: "from",
      key: "6",
    },
    {
      title: "Url",
      dataIndex: "url",
      key: "7",
    },
    {
      title: "Is Seen",
      dataIndex: "isSeen",
      key: "7",
      width: 100,
      render: (text: boolean) => (text ? "Yes" : "No"),
    },
    {
      title: "Action",
      key: "operation",
      fixed: "right",
      render: (record: any) => {
        const id = record.id;
        return (
          <>
            <Link to={`/notification/details/${id}`}> Details </Link> |
            <Link to={`/notification/edit/${id}`}> Edit </Link>
          </>
        );
      },
    },
  ];

  const reset = () => {
    loadData();
    setSearchText("");
  };

  const handleChange = (e: any) => {
    setSearchText(e.target.value);
    if (e.target.value === "") {
      loadData();
    }
  };
  const globalSearch = () => {
    const filteredData = data.filter((item: NotificationModel) => {
      const rowValues = Object.values(item).join("").toLowerCase();
      const searchTextLowerCase = searchText.toLowerCase();
      return rowValues.includes(searchTextLowerCase);
    });
    setData(filteredData);
  };
  return (
    <div>
      <Input
        placeholder="Search here"
        allowClear
        size="large"
        style={{ width: 500 }}
        onChange={handleChange}
        value={searchText}
      />
      <Button
        onClick={globalSearch}
        type="primary"
        style={{ marginLeft: "20px" }}
      >
        Search
      </Button>
      <Button onClick={reset} type="primary" style={{ marginLeft: "20px" }}>
        Reset
      </Button>
      <Link to={"/notification/create"}>
        <Button type="primary" style={{ marginLeft: "20px" }}>
          Create New Notification
        </Button>
      </Link>

      <Table
        columns={columns}
        dataSource={data}
        scroll={{ x: 1500 }}
        style={{ marginTop: "20px" }}
      />
    </div>
  );
}
