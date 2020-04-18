﻿using ASPT.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASPT.DataAccess {
    public class UserRepo {
        private RedisStore store;
        private Func<User, string> ToKey = x => $"user:{x.Id}";
        public UserRepo(RedisStore store) {
            this.store = store;
        }
        public async Task UpdateUserAsync(User user) {
            if (!await this.store.Database.KeyExistsAsync(user.Id)) {
                throw new NotSupportedException("User does not exist !");
            }
            var entries = Adapter.Adapt(user);
            var tran = this.store.Database.CreateTransaction();
            tran.AddCondition(Condition.KeyExists(ToKey(user)));
            await this.store.Database.HashSetAsync(ToKey(user), entries);

        }
        public async Task<bool> InsertUserAsync(User user) {
            var entries = Adapter.Adapt(user);
            if (this.store.Database.KeyExists(ToKey(user))) {
                throw new Exception("User exists");
            }
            var tran = this.store.Database.CreateTransaction();
            var rez = tran.AddCondition(Condition.KeyNotExists(ToKey(user)));
            await this.store.Database.HashSetAsync($"user:{user.Id}", entries);
            if (await tran.ExecuteAsync()) {
                throw new Exception("Could not do transaction");
            }
            return rez.WasSatisfied;
        }
        public async Task<bool> DeleteUserAsync(string id) {
            var deleted = await this.store.Database.KeyDeleteAsync($"user:{id}");
            return deleted;
        }
        public async Task<User> GetUserAsync(string id) {

            var entries = await this.store.Database.HashGetAllAsync($"user:{id}");
            var user = Adapter.Adapt(entries);
            return user;

        }

    }
}
