using System;
using System.Linq;
using Wekan.SDK.Mongo;
using Wekan.SDK.Utils;

namespace Wekan.SDK.HookMsg.Impl
{
   /// <summary>
   /// 看板成员发生变化
   /// </summary>
    public abstract class BoardMemberChanged : IHookAction
    {
        private readonly IWekanRepository repository;

        public BoardMemberChanged(IWekanRepository repository)
        {
            this.repository = repository;

        }

        public abstract string HookActionName { get; }

        public (string receivers, string msg) ParseHookMsg(WekanHookMsg hookMsg)
        {
            if (string.IsNullOrEmpty(hookMsg.boardId))
            {
                return (null, null);
            }
            var board = repository.GetBoardinfo(hookMsg.boardId);
            if (board == null)
            {
                return (null, null);
            }
            DoMore(board, hookMsg);
            return (
                board.Members.Select(c => c.Username).ToArray().JoinToString(),
                hookMsg.text
                );
        }

        protected virtual void DoMore(Boardinfo board, WekanHookMsg hookMsg) { }
    }

    /// <summary>
    /// 添加成员到看板
    /// </summary>
    public class AddBoardMemberAction : BoardMemberChanged
    {
        

        public AddBoardMemberAction(IWekanRepository repository) : base(repository)
        {
        }

        public override string HookActionName => WekanHookAction.act_addBoardMember;
    }

    /// <summary>
    /// 移看板成员
    /// </summary>
    public class RemoveBoardMemberAction : BoardMemberChanged
    {

        private readonly IWekanRepository repository;

        public RemoveBoardMemberAction(IWekanRepository repository):base(repository)
        {
            this.repository = repository;
        }

        public override string HookActionName => WekanHookAction.act_removeBoardMember;

        protected override void DoMore(Boardinfo board, WekanHookMsg hookMsg)
        {
            if (board.Members.Any(c => c.IsActive == false))
            {
                repository.RefreshBoardMember(hookMsg.boardId);
            }
        }
    }
}
