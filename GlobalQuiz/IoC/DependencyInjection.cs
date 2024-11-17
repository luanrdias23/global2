using GlobalQuiz.Domain.Interfaces;
using GlobalQuiz.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalQuiz.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
        {
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            return services;
        }
    }
}
