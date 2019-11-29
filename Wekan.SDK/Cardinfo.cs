using System;
namespace Wekan.SDK
{
    public class Cardinfo
    {
        /// <summary>
        /// 卡片名称
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// 被指派人员
        /// </summary>
        public string[] Assignees { get; set; }


        /// <summary>
        /// 成员列表
        /// </summary>
        public string[] Members { get; set; }

        public Cardinfo()
        {
        }
    }
}
