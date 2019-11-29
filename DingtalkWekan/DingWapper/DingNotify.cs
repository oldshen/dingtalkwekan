using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Wekan.SDK.HookMsg;

namespace DingtalkWekan.DingWapper
{
    public class DingNotify : IDingNotify
    {
        private readonly ILogger<DingNotify> logger;
        private readonly IDingWapper dingWapper;
        private readonly IHookActionFactory hookActionFactory;
       
        public DingNotify(IDingWapper dingWapper,  ILogger<DingNotify> logger, IHookActionFactory hookActionFactory)
        {
            this.dingWapper = dingWapper;
            this.logger = logger;
            this.hookActionFactory = hookActionFactory;
        }

        public async Task Notify(WekanHookMsg msg)
        {
            IHookAction hookMsgWapper = hookActionFactory.GetHookAction(msg.description);
         
            if (hookMsgWapper?.NeedNotify==true)
            {
                await Task.Delay(1000);
                var dingmsg = hookMsgWapper.ParseHookMsg(msg);
                dingWapper.SendDingMsg(dingmsg.receivers, dingmsg.msg);
                
            }
            await Task.CompletedTask;
        }
    }
}
