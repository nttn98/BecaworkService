﻿using BecaworkService.Models;
using BecaworkService.Models.Responses;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecaworkService.Interfaces
{
    public interface IFCMTokenLogService
    {
        Task<IEnumerable<FCMTokenLog>> GetFCMTokenLogs(int page, int pageSize);
        Task<QueryResult<FCMTokenLog>> GetFCMTokenLogs2(QueryParams queryParams);
        Task<FCMTokenLog> GetFCMTokenLogByID(long ID);

        /* Task<FCMTokenLog> AddFCMTokenLog(FCMTokenLog objFCMTokenLog);*/
        Task<FCMTokenLog> UpdateFCMTokenLog(FCMTokenLog objFCMTokenLog);
        bool DeleteFCMTokenLog(long ID);

    }
}
