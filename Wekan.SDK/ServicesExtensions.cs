using Microsoft.Extensions.DependencyInjection;
using Wekan.SDK.Api;
using Wekan.SDK.Api.Impl;
using Wekan.SDK.HookMsg;
using Wekan.SDK.HookMsg.Impl;
using Wekan.SDK.Mongo;
using Wekan.SDK.Mongo.Impl;
using Wekan.SDK.Services;
using Wekan.SDK.Services.Impl;

namespace Wekan.SDK
{
    public static class ServicesExtensions
    {

        public static IServiceCollection AddWekan(this IServiceCollection services)
        {
            services
                .AddSingleton<IWekanUser,WekanUser>()
                .AddSingleton<IWekanRepository,WekanRepository>()
                .AddSingleton<IWekanApi,WekanApi>()
                .AddSingleton<IHookAction,JoinCardAction>()
                .AddSingleton<IHookAction,JoinAssignedAction>()
                .AddSingleton<IHookAction,AddBoardMemberAction>()
                .AddSingleton<IHookAction,RemoveBoardMemberAction>()
                .AddSingleton<IHookActionFactory,HookActionFactory>()
                ;
            return services;
        }
    }
}
