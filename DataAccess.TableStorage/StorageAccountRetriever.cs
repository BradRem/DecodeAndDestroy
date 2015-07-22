using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DataAccess.TableStorage
{
    static class StorageAccountRetriever
    {
        public static CloudStorageAccount GetStorageAccount()
        {
            // Retrieve the storage account from the connection string.
            var connectionString = ConfigurationManager.ConnectionStrings["StorageConnectionString"];

            CloudStorageAccount storageAccount;
            if (connectionString != null)
            {
                storageAccount = CloudStorageAccount.Parse(connectionString.ToString());
            }
            else
            {
                storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            }

            return storageAccount;
        }
    }
}
