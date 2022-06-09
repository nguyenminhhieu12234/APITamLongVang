using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TuThienApi.Models.General;
using TuThienApi.Models.News;
using TuThienApi.Models.Project;
using TuThienApi.Models.Users;

namespace TuThienApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<
        UserEntity, 
        RoleEntity, 
        int, 
        UserClaimEntity, 
        UserRoleEntity, 
        UserLoginEntity, 
        RoleClaimsEntity, 
        UserTokenEntity>
    {
        #region Projects
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<ArticalEntity> Articals { get; set; }
        public DbSet<ExpenseEntity> Expenses { get; set; }
        public DbSet<ProcessEntity> Processes { get; set; }
        public DbSet<ProjectCategoryEntity> ProjectCategories { get; set; }
        #endregion

        #region Users
        public DbSet<FavoriteProjectEntity> FavoriteProjectEntities { get; set; }
        public DbSet<TransactionHistoryEntity> TransactionHistoryEntity { get; set; }
        #endregion

        #region News
        public DbSet<NewsEntity> News { get; set; }
        public DbSet<NewsCategoryEntity> NewsCategories { get; set; }
        #endregion

        #region General
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<EmailContentEntity> EmailContents { get; set; }
        public DbSet<FileEntity> Files { get; set; }
        public DbSet<SystemLogsEntity> Logs { get; set; }
        public DbSet<ReclaimEntity> Reclaims { get; set; }
        public DbSet<SystemConfigEntity> SystemConfigs { get; set; }
        #endregion

        private string ConnectionString { get; set; }

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
            if(!string.IsNullOrEmpty(ConnectionString))
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            QueryFilter(builder);
            BuildModel(builder);
        }

        public void QueryFilter(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                //1.Add the IsDeleted property
                //entityType.AddProperty("IsDeleted", typeof(bool)).SetDefaultValue(false);
                string name = entityType.DisplayName();

                //2.Create the query filter
                var parameter = Expression.Parameter(entityType.ClrType);

                // EF.Property<bool>(post, "IsDeleted")
                var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
                var isDeleteProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDeleted"));

                // EF.Property<bool>(post, "IsDeleted") == false
                BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeleteProperty, Expression.Constant(false));

                // post => EF.Property<bool>(post, "IsDeleted") == false
                var lambda = Expression.Lambda(compareExpression, parameter);

                builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }

        public override int SaveChanges()
        {
            //UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            //UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        //private void UpdateSoftDeleteStatuses()
        //{
        //    var entries = ChangeTracker.Entries();

        //    var countEntry = entries.Count();

        //    for (int i = 0; i < countEntry; i++)
        //    {
        //        var currentEntry = entries.FirstOrDefault();
        //        if (currentEntry.Entity.GetType() == typeof(UserEntity)) break;
        //        if (currentEntry == null) break;
        //        switch (currentEntry.State)
        //        {
        //            case EntityState.Added:
        //                currentEntry.CurrentValues["IsDeleted"] = false;
        //                break;
        //            case EntityState.Deleted:
        //                currentEntry.State = EntityState.Modified;
        //                currentEntry.CurrentValues["IsDeleted"] = true;
        //                break;
        //        }
        //    }
        //}

        public void BuildModel(ModelBuilder builder)
        {
            builder.Entity<FileEntity>(entity => {
                //entity.HasOne(x => x.Project)
                //    .WithMany()
                //    .OnDelete(DeleteBehavior.SetNull);

                //entity.HasOne(x => x.Process)
                //    .WithMany()
                //    .OnDelete(DeleteBehavior.SetNull);

                //entity.HasOne(x => x.Expense)
                //    .WithOne(x => x.File)
                //    .OnDelete(DeleteBehavior.SetNull);

                //entity.HasOne(x => x.Artical)
                //    .WithOne(x => x.File)
                //    .OnDelete(DeleteBehavior.SetNull);

                //entity.HasOne(x => x.News)
                //    .WithOne(x => x.File)
                //    .OnDelete(DeleteBehavior.SetNull);

                //entity.HasOne<CategoryEntity>(x => x.Category)
                //    .WithOne(x => x.File)
                //    .HasForeignKey<CategoryEntity>(x => x.FileId)
                //    .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
