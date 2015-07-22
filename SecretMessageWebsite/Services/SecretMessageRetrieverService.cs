using DataAccess;
using DataAccess.TableStorage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecretMessageWebsite.Services
{
    public class SecretMessageRetrieverService
    {
        private readonly IMessagesRepository _messagesRepo;
        private readonly ICipherService _cipherService;
        private readonly int _maxMessageLifetimeInHours;

        public SecretMessageRetrieverService(IMessagesRepository repo, ICipherService cipherService)
        {
            _messagesRepo = repo;
            _cipherService = cipherService;
            _maxMessageLifetimeInHours = 24;
        }

        public SecretMessageRetrieverService()
            : this(new MessagesRepository(), new RijnDaelCipherService())
        {
        }

        public string RetrieveMessage(string linkId, string code, string password)
        {
            var encodedDto = _messagesRepo.RetrieveEncodedData(linkId, DateTime.UtcNow.AddHours(-1 * _maxMessageLifetimeInHours)); 
            
            var key = string.IsNullOrWhiteSpace(code)
                ? password
                : code;

            var plainText = _cipherService.Decrypt(encodedDto.Data, key);

            return plainText;
        }

        public void DeleteMessage(string linkId)
        {
            _messagesRepo.DeleteMessage(linkId);
        }
    }
}