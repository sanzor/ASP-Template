using ASPT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASPT.Interfaces {
    public interface IUserService {
        /// <summary>
        /// Inserts user to database
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// 
        /// <returns></returns>
        Task<User> InsertAsync(User user);

        /// <summary>
        /// Updates target user in the db
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> UpdateAsync(User user);

        /// <summary>
        /// Searches for target user in the database
        /// </summary>
        /// <param name="id"></param>
        /// /// <exception cref="ArgumentNullException">Throws when there is no user in the db</exception>
        /// <returns></returns>
        Task<User> GetByIdAsync(long id);

        /// <summary>
        /// Deletes target user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// Retrieves users
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<User>> GetUsersAsync();
    }
}
