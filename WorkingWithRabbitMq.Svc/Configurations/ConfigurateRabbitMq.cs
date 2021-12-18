﻿using WorkingWithRabbitMq.Application.Model.Configurations;

namespace WorkingWithRabbitMq.Svc.Configurations
{
    public static class ConfigurateRabbitMq
    {
        public static void Configurations(WebApplicationBuilder builder)
        {
            builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMqConfiguration"));
        }
    }
}
