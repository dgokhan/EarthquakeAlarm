using System;
using System.IO;
using DepremAlarmi.Models;
using Shiny.IO;
using SQLite;

namespace DepremAlarmi
{
    public class SqliteConnection : SQLiteAsyncConnection
    {
        public SqliteConnection(IFileSystem fileSystem) : base(Path.Combine(fileSystem.AppData.FullName, "earthQuake.db"))
        {
            var conn = GetConnection();
            conn.CreateTable<JobLog>();
            conn.CreateTable<NotificationEvent>();
        }

        public AsyncTableQuery<JobLog> JobLogs => this.Table<JobLog>();
        public AsyncTableQuery<NotificationEvent> NotificationEvents => this.Table<NotificationEvent>();
    }
}