using HRPlatform.Domain.Entities;
using HRPlatform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HRPlatform.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<CandidateSkill> CandidateSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Candidate configuration
            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.ContactNumber)
                    .HasMaxLength(20);

                // Email as owned type
                entity.OwnsOne(c => c.Email, e =>
                {
                    e.Property(email => email.Value)
                        .HasColumnName("Email")
                        .IsRequired()
                        .HasMaxLength(100);
                });

                entity.Property(c => c.DateOfBirth)
                    .IsRequired();

                entity.Property(c => c.CreatedAt)
                    .IsRequired();

                entity.Property(c => c.UpdatedAt);

                // Email unique constraint will be handled at application level
                // Cannot use HasIndex with owned types easily

                // Relationships
                entity.HasMany(c => c.Skills)
                    .WithOne(cs => cs.Candidate)
                    .HasForeignKey(cs => cs.CandidateId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Skill configuration
            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(s => s.CreatedAt)
                    .IsRequired();

                entity.Property(s => s.UpdatedAt);

                // Unique constraint for Skill Name
                entity.HasIndex(s => s.Name)
                    .IsUnique();

                // Relationships
                entity.HasMany(s => s.CandidateSkills)
                    .WithOne(cs => cs.Skill)
                    .HasForeignKey(cs => cs.SkillId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // CandidateSkill (join table) configuration
            modelBuilder.Entity<CandidateSkill>(entity =>
            {
                entity.HasKey(cs => new { cs.CandidateId, cs.SkillId });

                entity.HasOne(cs => cs.Candidate)
                    .WithMany(c => c.Skills)
                    .HasForeignKey(cs => cs.CandidateId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(cs => cs.Skill)
                    .WithMany(s => s.CandidateSkills)
                    .HasForeignKey(cs => cs.SkillId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(cs => cs.AddedAt)
                    .IsRequired();
            });
        }
    }
}