using System;
using System.Collections.Generic;
using System.Linq;

namespace SecretMessageWebsite.Services
{
    public interface ICipherService
    {
        string Encrypt(string plainText, string key);
        string Decrypt(string encrypted, string key);
    }
}
