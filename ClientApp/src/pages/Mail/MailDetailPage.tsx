import React, { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Table, { ColumnsType } from "antd/es/table";
import { MailModel } from "../../models/MailModel";
import { Button, Col, Divider, Row, Space } from "antd";
import { Link } from "react-router-dom";
import { useEffect } from "react";
import axios from "axios";
import moment from "moment";

export const MailDetailPage = () => {
  const { id } = useParams();
  const [data, setData] = useState<MailModel | undefined>();
  let navigate = useNavigate();

  useEffect(() => {
    axios.get("/api/Mail/GetMailByID/" + id).then((res) => {
      console.log(res.data);
      if (res) {
        setData(res.data);
      }
    });
  }, []);

  return (
    <div className="detailPage">
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
              Is Send
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
      <Button
        className="detailPage-btn"
        type="primary"
        onClick={() => navigate(-1)}
      >
        Back
      </Button>
    </div>
  );
};
