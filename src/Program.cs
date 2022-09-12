using MessagePack;
using Microsoft.AspNetCore.Mvc;
using RedisMsgPackApi;
using StackExchange.Redis;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect("localhost"));

var app = builder.Build();

app.MapGet("/user/{userId}/jsonSerializer", async ([FromServices] IConnectionMultiplexer redis, [FromRoute] string userId) =>
{
    var dBase = redis.GetDatabase();

    var redisKey = $"user:{userId}";

    var redisContent = await dBase.StringGetAsync(redisKey);

    if (redisContent.HasValue)
    {
        return JsonSerializer.Deserialize<User>(redisContent.ToString());
    }

    var userFromRepo = GetUserDefault(userId);

    var jsonUser = JsonSerializer.Serialize(userFromRepo);

    await dBase.StringSetAsync(redisKey, jsonUser);

    userFromRepo.Souce = "From Repo";

    return userFromRepo;
})
.WithName("GetUserFromRedisWithJson");

app.MapGet("/user/{userId}/msgpack", async ([FromServices] IConnectionMultiplexer redis, [FromRoute] string userId) =>
{
    var dBase = redis.GetDatabase();

    var redisKey = $"user:{userId}";

    var redisContent = await dBase.StringGetAsync(redisKey);

    if (redisContent.HasValue)
    {
        return MessagePackSerializer.Deserialize<User>(redisContent);
    }

    var defaultUser = GetUserDefault(userId);

    var userMsgPack = MessagePackSerializer.Serialize(defaultUser);

    await dBase.StringSetAsync(redisKey, userMsgPack);

    defaultUser.Souce = "From Repo";

    return defaultUser;
})
.WithName("GetUserFromRedisWithMsgPack");

app.Run();

User GetUserDefault(string userid) => new User()
{
    Id = userid,
    FirstName = "Arvie",
    LastName = "Passfield",
    Email = "apassfield0@craigslist.org",
    Gender = "Male",
    IpAddress = "207.30.182.147",
    AppName = "Lotlux",
    Souce = "From Redis"
};
