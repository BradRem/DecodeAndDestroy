using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Ef
{
    class SecretMessageInitializer : DropCreateDatabaseIfModelChanges<SecretMessageContext>
    {
#if DEBUG
        // use the follow override to avoid "cannot drop database because it is already in use"
        public override void InitializeDatabase(SecretMessageContext context)
        {
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE",
                context.Database.Connection.Database));
            base.InitializeDatabase(context);
        }
#endif

        protected override void Seed(SecretMessageContext context)
        {
            var messages = new List<Message>();
            messages.ForEach(x => context.Messages.Add(x));

            context.SaveChanges();
        }
    }
}
