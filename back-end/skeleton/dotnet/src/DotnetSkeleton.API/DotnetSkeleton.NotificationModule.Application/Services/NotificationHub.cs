using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace DotnetSkeleton.NotificationModule.Application.Services
{
    public class NotificationHub : Hub
    {
        private readonly SignalROptions _signalROptions;

        public NotificationHub(IOptions<SignalROptions> signalROptions)
        {
            _signalROptions = signalROptions.Value;
        }

        public async Task SendNotificationAsync(string notification)
        {
            await Clients.All.SendAsync(_signalROptions.Channels.NotificationChannel, notification);
        }
    }
}