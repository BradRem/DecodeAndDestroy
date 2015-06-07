using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class MessageDto
    {
        public Guid MessageId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LinkId { get; set; }
        public string Data { get; set; }
    }
}
