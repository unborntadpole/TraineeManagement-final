namespace TraineeManagementApi.db;

using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.Models;

using System;
using System.Globalization;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }
    public DbSet<Trainee> Trainees { get; set; }
    public DbSet<Mentor> Mentors { get; set; }
    public DbSet<LearningTask> LearningTasks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<TaskAssignment> TaskAssignments { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<SubmissionFile> SubmissionFiles { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<ProcessingJob> ProcessingJobs { get; set; }

    private readonly string format = "yyyy-MM-dd HH:mm:ss.ffffff"; 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaskAssignment>()
            .HasOne(e => e.LearningTask)
            .WithMany(e => e.TaskAssignments)
            .HasForeignKey(e => e.LearningTaskId)
            .IsRequired();
        modelBuilder.Entity<TaskAssignment>()
            .HasOne(e => e.Trainee)
            .WithMany(e => e.TaskAssignments)
            .HasForeignKey(e => e.TraineeId)
            .IsRequired();
        modelBuilder.Entity<TaskAssignment>()
            .HasOne(e => e.Mentor)
            .WithMany(e => e.TaskAssignments)
            .HasForeignKey(e => e.MentorId)
            .IsRequired();

        // modelBuilder.Entity<Submission>()
        //     .HasOne(e => e.TaskAssignment)
        //     .WithOne(e => e.Submission)
        //     .HasForeignKey<Submission>(e => e.TaskAssignmentId)
        //     .IsRequired();
        modelBuilder.Entity<Submission>()
            .HasOne(e => e.TaskAssignment)
            .WithMany(e => e.Submissions)
            .HasForeignKey(e => e.TaskAssignmentId)
            .IsRequired();

        modelBuilder.Entity<SubmissionFile>()
            .HasOne(e => e.Submission)
            .WithOne(e => e.SubmissionFile)
            .HasForeignKey<SubmissionFile>(e => e.SubmissionId)
            .IsRequired();

        // modelBuilder.Entity<Review>()
        //     .HasOne(e => e.Submission)
        //     .WithOne(e => e.Review)
        //     .HasForeignKey<Review>(e => e.SubmissionId)
        //     .IsRequired();
        modelBuilder.Entity<Review>()
            .HasOne(e => e.Submission)
            .WithMany(e => e.Reviews)
            .HasForeignKey(e => e.SubmissionId)
            .IsRequired();
        modelBuilder.Entity<Review>()
            .HasOne(e => e.Mentor)
            .WithMany(e => e.Reviews)
            .HasForeignKey(e => e.MentorId)
            .IsRequired();

        // modelBuilder.Entity<User>()
        //     .Property(u => u.Id)
        //     .ValueGeneratedOnAdd();
        
        // modelBuilder.Entity<Trainee>()
        //     .Property(u => u.Id)
        //     .ValueGeneratedOnAdd();
            
        // modelBuilder.Entity<Mentor>()
        //     .Property(u => u.Id)
        //     .ValueGeneratedOnAdd();

        modelBuilder.Entity<User>().HasData(
            new User 
            { 
                Id = 1,
                Email = "samriddh.singh@zeuslearning.com", 
                Username = "admin", 
                PasswordHash = "AQAAAAIAAYagAAAAEP+QfNdJZtmZSCQUsvRTWt8NlKADYbY44q8GjYNIUhVn8c2ANxKiw50h4muvwf7ydg==", 
                Role = "Admin", 
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            }


        );

        modelBuilder.Entity<LearningTask>().HasData(
            
            new LearningTask
            {
                Id = 1,
                Title = "Task Tracker",
                Description = "Build a task tracker to creat",
                ExpectedTechStack = "manage tasks",
                DueDate = DateTime.ParseExact("2026-06-20 06:54:02.743000", format, CultureInfo.InvariantCulture),
                Status = "Published",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:56:08.624395", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:56:08.624396", format, CultureInfo.InvariantCulture),
            },
        
            new LearningTask
            {
                Id = 2,
                Title = "Trainee Management API - Phase 1",
                Description = "Build an API server with inMemory database",
                ExpectedTechStack = "ASP.NET Core WebAPI",
                DueDate = DateTime.ParseExact("2026-06-23 06:54:02.743000", format, CultureInfo.InvariantCulture),
                Status = "Published",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:57:37.782667", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:57:37.782668", format, CultureInfo.InvariantCulture),
            },
        
            new LearningTask
            {
                Id = 3,
                Title = "Trainee Management API - Phase 2",
                Description = "Build an API server with sql database",
                ExpectedTechStack = "ASP.NET Core WebAPI",
                DueDate = DateTime.ParseExact("2026-06-28 06:54:02.743000", format, CultureInfo.InvariantCulture),
                Status = "Published",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:57:58.632111", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:57:58.632112", format, CultureInfo.InvariantCulture),
            }
        );

        modelBuilder.Entity<Mentor>().HasData(
            
            new Mentor
            {
                Id = 1,
                Email = "abhay.gori@zeuslearning.com",
                FirstName = "Abhay",
                LastName = "Gori",
                Expertise = "ASP .Net Core",
                Status = "Active",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:50:09.571411", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:50:09.571412", format, CultureInfo.InvariantCulture),
            },
        
            new Mentor
            {
                Id = 2,
                Email = "mentor2@zeuslearning.com",
                FirstName = "Rheetik",
                LastName = "Sharma",
                Expertise = "ASP .Net Core",
                Status = "Active",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:50:31.077260", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:50:31.077261", format, CultureInfo.InvariantCulture),
            },
        
            new Mentor
            {
                Id = 3,
                Email = "mentor3@zeuslearning.com",
                FirstName = "Jay Prakash",
                LastName = "Yadav",
                Expertise = "ASP .Net Core",
                Status = "Active",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:50:50.967220", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:50:50.967221", format, CultureInfo.InvariantCulture),
            },
        
            new Mentor
            {
                Id = 4,
                Email = "mentor3@zeuslearning.com",
                FirstName = "Jay Prakash",
                LastName = "Yadav",
                Expertise = "ASP .Net Core",
                Status = "Active",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:51:16.940210", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:51:16.940211", format, CultureInfo.InvariantCulture),
            },
        
            new Mentor
            {
                Id = 5,
                Email = "mentor3@zeuslearning.com",
                FirstName = "Jay Prakash",
                LastName = "Yadav",
                Expertise = "ASP .Net Core",
                Status = "Active",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:51:17.105092", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:51:17.105093", format, CultureInfo.InvariantCulture),
            },
        
            new Mentor
            {
                Id = 6,
                Email = "mentor3@zeuslearning.com",
                FirstName = "Jay Prakash",
                LastName = "Yadav",
                Expertise = "ASP .Net Core",
                Status = "Active",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:51:17.284330", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:51:17.284330", format, CultureInfo.InvariantCulture),
            },
        
            new Mentor
            {
                Id = 7,
                Email = "mentor3@zeuslearning.com",
                FirstName = "Jay Prakash",
                LastName = "Yadav",
                Expertise = "ASP .Net Core",
                Status = "Active",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:51:17.453301", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:51:17.453302", format, CultureInfo.InvariantCulture),
            },
        
            new Mentor
            {
                Id = 8,
                Email = "mentor3@zeuslearning.com",
                FirstName = "Jay Prakash",
                LastName = "Yadav",
                Expertise = "ASP .Net Core",
                Status = "Active",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:51:17.624172", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:51:17.624173", format, CultureInfo.InvariantCulture),
            },
        
            new Mentor
            {
                Id = 9,
                Email = "mentor3@zeuslearning.com",
                FirstName = "Jay Prakash",
                LastName = "Yadav",
                Expertise = "ASP .Net Core",
                Status = "Active",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:51:17.775710", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:51:17.775711", format, CultureInfo.InvariantCulture),
            },
        
            new Mentor
            {
                Id = 10,
                Email = "mentor3@zeuslearning.com",
                FirstName = "Jay Prakash",
                LastName = "Yadav",
                Expertise = "ASP .Net Core",
                Status = "Active",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:51:17.931052", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:51:17.931052", format, CultureInfo.InvariantCulture),
            },
        
            new Mentor
            {
                Id = 11,
                Email = "mentor3@zeuslearning.com",
                FirstName = "Jay Prakash",
                LastName = "Yadav",
                Expertise = "ASP .Net Core",
                Status = "Active",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:51:18.083648", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:51:18.083649", format, CultureInfo.InvariantCulture),
            },
        
            new Mentor
            {
                Id = 12,
                Email = "mentor3@zeuslearning.com",
                FirstName = "Jay Prakash",
                LastName = "Yadav",
                Expertise = "ASP .Net Core",
                Status = "Active",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:51:18.297740", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:51:18.297741", format, CultureInfo.InvariantCulture),
            }
        );

        modelBuilder.Entity<Trainee>().HasData(
            
            new Trainee
            {
                Id = 1,
                Email = "trainee1@example.com",
                FirstName = "Samriddh",
                LastName = "Singh",
                Status = "Active",
                TechStack = "ASP.Net core",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:48:26.726042", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:48:26.726043", format, CultureInfo.InvariantCulture),
            },
        
            new Trainee
            {
                Id = 2,
                Email = "trainee2@example.com",
                FirstName = "Ankit",
                LastName = "Kumar",
                Status = "Active",
                TechStack = "ASP.Net core",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:48:42.651134", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:48:42.651135", format, CultureInfo.InvariantCulture),
            },
        
            new Trainee
            {
                Id = 3,
                Email = "trainee3@example.com",
                FirstName = "Yash",
                LastName = "Sharma",
                Status = "Active",
                TechStack = "ASP.Net core",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:48:56.664599", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:48:56.664600", format, CultureInfo.InvariantCulture),
            },
        
            new Trainee
            {
                Id = 4,
                Email = "trainee3@example.com",
                FirstName = "Yash",
                LastName = "Sharma",
                Status = "Active",
                TechStack = "ASP.Net core",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:49:00.694427", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:49:00.694428", format, CultureInfo.InvariantCulture),
            },
        
            new Trainee
            {
                Id = 5,
                Email = "trainee3@example.com",
                FirstName = "Yash",
                LastName = "Sharma",
                Status = "Active",
                TechStack = "ASP.Net core",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:49:00.858275", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:49:00.858276", format, CultureInfo.InvariantCulture),
            },
        
            new Trainee
            {
                Id = 6,
                Email = "trainee3@example.com",
                FirstName = "Yash",
                LastName = "Sharma",
                Status = "Active",
                TechStack = "ASP.Net core",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:49:01.014614", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:49:01.014615", format, CultureInfo.InvariantCulture),
            },
        
            new Trainee
            {
                Id = 7,
                Email = "trainee3@example.com",
                FirstName = "Yash",
                LastName = "Sharma",
                Status = "Active",
                TechStack = "ASP.Net core",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:49:01.146741", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:49:01.146742", format, CultureInfo.InvariantCulture),
            },
        
            new Trainee
            {
                Id = 8,
                Email = "trainee3@example.com",
                FirstName = "Yash",
                LastName = "Sharma",
                Status = "Active",
                TechStack = "ASP.Net core",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:49:01.320837", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:49:01.320838", format, CultureInfo.InvariantCulture),
            },
        
            new Trainee
            {
                Id = 9,
                Email = "trainee3@example.com",
                FirstName = "Yash",
                LastName = "Sharma",
                Status = "Active",
                TechStack = "ASP.Net core",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:49:01.484339", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:49:01.484339", format, CultureInfo.InvariantCulture),
            },
        
            new Trainee
            {
                Id = 10,
                Email = "trainee3@example.com",
                FirstName = "Yash",
                LastName = "Sharma",
                Status = "Active",
                TechStack = "ASP.Net core",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:49:01.654526", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:49:01.654527", format, CultureInfo.InvariantCulture),
            },
        
            new Trainee
            {
                Id = 11,
                Email = "trainee3@example.com",
                FirstName = "Yash",
                LastName = "Sharma",
                Status = "Active",
                TechStack = "ASP.Net core",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:49:01.815725", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:49:01.815726", format, CultureInfo.InvariantCulture),
            },
        
            new Trainee
            {
                Id = 12,
                Email = "trainee3@example.com",
                FirstName = "Yash",
                LastName = "Sharma",
                Status = "Active",
                TechStack = "ASP.Net core",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:49:01.974066", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:49:01.974067", format, CultureInfo.InvariantCulture),
            },
        
            new Trainee
            {
                Id = 13,
                Email = "trainee3@example.com",
                FirstName = "Yash",
                LastName = "Sharma",
                Status = "Active",
                TechStack = "ASP.Net core",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:49:02.151160", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:49:02.151161", format, CultureInfo.InvariantCulture),
            },
        
            new Trainee
            {
                Id = 14,
                Email = "trainee3@example.com",
                FirstName = "Yash",
                LastName = "Sharma",
                Status = "Active",
                TechStack = "ASP.Net core",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:49:02.295386", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:49:02.295387", format, CultureInfo.InvariantCulture),
            },
        
            new Trainee
            {
                Id = 15,
                Email = "trainee3@example.com",
                FirstName = "Yash",
                LastName = "Sharma",
                Status = "Active",
                TechStack = "ASP.Net core",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:49:02.449912", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:49:02.449913", format, CultureInfo.InvariantCulture),
            },
        
            new Trainee
            {
                Id = 16,
                Email = "trainee3@example.com",
                FirstName = "Yash",
                LastName = "Sharma",
                Status = "Active",
                TechStack = "ASP.Net core",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:49:02.608787", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:49:02.608788", format, CultureInfo.InvariantCulture),
            },
        
            new Trainee
            {
                Id = 17,
                Email = "trainee3@example.com",
                FirstName = "Yash",
                LastName = "Sharma",
                Status = "Active",
                TechStack = "ASP.Net core",
                CreatedDate = DateTime.ParseExact("2026-06-17 06:49:02.830615", format, CultureInfo.InvariantCulture),
                UpdatedDate = DateTime.ParseExact("2026-06-17 06:49:02.830616", format, CultureInfo.InvariantCulture),
            }
        );
    }
}

