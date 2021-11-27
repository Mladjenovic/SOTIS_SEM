using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using backend.Models;

#nullable disable

namespace backend.Data
{
    public partial class DbSotisContext : DbContext
    {
        public DbSotisContext()
        {
        }

        public DbSotisContext(DbContextOptions<DbSotisContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<KnowledgeSpace> KnowledgeSpaces { get; set; }
        public virtual DbSet<Problem> Problems { get; set; }
        public virtual DbSet<ProblemsStudent> ProblemsStudents { get; set; }
        public virtual DbSet<Profesor> Profesors { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Sumarise> Sumarises { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestResult> TestResults { get; set; }
        public virtual DbSet<TestResultsAnswer> TestResultsAnswers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Answers__Questio__4222D4EF");
            });

            modelBuilder.Entity<KnowledgeSpace>(entity =>
            {
                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.KnowledgeSpaces)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK__Knowledge__Subje__49C3F6B7");
            });

            modelBuilder.Entity<Problem>(entity =>
            {
                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Problems)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK__Problems__Subjec__45F365D3");

                entity.HasOne(d => d.Sumarize)
                    .WithMany(p => p.Problems)
                    .HasForeignKey(d => d.SumarizeId)
                    .HasConstraintName("FK__Problems__Sumari__46E78A0C");
            });

            modelBuilder.Entity<ProblemsStudent>(entity =>
            {
                entity.HasOne(d => d.Problem)
                    .WithMany()
                    .HasForeignKey(d => d.ProblemId)
                    .HasConstraintName("FK__ProblemsS__Probl__4BAC3F29");

                entity.HasOne(d => d.Student)
                    .WithMany()
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__ProblemsS__Stude__4AB81AF0");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasOne(d => d.Problem)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.ProblemId)
                    .HasConstraintName("FK__Questions__Probl__412EB0B6");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Questions__Secti__403A8C7D");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sections__TestId__3F466844");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasOne(d => d.Profesor)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.ProfesorId)
                    .HasConstraintName("FK__Subjects__Profes__44FF419A");
            });

            modelBuilder.Entity<Sumarise>(entity =>
            {
                entity.HasOne(d => d.KnowledgeSpace)
                    .WithMany(p => p.Sumarises)
                    .HasForeignKey(d => d.KnowledgeSpaceId)
                    .HasConstraintName("FK__Sumarises__Knowl__48CFD27E");

                entity.HasOne(d => d.Problem)
                    .WithMany(p => p.Sumarises)
                    .HasForeignKey(d => d.ProblemId)
                    .HasConstraintName("FK__Sumarises__Probl__47DBAE45");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasOne(d => d.Profesor)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.ProfesorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tests__ProfesorI__3B75D760");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tests__SubjectId__3C69FB99");
            });

            modelBuilder.Entity<TestResult>(entity =>
            {
                entity.HasOne(d => d.Student)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TestResul__Stude__3D5E1FD2");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TestResul__TestI__3E52440B");
            });

            modelBuilder.Entity<TestResultsAnswer>(entity =>
            {
                entity.HasKey(e => new { e.TestResultId, e.AnswerId })
                    .HasName("PK__TestResu__BF0E1087C53694B2");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.TestResultsAnswers)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TestResul__Answe__440B1D61");

                entity.HasOne(d => d.TestResult)
                    .WithMany(p => p.TestResultsAnswers)
                    .HasForeignKey(d => d.TestResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TestResul__TestR__4316F928");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
