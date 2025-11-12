using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatWS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;

        public NotificationController(IHubContext<NotificationHub, INotificationClient> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Notification notification)
        {
            await _hubContext.Clients.All.ReceiveNotification(notification.Name + ": " + notification.Message);
            return Ok();
        }
    }

    public class Notification
    {
        public string Name { get; set; }
        public string Message { get; set; }
    }
}
