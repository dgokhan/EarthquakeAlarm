using System;
using System.Threading.Tasks;
using Shiny.Notifications;

namespace DepremAlarmi.Controls.Services
{
    public class CoreDelegateServices
    {
        public CoreDelegateServices(SqliteConnection conn,
                                    INotificationManager notifications
           )
        {
            Connection = conn;
            Notifications = notifications;
        }

        public SqliteConnection Connection { get; }
        public INotificationManager Notifications { get; }

        public async Task SendNotification(string title, string message)
        {
            await Notifications.Send(title, message);
        }
    }
}
