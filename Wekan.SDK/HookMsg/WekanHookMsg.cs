using System;
namespace Wekan.SDK.HookMsg
{
    public class WekanHookMsg
    {
        /// <summary>
        /// 事件消息
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 卡片ID
        /// </summary>
        public string cardId { get; set; }

        /// <summary>
        /// 列表ID
        /// </summary>
        public string listId { get; set; }

        /// <summary>
        /// 源列表ID
        /// </summary>
        public string oldListId { get; set; }

        /// <summary>
        /// 看板ID
        /// </summary>
        public string boardId { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string user { get; set; }    //操作人

        /// <summary>
        /// 卡片标题
        /// </summary>
        public string card { get; set; }    //卡片标题

        /// <summary>
        /// //泳道ID
        /// </summary>
        public string swimlaneId { get; set; }

        /// <summary>
        /// //事件描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// //评论ID
        /// </summary>
        public string commentId { get; set; }

        public WekanHookMsg()
        {

        }

        public string GetActioUrl()
        {
            var span = text.AsSpan();
            if (span.IsEmpty)
            {
                return string.Empty;
            }
            int index = span.IndexOf("\nhttp");
            if (index > -1)
            {
                return span.Slice(index+1).ToString();
            }
            return string.Empty;
        }
    }

    /// <summary>
    ///  Hook 事件
    /// </summary>
    public class WekanHookAction
    {
        /// <summary>
        /// 移动卡片
        /// </summary>
        public const string act_moveCard = "act-moveCard";

        /// <summary>
        /// 新建卡片
        /// </summary>
        public const string act_createCard = "act-createCard";

        /// <summary>
        /// 卡片添加新成员
        /// </summary>
        public const string act_joinMember = "act-joinMember";

        /// <summary>
        ///  卡片添加被指派人
        /// </summary>
        public const string act_joinAssignee = "act-joinAssignee";

        /// <summary>
        ///  看板添加新成员
        /// </summary>
        public const string act_addBoardMember = "act-addBoardMember";

        /// <summary>
        /// 看板中移除成员
        /// </summary>
        public const string act_removeBoardMember="act-removeBoardMember";

        /// <summary>
        /// 添加清单科目
        /// </summary>
        public const string act_addChecklist = "act-addChecklist";

        /// <summary>
        /// 添加清单明细条目
        /// </summary>
        public const string act_addChecklistItem = "act-addChecklistItem";

        /// <summary>
        ///  完成清单明细条目
        /// </summary>
        public const string act_completeChecklist = "act-completeChecklist";

        /// <summary>
        /// 选中卡片
        /// </summary>
        public const string CardSelected = "CardSelected";

        /// <summary>
        /// 添加评论
        /// </summary>
        public const string act_addComment = "act-addComment";

        /// <summary>
        /// 添加标签
        /// </summary>
        public const string act_addedLabel = "act-addedLabel";

        /// <summary>
        /// 修改任务接收时间
        /// </summary>
        public const string act_a_receivedAt = "act-a-receivedAt";

        /// <summary>
        /// 修改任务开始时间
        /// </summary>
        public const string act_a_startAt = "act-a-startAt";

        /// <summary>
        /// 修改任务到期时间
        /// </summary>
        public const string act_a_dueAt = "act-a-dueAt";

        /// <summary>
        /// 修改任务终止时间
        /// </summary>
        public const string act_a_endAt = "act-a-endAt";

        /// <summary>
        /// 卡片归档
        /// </summary>
        public const string act_archivedCard = "act-archivedCard";

        /// <summary>
        /// 添加列表
        /// </summary>
        public const string act_createList = "act-createList";
    }
}
