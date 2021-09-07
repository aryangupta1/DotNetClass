using System;
namespace BankManagement
{
    public class UserAccount
    {
        public int AccountNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Balance { get; set; }
        public string TransactionHistory { get; set; }
    }
}
