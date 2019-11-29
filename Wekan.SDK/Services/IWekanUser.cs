using System;
namespace Wekan.SDK.Services
{
    public interface IWekanUser
    {
        /// <summary>
        ///  登录Wekan
        /// </summary>
        /// <param name="userInfo"></param>
        LoginResult Login(Userinfo userInfo);

        
    }
}
