using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DataAccess.Ef
{
    public class Message
    {
        [Key]
        public Guid MessageId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LinkId { get; set; }
        public string Data { get; set; }
    }
}
