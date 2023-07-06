import { Alert, Button, Col, Divider, Popconfirm, Row, message } from "antd";
import React, { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { NotificationModel } from "../../models/NotificationModel";
import axios from "axios";
import dayjs from "dayjs";
interface Props {
  notificationId?: number;
  refresh: any;
  handleCancel: any;
}

export default function NotificationDelete({
  notificationId,
  refresh,
  handleCancel,
}: Props) {
  const [data, setData] = useState<NotificationModel | undefined>();
  const navigate = useNavigate();
  const formatDate = (date: string) => {
    return dayjs(date).format("DD/MM/YY HH:mm:ss");
  };
  const confirm = (
    e: React.MouseEvent<HTMLElement, MouseEvent> | undefined
  ) => {
    axios
      .delete(`api/Notification/DeleteNofiti?ID=` + notificationId)
      .then(() => {
        message.success("Delete Notification Successful");
        refresh();
        handleCancel();
      });
  };

  const cancel = (e: React.MouseEvent<HTMLElement, MouseEvent> | undefined) => {
    message.error("Canceled");
  };

  useEffect(() => {
    axios
      .get(`api/Notification/GetNotificationByID/` + notificationId)
      .then((res) => {
        if (res) {
          setData(res.data);
        }
      });
  }, []);

  return (
    <div>
      <h1>Notification Delete</h1>
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
              xs={{ span: 5, offset: 1 }}
              lg={{ span: 6, offset: 2 }}
              className="title-details"
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
              className="title-details"
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
            >
              Is Seen
            </Col>
            <Col xs={{ span: 12, offset: 1 }} lg={{ span: 12, offset: 1 }}>
              {data.isSeen ? "Yes" : "No"}
            </Col>
          </Row>
          <Divider />
          <div className="under-action"></div>
        </div>
      ) : (
        <p>Not found</p>
      )}
      <div className="under-action">
        <Popconfirm
          title="Delete notification"
          description="Are you sure to delete this notification?"
          onConfirm={confirm}
          onCancel={cancel}
          okText="Yes"
          cancelText="No"
        >
          <Button type="primary" style={{ marginRight: "10px" }} danger>
            Delete
          </Button>
        </Popconfirm>
      </div>
    </div>
  );
}
