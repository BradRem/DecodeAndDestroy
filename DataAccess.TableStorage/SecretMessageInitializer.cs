using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.TableStorage
{
    public class SecretMessageInitializer
    {
        public void Init()
        {
            var storageAccount = StorageAccountRetriever.GetStorageAccount();

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            CloudTable table = tableClient.GetTableReference("messages");
            
            // Azure SDK for .NET needs to be installed.            
            // The Azure Storage Emulator also needs to be running.
            table.CreateIfNotExists();
        }

    }
}
