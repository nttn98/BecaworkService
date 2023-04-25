import { Col, Divider, Row } from "antd";
import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { NotificationModel } from "../../models/NotificationModel";
import axios from "axios";

export default function NotificationDetails() {
  const { id } = useParams();
  const [data, setData] = useState<NotificationModel | undefined>();

  useEffect(() => {
    axios
      .get(
        `https://64477eaa7bb84f5a3e402b8e.mockapi.io/api/data/notificationdetails/` +
          id
      )
      .then((res) => {
        if (res) {
          setData(res.data);
        }
      });
  }, []);

  return (
    <div>
      <h1>Notification Details</h1>
      <Divider />
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
              Created Time
            </Col>
            <Col span={4}>{data.createdTime}</Col>
          </Row>
          <Row>
            <Col span={4} className="title-details">
              Type
            </Col>
            <Col span={4}>{data.type}</Col>
          </Row>
          <Row>
            <Col span={4} className="title-details">
              Content
            </Col>
            <Col span={4}>{data.content}</Col>
          </Row>
          <Row>
            <Col span={4} className="title-details">
              Is Read
            </Col>
            <Col span={4}>{data.isRead ? "Yes" : "No"}</Col>
          </Row>
          <Row>
            <Col span={4} className="title-details">
              Email
            </Col>
            <Col span={4}>{data.email}</Col>
          </Row>
          <Row>
            <Col span={4} className="title-details">
              Last Modified
            </Col>
            <Col span={4}>{data.lastModified}</Col>
          </Row>
          <Row>
            <Col span={4} className="title-details">
              From
            </Col>
            <Col span={4}>{data.from}</Col>
          </Row>
          <Row>
            <Col span={4} className="title-details">
              Url
            </Col>
            <Col span={4}>{data.url}</Col>
          </Row>
          <Row>
            <Col span={4} className="title-details">
              Is Seen
            </Col>
            <Col span={4}>{data.isSeen ? "Yes" : "No"}</Col>
          </Row>
          <div className="under-action"></div>
        </div>
      ) : (
        <p>Not found</p>
      )}
    </div>
  );
}
