using BecaworkService.Models;
using BecaworkService.Models.Responses;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecaworkService.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationResponse> GetNotifications(int page, int pageSize);
        /*Task<NotificationResponse> GetNotifications1(QueryParams queryParams);*/
        Task<QueryResult<Notification>> GetNotifications2(QueryParams queryParams);
        Task<Notification> GetNotificationByID(long ID);
        Task<Notification> AddNotifi(Notification objNotifi);
        Task<Notification> UpdateNotifi(Notification objNotifi);

        bool DeleteNotifi(long ID);

    }
}
