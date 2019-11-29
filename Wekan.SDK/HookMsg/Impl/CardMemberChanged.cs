using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Wekan.SDK.Mongo;
using Wekan.SDK.Utils;

namespace Wekan.SDK.HookMsg.Impl
{

    /// <summary>
    /// 卡片成员发生变化
    /// </summary>
    public abstract class CardMemberChanged:IHookAction
    {
        //private readonly ILogger<CardMemberChanged> logger;
        private readonly IWekanRepository repository;

        public CardMemberChanged(IWekanRepository repository/*, ILogger<CardMemberChanged> logger*/)
        {
            this.repository = repository;
            //this.logger = logger;
        }


        public abstract string HookActionName { get; }

        public (string receivers, string msg) ParseHookMsg(WekanHookMsg hookMsg)
        {
            //logger.LogInformation($"新成员加入,cardid:{hookMsg.cardId}");
            var card = repository.GetCardinfo(hookMsg.cardId);
          
            return (ParseReceivers(card), ParseMsgText(card,hookMsg));
        }

        protected abstract string ParseReceivers(Cardinfo card);

        protected abstract string ParseMsgText(Cardinfo card, WekanHookMsg hookMsg);
    }

    /// <summary>
    ///  添加成员到卡片
    /// </summary>
    public class JoinCardAction : CardMemberChanged
    {
        private readonly IWekanRepository repository;
        public JoinCardAction(IWekanRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public override string HookActionName => WekanHookAction.act_joinMember;

        protected override string ParseMsgText(Cardinfo card, WekanHookMsg hookMsg)
        {
            return hookMsg.text;
        }

        protected override string ParseReceivers(Cardinfo card)
        {

            return card?.Members?.
                Select(c => repository.GetUserinfo(c)?.Username)?.
                ToArray()?.
                JoinToString();
        }
    }

    /// <summary>
    /// 添加被指派人员
    /// </summary>
    public class JoinAssignedAction : CardMemberChanged
    {
        private readonly IWekanRepository repository;
        public JoinAssignedAction(IWekanRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public override string HookActionName => WekanHookAction.act_joinAssignee;

        protected override string ParseMsgText(Cardinfo card, WekanHookMsg hookMsg)
        {
            return hookMsg.text.Replace("act-joinAssignee",$"被指派到任务[{hookMsg.card}]中");
        }

        protected override string ParseReceivers(Cardinfo card)
        {

            return card?.Assignees?.
                Select(c => repository.GetUserinfo(c)?.Username)?.
                ToArray()?.
                JoinToString();
        }
    }
}
