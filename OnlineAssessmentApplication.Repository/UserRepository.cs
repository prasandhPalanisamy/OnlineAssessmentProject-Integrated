using OnlineAssessmentApplication.DomainModel;
using OnlineAssessmentApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OnlineAssessmentApplication.Repository
{
    public interface IUserRepository
    {
        List<User> ValidateUser(UserViewModel viewModel);
        string FindRole(UserViewModel user);
        void Create(User user);
        IEnumerable<User> Display(string serach);
        void Delete(int Id);
        User Edit(int Id);
        void Update(User user);
        User GetUserByUserId(int id);
        void UpateUserDetails(User userData);
        void UpdateUserPasswordDetails(User dataToEdit);
        IEnumerable<Role> RoleDisplay();
        bool CheckMailId(string EmailId);
    }

    public class UserRepository : IUserRepository
    {
        readonly private AssessmentDbContext db;



        public UserRepository()
        {
            db = new AssessmentDbContext();
        }
        public List<User> ValidateUser(UserViewModel viewModel)
        {
            List<User> fetchedData = db.Users.Where(temp => temp.EmailID == viewModel.EmailID && temp.Password == viewModel.Password).ToList();
            return fetchedData;
        }
        public string FindRole(UserViewModel user)
        {
            User fetchedData = db.Users.Where(temp => temp.EmailID == user.EmailID && temp.Password == user.Password).FirstOrDefault();
            return fetchedData.Role.RoleName;
        }
        public void Create(User user)
        {

            try
            {

                db.Users.Add(user);
                db.SaveChanges();

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public IEnumerable<User> Display(string serach)
        {

            IEnumerable<User> data = db.Users.Where(User => User.Role.RoleName.ToLower().Contains(serach.ToLower()) || serach == null).ToList();
            return (data);


        }
        public void Delete(int Id)
        {

            User user = db.Users.Find(Id);
            db.Users.Remove(user);
            db.SaveChanges();

        }
        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

        }
        public User Edit(int Id)
        {

            User userData = db.Users.Find(Id);
            return userData;


        }

        public User GetUserByUserId(int id)
        {
            var userDetail = db.Users.Where(userDatas => userDatas.UserId == id).FirstOrDefault();
            return userDetail;
        }
        public void UpateUserDetails(User userData)
        {
            User fetchedUserData = db.Users.Where(userDatas => userDatas.UserId == userData.UserId).FirstOrDefault();
            if (fetchedUserData != null)
            {
                fetchedUserData.Name = userData.Name;
                fetchedUserData.PhoneNumber = userData.PhoneNumber;
                db.SaveChanges();
            }
        }
        public void UpdateUserPasswordDetails(User dataToEdit)
        {
            User fetchedUserData = db.Users.Where(userData => userData.UserId == dataToEdit.UserId).FirstOrDefault();
            if (fetchedUserData != null)
            {
                fetchedUserData.Password = dataToEdit.Password;
                db.SaveChanges();
            }
        }
        public IEnumerable<Role> RoleDisplay()
        {

            IEnumerable<Role> data = db.Roles.ToList();
            return (data);

        }
        public bool CheckMailId(string EmailId)
        {

            return db.Users.Where(User => User.EmailID.Contains(EmailId)).ToList().Count == 0;


        }
    }
}