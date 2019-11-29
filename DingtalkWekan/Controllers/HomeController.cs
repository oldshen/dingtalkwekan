using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DingtalkWekan.DingWapper;
using Wekan.SDK;
using Wekan.SDK.HookMsg;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System;
using Wekan.SDK.Services;

namespace DingtalkWekan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IConfiguration configuration;
        private readonly IDingWapper dingWapper;
        private readonly IWekanUser wekanUser;
        private readonly IDingNotify dingNotify;

        public HomeController(ILogger<HomeController> logger
            , IConfiguration configuration
            , IDingWapper dingWapper
            , IWekanUser wekanUser
            , IDingNotify dingNotify
            )
        {
            this.logger = logger;
            this.configuration = configuration;
            this.dingWapper = dingWapper;
            this.wekanUser = wekanUser;
            this.dingNotify = dingNotify;
        }

        public IActionResult Index()
        {
            ViewData["corpId"] = configuration["Ding:corpId"];
            return View();
        }

        /// <summary>
        /// 登录Wekan
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IActionResult Login(string code)
        {

            var dingUser = dingWapper.GetUserinfo(code);

            if (dingUser == null)
            {
                return new ContentResult() { Content = "获取钉钉用户信息失败,请关闭，再重新打开" };
            }
            //logger.LogInformation(fastJSON.JSON.ToJSON(dingUser));

            var wekanUserinfo = new Userinfo()
            {
                Username = dingUser.Userid,
                Fullname = dingUser.Username,
                Avatar = dingUser.Avatar,
                Email = dingUser.Email,
                Password = dingUser.Openid
            };

            var wekanResult = wekanUser.Login(wekanUserinfo);

            ViewData["Result"] = wekanResult;
            ViewData["WekanRoot"] = configuration["WekanRoot"];
            return View();
        }

        /// <summary>
        /// 接收Wekan的回调消息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Ding([FromBody] WekanHookMsg obj)
        {
            //logger.LogInformation(obj.GetActioUrl());
            Task.Factory.StartNew(async (state) =>
            {
                await dingNotify.Notify((WekanHookMsg)state);
            }, obj);
            return new ContentResult() { Content = "回调了" };
        }

    }
}
