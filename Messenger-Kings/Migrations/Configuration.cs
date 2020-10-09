namespace Messenger_Kings.Migrations
{
	using Messenger_Kings.Models;
	using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
	using System.Net;

	internal sealed class Configuration : DbMigrationsConfiguration<Messenger_Kings.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Messenger_Kings.Models.ApplicationDbContext";
        }

        protected override void Seed(Messenger_Kings.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.BankCategories.AddOrUpdate(
                new BankCategory()
                {
                    Bank_Name = "African Bank"
                },
                    new BankCategory()
                    {
                        Bank_Name = "Bidvest Bank"
                    },
                       new BankCategory()
                       {
                           Bank_Name = "Capitec Bank"
                       },
                          new BankCategory()
                          {
                              Bank_Name = "Nedbank"
                          },
                             new BankCategory()
                             {
                                 Bank_Name = "First National Bank"
                             },
                                new BankCategory()
                                {
                                    Bank_Name = "Absa"
                                }
                ) ;

            context.AccountCategories.AddOrUpdate(
                new AccountCategory()
                {
                    Account_Type= "Saving"
                },
                         new AccountCategory()
                         {
                             Account_Type = "Cheque"
                         }
                );

        }
        
    }
}
