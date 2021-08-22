using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace AzureRedis
{
    public class Favorite
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int Number { get; set; }
        public bool Truthy { get; set; }

        public Favorite()
        {
        }

        public bool AddToCache(ConnectionMultiplexer redisConnection)
        {
            string serializedVal = JsonConvert.SerializeObject(this);
            IDatabase db = redisConnection.GetDatabase();

            return db.StringSet("favorite:" + Name, serializedVal);
        }

        public static Favorite GetFromCache(ConnectionMultiplexer redisConnection, string Name)
        {
            IDatabase db = redisConnection.GetDatabase();

            var favoriteInfoString = db.StringGetAsync("favorite:" + Name).GetAwaiter().GetResult();
            var favoriteInfo = JsonConvert.DeserializeObject<Favorite>(favoriteInfoString);
            return favoriteInfo;
        }

        public override string ToString()
        {
            return $"Name: {this.Name}, Color: {this.Color}, Number: {this.Number}, Truthy: {this.Truthy}";
        }
    }
}