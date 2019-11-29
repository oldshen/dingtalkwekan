using System;
namespace Wekan.SDK.HookMsg
{
    public interface IHookActionFactory
    {
        IHookAction GetHookAction(string actionName);
    }
}
