using System;
using System.Collections.Generic;
using System.Linq;

namespace SecretMessageWebsite.Models
{
    public class SecretMessageModel
    {
        public string PlainText { get; set; }
        public string Key { get; set; }
        public string LinkId { get; set; }
        public string LinkUrl { get; set; }
    }
}