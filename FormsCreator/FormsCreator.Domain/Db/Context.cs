using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FormsCreator.Domain.Core;
using FormsCreator.Domain.Core.ContextInfo;
using FormsCreator.Domain.Core.Forms;
using FormsCreator.Domain.Core.Security;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FormsCreator.Domain.Db
{
    public class Context : DbContext, IDataProtectionKeyContext
    {
        private readonly DbConfig _config;
        private readonly ICurrentUserProvider _currentUserProvider;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="config">Konfiguracja bazy</param>
        /// <param name="currentUserProvider">Serwis z aktualnym użytkownikiem</param>
        public Context(DbConfig config, ICurrentUserProvider currentUserProvider)
        {
            _config = config;
            _currentUserProvider = currentUserProvider;
        }


        /// <summary>
        /// Klucze zabezpieczen danych
        /// </summary>
        public DbSet<DataProtectionKey> DataProtectionKeys { get; protected set; }
        
        /// <summary>
        /// Użytkownicy
        /// </summary>
        public DbSet<User> Users { get; protected set; }
        
        /// <summary>
        /// Definicje formularzu
        /// </summary>
        public DbSet<FormDefinition> FormDefinitions { get; protected set; }

        /// <summary>
        /// Formularze
        /// </summary>
        public DbSet<Form> Form { get; protected set; }
        
        /// <summary>
        /// Podczas konfiguracji DbContextu
        /// </summary>
        /// <param name="optionsBuilder">Budowniczy opcji DbContextu</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_config.ConnectionString, opt =>
                    {
                        opt.MigrationsAssembly(GetType().Assembly.FullName);
                        opt.MigrationsHistoryTable("schema_version");
                    })
                    //.LogTo(Console.WriteLine)
                    .UseSnakeCaseNamingConvention();
            }
        }

        /// <summary>
        /// Metoda wywoływana podczas tworzenia modelu
        /// </summary>
        /// <param name="modelBuilder">Budowniczy modelu</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DomainModule).Assembly);
        }
        
        /// <summary>
        /// Metoda do zapisu zmian
        /// </summary>
        /// <returns>Wynik zapisu</returns>
        public override int SaveChanges()
        {
            TimeStampEntities();
            return base.SaveChanges();
        }

        /// <summary>
        /// Metoda do zapisu zmian
        /// </summary>
        /// <returns>Wynik zapisu</returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            TimeStampEntities();
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Metoda do zapisu zmian bez propert Audytowych IAuditableEntity
        /// </summary>
        /// <returns>Wynik zapisu</returns>
        public async Task<int> SaveChangesWithoutAudit(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void TimeStampEntities()
        {
            //jeśli provider bazy nie jest postgresem
            if (!Database.IsNpgsql())
            {
                return;
            }

            var userId = _currentUserProvider.GetUserId();
            var timeStampTrackedEntries = ChangeTracker.Entries<IAuditableEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            foreach (var entry in timeStampTrackedEntries)
            {
                entry.Entity.ModifiedBy = userId;
                entry.Entity.ModifiedOn = DateTime.Now;

                if (entry.State != EntityState.Added)
                {
                    continue;
                }

                entry.Entity.CreatedBy = userId;
                entry.Entity.CreatedOn = DateTime.Now;
            }
        }
    }
}