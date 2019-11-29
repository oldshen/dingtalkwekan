using System;
using Microsoft.Extensions.DependencyInjection;

namespace DingtalkWekan.DingWapper
{
    public static class DingServiceExtensions
    {
        public static IServiceCollection AddDingWapper(this IServiceCollection services)
        {
            services.AddSingleton<IDingWapper, DingTalkWapper>()
                .AddSingleton<IDingNotify,DingNotify>();
            return services;
        }
    }
}
