using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Common.MsSql;
using Lykke.LykkeType.LykkeService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lykke.LykkeType.LykkeService.MsSqlRepositories.Repositories
{
    public class LykkeServiceRepository : ILykkeServiceRepository
    {
        private readonly IDbContextFactory<LykkeServiceContext> _contextFactory;

        public LykkeServiceRepository(IDbContextFactory<LykkeServiceContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        
    }
}
