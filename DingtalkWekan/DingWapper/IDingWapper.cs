using System;
namespace DingtalkWekan.DingWapper
{
    public interface IDingWapper
    {
        /// <summary>
        /// 获取钉钉用户信息
        /// </summary>
        /// <param name="code">JSPAI获取的code</param>
        /// <returns></returns>
        DingUserinfo GetUserinfo(string code);


        /// <summary>
        /// 发送钉消息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool SendDingMsg(string userid, string msg);

    }
}
