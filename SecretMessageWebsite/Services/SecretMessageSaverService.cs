using DataAccess;
using DataAccess.TableStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace SecretMessageWebsite.Services
{
    public class SecretMessageSaverService
    {
        private readonly IMessagesRepository _messagesRepo;
        private readonly ICipherService _cipherService;
        private readonly string _password;

        public string LinkId { get; set; }
        public string SystemCreatedPassword { get; set; }

        public SecretMessageSaverService(IMessagesRepository repo, ICipherService cipherService, string userPassword)
        {
            _messagesRepo = repo;
            _cipherService = cipherService;

            if (!string.IsNullOrWhiteSpace(userPassword))
            {
                _password = userPassword;
            }
            else // user did not supply a password
            {
                // make our own password
                SystemCreatedPassword = GetRandomPassword(16);
                _password = SystemCreatedPassword;
            }
        }

        public SecretMessageSaverService(string userPassword)
            : this(new MessagesRepository(), new RijnDaelCipherService(), userPassword)
        {
        }

        public bool SaveSecretMessage(string plainText)
        {
            var success = true;

            var encoded = _cipherService.Encrypt(plainText, _password);

            var createdOn = DateTime.UtcNow;
            LinkId = GetRandomPassword(16);
            _messagesRepo.SaveMessage(new MessageDto
            {
                MessageId = Guid.NewGuid(),
                CreatedOn = createdOn,
                LinkId = LinkId,
                Data = encoded
            });

            return success;
        }

        private string GetRandomPassword(int keySize)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var buffer = new byte[keySize];
                rng.GetBytes(buffer);
                var result = HttpServerUtility.UrlTokenEncode(buffer);
                return result;
            }
        }

    }
}