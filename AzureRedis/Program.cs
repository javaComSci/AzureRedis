using System;
using StackExchange.Redis;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace AzureRedis
{
    class Program
    {
        static void Main(string[] args)
        {
            var keyVaultName = Environment.GetEnvironmentVariable("KEY_VAULT_NAME");
            var kvUri = $"https://{keyVaultName}.vault.azure.net";
            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

            var connection = client.GetSecret("hiddenlocation1");
            var connectionString = connection.Value.Value;

            Console.WriteLine(connectionString);

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
