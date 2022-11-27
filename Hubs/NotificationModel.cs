using Microsoft.AspNetCore.SignalR;

namespace WebProjectMVC.Hubs
{
    public class NotificationModel
    {
        public string ltoUser { get; set; }
        public string ltoMessage { get; set; }    
        public string dealerUser { get; set; }
        public string dealerMessage { get; set; }
        public string connectionId { get; set; }
        public string userName { get; set; }
        public string groupName { get; set; }
       
    }
}
