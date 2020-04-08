using StackExchange.Redis;
using System;

namespace ASPT.DataAccess {
    public class RedisStore {
        public ConnectionMultiplexer Connection;
        public RedisStore(string connectionString) {
            this.Connection = ConnectionMultiplexer.Connect(connectionString);
        }
        public IDatabase Database => this.Connection.GetDatabase();
    }
}
