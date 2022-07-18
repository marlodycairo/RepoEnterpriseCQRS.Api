using EnterpriseCQRS.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseCQRS.Tests
{
    public class SqLiteDbFake
    {
        private DbContextOptions<CommittedCapacityContext> options;

        public SqLiteDbFake()
        {
            options = GetDbContextOptions;
        }

        public CommittedCapacityContext GetDbContext()
        {
            var context = new CommittedCapacityContext(options);

            context.Database.EnsureCreated();

            return context;
        }

        private DbContextOptions<CommittedCapacityContext> GetDbContextOptions
        {
            get
            {
                var connection = new SqliteConnection("DataSource=:memory:");

                connection.Open();

                var options = new DbContextOptionsBuilder<CommittedCapacityContext>()
                    .UseSqlite(connection)
                    .Options;

                return options;
            }
        }
    }
}
