using System;
using System.Collections.Generic;

namespace Wekan.SDK
{
    public class LoginResult
    {
        public string id { get; set; }

        public string token { get; set; }

        public string tokenExpires { get; set; }


        public LoginResult(IDictionary<string, object> dic)
        {
            this.id = dic["id"].ToString();
            this.token = dic["token"].ToString();
            this.tokenExpires = dic["tokenExpires"].ToString();
        }
    }
}
