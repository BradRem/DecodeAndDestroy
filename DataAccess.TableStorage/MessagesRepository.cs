using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace DataAccess.TableStorage
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly CloudStorageAccount _storageAccount;

        public MessagesRepository()
        {
            _storageAccount = StorageAccountRetriever.GetStorageAccount();
        }

        public void SaveMessage(MessageDto message)
        {
            // Create the table client.
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "messages" table.
            CloudTable table = tableClient.GetTableReference("messages");

            // Create a new message entity.
            var messageEntity = new Message(message.LinkId)
            {
                MessageId = message.MessageId,
                CreatedOn = message.CreatedOn,
                Data = message.Data
            };

            // Create the TableOperation that inserts the message entity.
            TableOperation insertOperation = TableOperation.Insert(messageEntity);

            // Execute the insert operation.
            table.Execute(insertOperation);
        }

        public EncodedDataDto RetrieveEncodedData(string linkId, DateTime oldestDate)
        {
            // Create the table client.
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "messages" table.
            CloudTable table = tableClient.GetTableReference("messages");

            // Create the table query
            TableQuery<Message> rangeQuery = new TableQuery<Message>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, linkId),
                    TableOperators.And,
                    TableQuery.GenerateFilterConditionForDate("CreatedOn", QueryComparisons.GreaterThan, oldestDate)));

            // Loop through the results, displaying information about the entity.
            var entity = table.ExecuteQuery(rangeQuery);
            var messageEntity = entity.FirstOrDefault();

            // Print the phone number of the result.
            var dto = new EncodedDataDto();
            if (messageEntity != null)
            {
                dto.Data = messageEntity.Data;
            }
            else
            {
            }
            return dto;
        }

        public void DeleteMessage(string linkId)
        {
            // Create the table client
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();

            //Create the CloudTable that represents the "message" table.
            CloudTable table = tableClient.GetTableReference("messages");

            // Create a retrieve operation that expects a message entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<Message>(linkId, linkId);

            // Execute the operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            // Assign the result to a message.
            var deleteEntity = (Message)retrievedResult.Result;

            // Create the Delete TableOperation.
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);

                // Execute the operation.
                table.Execute(deleteOperation);

                Console.WriteLine("Entity deleted.");
            }
            else
            {
                Console.WriteLine("Could not retrieve the entity.");
            }
        }

        public int DeleteMessagesOlderThanUTCDateOf(DateTime utcDate)
        {
            // Create the table client.
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "messages" table.
            CloudTable table = tableClient.GetTableReference("messages");

            // Create the table query.
            TableQuery<Message> rangeQuery = new TableQuery<Message>().Where(
                    TableQuery.GenerateFilterCondition("CreatedOn", QueryComparisons.GreaterThan, utcDate.ToString()));

            int count = 0;
            foreach (var entity in table.ExecuteQuery(rangeQuery))
            {
                // Create the Delete TableOperation.
                if (entity != null)
                {
                    TableOperation deleteOperation = TableOperation.Delete(entity);

                    // Execute the operation.
                    table.Execute(deleteOperation);
                    count++;
                }
            }

            return count;
        }
    }
}
