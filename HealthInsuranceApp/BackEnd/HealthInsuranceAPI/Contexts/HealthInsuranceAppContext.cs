using Microsoft.EntityFrameworkCore;
using HealthInsuranceAPI.Models;

namespace HealthInsuranceApp.Data
{
    public class HealthInsuranceAppContext : DbContext
    {
        public HealthInsuranceAppContext(DbContextOptions<HealthInsuranceAppContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<InsurancePolicy> InsurancePolicies { get; set; }
        public DbSet<CustomerPolicy> CustomerPolicies { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User - Customer relationship
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Customer>(c => c.UserID);

            // User - Agent relationship
            modelBuilder.Entity<Agent>()
                .HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<Agent>(a => a.UserID);

            // Customer - CustomerPolicy relationship
            modelBuilder.Entity<CustomerPolicy>()
                .HasOne(cp => cp.Customer)
                .WithMany(c => c.CustomerPolicies)
                .HasForeignKey(cp => cp.CustomerID);

            // InsurancePolicy - CustomerPolicy relationship
            modelBuilder.Entity<CustomerPolicy>()
                .HasOne(cp => cp.InsurancePolicy)
                .WithMany()
                .HasForeignKey(cp => cp.PolicyID);

            // CustomerPolicy - Claim relationship
            modelBuilder.Entity<Claim>()
                .HasOne(cl => cl.CustomerPolicy)
                .WithMany(cp => cp.Claims)
                .HasForeignKey(cl => cl.CustomerPolicyID);

            // CustomerPolicy - Payment relationship
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.CustomerPolicy)
                .WithMany(cp => cp.Payments)
                .HasForeignKey(p => p.CustomerPolicyID);
        }
    }
}
