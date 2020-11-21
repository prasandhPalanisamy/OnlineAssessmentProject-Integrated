using System.Data.Entity;


namespace OnlineAssessmentApplication.DomainModel
{
    public class AssessmentDbContext : DbContext
    {
        public AssessmentDbContext() : base("connectionStringToDB")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<StudentResponse> studentResponses { get; set; }
    }
}
