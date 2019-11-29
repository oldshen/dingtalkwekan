using System;
using System.Linq;

namespace Wekan.SDK.HookMsg
{
    /// <summary>
    /// Hook事件接口
    /// </summary>
    public interface IHookAction
    {
        static string[] notifyActions = {
            WekanHookAction.act_joinMember,
            WekanHookAction.act_joinAssignee,
            WekanHookAction.act_addBoardMember,
            WekanHookAction.act_removeBoardMember
        };

        public bool NeedNotify => notifyActions.Any(c => c == HookActionName);

        (string receivers, string msg) ParseHookMsg(WekanHookMsg hookMsg);


        string HookActionName { get; }

    }
}
