using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public interface IMessagesRepository
    {
        void SaveMessage(MessageDto message);
        EncodedDataDto RetrieveEncodedData(string linkId, DateTime oldestDate);
        void DeleteMessage(string linkId);
        int DeleteMessagesOlderThanUTCDateOf(DateTime utcDate);
    }
}
