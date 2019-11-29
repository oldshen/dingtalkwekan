using System;
namespace Wekan.SDK.Api
{
    /// <summary>
    ///  调用Wekan的Api接口
    /// </summary>
    public interface IWekanApi
    {
        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        bool Register(string username, string password, string email);


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        LoginResult Login(string username, string password);
    }
}
