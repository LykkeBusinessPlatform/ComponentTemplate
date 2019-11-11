using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.LykkeService.Settings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
#if (aztrepos)

        [AzureTableCheck]
        public string AzureDbConnString { get; set; }
#endif
#if (mssqlrepos)

        [SqlCheck]
        public string SqlDbConnString { get; set; }
#endif
    }
}
