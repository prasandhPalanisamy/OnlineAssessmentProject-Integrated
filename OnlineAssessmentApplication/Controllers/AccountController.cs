using OnlineAssessmentApplication.Filters;
using OnlineAssessmentApplication.ServiceLayer;
using OnlineAssessmentApplication.ViewModel;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CaptchaMvc.HtmlHelpers;
using System.Collections.Generic;
using OnlineAssessmentApplication.DomainModel;
using AutoMapper;

namespace OnlineAssessmentApplication.Controllers
{
    /// <summary>
    /// User Login and Logout
    /// And Also CRED operations for User
    /// </summary>
    //[LogCustomExceptionFilter]
    public class AccountController : Controller
    {
        // GET: Account
        readonly IUserService userService;
        public AccountController()
        {

        }
        public AccountController(IUserService service)
        {
            this.userService = service;
        }
        // GET: Account
        [HttpGet]

        public ActionResult Login()
        {
            // ViewBag.randomNumber=userService.GenerateRandomNumber();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserViewModel user)
        {

            if (ModelState.IsValid)
            {
                if (this.IsCaptchaValid(string.Empty))
                {
                    UserViewModel validatedData = userService.ValidateUser(user);
                    if (validatedData != null)
                    {
                        Session["CurrentUserID"] = validatedData.UserId;
                        Session["CurrentUserEmailId"] = validatedData.EmailID;
                        string role = userService.FindRole(user);
                        FormsAuthentication.SetAuthCookie(validatedData.Name, false);
                        var authTicket = new FormsAuthenticationTicket(1, validatedData.Name, DateTime.Now, DateTime.Now.AddMinutes(20), false, role);
                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        HttpContext.Response.Cookies.Add(authCookie);
                        TempData["Login_successfull_msg"] = "You are successfully logged in";
                        return RedirectToAction("index", "dashboard");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Email or Password");
                        return View(user);
                    }
                }
                else
                {
                    ViewBag.Error = "Captcha is not valid";
                    return View(user);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Email or Password");
                return View();
            }
        }





        [HttpGet]
        [AuthenticationFilter]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [AuthenticationFilter]
        public ActionResult Display(string search)
        {
            IEnumerable<User> data = userService.Display(search);
            var mapcategory = new MapperConfiguration(configurationExpression => { configurationExpression.CreateMap<User, CreateViewModel>(); configurationExpression.IgnoreUnmapped(); });
            IMapper mapper = mapcategory.CreateMapper();
            var userData = mapper.Map<IEnumerable<User>, IEnumerable<CreateViewModel>>(data);
            IEnumerable<Role> Roles = userService.RoleDisplay();
            ViewBag.Roles = new SelectList(Roles, "RoleId", "RoleName");
            return View(userData);
        }
        [AuthenticationFilter]
        public ActionResult Create()
        {

            IEnumerable<Role> Roles = userService.RoleDisplay();
            ViewBag.Roles = new SelectList(Roles, "RoleId", "RoleName");
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthenticationFilter]
        public ActionResult Create(CreateViewModel createviewmodel)
        {

            createviewmodel.CreatedBy = Convert.ToInt32(Session["CurrentUserId"]);

            bool value = userService.CheckMailId(createviewmodel.EmailID);

            if ((value) && (ModelState.IsValid))
            {
                var mapcategory = new MapperConfiguration(configurationExpression => { configurationExpression.CreateMap<CreateViewModel, User>(); configurationExpression.IgnoreUnmapped(); });
                IMapper mapper = mapcategory.CreateMapper();
                var userData = mapper.Map<CreateViewModel, User>(createviewmodel);
                IEnumerable<Role> Roles = userService.RoleDisplay();
                ViewBag.Roles = new SelectList(Roles, "RoleId", "RoleName");
                userService.Create(userData);
                return RedirectToAction("Display");
            }

            else
            {
                ModelState.AddModelError(string.Empty, "Enter valid Data/E-mailId already registered");
                IEnumerable<Role> Roles = userService.RoleDisplay();
                ViewBag.Roles = new SelectList(Roles, "RoleId", "RoleName");
                return View(createviewmodel);
            }


        }
        [AuthenticationFilter]
        public ActionResult Delete(int userId)
        {

            userService.Delete(userId);

            return RedirectToAction("Display");
        }
        [AuthenticationFilter]
        public ActionResult Edit(int userId)
        {


            IEnumerable<Role> Roles = userService.RoleDisplay();
            ViewBag.Roles = new SelectList(Roles, "RoleId", "RoleName");
            User user = userService.Edit(userId);
            var mapcategory = new MapperConfiguration(configurationExpression => { configurationExpression.CreateMap<User, CreateViewModel>(); configurationExpression.IgnoreUnmapped(); });
            IMapper mapper = mapcategory.CreateMapper();
            var userData = mapper.Map<User, CreateViewModel>(user);
            return View(userData);



        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthenticationFilter]
        public ActionResult Update(CreateViewModel createviewmodel)
        {

            if (ModelState.IsValid)
            {

                createviewmodel.ModifiedBy = Convert.ToInt32(Session["CurrentUserId"]);
                var mapcategory = new MapperConfiguration(configurationExpression => { configurationExpression.CreateMap<CreateViewModel, User>(); configurationExpression.IgnoreUnmapped(); });
                IMapper mapper = mapcategory.CreateMapper();
                var userData = mapper.Map<CreateViewModel, User>(createviewmodel);
                IEnumerable<Role> Roles = userService.RoleDisplay();
                ViewBag.Roles = new SelectList(Roles, "RoleId", "RoleName");
                userService.Update(userData);
                TempData["Message"] = "updated";

                return RedirectToAction("Display");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Enter valid Data");
                IEnumerable<Role> Roles = userService.RoleDisplay();
                ViewBag.Roles = new SelectList(Roles, "RoleId", "RoleName");
                return View("Edit", createviewmodel);
            }


        }

        [UserAuthorizationFilter]
        public ActionResult ChangeProfile()
        {
            int id = Convert.ToInt32(Session["CurrentUserId"]);
            UserViewModel userDetail = userService.GetUserByUserId(id);
            EditUserViewModel dataToEdit = new EditUserViewModel() { EmailID = userDetail.EmailID, Name = userDetail.Name, PhoneNumber = userDetail.PhoneNumber, UserId = userDetail.UserId };
            return View(dataToEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilter]
        public ActionResult ChangeProfile(EditUserViewModel editedData)
        {

            if (ModelState.IsValid)
            {
                editedData.UserId = Convert.ToInt32(Session["CurrentUserID"]);
                this.userService.UpdateUserDetails(editedData);
                Session["CurrentUserName"] = editedData.Name;
                return RedirectToAction("index", "dashboard");

            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(editedData);
            }
        }
        [UserAuthorizationFilter]

        public ActionResult ChangePassword()
        {

            int id = Convert.ToInt32(Session["CurrentUserId"]);
            UserViewModel userDetail = userService.GetUserByUserId(id);
            EditUserPasswordViewModel dataToEdit = new EditUserPasswordViewModel() { UserId = userDetail.UserId, Password = string.Empty, ConfirmPassword = string.Empty };
            return View(dataToEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilter]
        public ActionResult ChangePassword(EditUserPasswordViewModel editedUserData)
        {
            if (ModelState.IsValid)
            {
                this.userService.UpdateUserPasswordDetails(editedUserData);
                return RedirectToAction("index", "dashboard");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(editedUserData);
            }
        }
    }
}