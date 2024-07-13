using Forum.Application.Services.Implementations.Account;
using Forum.Application.Services.Implementations.Content.Banner;
using Forum.Application.Services.Implementations.Question;
using Forum.Application.Services.Implementations.Tag;
using Forum.Application.Services.Interfaces.Account;
using Forum.Application.Services.Interfaces.Content.Banner;
using Forum.Application.Services.Interfaces.Question;
using Forum.Application.Services.Interfaces.Tag;
using Forum.Data.Repositories.Implementations.Account;
using Forum.Data.Repositories.Implementations.Content.Banner;
using Forum.Data.Repositories.Implementations.Question;
using Forum.Data.Repositories.Implementations.Tag;
using Forum.Data.Repositories.Interfaces.Account;
using Forum.Data.Repositories.Interfaces.Content.Banner;
using Forum.Data.Repositories.Interfaces.Question;
using Forum.Data.Repositories.Interfaces.Tag;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.IoC;

public class DependencyContainer
{
    public static void RegisterDependencies(IServiceCollection services)
    {
        #region Services

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IQuestionCategoryService, QuestionCategoryService>();
        services.AddScoped<IQuestionTagService, QuestionTagService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IBannerRepository, BannerRepository>();

        #endregion

        #region Repositories

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IQuestionCategoryRepository, QuestionCategoryRepository>();
        services.AddScoped<IQuestionTagRepository, QuestionTagRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IBannerService, BannerService>();

        #endregion
    }
}