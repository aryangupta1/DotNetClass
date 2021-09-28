using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace BankManagement
{
    class LogIn
    {
        string username;
        string password;
        public void UserInterface()
        {
            //Clear login screen
            Console.Clear();
            //Render UI
            Console.WriteLine("\t\t ================================");
            Console.WriteLine("\t\t|     Welcome to My Bank        |");
            Console.WriteLine("\t\t ================================");
            Console.WriteLine("\t\t|     Login to start            |");
            Console.WriteLine("\t\t| \t\t\t        |");
            Console.Write("\t\t| Username: ");
            int loginCursorX = Console.CursorTop;
            int LoginCursorY = Console.CursorLeft;
            Console.WriteLine("\t\t\t|");
            Console.Write("\t\t| Password: ");
            int passwordCursorX = Console.CursorTop;
            int passwordCursorY = Console.CursorLeft;
            Console.WriteLine("\t\t\t|");
            Console.Write("\t\t| ");
            int errorCursorX = Console.CursorTop;
            int errorCursorY = Console.CursorLeft;
            Console.WriteLine("\t\t\t        |");
            Console.WriteLine("\t\t ===============================");
            Console.SetCursorPosition(LoginCursorY, loginCursorX);
            username = Console.ReadLine();
            Console.SetCursorPosition(passwordCursorY, passwordCursorX);
            //Mask password with ***
            ConsoleKeyInfo keyInput;
            do
            {
                keyInput = Console.ReadKey(true);
                if (keyInput.Key != ConsoleKey.Enter)
                {
                    password += keyInput.KeyChar.ToString();
                    Console.Write("*");
                }
            }
            while (keyInput.Key != ConsoleKey.Enter);
            //Check username and password
            var loginFile = File.ReadAllText("login.txt");
            string[] loginDetails = loginFile.Split('|');
            //loginDetails[0] = username
            //loginDetails[1] = password
            if (loginDetails[0].Equals(username) && loginDetails[1].Equals(password))
            {
                Console.SetCursorPosition(errorCursorY, errorCursorX);
                Console.WriteLine("Valid Credentials \t\t| ");
                Console.ReadKey();
                MainMenu mainMenu = new MainMenu();
                mainMenu.UserInterface();
            }
            else
            {
                //Reset password, otherwise it will keep incrementing. E.g. passwordpasswordpassword
                password = "";
                Console.SetCursorPosition(errorCursorY, errorCursorX);
                Console.WriteLine("Invalid Credentials \t\t| ");
                Console.ReadKey();
                UserInterface();
            }
            
        }
    }
    class MainMenu
    {
        public void UserInterface()
        {
            //Clear screen
            Console.Clear();
            //Render Main menu UI
            Console.WriteLine("\t\t ================================");
            Console.WriteLine("\t\t|     Welcome to My Bank        |");
            Console.WriteLine("\t\t ===============================");
            Console.WriteLine("\t\t| \t\t\t        |");
            Console.Write("\t\t| 1. Create a new account");
            Console.WriteLine("       |");
            Console.Write("\t\t| 2. Search for an account");
            Console.WriteLine("      |");
            Console.Write("\t\t| 3. Deposit");
            Console.WriteLine("\t\t\t|");
            Console.Write("\t\t| 4. Withdraw");
            Console.WriteLine("\t\t\t|");
            Console.Write("\t\t| 5. A/C statement");
            Console.WriteLine("\t\t|");
            Console.Write("\t\t| 6. Delete account");
            Console.WriteLine("\t\t|");
            Console.Write("\t\t| 7. Exit");
            Console.WriteLine("\t\t\t|");
            Console.WriteLine("\t\t --------------------------------");
            Console.Write("\t\t| Enter your choice(1-7): ");
            int cursorX = Console.CursorTop;
            int cursorY = Console.CursorLeft;
            Console.WriteLine("      |");
            Console.SetCursorPosition(cursorY, cursorX);
            try
            {
                int userChoice = short.Parse(Console.ReadLine());
                Console.WriteLine("\t\t ===============================");
                //Navigation based on users choice
                Account account = new Account();
                Email email = new Email();
                switch (userChoice)
                {
                    case 1:
                        account.Create();
                        break;
                    case 2:
                        account.Search();
                        break;
                    case 3:
                        account.Deposit();
                        break;
                    case 4:
                        account.Withdraw();
                        break;
                    case 5:
                        email.AccountStatement();
                        break;
                    case 6:
                        account.Delete();
                        break;
                    case 7:
                        LogIn login = new LogIn();
                        login.UserInterface();
                        break;
                    //All other numbers
                    default:
                        Console.Write("\t\t Incorrect input, try again.");
                        Console.ReadKey();
                        UserInterface();
                        break;
                }
            }
            //If non-numbers are entered
            catch
            {
                Console.Write("\t\t Incorrect input, try again.");
                Console.ReadKey();
                UserInterface();
            }
        }
    }
    class Account
    {
        public void Create()
        {
            //Clear screen
            Console.Clear();
            //Render create account UI
            Console.WriteLine("\t\t===================================");
            Console.WriteLine("\t\t|     Create a new account         |");
            Console.WriteLine("\t\t===================================");
            Console.Write("\t\t|    Enter the details");
            Console.WriteLine("             |");
            string[] userAccountFields = new string[5] {"First Name: ", "Last Name: ", "Address: ", "Phone: ", "Email: "};
            UserAccount newUser = new UserAccount();
            //Call function to display creation form
            try
            {
                EnterAccountInformation(userAccountFields, newUser);
            }
            catch
            {
                Console.Write("\t\t|Incorrect details, try again");
                Console.ReadKey();
                Create();
            }
            //Check account fields, if anything is incorrect call function again so the user can re-enter
            Console.WriteLine("\t\t====================================");
            Console.Write("\t\t|Are the details correct (y/n)?:");
            int confirmationCursorX = Console.CursorTop;
            int confirmationCursorY = Console.CursorLeft;
            Console.SetCursorPosition(confirmationCursorY, confirmationCursorX);
            char isInformationCorrect = Console.ReadKey().KeyChar;
            Console.WriteLine("  |");
            switch (isInformationCorrect)
            {
                case 'y':
                    Console.WriteLine("\t\t====================================");
                    Console.WriteLine("\t\t|Account created !                 |");
                    Console.WriteLine("\t\t|Details will be provided by email |");
                    Random accountNumber = new Random();
                    //Create unique 8 digit number
                    newUser.AccountNumber = accountNumber.Next(10000000, 99999999);
                    Console.WriteLine("\t\t|Account number is: " + newUser.AccountNumber + "\t   |");
                    break;
                case 'n':
                    Create();
                    break;
                default:
                    break;
            }
            //Write account details to text file
            string[] accountDetails =
            {
                $"First Name|{newUser.FirstName}",$"Last Name |{newUser.LastName}",$"Address|{newUser.Address}",
                $"Phone|{newUser.Phone}",$"Email|{newUser.Email}",$"AccountNo|{newUser.AccountNumber}",
                $"Balance|{newUser.Balance}"
            };
            File.WriteAllLines($"{newUser.AccountNumber}.txt", accountDetails);
            //Email details to account
            Email email = new Email();
            email.Confirmation(newUser.Email, newUser.AccountNumber);
            Console.ReadKey();
            MainMenu mainMenu = new MainMenu();
            mainMenu.UserInterface();
        }
        public void EnterAccountInformation(string[] userAccountFields, UserAccount newUser)
        {
            for (int i = 0; i < userAccountFields.Length; i++)
            {
                Console.Write("\t\t| {0}", userAccountFields[i]);
                int cursorX = Console.CursorTop;
                int cursorY = Console.CursorLeft;
                Console.WriteLine("\t\t\t   |");
                Console.SetCursorPosition(cursorY, cursorX);
                switch (userAccountFields[i])
                {
                    case "First Name: ":
                        newUser.FirstName = Console.ReadLine();
                        break;
                    case "Last Name: ":
                        newUser.LastName = Console.ReadLine();
                        break;
                    case "Address: ":
                        newUser.Address = Console.ReadLine();
                        break;
                    case "Phone: ":
                        newUser.Phone = int.Parse(Console.ReadLine());
                        break;
                    case "Email: ":
                        newUser.Email = Console.ReadLine();
                        if (!newUser.Email.Contains("@"))
                        {
                            Console.Write("\t\t|Incorrect details, try again");
                            Console.ReadKey();
                            Create();
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        public void Search()
        {
            //Clear screen
            Console.Clear();
            //Render search account UI
            Console.WriteLine("\t\t===================================");
            Console.WriteLine("\t\t|     Search an account           |");
            Console.WriteLine("\t\t===================================");
            Console.Write("\t\t|    Enter the details");
            Console.WriteLine("            |");
            Console.Write("\t\t| Account Number: ");
            int cursorX = Console.CursorTop;
            int cursorY = Console.CursorLeft;
            Console.WriteLine("\t\t  |");
            Console.SetCursorPosition(cursorY, cursorX);

            int accountNumber = Int32.Parse(Console.ReadLine());
            //If account exists
            if (File.Exists($"{accountNumber}.txt"))
            {
                Console.Write("\t\t| Account found!");
                Console.WriteLine("                  |");
                Console.WriteLine("\t\t===================================");
                string[] accountInformation = File.ReadAllLines($"{accountNumber}.txt");
                DisplayAccountInformation(accountInformation, false);
                Console.WriteLine("\t\t===================================");
            }
            //If account doesn't exist, search again with message account not found
            else
            {
                if (CheckAccount(true).Equals(true))
                {
                    Search();
                }
                else
                {
                    MainMenu menu = new MainMenu();
                    menu.UserInterface();
                }
            }
            //After a successful search, ask if user wants to search again
            if (CheckAccount(false).Equals(true))
            {
                Search();
            }
            else
            {
                MainMenu menu = new MainMenu();
                menu.UserInterface();
            }
        }
        public void DisplayAccountInformation(string[] accountInformation, bool lastFiveTransactions)
        {
            //Check if this is being used in display account statement method
            if (lastFiveTransactions)
            {
                //Display 0-6 general account details. E.g. Name, email etc
                for (int x = 0; x < 7; x++)
                {
                    //Get rid of delimiter and replace with :
                    accountInformation[x] = accountInformation[x].Replace("|", ":");
                    Console.WriteLine($"\t\t  {accountInformation[x]}");
                }
                //Length - 5 is the last 5 transactions, anything in between will not be counted as they are old transactions
                for (int x = accountInformation.Length - 5; x < accountInformation.Length; x++)
                {
                    accountInformation[x] = accountInformation[x].Replace("|", ":");
                    Console.WriteLine($"\t\t  {accountInformation[x]}");
                }
            }
            else //Used in search account and delete methods
            {
                //Loop through each field in account information file
                for (int i = 0; i < accountInformation.Length; i++)
                {
                    accountInformation[i] = accountInformation[i].Replace("|", ":");
                    Console.WriteLine($"\t\t  {accountInformation[i]}");
                }
            }

        }
        public void Deposit()
        {
            Account account = new Account();
            //Clear screen
            Console.Clear();
            //Render deposit UI
            Console.WriteLine("\t\t===================================");
            Console.WriteLine("\t\t|     Deposit                     |");
            Console.WriteLine("\t\t===================================");
            Console.Write("\t\t|    Enter the details");
            Console.WriteLine("            |");
            Console.Write("\t\t| Account Number: ");
            int cursorX = Console.CursorTop;
            int cursorY = Console.CursorLeft;
            Console.WriteLine("\t\t  |");
            Console.SetCursorPosition(cursorY, cursorX);
            //Find account
            int accountNumber = Int32.Parse(Console.ReadLine());
            string fileName = accountNumber.ToString();
            //If account exists, deposit into account and return to menu
            if (File.Exists($"{fileName}.txt"))
            {
                Console.Write("\t\t| Account found!");
                Console.WriteLine("                  |");
                Console.Write("\t\t| Amount: $");
                int xCursor = Console.CursorTop;
                int yCursor = Console.CursorLeft;
                Console.WriteLine("\t\t          |");
                Console.SetCursorPosition(yCursor, xCursor);
                int balance = int.Parse(Console.ReadLine());
                int amount = balance; //Save for deposit amount
                //Add balance to existing balance and write to file
                var currentBalance = Helpers.GetSpecificLine($"{fileName}.txt", 7).Replace("Balance|", "");
                balance = int.Parse(currentBalance) + balance;
                string[] accountInformationArray = File.ReadAllLines($"{accountNumber}.txt");
                //Replace old balance with new balance after deposit
                for (int i = 0; i < accountInformationArray.Length; i++)
                {
                    if (accountInformationArray[i].Contains("Balance|"))
                    {
                        accountInformationArray[i] = "Balance| " + balance;
                    }
                }
                //accountInformation = accountInformation.Insert(Helpers.GetSpecificLine($"{fileName}.txt", 7).in, accountInformationArray[6]);
                File.WriteAllLines($"{fileName}.txt", accountInformationArray); 
                Console.Write("\t\t| Deposit Successful!");
                Console.WriteLine("             |");
                Console.WriteLine("\t\t===================================");
                Console.ReadKey();
                //Add transaction to history on file
                account.WriteTransaction(fileName, "Deposit", amount, balance);
                MainMenu mainMenu = new MainMenu();
                mainMenu.UserInterface();
            }
            //if account not found, search again
            else
            {
                if (CheckAccount(true).Equals(true))
                {
                    Deposit();
                }
                else
                {
                    MainMenu menu = new MainMenu();
                    menu.UserInterface();
                }
            }

        }
        public void Withdraw()
        {
            Account account = new Account();
            //Clear screen
            Console.Clear();
            //Render withdraw UI
            Console.WriteLine("\t\t===================================");
            Console.WriteLine("\t\t|     Withdraw                    |");
            Console.WriteLine("\t\t===================================");
            Console.Write("\t\t|    Enter the details");
            Console.WriteLine("            |");
            Console.Write("\t\t| Account Number: ");
            int cursorX = Console.CursorTop;
            int cursorY = Console.CursorLeft;
            Console.WriteLine("\t\t  |");
            Console.SetCursorPosition(cursorY, cursorX);
            //Find account
            int accountNumber = Int32.Parse(Console.ReadLine());
            string fileName = accountNumber.ToString();
            //If account found withdraw balance and return to menu
            if (File.Exists($"{fileName}.txt"))
            {
                Console.Write("\t\t| Account found!");
                Console.WriteLine("                  |");
                Console.Write("\t\t| Amount: $");
                int xCursor = Console.CursorTop;
                int yCursor = Console.CursorLeft;
                Console.WriteLine("\t\t          |");
                Console.SetCursorPosition(yCursor, xCursor);
                int balance = int.Parse(Console.ReadLine());
                int amount = balance; //Save this value as withdrawal amount
                //Subtract balance from existing balance and write to file
                var currentBalance = Helpers.GetSpecificLine($"{fileName}.txt", 7).Replace("Balance|", "");
                balance = int.Parse(currentBalance) - balance;
                //Check if balance is > 0
                if (balance < 0)
                {
                    Console.Write("\t\t| Insufficient Balance, try again.");
                    Console.WriteLine("|");
                    Console.ReadKey();
                    Withdraw();
                }
                string[] accountInformationArray = File.ReadAllLines($"{accountNumber}.txt");
                //Replace old balance with new balance after withdrawal
                for (int i = 0; i < accountInformationArray.Length; i++)
                {
                    if (accountInformationArray[i].Contains("Balance|"))
                    {
                        accountInformationArray[i] = "Balance| " + balance;
                    }
                }
                File.WriteAllLines($"{fileName}.txt", accountInformationArray);
                Console.Write("\t\t| Withdrawal Successful!");
                Console.WriteLine("          |");
                Console.WriteLine("\t\t===================================");
                Console.ReadKey();
                //Add to transaction history on file
                account.WriteTransaction(fileName, "Withdraw", amount, balance);
                //Return to main menu
                MainMenu mainMenu = new MainMenu();
                mainMenu.UserInterface();
            }
            //If account not found search again
            else
            {
                if (CheckAccount(true).Equals(true))
                {
                    Withdraw();
                }
                else
                {
                    MainMenu menu = new MainMenu();
                    menu.UserInterface();
                }
            }
        }
        public void Delete()
        {
            Account account = new Account();
            MainMenu menu = new MainMenu();
            //Clear screen
            Console.Clear();
            //Render withdraw UI
            Console.WriteLine("\t\t===================================");
            Console.WriteLine("\t\t|     Delete an account           |");
            Console.WriteLine("\t\t===================================");
            Console.Write("\t\t|    Enter the details");
            Console.WriteLine("            |");
            Console.Write("\t\t| Account Number: ");
            int cursorX = Console.CursorTop;
            int cursorY = Console.CursorLeft;
            Console.WriteLine("\t\t  |");
            Console.SetCursorPosition(cursorY, cursorX);
            //Find account
            int accountNumber = Int32.Parse(Console.ReadLine());
            string fileName = accountNumber.ToString();
            //Check if file exists, if not ask to check again
            if (File.Exists($"{fileName}.txt"))
            {
                //Display account details, ask to delete, then delete or reset screen
                string[] accountInformation = File.ReadAllLines($"{accountNumber}.txt");
                DisplayAccountInformation(accountInformation, false);
                Console.WriteLine("\t\t===================================");
                Console.Write("\t\t|Delete account (y/n)?:");
                int xCursor = Console.CursorTop;
                int yCursor = Console.CursorLeft;
                Console.WriteLine("\t\t  |");
                Console.SetCursorPosition(yCursor, xCursor);
                char yesOrNo = Console.ReadKey().KeyChar;
                //Delete account and return to main menu, or just return to main menu
                switch (yesOrNo)
                {
                    case 'y':
                        File.Delete($"{fileName}.txt");
                        menu.UserInterface();
                        break;
                    case 'n':
                        menu.UserInterface();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //If account not found search again
                if (CheckAccount(true).Equals(true))
                {
                    Delete();
                }
                else
                {
                    menu.UserInterface();
                }
            }
        }
        public void WriteTransaction(string fileName, string transactionType, int transactionAmount, int totalBalance)
        {
            DateTime date = DateTime.Today;
            string today = date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            using(StreamWriter sw = File.AppendText($"{fileName}.txt"))
            {
                sw.WriteLine($"{today}|" + $"{transactionType}|{transactionAmount}" + $"|{totalBalance}");
                sw.Close();
            }
        }
        public bool CheckAccount(bool accountNotFound)
        {
            if (accountNotFound)
            {
                Console.Write("\t\t| Account not found!");
                Console.WriteLine("              |");
            }
            Console.Write("\t\t| Check another account (y/n)?");
            int confirmationCursorX = Console.CursorTop;
            int confirmationCursorY = Console.CursorLeft;
            Console.SetCursorPosition(confirmationCursorY, confirmationCursorX);
            char yesOrNo = Console.ReadKey().KeyChar;
            bool searchAgain = false;
            switch (yesOrNo)
            {
                case 'y':
                    searchAgain = true;
                    break;
                case 'n':
                    searchAgain = false;
                    break;
                default:
                    break;
            }
            return searchAgain;
        }
    }
    class Email
    {
        public SmtpClient smtpClient()
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("aryantesting2001@gmail.com", "dotNetTestingAryan"),
                EnableSsl = true
            };
            return smtpClient;
        }
        public void Confirmation(string recipient, int accountNumber)
        {
            var client = smtpClient();
            string file = File.ReadAllText($"{accountNumber}.txt");
            var mailMessage = new MailMessage
            {
                From = new MailAddress("aryantesting2001@gmail.com"),
                Subject = "Your account details",
                Body = file
            };
            mailMessage.To.Add(recipient);
            client.Send(mailMessage);
        }
        public void AccountStatement()
        {
            Account account = new Account();
            MainMenu mainMenu = new MainMenu();
            //Clear screen
            Console.Clear();
            //Render withdraw UI
            Console.WriteLine("\t\t===================================");
            Console.WriteLine("\t\t|     Account Statement           |");
            Console.WriteLine("\t\t===================================");
            Console.Write("\t\t|    Enter the details");
            Console.WriteLine("            |");
            Console.Write("\t\t| Account Number: ");
            int cursorX = Console.CursorTop;
            int cursorY = Console.CursorLeft;
            Console.WriteLine("\t\t  |");
            Console.SetCursorPosition(cursorY, cursorX);
            //Find account
            int accountNumber = Int32.Parse(Console.ReadLine());
            string fileName = accountNumber.ToString();
            string[] accountInformation = File.ReadAllLines($"{fileName}.txt");
            if (File.Exists($"{fileName}.txt"))
            {
                Console.WriteLine("\t\t|    Current Statement            |");
                account.DisplayAccountInformation(accountInformation, true);
                Console.WriteLine("\t\t===================================");
            }
            //If account not found search again
            else
            {
                if (account.CheckAccount(true).Equals(true))
                {
                    AccountStatement();
                }
                else
                {
                    MainMenu menu = new MainMenu();
                    menu.UserInterface();
                }
            }
            //Ask to send statement
            Console.Write("\t\t|Email statement (y/n)?:");
            int xCursor = Console.CursorTop;
            int yCursor = Console.CursorLeft;
            Console.WriteLine("\t  |");
            Console.SetCursorPosition(yCursor, xCursor);
            char yesOrNo = Console.ReadKey().KeyChar;
            //Proceed to send account statement and return to main menu, or just return to main menu
            switch (yesOrNo)
            {
                case 'y':
                    break;
                case 'n':
                    mainMenu.UserInterface();
                    break;
                default:
                    break;
            }
            //Create new list with user information and last 5 transactions to write to a new file, that will be emailed to user
            List<string> accountStatement = new List<string>();
            //0->6 is general account details
            for (int i = 0; i<7; i++)
            {
                accountStatement.Add(accountInformation[i]);
            }
            //Add 5 most recent transactions
            for (int x = accountInformation.Length - 5; x < accountInformation.Length; x++)
            {
                accountStatement.Add(accountInformation[x]);
            }
            //Convert to array and write to file
            String[] statement = accountStatement.ToArray();
            using (FileStream fs = File.Create($"{accountNumber}Statement.txt"))
            {
                fs.Close();
            }
            File.WriteAllLines($"{accountNumber}Statement.txt", statement);
            string file = File.ReadAllText($"{accountNumber}Statement.txt");
            var client = smtpClient();
            var mailMessage = new MailMessage
            {
                From = new MailAddress("aryantesting2001@gmail.com"),
                Subject = "Your account statement",
                Body = file, 
            };
            //Get account email
            string email = Helpers.GetSpecificLine($"{fileName}.txt", 5).Replace("Email|", "");
            mailMessage.To.Add(email);
            client.Send(mailMessage);
            Console.ReadKey();
            mainMenu.UserInterface();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            LogIn logIn = new LogIn();
            //Create Login UI
            logIn.UserInterface();
        }
    }
    //Helper methods
    public static class Helpers
    {
        //Helps with deposits and withdrawals to find the text after 'Balance|'
        public static string GetSpecificLine(string fileName, int lineNumber)
        {
            using (var streamReader = new StreamReader(fileName))
            {
                for (int i = 1; i < lineNumber; i++)
                    streamReader.ReadLine();
                return streamReader.ReadLine();
            }
        }
    }
}
