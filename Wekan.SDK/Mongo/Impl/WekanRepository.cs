using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Wekan.SDK.Mongo.Impl
{
    internal class WekanRepository : IWekanRepository
    {
        const string mongodbDataBase = "wekan";

        private readonly ILogger<WekanRepository> logger;
        private IMongoDatabase mongoDatabase;
        
        public WekanRepository(IConfiguration configuration,ILogger<WekanRepository> logger)
        {
            this.logger = logger;
            MongoClient client = new MongoClient(configuration["WekanMongoDB"]);

            mongoDatabase = client.GetDatabase(mongodbDataBase);
        }

        public Cardinfo GetCardinfo(string cardid)
        {
            var collection = mongoDatabase.GetCollection<BsonDocument>("cards");
            

            var card= collection?.Find(b => b["_id"]== cardid)?.FirstOrDefault();
            if (card != null)
            {
                return new Cardinfo() {
                    Title=card["title"].AsString,
                    Assignees=card["assignees"].AsBsonArray.Select(c=>c.AsString).ToArray(),
                    Members =card["members"].AsBsonArray.Select(c=>c.AsString).ToArray()
                };
            }
            return null;
        }

        public Userinfo GetUserinfo(string id)
        {
            var collection = mongoDatabase.GetCollection<BsonDocument>("users");
            var user = collection?.Find(b => b["_id"] == id)?.FirstOrDefault();
            if (user != null)
            {
                var profile = user["profile"].AsBsonDocument;
                return new Userinfo()
                {
                    Username = user["username"].AsString,
                    Email=user["emails"].AsBsonArray?.FirstOrDefault() ?.AsBsonDocument?.GetElement("address").ToString(),
                    Fullname=profile["fullname"]?.AsString,
                    Avatar=profile["avatarUrl"]?.AsString
                };
            }
            return null;
        }

        public bool UpdateUserProfile(Userinfo userinfo)
        {
            var collection = mongoDatabase.GetCollection<BsonDocument>("users");
            var updateDefinition = Builders<BsonDocument>.Update
                .Set("profile.fullname", userinfo.Fullname)
                .Set("profile.avatarUrl", userinfo.Avatar)
                .Set("profile.language", "zh-CN");

            var res = collection?.UpdateOne(b => b["username"] == userinfo.Username, updateDefinition);

            return res.ModifiedCount > 0;
        }

        public bool UserExist(string username)
        {
            var collection = mongoDatabase.GetCollection<BsonDocument>("users");
            var user = collection?.Find (b => b["username"] == username)?.FirstOrDefault();

            return user != null;
        }


        public bool RefreshBoardMember(string boardid)
        {
            var collection = mongoDatabase.GetCollection<BsonDocument>("boards");

            var board = collection?.Find(b => b["_id"] == boardid)?.FirstOrDefault();
            if (board == null)
            {
                return true;
            }
            var members = board["members"].AsBsonArray.Where(c=>c["isActive"].AsBoolean==true);

            var updateDefinition = Builders<BsonDocument>.Update
                .Set("members", members);

            var res=collection?.UpdateOne(b => b["_id"] == boardid, updateDefinition);

            return res.ModifiedCount > 0;

        }


        public Boardinfo GetBoardinfo(string boardid)
        {
            var collection = mongoDatabase.GetCollection<BsonDocument>("boards");
            var board = collection?.Find(b => b["_id"] == boardid)?.FirstOrDefault();
            if (board == null)
            {
                return null;
            }
            return new Boardinfo()
            {
                Title = board["title"].AsString,
                Members = board["members"].AsBsonArray
                    .Select(c => new BoardMember() {
                        Userid = c["userId"].AsString,
                        Username=GetUserinfo(c["userId"].AsString)?.Username,
                        IsActive = c["isActive"].AsBoolean
                    })?.ToArray()
            };
        }
    }
}
