using Microsoft.EntityFrameworkCore;
using QAM.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace QAM.Data.DBOperations
{
    public class QmDbContext:DbContext
    {
        public QmDbContext(DbContextOptions<QmDbContext> options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagSubject> TagSubjects { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-Many ilişki konfigürasyonu
            modelBuilder.Entity<Contact>()
                .HasOne(p => p.User)            // Contact sınıfındaki User property'sine dayalı olarak bir Category
                .WithMany(c => c.Contacts)      // User sınıfındaki Contacts koleksiyonuna dayalı olarak birçok Contact
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction); 

            // One-to-Many ilişki konfigürasyonu
            modelBuilder.Entity<Favorite>()
                .HasOne(p => p.User)            
                .WithMany(c => c.Favorites)      
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            // One-to-Many ilişki konfigürasyonu
            modelBuilder.Entity<Favorite>()
                .HasOne(p => p.Subject)
                .WithMany(c => c.Favorites)
                .HasForeignKey(p => p.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);


            // One-to-Many ilişki konfigürasyonu
            modelBuilder.Entity<Question>()
                .HasOne(p => p.Subject)
                .WithMany(c => c.Questions)
                .HasForeignKey(p => p.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);


            // One-to-Many ilişki konfigürasyonu
            modelBuilder.Entity<Subject>()
                .HasOne(p => p.User)
                .WithMany(c => c.Subjects)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            // One-to-Many ilişki konfigürasyonu
            modelBuilder.Entity<TagSubject>()
                .HasOne(p => p.Tag)
                .WithMany(c => c.Subjects)
                .HasForeignKey(p => p.TagId)
                .OnDelete(DeleteBehavior.NoAction);


            // One-to-Many ilişki konfigürasyonu
            modelBuilder.Entity<TagSubject>()
                .HasOne(p => p.Subject)
                .WithMany(c => c.Tags)
                .HasForeignKey(p => p.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);


            // One-to-Many ilişki konfigürasyonu
            modelBuilder.Entity<User>()
                .HasOne(p => p.Role)
                .WithMany(c => c.Users)
                .HasForeignKey(p => p.RoleId)
                .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new TagSubjectConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
