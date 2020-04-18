using ASPT.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASPT.DataAccess {
    public class UserRepository:GenericRepository<User> {
        public UserRepository(ASPTContext context):base(context) {

        }
    }
}
