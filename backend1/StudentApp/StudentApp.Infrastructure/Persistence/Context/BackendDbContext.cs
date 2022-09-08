using Microsoft.EntityFrameworkCore;
using StudentApp.Domain.Models;

namespace StudentApp.Infrastructure.Persistence.Context
{
    public partial class BackendDbContext : DbContext
    {
        public BackendDbContext()
        {
        }
        public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Enrolment> Enrolments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:nzmsa2022pokemon.database.windows.net,1433;Initial Catalog=NZMSA2022_Pokemon;Persist Security Info=False;User ID=nzmsa2022pokemon;Password=Phu17022001;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrolment>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Enrolment>()
                .HasOne(s => s.Student)
                .WithMany(e => e.Enrolments)
                .HasForeignKey(bc => bc.StudentId);
            modelBuilder.Entity<Enrolment>()
                .HasOne(s => s.Subject)
                .WithMany(e => e.Enrolments)
                .HasForeignKey(bc => bc.SubjectId);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
