export type MailModel = {
    id: bigint;
    email: string;
    emailContent: string
    subject: string;
    createby: string;
    createTime :string;
    isSend : boolean;
    sendTime : string;
    sentStatus : string;
    emailCC : string;
    emailBCC: string;
    fromDate: string;
    toDate : string;
    mailType: bigint;
  };