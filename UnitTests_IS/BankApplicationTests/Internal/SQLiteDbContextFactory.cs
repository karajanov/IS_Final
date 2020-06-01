using AutoMapper;
using BankApplication.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;

namespace BankApplicationTests.Internal
{
    public class SQLiteDbContextFactory 
        : IDisposable
    {
        private DbConnection connection;
   
        private DbContextOptions<BankDataContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<BankDataContext>()
                .UseSqlite(connection)
                .Options;
        }

        public BankDataContext CreateContext()
        {
            if (connection == null)
            {
                connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();

                var options = CreateOptions();
                using var context = new BankDataContext(options);
                context.Database.EnsureCreated();
            }

            return new BankDataContext(CreateOptions());
        }

        public void Dispose()
        {
            if (connection == null)
            {
                connection.Dispose();
                connection = null;
            }
        }
    }
}
