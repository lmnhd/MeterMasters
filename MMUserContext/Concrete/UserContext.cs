using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMUserContext.Entities;

namespace MMUserContext.Concrete
{
    public class UserContext : DbContext
    {
        public UserContext() : base("DefaultConnection")
        {
            
        }

        public DbSet<MixClient> MixClients { get; set; }
        public DbSet<MixRequest> MixRequests { get; set; }
        public DbSet<Mix> Mixs { get; set; } 
    }
    
}
