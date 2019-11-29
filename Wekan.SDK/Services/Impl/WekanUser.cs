using System;
using Wekan.SDK.Api;
using Wekan.SDK.Mongo;

namespace Wekan.SDK.Services.Impl
{
    public class WekanUser:IWekanUser
    {
        private readonly IWekanRepository repository;
        private readonly IWekanApi wekanApi;

        public WekanUser(IWekanRepository repository, IWekanApi wekanApi)
        {
            this.repository = repository;
            this.wekanApi = wekanApi;
        }

        public LoginResult Login(Userinfo userinfo)
        {
            if (!repository.UserExist(userinfo.Username))
            {
                wekanApi.Register(userinfo.Username, userinfo.Password, userinfo.Email);
            }
            repository.UpdateUserProfile(userinfo);

            return wekanApi.Login(userinfo.Username, userinfo.Password);
        }
    }
}
