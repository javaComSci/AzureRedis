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

            Console.WriteLine("Name");
            var name = Console.ReadLine();
            Console.WriteLine("Color");
            var color = Console.ReadLine();
            Console.WriteLine("Number");
            var number = Console.ReadLine();
            Console.WriteLine("Truthy");
            var truthy = Console.ReadLine();

            var favorite = new Favorite()
            {
                Name = name,
                Color = color,
                Number = Int32.Parse(number),
                Truthy = Boolean.Parse(truthy)
            };

            favorite.AddToCache(redisConnection);
            Favorite f = Favorite.GetFromCache(redisConnection, name);
            Console.WriteLine(f.ToString());

            redisConnection.Dispose();
            redisConnection = null;
        }
    }
}
