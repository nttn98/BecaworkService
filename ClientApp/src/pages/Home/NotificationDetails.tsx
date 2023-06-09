import { Button, Col, Divider, Row } from "antd";
import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { NotificationModel } from "../../models/NotificationModel";
import dayjs from "dayjs";
import axios from "axios";

interface Props {
  notificationId?: number;
  refresh: any;
  handleCancel: any;
}

export default function NotificationDetails({
  notificationId,
  refresh,
  handleCancel,
}: Props) {
  const [data, setData] = useState<NotificationModel | undefined>();
  const formatDate = (date: string) => {
    return dayjs(date).format("DD/MM/YY HH:mm:ss");
  };

  const fetchData = (notificationId: number) => {
    axios
      .get(
        `https://localhost:5001/api/Notification/GetNotificationByID/` +
          notificationId
      )
      .then((res) => {
        if (res) {
          setData(res.data);
        }
      });
  };

  useEffect(() => {
    if (notificationId) {
      fetchData(notificationId);
    }
  }, [notificationId]);

  return (
    <div>
      <h1>Notification Details</h1>
      <Divider />
      {data ? (
        <div className="showDetails">
          <Row>
            <Col
              xs={{ span: 5, offset: 1 }}
              lg={{ span: 6, offset: 2 }}
              className="title-details"
            >
              ID
            </Col>
            <Col xs={{ span: 12, offset: 1 }} lg={{ span: 12, offset: 1 }}>
              {notificationId}
            </Col>
          </Row>
          <Divider />
          <Row>
            <Col
              className="title-details"
              xs={{ span: 5, offset: 1 }}
              lg={{ span: 6, offset: 2 }}
            >
              Created Time
            </Col>
            <Col xs={{ span: 12, offset: 1 }} lg={{ span: 12, offset: 1 }}>
              {formatDate(data.createdTime)}
            </Col>
          </Row>
          <Divider />
          <Row>
            <Col
              xs={{ span: 5, offset: 1 }}
              lg={{ span: 6, offset: 2 }}
              className="title-details"
            >
              Type
            </Col>
            <Col xs={{ span: 12, offset: 1 }} lg={{ span: 12, offset: 1 }}>
              {data.type}
            </Col>
          </Row>
          <Divider />
          <Row>
            <Col
              xs={{ span: 5, offset: 1 }}
              lg={{ span: 6, offset: 2 }}
              className="title-details"
            >
              Content
            </Col>
            <Col xs={{ span: 12, offset: 1 }} lg={{ span: 12, offset: 1 }}>
              {data.content}
            </Col>
          </Row>
          <Divider />
          <Row>
            <Col
              xs={{ span: 5, offset: 1 }}
              lg={{ span: 6, offset: 2 }}
              className="title-details
             "
            >
              Is Read
            </Col>
            <Col xs={{ span: 12, offset: 1 }} lg={{ span: 12, offset: 1 }}>
              {data.isRead ? "Yes" : "No"}
            </Col>
          </Row>
          <Divider />
          <Row>
            <Col
              xs={{ span: 5, offset: 1 }}
              lg={{ span: 6, offset: 2 }}
              className="title-details"
              span={6}
            >
              Email
            </Col>
            <Col xs={{ span: 12, offset: 1 }} lg={{ span: 12, offset: 1 }}>
              {data.email}
            </Col>
          </Row>
          <Divider />
          <Row>
            <Col
              xs={{ span: 5, offset: 1 }}
              lg={{ span: 6, offset: 2 }}
              className="title-details"
              span={6}
            >
              Last Modified
            </Col>
            <Col xs={{ span: 12, offset: 1 }} lg={{ span: 12, offset: 1 }}>
              {formatDate(data.lastModified)}
            </Col>
          </Row>
          <Divider />
          <Row>
            <Col
              xs={{ span: 5, offset: 1 }}
              lg={{ span: 6, offset: 2 }}
              className="title-details"
              span={6}
            >
              From
            </Col>
            <Col xs={{ span: 12, offset: 1 }} lg={{ span: 12, offset: 1 }}>
              {data.from}
            </Col>
          </Row>
          <Divider />
          <Row>
            <Col
              xs={{ span: 5, offset: 1 }}
              lg={{ span: 6, offset: 2 }}
              className="title-details"
              span={6}
            >
              Url
            </Col>
            <Col xs={{ span: 12, offset: 1 }} lg={{ span: 12, offset: 1 }}>
              {data.url}
            </Col>
          </Row>
          <Divider />
          <Row>
            <Col
              xs={{ span: 5, offset: 1 }}
              lg={{ span: 6, offset: 2 }}
              className="title-details"
              span={6}
            >
              Is Seen
            </Col>
            <Col xs={{ span: 12, offset: 1 }} lg={{ span: 12, offset: 1 }}>
              {data.isSeen ? "Yes" : "No"}
            </Col>
          </Row>
          <Divider />
        </div>
      ) : (
        <p>Not found</p>
      )}
    </div>
  );
}
