# Simple Bank Management System

## Instructions

### Pressing enter to continue workflow

There is only one instruction and that is to press the enter button at certain times during the appliations workflow for it to continue. (Due to the use of ```Console.ReadKey()```) The following places are:

1. When logging in, when the valid/invalid credentials message is visible
2. If the error message "Incorrect input, try again." Is shown
3. After an account is successfully created
4. After a successful deposit
5. If the error message "Insufficient balance, try again." is shown
6. After a successful withdrawal
7. After typing 'y' to request an account statement. You may have to wait a few seconds for the SMTP client to send through the email''

## File locations

Account files, account statement files, and login.txt are found in BankManagement --> bin --> Debug --> netcoreapp3.1 

## Email Functionality 

Account confirmation/statement emails may take a minute or two to arrive (Make sure to check spam/junk folder too).