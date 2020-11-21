using AutoMapper;
using OnlineAssessmentApplication.DomainModel;
using OnlineAssessmentApplication.Repository;
using OnlineAssessmentApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineAssessmentApplication.ServiceLayer
{
    public interface IUserService
    {
        UserViewModel ValidateUser(UserViewModel user);
        void Create(User user);
        void Delete(int UserId);
        User Edit(int UserId);

        void Update(User user);
        IEnumerable<User> Display(string serach);

        string FindRole(UserViewModel user);
        UserViewModel GetUserByUserId(int id);
        void UpdateUserDetails(EditUserViewModel userData);
        void UpdateUserPasswordDetails(EditUserPasswordViewModel dataToEdit);
        IEnumerable<Role> RoleDisplay();
        bool CheckMailId(string EmailId);
    }
    public class UserService : IUserService
    {
        readonly IUserRepository userRepository;
        public UserService()
        {
            this.userRepository = new UserRepository();
        }
        public UserViewModel ValidateUser(UserViewModel user)
        {
            User fetchedData = null;
            fetchedData = userRepository.ValidateUser(user).FirstOrDefault();
            UserViewModel sensitiveData = null;
            if (fetchedData != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                sensitiveData = mapper.Map<User, UserViewModel>(fetchedData);

            }
            return sensitiveData;
        }
        public string FindRole(UserViewModel user)
        {
            return userRepository.FindRole(user);

        }
        public void Create(User user)
        {
            user.CreatedDate = DateTime.Now.Date;
            userRepository.Create(user);
        }
        public void Delete(int UserId)
        {
            userRepository.Delete(UserId);
        }
        public User Edit(int UserId)
        {
            return userRepository.Edit(UserId);
        }
        public void Update(User user)
        {
            user.ModifiedDate = DateTime.Now.Date;
            userRepository.Update(user);
        }

        public IEnumerable<User> Display(string serach)
        {
            return (userRepository.Display(serach));
        }
        public UserViewModel GetUserByUserId(int id)
        {
            User userDetail = userRepository.GetUserByUserId(id);
            UserViewModel fetchedData = null;
            if (userDetail != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                fetchedData = mapper.Map<User, UserViewModel>(userDetail);

            }
            return fetchedData;
        }
        public void UpdateUserDetails(EditUserViewModel userData)
        {
            User dataToEdit = null;
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            dataToEdit = mapper.Map<EditUserViewModel, User>(userData);
            userRepository.UpateUserDetails(dataToEdit);


        }
        public void UpdateUserPasswordDetails(EditUserPasswordViewModel dataToEdit)
        {
            User fetchedData = null;
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserPasswordViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            fetchedData = mapper.Map<EditUserPasswordViewModel, User>(dataToEdit);
            userRepository.UpdateUserPasswordDetails(fetchedData);
        }
        public IEnumerable<Role> RoleDisplay()
        {
            return (userRepository.RoleDisplay());
        }
        public bool CheckMailId(string EmailId)
        {
            return (userRepository.CheckMailId(EmailId));
        }
    }
}