using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Ef
{
    public class MessagesRepository : IMessagesRepository
    {
        public void SaveMessage(MessageDto message)
        {
            using (var dbContext = new SecretMessageContext())
            {
                var row = new Message
                {
                    MessageId = message.MessageId,
                    CreatedOn = message.CreatedOn,
                    LinkId = message.LinkId,
                    Data = message.Data
                };
                dbContext.Messages.Add(row);
                dbContext.SaveChanges();
            }
        }


        public EncodedDataDto RetrieveEncodedData(string linkId, DateTime oldestDate)
        {
            using (var dbContext = new SecretMessageContext())
            {
                var row = dbContext.Messages
                    .Where(x => x.CreatedOn >= oldestDate)
                    .Where(x => x.LinkId.Equals(linkId))
                    .Select(x => new EncodedDataDto 
                    {
                        Data = x.Data
                    })
                    .SingleOrDefault();

                return row;
            }
        }


        public void DeleteMessage(string linkId)
        {
            using (var dbContext = new SecretMessageContext())
            {
                var row = dbContext.Messages
                    .Where(x => x.LinkId.Equals(linkId))
                    .SingleOrDefault();
                if (row != null)
                {
                    dbContext.Messages.Remove(row);
                    dbContext.SaveChanges();
                }
            }
        }



        public int DeleteMessagesOlderThanUTCDateOf(DateTime utcDate)
        {
            using (var dbContext = new SecretMessageContext())
            {
                var rows = dbContext.Messages
                    .Where(x => x.CreatedOn < utcDate);

                var count = rows.Count();
                dbContext.Messages.RemoveRange(rows);
                dbContext.SaveChanges();

                return count;
            }
        }
    }
}
