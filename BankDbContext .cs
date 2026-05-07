using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Session04_EFcore
{
    internal class BankDbContext : DbContext
    {


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=BankDB;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
       

            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Manager)
                .WithOne(m => m.Branch)
                .HasForeignKey<Manager>(m => m.BranchCode);

        

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Branch)
                .WithMany(b => b.Accounts)
                .HasForeignKey(a => a.BranchCode);


            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountNumber);

          

            modelBuilder.Entity<CustomerAccount>()
                .HasKey(ca => new
                {
                    ca.CustomerId,
                    ca.AccountNumber
                });

            modelBuilder.Entity<CustomerAccount>()
                .HasOne(ca => ca.Customer)
                .WithMany(c => c.CustomerAccounts)
                .HasForeignKey(ca => ca.CustomerId);

            modelBuilder.Entity<CustomerAccount>()
                .HasOne(ca => ca.Account)
                .WithMany(a => a.CustomerAccounts)
                .HasForeignKey(ca => ca.AccountNumber);


            modelBuilder.Entity<Branch>().HasData(
                new Branch
                {
                    Code = 1,
                    Name = "Cairo Branch",
                    Address = "Nasr City",
                    PhoneNumber = "01000000000"
                },
                new Branch
                {
                    Code = 2,
                    Name = "Alex Branch",
                    Address = "Smouha",
                    PhoneNumber = "01111111111"
                }
            );

            modelBuilder.Entity<Manager>().HasData(
                new Manager
                {
                    Id = 1,
                    FullName = "Ahmed Ali",
                    Email = "ahmed@gmail.com",
                    PhoneNumber = "01222222222",
                    HireDate = new DateTime(2020, 1, 1),
                    BranchCode = 1
                },
                new Manager
                {
                    Id = 2,
                    FullName = "Sara Mohamed",
                    Email = "sara@gmail.com",
                    PhoneNumber = "01555555555",
                    HireDate = new DateTime(2021, 5, 10),
                    BranchCode = 2
                }
            );

        }
        public DbSet<Branch> Branches { get; set; }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<CustomerAccount> CustomerAccounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
