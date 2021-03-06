﻿using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.LykkeService.Settings.JobSettings
{
    public class RabbitMqSettings
    {
#if rabbitsub
        public RabbitMqExchangeSettings Subscribers { get; set; }
#endif
#if (rabbitpub)
        public RabbitMqExchangeSettings Publishers { get; set; }
#endif
    }

    public class RabbitMqExchangeSettings
    {
        [AmqpCheck]
        public string ConnectionString { get; set; }
    }
}
