using Intranet.Data;
using Intranet.Hubs;
using Intranet.Models;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using TableDependency.SqlClient.Base.Abstracts;
using TableDependency.SqlClient.Exceptions;

namespace Intranet.SubscribeTableDependencies
{
    public class SubscribeNotificationTableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<Notification> tableDependency;
        ConnectionHub connectionHub;
        private readonly ApplicationDbContext _db;

        public SubscribeNotificationTableDependency(ConnectionHub connectionHub, ApplicationDbContext db)
        {
            this.connectionHub = connectionHub;
            _db = db;
        }


        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<Notification>(connectionString, "Notifications");
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }


        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(HubConnection)} SqlTableDependency error: {e.Error.Message}");
            throw e.Error;
        }

        private async void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Notification> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                var notification = e.Entity;
                await connectionHub.SendNotificationToAll(notification.Id);
            }
        }
    }
}
