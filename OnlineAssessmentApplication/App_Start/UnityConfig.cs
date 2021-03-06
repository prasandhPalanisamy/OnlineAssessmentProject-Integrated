using OnlineAssessmentApplication.Repository;
using OnlineAssessmentApplication.ServiceLayer;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace OnlineAssessmentApplication
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ITestService, TestService>();
            container.RegisterType<ITestRepository, TestRepository>();
            container.RegisterType<IQuestionService, QuestionService>();
            container.RegisterType<IQuestionRepository, QuestionRepository>();
            container.RegisterType<IAnswerService, AnswerService>();
            container.RegisterType<IAnswerRepository, AnswerRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}