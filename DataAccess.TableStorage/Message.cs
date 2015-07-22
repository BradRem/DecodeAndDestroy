using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.TableStorage
{
    public class Message : TableEntity
    {
        public Message(string linkId)
        {
            this.PartitionKey = linkId;
            this.RowKey = linkId;
        }

        public Message()
        {

        }

        public Guid MessageId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Data { get; set; }
    }
}
