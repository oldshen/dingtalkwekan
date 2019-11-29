using System;
namespace Wekan.SDK.Mongo
{
    public interface IWekanRepository
    {
        /// <summary>
        /// 用户是否存在
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool UserExist(string username);


        /// <summary>
        /// 更新用户档案
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        bool UpdateUserProfile(Userinfo userinfo);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Userinfo GetUserinfo(string id);

        /// <summary>
        /// 获取卡片信息
        /// </summary>
        /// <param name="cardid"></param>
        /// <returns></returns>
        Cardinfo GetCardinfo(string cardid);

        /// <summary>
        /// 刷新看板成员
        /// </summary>
        /// <param name="boardid"></param>
        /// <returns></returns>
        bool RefreshBoardMember(string boardid);

        /// <summary>
        /// 获取看板信息
        /// </summary>
        /// <param name="boardid"></param>
        /// <returns></returns>
        Boardinfo GetBoardinfo(string boardid);
    }
}
