using System;
using System.Collections.Generic;
using Top.Api.Util;
using Top.Api;
using Top.Api.DingTalk;

namespace DingTalk.Api.Request
{
    /// <summary>
    /// TOP API: dingtalk.smartwork.attends.getleaveapproveduration
    /// </summary>
    public class SmartworkAttendsGetleaveapprovedurationRequest : BaseDingTalkRequest<DingTalk.Api.Response.SmartworkAttendsGetleaveapprovedurationResponse>
    {
        /// <summary>
        /// 请假开始时间
        /// </summary>
        public Nullable<DateTime> FromDate { get; set; }

        /// <summary>
        /// 请假结束时间
        /// </summary>
        public Nullable<DateTime> ToDate { get; set; }

        /// <summary>
        /// 员工在企业内的UserID，企业用来唯一标识用户的字段。
        /// </summary>
        public string Userid { get; set; }

        #region IDingTalkRequest Members

        public override string GetApiName()
        {
            return "dingtalk.smartwork.attends.getleaveapproveduration";
        }

        public override string GetApiCallType()
        {
            return DingTalkConstants.CALL_TYPE_TOP;
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("from_date", this.FromDate);
            parameters.Add("to_date", this.ToDate);
            parameters.Add("userid", this.Userid);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("from_date", this.FromDate);
            RequestValidator.ValidateRequired("to_date", this.ToDate);
            RequestValidator.ValidateRequired("userid", this.Userid);
        }

        #endregion
    }
}
