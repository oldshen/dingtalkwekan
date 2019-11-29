using System;
using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DingtalkWekan.DingWapper
{
    public class DingTalkWapper:IDingWapper
    {
        
        private readonly string appkey ;
        private readonly string appsecret;
        private readonly long agentid;

        private readonly ILogger<DingTalkWapper> logger;
        public DingTalkWapper(IConfiguration configuration,ILogger<DingTalkWapper> logger)
        {
            appkey = configuration["Ding:appkey"];
            appsecret = configuration["Ding:appsecret"];
            agentid = Convert.ToInt64(configuration["Ding:agentid"]);
            this.logger = logger;
            //logger.LogInformation($"appkey={appkey}\tappsecret={appsecret}\tagentid={agentid}");

        }

        private string getDingUserid(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return string.Empty;
            }

            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/user/getuserinfo");
            OapiUserGetuserinfoRequest request = new OapiUserGetuserinfoRequest();
            request.Code = code;
            request.SetHttpMethod("GET");
            OapiUserGetuserinfoResponse response = client.Execute(request, getDingAccessToken());

            if (response.IsError)
            {
                logger.LogError(response.Errmsg);
                return string.Empty;
            }
            return response.Userid;
        }


        public DingUserinfo GetUserinfo(string code)
        {
            string userid = getDingUserid(code);
            return getDingUserInfo(userid);
        }

        private DingUserinfo getDingUserInfo(string userid)
        {
            if (string.IsNullOrEmpty(userid))
            {
                return null;
            }
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/user/get");
            OapiUserGetRequest request = new OapiUserGetRequest();
            request.Userid = userid;
            request.SetHttpMethod("GET");
            OapiUserGetResponse response = client.Execute(request, getDingAccessToken());
            // logger.LogInformation("OapiUserGetResponse:" + fastJSON.JSON.ToJSON(response));
            if (response.IsError)
            {
                logger.LogError(response.Errmsg);
                return null;
            }
            return new DingUserinfo()
            {
                Userid = response.Userid,
                Username = response.Name,
                Avatar = response.Avatar,
                Email = response.Email,
                Openid = response.OpenId,
                Unionid = response.Unionid
            };
        }

        static string accessToken = "";
        static DateTime lastgetTokenTime = DateTime.MinValue;

        const int expiredTime = 7000;//(s)
        private bool isInVaildTime()
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                return false;
            }
            int totalSeconds = (int)DateTime.Now.Subtract(lastgetTokenTime).TotalSeconds;

            return totalSeconds < expiredTime;
        }

        private string getDingAccessToken()
        {
            if (isInVaildTime())
            {
                return accessToken;
            }

            lastgetTokenTime = DateTime.Now;
            DefaultDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/gettoken");
            OapiGettokenRequest request = new OapiGettokenRequest();
            request.Appkey = appkey;
            request.Appsecret = appsecret;
            request.SetHttpMethod("GET");
            OapiGettokenResponse response = client.Execute(request);

            accessToken = response.IsError ? string.Empty : response.AccessToken;

            return accessToken;
        }

        /// <summary>
        /// 发送Ding消息
        /// </summary>
        /// <param name="recvs"></param>
        /// <param name="msgContent"></param>
        /// <returns></returns>
        public bool SendDingMsg(string recvs, string msgContent)
        {
            if(string.IsNullOrEmpty(recvs) || string.IsNullOrEmpty(msgContent))
            {
                return false;
            }
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2");

            OapiMessageCorpconversationAsyncsendV2Request request = new OapiMessageCorpconversationAsyncsendV2Request();
            request.UseridList = recvs;
            request.AgentId = agentid;
            request.ToAllUser = false;

            OapiMessageCorpconversationAsyncsendV2Request.MsgDomain msg = new OapiMessageCorpconversationAsyncsendV2Request.MsgDomain();
            msg.Msgtype = "text";
            msg.Text = new OapiMessageCorpconversationAsyncsendV2Request.TextDomain();
            msg.Text.Content = msgContent;

            request.Msg_ = msg;

            OapiMessageCorpconversationAsyncsendV2Response response = client.Execute(request, getDingAccessToken());
            //logger.LogInformation(fastJSON.JSON.ToJSON(response));
            return response.IsError;
        }
    }
}
