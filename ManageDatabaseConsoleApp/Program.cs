using DataAccess.Ef;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ManageDatabaseConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dateUTC = DateTime.UtcNow.AddHours(-24);

            var repo = new MessagesRepository();
            var count = repo.DeleteMessagesOlderThanUTCDateOf(dateUTC);
            Console.WriteLine(string.Format("Removed {0} outdated messages", count));
        }
    }
}
