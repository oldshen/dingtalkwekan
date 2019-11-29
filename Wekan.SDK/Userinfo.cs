using System;

namespace Wekan.SDK
{
    public class Userinfo
    {
        /// <summary>
        /// 钉钉Userid 对应 wekan 的username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 全称
        /// </summary>
        public string Fullname { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 钉钉返回的Openid  对应 wekan 的 password
        /// </summary>
        public string Password { get; set; }

       /// <summary>
       /// 邮箱
       /// </summary>
        public string Email { get; set; }


        public Userinfo()
        {

        }
    }
}
