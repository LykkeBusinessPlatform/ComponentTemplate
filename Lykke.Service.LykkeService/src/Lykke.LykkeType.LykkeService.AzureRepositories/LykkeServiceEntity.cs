using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureStorage;
using Common;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lykke.LykkeType.LykkeService.AzureRepositories
{
    public class LykkeServiceEntity : TableEntity
    {
        public static string GeneratePartitionKey()
        {
            throw new NotImplementedException();
        }

        public static string GenerateRowKey()
        {
            throw new NotImplementedException();
        }
    }
}
