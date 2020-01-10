using AllTech.DomainClasses.News;
using AllTech.DomainClasses.NewsGroup;
using AllTech.DomainClasses.User;
using AllTech.DomainClasses.Wallet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllTech.DataLayer.Context
{
    public class AllTechDbContext: DbContext
    {
        public AllTechDbContext(DbContextOptions<AllTechDbContext> options):base(options)
        {

        }


        public DbSet<NewsGroup> NewsGroups { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        #region Wallet

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }


        #endregion



    }
}
