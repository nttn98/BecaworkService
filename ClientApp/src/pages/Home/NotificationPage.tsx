import { Button, Input, InputRef, Space, Table, Tag } from "antd";
import { ColumnsType } from "antd/es/table";
import React, { FC, useEffect, useRef, useState } from "react";
import axios from "axios";
import { ColumnType, FilterConfirmProps } from "antd/es/table/interface";
import { SearchOutlined } from "@ant-design/icons";
import Highlighter from "react-highlight-words";

interface DataType {
  id: string;
  name: string;
  trips: string;
  // key: number;
  // id: number;
  // createdTime: string;
  // type: string;
  // content: string;
  // isRead: number;
  // email: string;
  // lastModified: string;
  // from: string;
  // url: string;
  // isSeen: number;
}

type DataIndex = keyof DataType;
export default function NotificationPage() {
  const [data, setData] = useState([]);
  // const [page, setPage] = useState(1);

  const [searchText, setSearchText] = useState("");
  const [searchedColumn, setSearchedColumn] = useState("");
  const searchInput = useRef<InputRef>(null);

  useEffect(() => {
    axios
      .get("https://api.instantwebtools.net/v1/passenger?page=0&size=10")
      .then((res) => {
        console.log(res.data);
        if (res) {
          setData(res.data.data);
        }
      });
  }, []);
  const handleSearch = (
    selectedKeys: string[],
    confirm: (param?: FilterConfirmProps) => void,
    dataIndex: DataIndex
  ) => {
    confirm();
    setSearchText(selectedKeys[0]);
    setSearchedColumn(dataIndex);
  };

  const handleReset = (clearFilters: () => void) => {
    clearFilters();
    setSearchText("");
  };

  const getColumnSearchProps = (
    dataIndex: DataIndex
  ): ColumnType<DataType> => ({
    filterDropdown: ({
      setSelectedKeys,
      selectedKeys,
      confirm,
      clearFilters,
      close,
    }) => (
      <div style={{ padding: 8 }} onKeyDown={(e) => e.stopPropagation()}>
        <Input
          ref={searchInput}
          placeholder={`Search ${dataIndex}`}
          value={selectedKeys[0]}
          onChange={(e) =>
            setSelectedKeys(e.target.value ? [e.target.value] : [])
          }
          onPressEnter={() =>
            handleSearch(selectedKeys as string[], confirm, dataIndex)
          }
          style={{ marginBottom: 8, display: "block" }}
        />
        <Space>
          <Button
            type="primary"
            onClick={() =>
              handleSearch(selectedKeys as string[], confirm, dataIndex)
            }
            icon={<SearchOutlined />}
            size="small"
            style={{ width: 90 }}
          >
            Search
          </Button>
          <Button
            onClick={() => clearFilters && handleReset(clearFilters)}
            size="small"
            style={{ width: 90 }}
          >
            Reset
          </Button>
          <Button
            type="link"
            size="small"
            onClick={() => {
              confirm({ closeDropdown: false });
              setSearchText((selectedKeys as string[])[0]);
              setSearchedColumn(dataIndex);
            }}
          >
            Filter
          </Button>
          <Button
            type="link"
            size="small"
            onClick={() => {
              close();
            }}
          >
            close
          </Button>
        </Space>
      </div>
    ),
    filterIcon: (filtered: boolean) => (
      <SearchOutlined style={{ color: filtered ? "#1890ff" : undefined }} />
    ),
    onFilter: (value, record) =>
      record[dataIndex]
        .toString()
        .toLowerCase()
        .includes((value as string).toLowerCase()),
    onFilterDropdownOpenChange: (visible) => {
      if (visible) {
        setTimeout(() => {
          if (searchInput.current) {
            searchInput.current.select();
          }
        }, 100);
      }
    },
    render: (text) =>
      searchedColumn === dataIndex ? (
        <Highlighter
          highlightStyle={{ backgroundColor: "#ffc069", padding: 0 }}
          searchWords={[searchText]}
          autoEscape
          textToHighlight={text ? text.toString() : ""}
        />
      ) : (
        text
      ),
  });

  const columns: ColumnsType<DataType> = [
    {
      title: "Id",
      dataIndex: "_id",
      ...getColumnSearchProps(_id),
    },
    {
      title: "Name",
      dataIndex: "name",
    },
    {
      title: "Trips",
      dataIndex: "trips",
    },
    // {
    //   title: "Id",
    //   width: 50,
    //   dataIndex: "id",
    //   key: "1",
    // ...getColumnSearchProps('id'),
    // },
    // {
    //   title: "Created Time",
    //   width: 150,
    //   dataIndex: "createdTime",
    //   key: "2",
    //  ...getColumnSearchProps('createdTime'),
    // },
    // {
    //   title: "Type",
    //   dataIndex: "type",
    //   key: "3",
    //   width: 100,
    // },
    // {
    //   title: "Content",
    //   dataIndex: "content",
    //   key: "4",
    //   width: 200,
    //...getColumnSearchProps('content'),
    // },
    // {
    //   title: "Is Read",
    //   dataIndex: "isRead",
    //   key: "5",
    //   width: 100,
    // },
    // {
    //   title: "Email",
    //   dataIndex: "email",
    //   key: "6",
    //   width: 250,
    //...getColumnSearchProps('email'),
    // },
    // {
    //   title: "Last Modified",
    //   dataIndex: "lastModified",
    //   key: "5",
    //   width: 150,
    //...getColumnSearchProps('lastModified'),
    // },
    // {
    //   title: "From",
    //   dataIndex: "from",
    //   key: "6",
    //   width: 150,
    //...getColumnSearchProps('from'),
    // },
    // {
    //   title: "Url",
    //   dataIndex: "url",
    //   key: "7",
    //   width: 250,
    //...getColumnSearchProps('url'),
    // },
    // {
    //   title: "Is Seen",
    //   dataIndex: "isSeen",
    //   key: "7",
    //   width: 100,
    //...getColumnSearchProps('isSeen'),
    // },
    // {
    //   title: "Action",
    //   key: "operation",
    //   fixed: "right",
    //   width: 100,
    //   render: () => <a>action</a>,
    // },
  ];
  return (
    <div>
      <Table columns={columns} dataSource={data} scroll={{ x: 1500 }} />
    </div>
  );
}
