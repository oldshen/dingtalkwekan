using System;
namespace DingtalkWekan.DingWapper
{
    public class DingUserinfo
    {
        /// <summary>
        /// 钉钉Userid 对应 wekan 的username
        /// </summary>
        public string Userid { get; set; }

        /// <summary>
        /// 全称
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 钉钉返回的Openid  对应 wekan 的 password
        /// </summary>
        public string Openid { get; set; }

        public string Unionid { get; set; }

        public string Email { get; set; }


        public DingUserinfo()
        {

        }
    }
}
