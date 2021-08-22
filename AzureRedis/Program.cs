using System;
using StackExchange.Redis;

namespace AzureRedis
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "";
            var redisConnection = ConnectionMultiplexer.Connect(connectionString);

            IDatabase db = redisConnection.GetDatabase();

            bool wasSet = db.StringSet("favorite:color", "orange");

            redisConnection.Dispose();
            redisConnection = null;
            Console.WriteLine("Finishing the addition to redis");
       
        }
    }
}
