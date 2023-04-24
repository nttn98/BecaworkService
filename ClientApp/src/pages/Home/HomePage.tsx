import { Space, Table, Tag } from "antd";
import { ColumnsType } from "antd/es/table";
import React, { FC, useEffect, useState } from "react";
import axios from "axios";

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

const columns: ColumnsType<DataType> = [
  {
    title: "Id",
    dataIndex: "_id",
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
  // },
  // {
  //   title: "Created Time",
  //   width: 150,
  //   dataIndex: "createdTime",
  //   key: "2",
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
  // },
  // {
  //   title: "Last Modified",
  //   dataIndex: "lastModified",
  //   key: "5",
  //   width: 150,
  // },
  // {
  //   title: "From",
  //   dataIndex: "from",
  //   key: "6",
  //   width: 150,
  // },
  // {
  //   title: "Url",
  //   dataIndex: "url",
  //   key: "7",
  //   width: 250,
  // },
  // {
  //   title: "Is Seen",
  //   dataIndex: "isSeen",
  //   key: "7",
  //   width: 100,
  // },
  // { title: "Column 8", dataIndex: "address", key: "8" },
  // {
  //   title: "Action",
  //   key: "operation",
  //   fixed: "right",
  //   width: 100,
  //   render: () => <a>action</a>,
  // },
];

// const data: DataType[] = [];
// for (let i = 0; i < data.length; i++) {
//   data.push({
//     key: i,
//     id: data.id,
//     createdTime: "2014-12-24 23:12:00",
//     type: "QuyTrinh",
//     content: "Phiếu Họp cuối năm đã được yêu cầu xử lý",
//     isRead: 1,
//     email: "ssssssssssssssss@vntt.com.vn",
//     lastModified: "2023-02-01 16:58:09.5461927",
//     from: "Nhân viên 1",
//     url: "http://workflow.becawork.vn/workflow/myworkflow/detail/31",
//     isSeen: 1,
//   });
// }

export const HomePage = () => {
  const [data, setData] = useState([]);
  const [page, setPage] = useState(1);
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

  return (
    <div>
      <Table columns={columns} dataSource={data} scroll={{ x: 1500 }} />
    </div>
  );
};

// return (
//   <div>

//     {/* <div>
//       <ChildComponent title="Child text">
//         <div>
//           asdsadas
//           <div>asdsadsads</div>
//         </div>
//       </ChildComponent>
//     </div> */}
//   </div>
//   );
// };

// interface ChildProps {
//   title: string;
//   children: React.ReactNode;
// }

// const ChildComponent = (props: ChildProps) => {
//   const { title, children } = props;
//   return (
//     <div>
//       Hau adsadsa: {title}
//       <div>Children: {children}</div>
//     </div>
//   );
// };
