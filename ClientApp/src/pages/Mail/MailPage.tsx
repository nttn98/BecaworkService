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
  DatePicker,
  Popconfirm,
} from "antd";
import { ColumnsType } from "antd/es/table";
import React, { FC, useEffect, useMemo, useState } from "react";
import axios from "axios";
import { Label } from "reactstrap";
import { Link } from "react-router-dom";
import { MailModel } from "../../models/MailModel";
import { parse } from "path";
import { type } from "os";
import dayjs from "dayjs";
import moment from "moment";
import { debug } from "console";
import { RangePickerProps } from "antd/es/date-picker";

const { RangePicker } = DatePicker;

export default function MailPage() {
  const [data, setData] = useState([]);
  const [page, setPage] = useState(1);
  const [loading, setLoading] = useState(false);
  const [totalItems, settotalItems] = useState();
  const [pageSize, setPageSize] = useState(10);
  const [searchText, setSearchText] = useState<string | undefined>();

  const [sortBy, setSortBy] = useState<any>();
  const [sortOrder, setSortOrder] = useState<boolean | "">();

  const [isSend, setisSend] = useState<string | undefined>();

  const [fromDate, setFromDate] = useState<any>();
  const [toDate, setToDate] = useState<any>();

  const DATE_FORMAT = "YYYY-MM-DDTHH:mm:ss.sss";

  const handTitleClick = (column: string) => {
    if (sortBy === column && sortOrder == true) {
      setSortOrder(false);
    } else {
      setSortBy(column);
      setSortOrder(true);
    }
  };
  const columns: ColumnsType<MailModel> = [
    {
      title: "Id",
      dataIndex: "id",
      onHeaderCell: () => ({
        onClick: () => handTitleClick("id"),
      }),
    },
    {
      title: "Email",
      dataIndex: "email",
      onHeaderCell: () => ({
        onClick: () => handTitleClick("email"),
      }),
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
      title: "Create time",
      dataIndex: "createTime",
      render: (text: string) =>
        text ? dayjs(text).format("YYYY-MM-DD") : null,
    },
    {
      title: "Send time",
      dataIndex: "sendTime",
      render: (text: string) =>
        text ? dayjs(text).format("YYYY-MM-DD") : null,
    },
    {
      title: "Create by",
      dataIndex: "createBy",
      onHeaderCell: () => ({
        onClick: () => handTitleClick("createBy"),
      }),
    },
    {
      title: "Action",
      key: "action",
      render: (_, record) => (
        <Space size="middle">
          <Link to={"/mail/" + record.id}>Detail</Link>
          <Link to={"/mail/update/" + record.id}>Update</Link>
          <Popconfirm
            title="Delete the mail"
            description="Are you sure to delete this mail?"
            onConfirm={() => handleDelete(record.id)}
            okText="Yes"
            cancelText="No"
          >
            <Button type="link">Delete</Button>
          </Popconfirm>
          {record.isSend === false && (
            <Button type="link" onClick={() => handleResend(record.id)}>
              Resend
            </Button>
          )}
        </Space>
      ),
    },
  ];

  const handleDelete = (mailID: number) => {
    try {
      axios
        .delete(`/api/Mail/DeleteMail/${mailID}`)
        .then((response) => console.log(response));
      message.success(`Delete successful`);
      console.log(`delete success id :${mailID}`);
      fetchData();
    } catch {
      message.error("delete error");
    }
  };
  const handleResend = async (mailID: number) => {
    setLoading(true);
    try {
      console.log(mailID);
      await axios.post(`/api/Mail/SendMailBySMTP/${mailID}`);
      message.success("Resend Mail Successfully");
      fetchData();
    } catch {
      message.error("Resend loi");
    }
    setLoading(false);
  };

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
      const result = await axios.get(`/api/Mail/GetMails`, { params });
      setData(result.data.items);
      settotalItems(result.data.totalItems);
    } catch {
      message.error("Loi");
    }
    setLoading(false);
  };

  const onChangeIsSend = async (value: string) => {
    setisSend(value);
  };

  const handleDateChange = async (dates: any) => {
    setFromDate(dates[0]);
    setToDate(dates[1]);
  };

  function onReset() {
    setSortBy(undefined);
    setSortOrder(undefined);
    setSearchText(undefined);
    setisSend(undefined);
    setFromDate(undefined);
    setToDate(undefined);
  }

  const params = {
    page,
    pageSize,
    Content: searchText,
    sortBy,
    isSortAscending: sortOrder,
    isSend,
    fromDate,
    toDate,
  };

  useEffect(() => {
    fetchData();
  }, [fromDate, toDate, sortOrder, sortBy, isSend, page, pageSize]);

  useEffect(() => {
    const debounceTimer = setTimeout(() => {
      fetchData();
    }, 300);
    return () => clearTimeout(debounceTimer);
  }, [searchText]);

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
      <Button type="primary">
        <Link to={"/mail/create"}>Create new mail</Link>
      </Button>
      <Select
        style={{ width: 100, margin: 10 }}
        placeholder="Select a person"
        optionFilterProp="children"
        onChange={onChangeIsSend}
        value={isSend}
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

      <RangePicker
        format={"YYYY-MM-DD"}
        onChange={handleDateChange}
        value={[fromDate, toDate]}
      />

      <Button onClick={onReset}> reset</Button>

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
