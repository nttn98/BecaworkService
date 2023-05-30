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

const { RangePicker } = DatePicker;

export default function MailPage() {
  const [data, setData] = useState([]);
  const [page, setPage] = useState(1);
  const [loading, setLoading] = useState(false);
  const [totalItems, settotalItems] = useState();
  const [pageSize, setPageSize] = useState(10);
  const [searchText, setSearchText] = useState<string | undefined>();
  const [isSend, setisSend] = useState<string | undefined>();
  const [sortBy, setSortBy] = useState<string | undefined>();
  const [isAscend, setisAscend] = useState<boolean | undefined>();

  const [rangeDate, setRangeDate] = useState([] || "");
  const [fromDate, setFromDate] = useState("");
  const [toDate, setToDate] = useState("");
  const [dropdownOptions, setDropdownOptions] = useState([]);

  const columns: ColumnsType<MailModel> = [
    {
      title: "Id",
      dataIndex: "id",

      onHeaderCell: () => ({
        onClick: () => handleTitleClick("id"),
      }),
    },
    {
      title: "Email",
      dataIndex: "email",

      onHeaderCell: () => ({
        onClick: () => handleTitleClick("email"),
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

  const handleTitleClick = (column: string) => {
    setSortBy(column);
    console.log(sortBy);
    console.log(isAscend);
    if (sortBy === column && isAscend == true) {
      setisAscend(false);
    } else {
      setSortBy(column);
      setisAscend(true);
      console.log(isAscend);
    }
  };

  const onShowSizeChange: PaginationProps["onShowSizeChange"] = (
    current,
    pageSize
  ) => {
    console.log(current, pageSize);
    setPageSize(pageSize);
  };

  const params = {
    page,
    pageSize,
    Content: searchText,
    isSend,
    sortBy: sortBy,
    IsSortAscending: isAscend,
    //createTime: `${fromDate} ${toDate}`,
  };

  const handleInputChange = async () => {
    setLoading(true);
    console.log(isSend);
    try {
      const result = await axios.get("/api/Mail/GetMails2", {
        params,
      });
      setData(result.data.items);
      settotalItems(result.data.totalItems);
      console.log(result);
    } catch {
      message.error("Loi");
    }
    setLoading(false);
  };

  const handleDateChange = (dates: any) => {
    if (dates && dates.length === 2) {
      const [start, end] = dates;
      setFromDate(dayjs(start).format("YYYY-MM-DDTHH:mm:ss.sss"));
      setToDate(dayjs(end).format("YYYY-MM-DDTHH:mm:ss.sss"));
      console.log(fromDate);
      console.log(toDate);
    } else {
      setFromDate("");
      setToDate("");
    }
    console.log(fromDate);
    console.log(toDate);
  };

  function onReset() {
    setSortBy(undefined);
    setisAscend(undefined);
    setSearchText(undefined);
    setisSend(undefined);
    handleDateChange([]);
    handleInputChange();
  }

  const onChange = async (value: string) => {
    setisSend(value);
  };

  const onSearch = (value: string) => {
    console.log("search:", value);
  };

  useEffect(() => {
    handleInputChange();
  }, [fromDate, toDate, sortBy, isSend, searchText, page, pageSize, isAscend]);

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
      <Button onClick={onReset} type="primary">
        {" "}
        reset
      </Button>
      <RangePicker onChange={handleDateChange} />
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
