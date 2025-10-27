using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Sample bank code for 08120 Week 31 Lab work
// Manages account and bank details
// Rob Miles February 2014

namespace nicorueda
{
    class Program
    {
        static void Main(string[] args)
        {

            Bank friendlyBank = new Bank("The Friendly Bank");
            Bank evilBank = new Bank("The Evil Bank");

            // TODO: Need to add some code that will create a large number of "fake" accounts

            Account rob = friendlyBank.AddAccount("Rob", "Hull", 100);
            Account rob2 = evilBank.AddAccount("Rob", "Hull", 100);
            Account rob3 = friendlyBank.AddAccount("hinilulu", "Manacor", 0);
            Console.WriteLine(rob.Equals(rob2));
            Console.WriteLine("Account created with account number: " + rob.AccountNumber);
            Console.WriteLine("Account created with account number: " + rob2.AccountNumber);
            Console.WriteLine("Account created with account number: " + rob3.AccountNumber);
            friendlyBank.Save("test.txt");

            // TODO: Need to compare the loaded bank with the original to make sure they are the same

            Bank loadedBank = Bank.Load("test.txt");
        }
    }
}
