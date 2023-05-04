using BecaworkService.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecaworkService.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationResponse> GetNotifications(int page, int pageSize);
        Task<Notification> GetNotificationByID(long ID);
        Task<Notification> AddNotifi(Notification objNotifi);
        Task<Notification> UpdateNotifi(Notification objNotifi);

        bool DeleteNotifi(long ID);

    }
}
