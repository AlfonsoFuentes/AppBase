namespace Infrastructure.Contexts
{
    public class BlazorHeroContext : AuditableContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAppCache _cache;
        private string CurrentTenant { get; set; }

        public BlazorHeroContext(DbContextOptions<BlazorHeroContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService, IAppCache cache)
            : base(options)
        {
            _currentUserService = currentUserService;
            _dateTimeService = dateTimeService;
            CurrentTenant = currentUserService.UserId;
            _cache = cache;
        }

        public DbSet<ChatHistory<BlazorHeroUser>> ChatHistories { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTimeService.NowUtc;
                        entry.Entity.CreatedBy = string.IsNullOrEmpty(_currentUserService.UserId) ? "CreatedUserName" : _currentUserService.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTimeService.NowUtc;
                        //entry.Entity.La = _currentUserService.UserId;
                        break;
                }
            }
            try
            {
                if (_currentUserService.UserId == null)
                {
                    return await base.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    return await base.SaveChangesAsync(_currentUserService.UserId, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                string exm = ex.Message;
            }
            return 0;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.Name is "LastModifiedBy" or "CreatedBy"))
            {
                property.SetColumnType("nvarchar(128)");
            }

            base.OnModelCreating(builder);
            builder.Entity<ChatHistory<BlazorHeroUser>>(entity =>
            {
                entity.ToTable("ChatHistory");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.ChatHistoryFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.ChatHistoryToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            builder.Entity<BlazorHeroUser>(entity =>
            {
                entity.ToTable(name: "Users", "Identity");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<BlazorHeroRole>(entity =>
            {
                entity.ToTable(name: "Roles", "Identity");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles", "Identity");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims", "Identity");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins", "Identity");
            });

            builder.Entity<BlazorHeroRoleClaim>(entity =>
            {
                entity.ToTable(name: "RoleClaims", "Identity");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens", "Identity");
            });
        }


        public async Task<int> SaveChangesAndRemoveCacheAsync(CancellationToken cancellationToken, params string[] cacheKeys)
        {
            var result = await SaveChangesAsync(cancellationToken);
            foreach (var cacheKey in cacheKeys)
            {
                var key = $"{cacheKey}-{CurrentTenant}";
                _cache.Remove(key);
            }
            return result;
        }

        public Task<T> GetOrAddCacheAsync<T>(string key, Func<Task<T>> addItemFactory)
        {
            if (_cache == null)
            {
                throw new ArgumentNullException("cache");
            }
            key = $"{key}-{CurrentTenant}";
            return _cache.GetOrAddAsync(key, addItemFactory);
        }


    }
}
