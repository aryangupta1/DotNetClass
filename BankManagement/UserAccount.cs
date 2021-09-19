using System;
namespace BankManagement
{
    [Serializable]
    public class UserAccount
    {
        public int AccountNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public int Balance { get; set; }
        public UserTransaction[] TransactionHistory { get; set; }
    }
    public class UserTransaction
    {
        public string TransactionType { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public int NewBalance { get; set; }
    }
}
