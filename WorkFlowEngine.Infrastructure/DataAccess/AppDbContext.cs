using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkFlowEngine.Domain.Entities;
using WorkFlowEngine.Domain.Enums;
using WorkFlowEngine.Domain.Primitives;

namespace WorkFlowEngine.Infrastructure.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ActivityAction>().HasIndex(s => new { s.ActionId, s.ActivityId }).IsUnique(true);
            builder.Entity<ProcessInstanceUser>().HasIndex(s => s.DestinationName);
            builder.Entity<Activity>().Property(s => s.RuleOption).IsRequired(true);
            builder.Entity<Activity>().Property(s => s.Mode).HasConversion(
                new ValueConverter<ActivityMode, string>(
                    value => value.ToString(),
                    value => (ActivityMode)Enum.Parse(typeof(ActivityMode), value, true)
                ));

            builder.Entity<Activity>().Property(s => s.RuleOption).HasConversion(
                new ValueConverter<RuleOption, string>(
                    value => value.ToString(),
                    value => (RuleOption)Enum.Parse(typeof(RuleOption), value, true)
                    ));

            builder.Entity<Activity>().Property(s => s.ActivityType).HasConversion(
                new ValueConverter<ActivityType, string>(
                    value => value.ToString(),
                    value => (ActivityType)Enum.Parse(typeof(ActivityType), value, true)
                ));

            builder.Entity<TransitionConditions>(s =>
            {
                s.Property(o => o.BinaryOperator).HasConversion(
                    new ValueConverter<BinaryOperator, string>(
                    value => value.ToString(),
                    value => (BinaryOperator)Enum.Parse(typeof(BinaryOperator), value, true)
                    ));

                s.Property(o => o.Operator).HasConversion(
                    new ValueConverter<Operator, string>(
                    value => value.ToString(),
                    value => (Operator)Enum.Parse(typeof(Operator), value, true)
                ));
            });
            builder.Entity<TransitionConditions>().
             HasIndex(s => new { s.TransitionId, s.DataFieldId, s.Value, s.Operator }).
            IsUnique(true);

            builder.Entity<Transition>().HasOne(s => s.CurrentActivity).WithMany()
                .OnDelete(DeleteBehavior.ClientNoAction);
            builder.Entity<Transition>().HasOne(s => s.NextActivity).WithMany()
                .OnDelete(DeleteBehavior.ClientNoAction);
            builder.Entity<ProcessInstance>().HasIndex(s => s.ApplicationNumber);
            builder.Entity<ProcessInstanceHistory>().HasOne(s => s.CurrentActivity).
                WithMany().OnDelete(DeleteBehavior.Restrict).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ProcessInstanceHistory>().HasOne(s => s.NextActivity).WithMany().OnDelete(DeleteBehavior.NoAction);

        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseLookup);
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                    ((BaseLookup)entry.Entity).CreationDate = DateTime.Now;
                else if (entry.State == EntityState.Modified)
                    ((BaseLookup)entry.Entity).ModificationDate = DateTime.Now;
            }
            return base.SaveChanges();
        }
        public DbSet<Domain.Entities.Action> Actions { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityAction> ActivitiesActions { get; set; }
        public DbSet<DestinationType> DestinationTypes { get; set; }
        public DbSet<ActivityDestinationType> ActivityDestinationTypes { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<ProcessActivity> ProcessActivities { get; set; }
        public DbSet<ProcessInstance> ProcessInstances { get; set; }
        public DbSet<ProcessInstanceUser> ProcessInstanceUsers { get; set; }
        public DbSet<ProcessInstanceHistory> processInstanceHistories { get; set; }
        public DbSet<Transition> transitions { get; set; }
        public DbSet<TransitionConditions> transitionConditions { get; set; }
        public DbSet<DataField> DataFields { get; set; }
    }
}
