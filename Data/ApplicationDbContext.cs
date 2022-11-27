using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebProjectMVC.Models;

namespace WebProjectMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PhotoDetail> PhotoDetail { get; set; }
        public DbSet<Reaction> Reaction { get; set; }
        public DbSet<PhotoReactions> PhotoReactions { get; set; }
        public DbSet<PhotoDetailReactions> PhotoDetailReactions { get; set; }
        public DbSet<CommentReactions> CommentReactions { get; set; }
        public DbSet<PhotoDetailComment> PhotoDetailComment { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CategoryPhotos>()
            .HasKey(t => new { t.CategoryId, t.PhotosId });

            //One(Category)toMany(Photo)
            builder.Entity<CategoryPhotos>()
                .HasOne(pt => pt.Category)
                .WithMany(p => p.CategoryPhotos)
                .HasForeignKey(pt => pt.CategoryId);

            //One(Photo)toMany(Category)
            builder.Entity<CategoryPhotos>()
                .HasOne(pt => pt.Photos)
                .WithMany(t => t.CategoryPhotos)
                .HasForeignKey(pt => pt.PhotosId);

            //One(User)withMany(Photo)
            builder.Entity<Photo>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.Photos)
                .HasForeignKey(pt => pt.ApplicationUserId);

            //One(Photo)WithMany(Reaction)
            builder.Entity<PhotoReactions>()
                .HasOne(pt => pt.Photos)
                .WithMany(t => t.PhotoReactions)
                .HasForeignKey(pt => pt.PhotoId);

            //One(Photo)WithMany(PhotoDetails)
            builder.Entity<PhotoDetail>()
                .HasOne(pt => pt.Photo)
                .WithMany(t => t.PhotoDetails)
                .HasForeignKey(pt => pt.PhotoId);

            //One(PhotoDetail)WithMany(Reaction)
            builder.Entity<PhotoDetailReactions>()
                .HasOne(pt => pt.PhotoDetail)
                .WithMany(t => t.PhotoDetailReactions)
                .HasForeignKey(pt => pt.PhotoDetailId);

            //One(User)withMany(PhotoDetailReaction)
            builder.Entity<PhotoDetailReactions>()
                .HasKey(pt => new { pt.PhotoDetailId, pt.ApplicationUserId });
            builder.Entity<PhotoDetailReactions>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.PhotoDetailReactions)
                .HasForeignKey(pt => pt.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            //One(PhotoDetail)WithMany(PhotoDetailComments)
            builder.Entity<PhotoDetailComment>()
                .HasOne(pt => pt.PhotoDetail)
                .WithMany(t => t.PhotoDetailComments)
                .HasForeignKey(pt => pt.PhotoDetailId);

            //One(User)withMany(PhotoReaction)
            builder.Entity<PhotoReactions>()
                .HasKey(pt => new { pt.PhotoId, pt.ApplicationUserId });
            //builder.Entity<PhotoReactions>()
            //    .HasIndex(i => new { i.PhotoId, i.ReactionId });
            builder.Entity<PhotoReactions>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.PhotoReactions)
                .HasForeignKey(pt => pt.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            //One(User)withMany(Comment)
            builder.Entity<Comment>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.Comments)
                .HasForeignKey(pt => pt.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            //One(User)withMany(CommentReactions)
            builder.Entity<CommentReactions>()
                .HasKey(pt => new { pt.CommentId, pt.ApplicationUserId, pt.ReactionId });
            builder.Entity<CommentReactions>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.CommentReactions)
                .HasForeignKey(pt => pt.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}