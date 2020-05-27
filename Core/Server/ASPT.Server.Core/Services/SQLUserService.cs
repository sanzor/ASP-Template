using ASPT.Interfaces;
using ASPT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Core;
using ASPT.DataAccess;
using System.Linq;

namespace ASPT.Server.Core {
    public class SQLUserService : IUserService {
        private ILogger log = Log.ForContext<SQLUserService>();
        private IGenericRepository<User> userRepo;
        public SQLUserService(IGenericRepository<User> userRepo) {
            this.userRepo = userRepo;
        }
        public Task<bool> DeleteAsync(long id) {
            var deleted = this.userRepo.Remove(id);
            return Task.FromResult(deleted);
        }

        public async Task<User> GetByIdAsync(long id) {
            try {
                var user = await this.userRepo.GetByIdAsync(id) ?? throw new ArgumentNullException("Could not find user");
                return user;
            } catch (ArgumentException ex) {
                log.Error($"Could not find user with id:{id}");
                throw;
            }catch(Exception ex) {
                log.Error(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetUsersAsync() {
            try {
                var users = await this.userRepo.GetAsync();
                return users.AsEnumerable();
            } catch (Exception ex) {
                log.Error("Could not fetch users", ex.Message);
                throw;
            }
           
        }

        public async Task<User> InsertAsync(User user) {
            try {
                await this.userRepo.InsertAsync(user);
                return user;
            } catch (Exception ex) {
                log.Error("Could not insert user", ex.Message);
                throw;
            }
        }

        public Task<User> UpdateAsync(User user) {
            try {
                this.userRepo.Update(user);
                return Task.FromResult(user);
            } catch (Exception ex) {
                log.Error("Could not update user", ex.Message);
                throw;
            }
        }
    }
}
