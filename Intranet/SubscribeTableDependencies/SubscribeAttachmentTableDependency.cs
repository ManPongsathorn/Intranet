using Intranet.Data;
using Intranet.Hubs;
using Intranet.Models;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Exceptions;

namespace Intranet.SubscribeTableDependencies
{
    public class SubscribeAttachmentTableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<Log_Attachment> tableDependency;
        ConnectionHub connectionHub;
        private readonly ApplicationDbContext _db;

        public SubscribeAttachmentTableDependency(ConnectionHub connectionHub, ApplicationDbContext db)
        {
            this.connectionHub = connectionHub;
            _db = db;
        }


        public void SubscribeTableDependency(string connectionString)
        {
            try
            {
                tableDependency = new SqlTableDependency<Log_Attachment>(connectionString, "Log_Attachments");
                tableDependency.OnChanged += TableDependency_OnChanged;
                tableDependency.OnError += TableDependency_OnError;
                tableDependency.Start();
            }
            catch (UserWithNoPermissionException ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"UserWithNoPermissionException: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Catch other exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(HubConnection)} SqlTableDependency error: {e.Error.Message}");
        }

        private async void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Log_Attachment> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                var log_Attachment = e.Entity;
                await connectionHub.SendAttachmentToAll(log_Attachment.AttachmentId);
            }
        }
    }
}
