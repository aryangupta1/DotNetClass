using System;
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
            password = Console.ReadLine();
            
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
            Console.WriteLine("\t\t ================================");
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
            int userChoice = short.Parse(Console.ReadLine());
            Console.WriteLine(userChoice);
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
                case 7:
                    LogIn login = new LogIn();
                    login.UserInterface();
                    break;
                default:
                    UserInterface();
                    break;
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
            EnterAccountInformation(userAccountFields, newUser);
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
                for (int i = 0; i< accountInformation.Length; i++)
                {
                    //Get rid of delimiter and replace with :
                    accountInformation[i] = accountInformation[i].Replace("|", ":");
                    Console.WriteLine($"\t\t  {accountInformation[i]}");

                }
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
        public void Deposit()
        {
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
                //Add balance to existing balance and write to file
                string accountInformation = File.ReadAllText($"{fileName}.txt");
                var currentBalance = accountInformation.TextAfter("Balance|");
                balance = int.Parse(currentBalance) + balance;
                accountInformation = accountInformation.Replace(currentBalance, $"{balance}");
                File.WriteAllText($"{fileName}.txt", accountInformation);
                Console.Write("\t\t| Deposit Successful!");
                Console.WriteLine("             |");
                Console.WriteLine("\t\t===================================");
                Console.ReadKey();
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
                //Subtract balance from existing balance and write to file
                string accountInformation = File.ReadAllText($"{fileName}.txt");
                var currentBalance = accountInformation.TextAfter("Balance|");
                balance = int.Parse(currentBalance) - balance;
                accountInformation = accountInformation.Replace(currentBalance, $"{balance}");
                File.WriteAllText($"{fileName}.txt", accountInformation);
                Console.Write("\t\t| Deposit Successful!");
                Console.WriteLine("             |");
                Console.WriteLine("\t\t===================================");
                Console.ReadKey();
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
            //Clear screen
            Console.Clear();
            //Render AC Statement UI
            Console.WriteLine("\t\t===================================");
            Console.WriteLine("\t\t|     Statement                   |");
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
    public static class Extension
    {
        //Helps with deposits and withdrawals to find the text after 'Balance|'dus
        public static string TextAfter(this string value, string search)
        {
            return value.Substring(value.IndexOf(search) + search.Length);
        }
    }
}
