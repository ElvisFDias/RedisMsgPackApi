using MessagePack;
using StackExchange.Redis;

namespace RedisMsgPackApi
{
    [MessagePackObject]
    public class User
    {
        [Key(0)]
        public string Id { get; set; }
        [Key(1)]
        public string FirstName { get; set; }
        [Key(2)]
        public string LastName { get; set; }
        [Key(3)]
        public string Email { get; set; }
        [Key(4)]
        public string Gender { get; set; }
        [Key(5)]
        public string IpAddress { get; set; }
        [Key(6)]
        public string AppName { get; set; }
        [Key(7)]
        public string Souce { get; set; }

    }
}
