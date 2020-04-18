using ASPT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;
using ASPT.Conventions;
using Microsoft.Extensions.Configuration;

namespace ASPT.DataAccess {
    public class ASPTContext:DbContext {
        public DbSet<User> Users { get; set; }
        public ASPTContext(DbContextOptions options):base(options) {

        }
        public ASPTContext() {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (optionsBuilder.IsConfigured) {
                return;
            }
            string commonPath = Directory.GetParent(Assembly.GetExecutingAssembly().FullName).FullName;
            string result = Path.Combine(commonPath, Constants.CONFIG_FILE);
            var config = new ConfigurationBuilder().AddJsonFile(result).Build();
        }
    }
}
