using Forum.Domain.Models.Account;
using Forum.Domain.Models.Content;
using Forum.Domain.Models.Questions;
using Forum.Domain.Models.Tags;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data.Context;

public class ForumDbContext : DbContext
{
    #region ctor

    public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
    {
    }

    #endregion

    #region Db set

    public DbSet<User> Users { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<QuestionCategory> QuestionCategories { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionVisit> QuestionVisits { get; set; }
    public DbSet<QuestionTag> QuestionTags { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<RequestTag> RequestTags { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relation in
                 modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
        {
            relation.DeleteBehavior = DeleteBehavior.Restrict;
        }

        base.OnModelCreating(modelBuilder);
    }
}