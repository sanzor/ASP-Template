using ASPT.DataAccess;
using ASPT.Interfaces;
using ASPT.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASPT.Server.Core {
    public class RedisUserService : IUserService {
        private ILogger log = Log.ForContext<RedisUserService>();
        private UserRepo userRepo;
        public RedisUserService(UserRepo store) {
            this.userRepo = store;
        }
        public Task<bool> DeleteAsync(long id) {
            return userRepo.DeleteUserAsync(id.ToString());
        }

        public Task<User> GetByIdAsync(long id) {
            return this.userRepo.GetUserAsync(id.ToString());
        }

        public Task<IEnumerable<User>> GetUsersAsync() {
            throw new NotImplementedException();
        }

        public async Task<User> InsertAsync(User user) {
            var inserted=await  this.userRepo.InsertUserAsync(user);
            var record = await this.userRepo.GetUserAsync(user.Id);
            return record;
        }

        public Task<User> UpdateAsync(User user) {
            throw new NotImplementedException();
        }
    }
}
