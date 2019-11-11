using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.LykkeService.Settings.JobSettings
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
