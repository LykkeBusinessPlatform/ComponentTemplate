using System.Data.Common;
using JetBrains.Annotations;
using Lykke.Common.MsSql;
using Microsoft.EntityFrameworkCore;

namespace Lykke.LykkeType.LykkeService.MsSqlRepositories
{
    public class LykkeServiceContext : MsSqlContext
    {
        private const string Schema = ""; // TODO put proper schema name here

        // empty constructor needed for EF migrations
        [UsedImplicitly]
        public LykkeServiceContext()
            : base(Schema)
        {
        }

        public LykkeServiceContext(string connectionString, bool isTraceEnabled)
            : base(Schema, connectionString, isTraceEnabled)
        {
        }

        //Needed constructor for using InMemoryDatabase for tests
        public LykkeServiceContext(DbContextOptions options)
            : base(Schema, options)
        {
        }

        public LykkeServiceContext(DbConnection dbConnection)
            : base(Schema, dbConnection)
        {
        }

        protected override void OnLykkeModelCreating(ModelBuilder modelBuilder)
        {
            // TODO put db entities models building code here
        }
    }
}
