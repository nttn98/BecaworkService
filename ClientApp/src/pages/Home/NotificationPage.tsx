import { Button, Input, Table, DatePicker, Pagination, Modal } from "antd";
import { Select } from "antd";
import { ColumnsType } from "antd/es/table";
import React, { useEffect, useState } from "react";
import axios from "axios";
import { Link } from "react-router-dom";
import { NotificationModel } from "../../models/NotificationModel";
import dayjs from "dayjs";
import { SelectValue } from "antd/lib/select";
import {
  EyeFilled,
  EditFilled,
  DeleteFilled,
  CloseCircleTwoTone,
} from "@ant-design/icons";
import NotificationDetails from "./NotificationDetails";
import NotificationEdit from "./NotificationEdit";
import NotificationDelete from "./NotificationDelete";
import NotificationCreate from "./NotificationCreate";

export default function NotificationPage() {
  const [data, setData] = useState([]);
  const [searchText, setSearchText] = useState("");
  const [debounceSearchText, setDebounceSearchText] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalItems, setTotalItems] = useState(0);
  const [loading, setLoading] = useState(false);
  const { RangePicker } = DatePicker;
  const [fromDate, setFromDate] = useState<any>();
  const [toDate, setToDate] = useState<any>();
  const [isRead, setIsRead] = useState<SelectValue>("");
  const [isSeen, setIsSeen] = useState<SelectValue>("");
  const [sortBy, setSortBy] = useState<string>(""); //column name
  const [isSortAscending, setIsSortAscending] = useState<boolean | undefined>(
    undefined
  );
  const [modalVisible, setModalVisible] = useState(false);
  const [notificationId, setNotificationId] = useState<number>();
  const [modalType, setModalType] = useState("");

  const onSort = (column: string) => {
    if (column === sortBy) {
      if (isSortAscending == true) {
        setIsSortAscending(false);
      } else {
        setIsSortAscending(undefined); //no sort
        setSortBy("");
      }
    } else {
      setSortBy(column);
      setIsSortAscending(true);
    }
  };

  useEffect(() => {
    setCurrentPage(1); //set to page 1 when search done
  }, [debounceSearchText, isRead, isSeen, fromDate, toDate]);

  useEffect(() => {
    console.log("Updated ID:", notificationId);
  }, [notificationId]);

  useEffect(() => {
    const searchTextData = setTimeout(() => {
      setDebounceSearchText(searchText);
    }, 500);
    return () => clearTimeout(searchTextData);
  }, [searchText]);

  useEffect(() => {
    fetchData();
  }, [
    debounceSearchText,
    currentPage,
    pageSize,
    isRead,
    isSeen,
    fromDate,
    toDate,
    sortBy,
    isSortAscending,
  ]);

  const fetchData = async () => {
    setLoading(true);
    try {
      const params: any = {
        Content: debounceSearchText,
        page: currentPage,
        pageSize: pageSize,
        fromDate,
        toDate,
        sortBy,
        isSortAscending,
      };
      console.log(params);
      if (isRead !== "") {
        params.isRead = isRead;
      }

      if (isSeen !== "") {
        params.isSeen = isSeen;
      }

      const dataRes = await axios.get(`/api/Notification/GetNotifications2`, {
        params,
      });

      if (dataRes.status === 200) {
        setData(dataRes.data.items);
        setTotalItems(dataRes.data.totalItems);
      }
    } catch (e) {
      console.log(e);
    }
    setLoading(false);
  };

  const reset = () => {
    setSearchText("");
    setDebounceSearchText("");
    setFromDate(undefined);
    setToDate(undefined);
    setIsRead("");
    setIsSeen("");
  };

  const handleDate = (dates: any) => {
    if (dates) {
      setFromDate(dates[0]);
      setToDate(dates[1]);
    } else {
      setFromDate(undefined);
      setToDate(undefined);
    }
  };

  const handleSearchChange = (e: any) => {
    setSearchText(e.target.value);
  };

  const handlePageChange = (page: number, pageSize?: number) => {
    setCurrentPage(page);
    if (pageSize) {
      setPageSize(pageSize);
    }
  };

  const handleReadChange = (value: SelectValue) => {
    setIsRead(value);
  };

  const handleSeenChange = (value: SelectValue) => {
    setIsSeen(value);
  };
  const showDetailsModal = (id: number) => {
    setModalType("details");
    setModalVisible(true);
    setNotificationId(id);
  };
  const showEditModal = (id: number) => {
    setModalType("edit");
    setModalVisible(true);
    setNotificationId(id);
  };

  const showDeleteModal = (id: number) => {
    setModalType("delete");
    setModalVisible(true);
    setNotificationId(id);
  };
  const showCreateModal = () => {
    setModalType("create");
    setModalVisible(true);
  };
  const handleCancel = () => {
    setModalVisible(false);
  };
  const columns: ColumnsType<NotificationModel> = [
    {
      title: "Id",
      width: 120,
      dataIndex: "id",
      key: "1",
      sorter: true, // Enable sorting for this column
      onHeaderCell: (column) => {
        return {
          onClick: () => onSort("id"),
        };
      },
    },
    {
      title: "Created Time",
      dataIndex: "createdTime",
      key: "2",
      render: (date: string) => dayjs(date).format("DD/MM/YYYY"),
      width: 200,
      sorter: true,
      onHeaderCell: (column) => {
        return {
          onClick: () => onSort("createdTime"),
        };
      },
    },
    {
      title: "Type",
      dataIndex: "type",
      key: "3",
      width: 200,
      sorter: true,
      onHeaderCell: (column) => {
        return {
          onClick: () => onSort("type"),
        };
      },
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
      key: "7",
      render: (date: string) => dayjs(date).format("DD/MM/YYYY"),
      width: 200,
      sorter: true,
      onHeaderCell: (column) => {
        return {
          onClick: () => onSort("lastModified"),
        };
      },
    },
    {
      title: "From",
      dataIndex: "from",
      width: 200,
      key: "8",
      sorter: true,
      onHeaderCell: (column) => {
        return {
          onClick: () => onSort("from"),
        };
      },
    },
    {
      title: "Url",
      dataIndex: "url",
      key: "9",
    },
    {
      title: "Is Seen",
      dataIndex: "isSeen",
      key: "10",
      width: 100,
      render: (text: boolean) => (text ? "Yes" : "No"),
    },
    {
      title: "Action",
      key: "operation",
      fixed: "right",
      width: 150,
      render: (record: any) => {
        const id = record.id;

        return (
          <>
            <EyeFilled
              style={{ color: "blue" }}
              onClick={() => showDetailsModal(record.id)}
            />

            <EditFilled
              style={{
                color: "orange",
                marginLeft: "15px",
                marginRight: "15px",
              }}
              onClick={() => showEditModal(record.id)}
            />

            <DeleteFilled
              style={{ color: "red" }}
              onClick={() => showDeleteModal(record.id)}
            />
          </>
        );
      },
    },
  ];

  return (
    <div>
      <div className="topFunction">
        <div>
          <Input
            placeholder="Search here"
            allowClear
            size="large"
            style={{ width: 300 }}
            onChange={handleSearchChange}
            value={searchText}
          />
          <RangePicker
            format={"DD/MM/YYYY"}
            onChange={handleDate}
            size="large"
            style={{ marginLeft: "20px", paddingBottom: "6px" }}
            value={[fromDate, toDate]}
          />
          <span className="span_title"> Is Read</span>
          <Select value={isRead} onChange={handleReadChange}>
            <Select.Option value=""> -- </Select.Option>
            <Select.Option value="true">Yes</Select.Option>
            <Select.Option value="false">No</Select.Option>
          </Select>
          <span className="span_title">Is Seen</span>
          <Select value={isSeen} onChange={handleSeenChange}>
            <Select.Option value=""> -- </Select.Option>
            <Select.Option value="true"> Yes </Select.Option>
            <Select.Option value="false">No</Select.Option>
          </Select>

          <Button
            onClick={fetchData}
            type="primary"
            style={{ marginLeft: "20px" }}
          >
            Search
          </Button>
          <Button onClick={reset} type="primary" style={{ marginLeft: "20px" }}>
            Reset
          </Button>
        </div>
        <div>
          <Button
            type="primary"
            style={{ marginTop: "20px" }}
            onClick={showCreateModal}
          >
            Create New Notification
          </Button>
        </div>
      </div>
      <Table
        columns={columns}
        dataSource={data}
        rowKey={(record: NotificationModel) => record.id.toString()}
        pagination={false}
        scroll={{ x: 2500 }}
        style={{ marginTop: "20px" }}
        loading={loading}
      />
      <Pagination
        current={currentPage}
        pageSize={pageSize}
        total={totalItems}
        onChange={handlePageChange}
      />
      <Modal
        style={{ top: 20 }}
        width={800}
        open={modalVisible}
        footer={null}
        onCancel={handleCancel}
        closeIcon={
          <CloseCircleTwoTone twoToneColor="red" style={{ fontSize: "20px" }} />
        }
      >
        {modalType === "create" && (
          <NotificationCreate refresh={fetchData} handleCancel={handleCancel} />
        )}
        {modalType === "details" && (
          <NotificationDetails
            refresh={fetchData}
            notificationId={notificationId}
            handleCancel={handleCancel}
          />
        )}
        {modalType === "edit" && (
          <NotificationEdit
            refresh={fetchData}
            notificationId={notificationId}
            handleCancel={handleCancel}
          />
        )}
        {modalType === "delete" && (
          <NotificationDelete
            refresh={fetchData}
            notificationId={notificationId}
            handleCancel={handleCancel}
          />
        )}
      </Modal>
    </div>
  );
}
