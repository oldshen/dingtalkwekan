using System;
using System.Threading.Tasks;
using Wekan.SDK.HookMsg;

namespace DingtalkWekan.DingWapper
{
    public interface IDingNotify
    {
        Task Notify(WekanHookMsg msg);
    }
}
