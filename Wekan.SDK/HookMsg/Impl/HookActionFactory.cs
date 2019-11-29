using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Wekan.SDK.HookMsg.Impl
{

    public class HookActionFactory:IHookActionFactory
    {
        private readonly IServiceProvider serviceProvider;
        public HookActionFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public  IHookAction GetHookAction(string actionName)
        {
            
            var hooks = serviceProvider.GetServices<IHookAction>();

            foreach(var hook in hooks)
            {
                if (hook.HookActionName == actionName)
                {
                    return hook;
                }
            }
            return null;
        }
    }
}
