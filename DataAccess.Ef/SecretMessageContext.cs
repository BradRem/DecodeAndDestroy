using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Ef
{
    public class SecretMessageContext : DbContext
    {
        public SecretMessageContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<SecretMessageContext>(new SecretMessageInitializer());
        }

        public DbSet<Message> Messages { get; set; }

    }
}
