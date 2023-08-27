using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tasks.Data.Models
{
    public partial class TaskDatabaseContext : DbContext
    {
        public TaskDatabaseContext()
        {
        }

        public TaskDatabaseContext(DbContextOptions<TaskDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AttachmentEntity> Attachments { get; set; } = null!;
        public virtual DbSet<CommentEntity> Comments { get; set; } = null!;
        public virtual DbSet<ProjectEntity> Projects { get; set; } = null!;
        public virtual DbSet<ProjectStatus> ProjectStatuses { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<SystemUserEntity> SystemUsers { get; set; } = null!;
        public virtual DbSet<TagEntity> Tags { get; set; } = null!;
        public virtual DbSet<TaskEntity> Tasks { get; set; } = null!;
        public virtual DbSet<TaskCategoryEntity> TaskCategories { get; set; } = null!;
        public virtual DbSet<TaskPriority> TaskPriorities { get; set; } = null!;
        public virtual DbSet<TaskStatusEntity> TaskStatuses { get; set; } = null!;
        public virtual DbSet<UserEntity> Users { get; set; } = null!;
        public virtual DbSet<UserSettingEntity> UserSettings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttachmentEntity>(entity =>
            {
                entity.Property(e => e.AttachmentId).HasColumnName("AttachmentID");

                entity.Property(e => e.FileName).HasMaxLength(100);

                entity.Property(e => e.FilePath).HasMaxLength(500);

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.UploadDate).HasColumnType("datetime");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK__Attachmen__TaskI__45F365D3");
            });

            modelBuilder.Entity<CommentEntity>(entity =>
            {
                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.CommentDate).HasColumnType("datetime");

                entity.Property(e => e.CommentText).HasMaxLength(500);

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK__Comments__TaskID__4222D4EF");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Comments__UserID__4316F928");
            });

            modelBuilder.Entity<ProjectEntity>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectName).HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Project_ProjectStatuses");
            });

            modelBuilder.Entity<ProjectStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__ProjectS__C8EE2043F23479E5");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusName).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<SystemUserEntity>(entity =>
            {
                entity.ToTable("SystemUser");

                entity.Property(e => e.SystemUserId).HasColumnName("SystemUserID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.PasswordHash).HasMaxLength(128);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SystemUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_SystemUser_Users");
            });

            modelBuilder.Entity<TagEntity>(entity =>
            {
                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.TagName).HasMaxLength(50);
            });

            modelBuilder.Entity<TaskEntity>(entity =>
            {
                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.PriorityId).HasColumnName("PriorityID");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Tasks__CategoryI__59063A47");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.PriorityId)
                    .HasConstraintName("FK__Tasks__PriorityI__59FA5E80");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__Tasks__ProjectID__74AE54BC");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Tasks__StatusID__6B24EA82");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Tasks__UserID__3F466844");

                entity.HasMany(d => d.DependentTasks)
                    .WithMany(p => p.Tasks)
                    .UsingEntity<Dictionary<string, object>>(
                        "TaskDependency",
                        l => l.HasOne<TaskEntity>().WithMany().HasForeignKey("DependentTaskId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__TaskDepen__Depen__4D94879B"),
                        r => r.HasOne<TaskEntity>().WithMany().HasForeignKey("TaskId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__TaskDepen__TaskI__4CA06362"),
                        j =>
                        {
                            j.HasKey("TaskId", "DependentTaskId").HasName("PK__TaskDepe__5FF3166E57AA29E8");

                            j.ToTable("TaskDependencies");

                            j.IndexerProperty<int>("TaskId").HasColumnName("TaskID");

                            j.IndexerProperty<int>("DependentTaskId").HasColumnName("DependentTaskID");
                        });

                entity.HasMany(d => d.Tags)
                    .WithMany(p => p.Tasks)
                    .UsingEntity<Dictionary<string, object>>(
                        "TaskTag",
                        l => l.HasOne<TagEntity>().WithMany().HasForeignKey("TagId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__TaskTags__TagID__68487DD7"),
                        r => r.HasOne<TaskEntity>().WithMany().HasForeignKey("TaskId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__TaskTags__TaskID__6754599E"),
                        j =>
                        {
                            j.HasKey("TaskId", "TagId").HasName("PK__TaskTags__AA3E86750EF83E15");

                            j.ToTable("TaskTags");

                            j.IndexerProperty<int>("TaskId").HasColumnName("TaskID");

                            j.IndexerProperty<int>("TagId").HasColumnName("TagID");
                        });

                entity.HasMany(d => d.Tasks)
                    .WithMany(p => p.DependentTasks)
                    .UsingEntity<Dictionary<string, object>>(
                        "TaskDependency",
                        l => l.HasOne<TaskEntity>().WithMany().HasForeignKey("TaskId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__TaskDepen__TaskI__4CA06362"),
                        r => r.HasOne<TaskEntity>().WithMany().HasForeignKey("DependentTaskId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__TaskDepen__Depen__4D94879B"),
                        j =>
                        {
                            j.HasKey("TaskId", "DependentTaskId").HasName("PK__TaskDepe__5FF3166E57AA29E8");

                            j.ToTable("TaskDependencies");

                            j.IndexerProperty<int>("TaskId").HasColumnName("TaskID");

                            j.IndexerProperty<int>("DependentTaskId").HasColumnName("DependentTaskID");
                        });
            });

            modelBuilder.Entity<TaskCategoryEntity>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__TaskCate__19093A2B12574FDC");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<TaskPriority>(entity =>
            {
                entity.HasKey(e => e.PriorityId)
                    .HasName("PK__TaskPrio__D0A3D0DE1DC0109B");

                entity.Property(e => e.PriorityId).HasColumnName("PriorityID");

                entity.Property(e => e.PriorityName).HasMaxLength(50);
            });

            modelBuilder.Entity<TaskStatusEntity>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__TaskStat__C8EE2043928E9874");

                entity.HasIndex(e => e.StatusOrder, "UC_StatusOrder")
                    .IsUnique();

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusName).HasMaxLength(50);
            });

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasMany(d => d.Projects)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserProject",
                        l => l.HasOne<ProjectEntity>().WithMany().HasForeignKey("ProjectId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__UserProje__Proje__73BA3083"),
                        r => r.HasOne<UserEntity>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__UserProje__UserI__72C60C4A"),
                        j =>
                        {
                            j.HasKey("UserId", "ProjectId").HasName("PK__UserProj__00E9674125878C19");

                            j.ToTable("UserProjects");

                            j.IndexerProperty<int>("UserId").HasColumnName("UserID");

                            j.IndexerProperty<int>("ProjectId").HasColumnName("ProjectID");
                        });

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRole",
                        l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__UserRoles__RoleI__3C69FB99"),
                        r => r.HasOne<UserEntity>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__UserRoles__UserI__3B75D760"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId").HasName("PK__UserRole__AF27604F542EA41E");

                            j.ToTable("UserRoles");

                            j.IndexerProperty<int>("UserId").HasColumnName("UserID");

                            j.IndexerProperty<int>("RoleId").HasColumnName("RoleID");
                        });
            });

            modelBuilder.Entity<UserSettingEntity>(entity =>
            {
                entity.HasKey(e => e.SettingId)
                    .HasName("PK__UserSett__54372AFDC787DBD7");

                entity.Property(e => e.SettingId).HasColumnName("SettingID");

                entity.Property(e => e.SettingName).HasMaxLength(50);

                entity.Property(e => e.SettingValue).HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSettings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserSetti__UserI__6E01572D");
            });

            this.OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
