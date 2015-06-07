using DataAccess;
using DataAccess.Ef;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecretMessageWebsite.Services
{
    public class ManageDatabaseService
    {
        private readonly IMessagesRepository _messagesRepo;

        public ManageDatabaseService(IMessagesRepository repo)
        {
            _messagesRepo = repo;
        }

        public ManageDatabaseService()
            : this(new MessagesRepository())
        {
        }

        public void RemoveOldMessages(DateTime dateUTC)
        {
            _messagesRepo.DeleteMessagesOlderThanUTCDateOf(dateUTC);
        }
    }
}