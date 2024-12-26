using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace University_System.Models;

public partial class UniversitySystemContext : DbContext
{
    public UniversitySystemContext()
    {
    }

    public UniversitySystemContext(DbContextOptions<UniversitySystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DepartmentCourse> DepartmentCourses { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentCourse> StudentCourses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=AD\\ABDULRAHMANDIAA;Database=University_System;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Cno).HasName("PK__Course__C1FF675B5119DD31");

            entity.ToTable("Course");

            entity.HasIndex(e => e.Name, "UQ__Course__737584F6803E1F69").IsUnique();

            entity.Property(e => e.Cno).HasColumnName("CNo");
            entity.Property(e => e.Describtion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.SchNoNavigation).WithMany(p => p.Courses)
                .HasForeignKey(d => d.SchNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Course__SchNo__403A8C7D");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Dno).HasName("PK__Departme__C035B8E287126A39");

            entity.ToTable("Department");

            entity.HasIndex(e => e.Name, "UQ__Departme__737584F6F1CDF9D8").IsUnique();

            entity.Property(e => e.Dno).HasColumnName("DNo");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<DepartmentCourse>(entity =>
        {
            entity.HasKey(e => new { e.Dno, e.Cno }).HasName("PK__Departme__6C2A4E9704EB2EC3");

            entity.ToTable("Department_Courses");

            entity.Property(e => e.Dno).HasColumnName("DNo");
            entity.Property(e => e.Cno).HasColumnName("CNo");
            entity.Property(e => e.Semester)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.CnoNavigation).WithMany(p => p.DepartmentCourses)
                .HasForeignKey(d => d.Cno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Department___CNo__47DBAE45");

            entity.HasOne(d => d.DnoNavigation).WithMany(p => p.DepartmentCourses)
                .HasForeignKey(d => d.Dno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Department___DNo__46E78A0C");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.SchNo).HasName("PK__Schedule__CAD98F70B56F69C0");

            entity.ToTable("Schedule");

            entity.Property(e => e.Day)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Sid).HasName("PK__Student__CA19595048054A47");

            entity.ToTable("Student");

            entity.Property(e => e.Sid).HasColumnName("SId");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Dno).HasColumnName("DNo");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Fname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("LName");
            entity.Property(e => e.PhoneNumebr)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.DnoNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.Dno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student__DNo__3A81B327");
        });

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.HasKey(e => new { e.Sid, e.Cno }).HasName("PK__Student___6606AF256A89E5F0");

            entity.ToTable("Student_Courses");

            entity.Property(e => e.Sid).HasColumnName("SId");
            entity.Property(e => e.Cno).HasColumnName("CNo");

            entity.HasOne(d => d.CnoNavigation).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.Cno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student_Cou__CNo__440B1D61");

            entity.HasOne(d => d.SidNavigation).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.Sid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student_Cou__SId__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
