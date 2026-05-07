using Microsoft.EntityFrameworkCore;

namespace Assignment_Session04_EFcore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Part03
            using BankDbContext db = new BankDbContext();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== BANK MANAGEMENT SYSTEM =====");
                Console.WriteLine("1. Add New Customer");
                Console.WriteLine("2. Open New Account");
                Console.WriteLine("3. Update Account Status");
                Console.WriteLine("4. Remove Account From Customer");
                Console.WriteLine("5. List All Customers");
                Console.WriteLine("0. Exit");

                Console.Write("Choose Option: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddCustomer(db);
                            break;

                        case "2":
                            OpenAccount(db);
                            break;

                        case "3":
                            UpdateAccountStatus(db);
                            break;

                        case "4":
                            RemoveAccount(db);
                            break;

                        case "5":
                            ListCustomers(db);
                            break;

                        case "0":
                            return;

                        default:
                            Console.WriteLine("Invalid Choice!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
            }
        }

      
        // Add Customer
     

        static void AddCustomer(BankDbContext db)
        {
            Console.Write("Full Name: ");
            string name = Console.ReadLine();

            Console.Write("National ID: ");
            string nationalId = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Phone Number: ");
            string phone = Console.ReadLine();

            Console.Write("Address: ");
            string address = Console.ReadLine();

            Console.Write("Customer Type: ");
            string type = Console.ReadLine();

            Console.Write("Date Of Birth: ");

            DateTime dob;

            while (!DateTime.TryParse(Console.ReadLine(), out dob))
            {
                Console.Write("Invalid Date, Try Again: ");
            }

            Customer customer = new Customer()
            {
                FullName = name,
                NationalId = nationalId,
                Email = email,
                PhoneNumber = phone,
                Address = address,
                CustomerType = type,
                DateOfBirth = dob
            };

            db.Customers.Add(customer);

            db.SaveChanges();

            Console.WriteLine("Customer Added Successfully");
        }

        // Open Account
   

        static void OpenAccount(BankDbContext db)
        {
            Console.Write("Account Number: ");

            int accNum;

            while (!int.TryParse(Console.ReadLine(), out accNum))
            {
                Console.Write("Invalid Number, Try Again: ");
            }

            Console.Write("Account Type: ");
            string accType = Console.ReadLine();

            Console.Write("Branch Code: ");

            int branchCode;

            while (!int.TryParse(Console.ReadLine(), out branchCode))
            {
                Console.Write("Invalid Number, Try Again: ");
            }

            var branch = db.Branches.Find(branchCode);

            if (branch == null)
            {
                Console.WriteLine("Branch Not Found!");
                return;
            }

            Console.Write("Customer Id: ");

            int customerId;

            while (!int.TryParse(Console.ReadLine(), out customerId))
            {
                Console.Write("Invalid Number, Try Again: ");
            }

            var customer = db.Customers.Find(customerId);

            if (customer == null)
            {
                Console.WriteLine("Customer Not Found!");
                return;
            }

            Console.Write("Ownership Type (Primary / CoHolder): ");
            string ownership = Console.ReadLine();

            Account account = new Account()
            {
                AccountNumber = accNum,
                AccountType = accType,
                CurrentBalance = 0,
                OpeningDate = DateTime.Now,
                BranchCode = branchCode
            };

            db.Accounts.Add(account);

            db.SaveChanges();

            CustomerAccount customerAccount = new CustomerAccount()
            {
                CustomerId = customerId,
                AccountNumber = accNum,
                OwnershipType = ownership,
                OwnershipStartDate = DateTime.Now,
                AccountStatus = "Active"
            };

            db.CustomerAccounts.Add(customerAccount);

            db.SaveChanges();

            Console.WriteLine("Account Opened Successfully");
        }

        // Update Account Status
    

        static void UpdateAccountStatus(BankDbContext db)
        {
            Console.Write("Account Number: ");

            int accNum;

            while (!int.TryParse(Console.ReadLine(), out accNum))
            {
                Console.Write("Invalid Number, Try Again: ");
            }

            Console.Write("Customer Id: ");

            int customerId;

            while (!int.TryParse(Console.ReadLine(), out customerId))
            {
                Console.Write("Invalid Number, Try Again: ");
            }

            var customerAccount = db.CustomerAccounts
                .FirstOrDefault(ca =>
                    ca.AccountNumber == accNum &&
                    ca.CustomerId == customerId);

            if (customerAccount == null)
            {
                Console.WriteLine("Record Not Found!");
                return;
            }

            customerAccount.AccountStatus =
                customerAccount.AccountStatus == "Active"
                ? "Inactive"
                : "Active";

            db.SaveChanges();

            Console.WriteLine("Status Updated Successfully");
        }

    
        // Remove Account
     

        static void RemoveAccount(BankDbContext db)
        {
            Console.Write("Account Number: ");

            int accNum;

            while (!int.TryParse(Console.ReadLine(), out accNum))
            {
                Console.Write("Invalid Number, Try Again: ");
            }

            Console.Write("Customer Id: ");

            int customerId;

            while (!int.TryParse(Console.ReadLine(), out customerId))
            {
                Console.Write("Invalid Number, Try Again: ");
            }

            var customerAccount = db.CustomerAccounts
                .FirstOrDefault(ca =>
                    ca.AccountNumber == accNum &&
                    ca.CustomerId == customerId);

            if (customerAccount == null)
            {
                Console.WriteLine("Relationship Not Found!");
                return;
            }

            db.CustomerAccounts.Remove(customerAccount);

            db.SaveChanges();

            Console.WriteLine("Removed Successfully");
        }

          // List Customers


        static void ListCustomers(BankDbContext db)
        {
            var customers = db.Customers
                .Include(c => c.CustomerAccounts)
                .ThenInclude(ca => ca.Account)
                .ToList();

            foreach (var customer in customers)
            {
                Console.WriteLine($"\nCustomer ID: {customer.Id}");
                Console.WriteLine($"Name: {customer.FullName}");
                Console.WriteLine($"Phone: {customer.PhoneNumber}");

                foreach (var ca in customer.CustomerAccounts)
                {
                    Console.WriteLine($"   Account Number: {ca.Account.AccountNumber}");
                    Console.WriteLine($"   Account Type: {ca.Account.AccountType}");
                    Console.WriteLine($"   Balance: {ca.Account.CurrentBalance}");
                    Console.WriteLine($"   Status: {ca.AccountStatus}");
                }

                Console.WriteLine("----------------------------------");
            }
           #endregion
        }
    }
}
