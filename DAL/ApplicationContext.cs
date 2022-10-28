using System;
using Core.Entity;
using Entity.Customers;
using Entity.Logs;
using Entity.Policies;
using Entity.SystemUsers;
using Mapping.Customers;
using Mapping.Logging;
using Mapping.Policies;
using Mapping.SystemUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static Core.Enums.Enums;

namespace DAL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //SYSTEMUSER FOLDER
            new UserMapping(builder.Entity<User>());
            new ForgatPasswordMapping(builder.Entity<ForgatPassword>());

            //Customers FOLDER
            new CustomerMapping(builder.Entity<Customer>());
            new AgentMapping(builder.Entity<Agent>());

            //Policies FOLDER
            new PolicyMapping(builder.Entity<Policy>());

            //Logs FOLDER
            new LogMapping(builder.Entity<Log>());

        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            if (ChangeTracker.HasChanges())
            {
                foreach (var item in ChangeTracker.Entries())
                {
                    var temp = (BaseEntity)item.Entity;
                    switch (item.State)
                    {
                        case EntityState.Detached:
                            break;
                        case EntityState.Unchanged:
                            break;
                        case EntityState.Added:
                            temp.RecordStatus = RecordStatus.Active;
                            temp.CreateDate = DateTime.UtcNow;
                            temp.UpdateDate = DateTime.UtcNow;
                            break;
                        case EntityState.Deleted:
                            temp.RecordStatus = RecordStatus.Deleted;
                            temp.UpdateDate = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            temp.UpdateDate = DateTime.UtcNow;
                            break;
                        default:
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}

