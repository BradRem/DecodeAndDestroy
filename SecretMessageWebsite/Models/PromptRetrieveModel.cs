using System;
using System.Collections.Generic;
using System.Linq;

namespace SecretMessageWebsite.Models
{
    public class PromptRetrieveModel
    {
        public string LinkId { get; set; }
        public string Code { get; set; }
        public bool UsesGlobalKey { get; set; }
        public string ErrorMessage { get; set; }
    }
}