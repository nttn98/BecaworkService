import React, { useState } from "react";
import { useParams } from "react-router-dom";
import Table, { ColumnsType } from "antd/es/table";
import { MailModel } from "../../models/MailModel";
import { Col, Divider, Row, Space } from "antd";
import { Link } from "react-router-dom";
import { useEffect } from "react";
import axios from "axios";
import moment from "moment";

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
    title: "Create time",
    dataIndex: "createTime",
  },
  {
    title: "Is sent",
    dataIndex: "isSend",
  },
  {
    title: "Send time",
    dataIndex: "sendTime",
  },
  {
    title: "Send status",
    dataIndex: "sendStatus",
  },
];

export const MailDetailPage = () => {
  const { id } = useParams();
  const [data, setData] = useState<MailModel | undefined>();

  useEffect(() => {
    axios
      .get("https://localhost:5001/api/Mail/GetMailByID/" + id)
      .then((res) => {
        console.log(res.data);
        if (res) {
          setData(res.data);
        }
      });
  }, []);

  return (
    <div>
      <h1>Mail Details</h1>
      {data ? (
        <div className="showDetails">
          <Row>
            <Col span={4} className="title-details">
              ID
            </Col>
            <Col span={4}>{id}</Col>
          </Row>
          <Row>
            <Col span={4} className="title-details">
              Email
            </Col>
            <Col span={4}>{data.email}</Col>
          </Row>
          <Row>
            <Col span={4} className="title-details">
              Subject
            </Col>
            <Col span={4}>{data.subject}</Col>
          </Row>
          <Row>
            <Col span={4} className="title-details">
              Create By
            </Col>
            <Col span={4}>{data.createby}</Col>
          </Row>
          <Row>
            <Col span={4} className="title-details">
              Create Time
            </Col>
            <Col span={4}>
              {moment(data.createTime).format("DD/MM/YYYY HH:mm:ss")}
            </Col>
          </Row>
          {/* <Row>
            <Col span={4} className="title-details">
              Content
            </Col>
            <Col span={4}>{data.emailContent}</Col>
          </Row> */}
          <Row>
            <Col span={4} className="title-details">
              Send time
            </Col>
            <Col span={4}>
              {moment(data.sendTime).format("DD/MM/YYYY HH:mm:ss")}
            </Col>
          </Row>
          <Row>
            <Col span={4} className="title-details">
              Is Read
            </Col>
            <Col span={4}>{data.isSend ? "Yes" : "No"}</Col>
          </Row>

          <Row>
            <Col span={4} className="title-details">
              Send status
            </Col>
            <Col span={4}>{data.sentStatus}</Col>
          </Row>
          <Row>
            <Col span={4} className="title-details">
              Mail type
            </Col>
            <Col span={4}>{data.mailType}</Col>
          </Row>
          <div className="under-action"></div>
        </div>
      ) : (
        <p>Not found</p>
      )}
    </div>
  );
};
