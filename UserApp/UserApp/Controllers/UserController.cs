using UserApp.CommonFiles.DTO;
using UserApp.Infrastructure;
using reCaptcha;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserApp.Models;
using System.Security.Cryptography;
using System.Text;
using System.Reflection;

namespace UserApp.Controllers
{
    public class UserController : Controller
    {
        //By default, accessing the User Controller without a specified action, you are prompted to create a user
        public ActionResult Index()
        {
            return RedirectToAction("CreateUser");
        }


        //Create User model and check if the last session has tried to create a user and fills out the needed info in the view
        //if the model existed 
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Recaptcha = ReCaptcha.GetHtml(ConfigurationManager.AppSettings["ReCaptcha:SiteKey"]);
            ViewBag.publicKey = ConfigurationManager.AppSettings["ReCaptcha:SiteKey"];
            UserModel viewModel;
            viewModel = (UserModel)TempData["userModel"] ?? new UserModel();
            return View(viewModel);
        }


        //After submit is clicked
        [HttpPost]
        public ActionResult CheckUserModel(UserModel viewModel)
        {
            //Checks if the user is valid and has agreed to the terms and validated the captcha
            if (viewModel.Agreement && ModelState.IsValid && ReCaptcha.Validate(ConfigurationManager.AppSettings["ReCaptcha:SecretKey"]))
            {
                UserDTO userDTO = new UserDTO
                {
                    Password = ComputeHash(viewModel.Password), //Put Hash of password in the database
                    SecretAnswer = ComputeHash(viewModel.SecretAnswer) //Put Hash of secret answer in the database
                };
                foreach (PropertyInfo property in userDTO.GetType()
                    .GetProperties()
                    .Where(property => property.Name != "ID" && property.Name != "Password" && property.Name != "SecretAnswer"))
                {
                    property.SetValue(userDTO, viewModel.GetType().GetProperty(property.Name).GetValue(viewModel).ToString());
                }

                //Submits the user to the database via the service
                new UserService().Insert(userDTO);

                return View();
            }
            //User info was not valid, so the model is stored and you are prompted to create the user
            else
            {
                ViewBag.RecaptchaLastErrors = ReCaptcha.GetLastErrors(HttpContext);

                ViewBag.publicKey = ConfigurationManager.AppSettings["ReCaptcha:SiteKey"];

                TempData["agreement"] = "You have not agreed to our terms and conditions";
                TempData["userModel"] = viewModel;
                return RedirectToAction("CreateUser");
            }
        }


        /*Create User model and check if the last session 
         * has tried to create a user and fills out the 
         * needed info in the view if the model existed
         */
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Recaptcha = ReCaptcha.GetHtml(ConfigurationManager.AppSettings["ReCaptcha:SiteKey"]);
            ViewBag.publicKey = ConfigurationManager.AppSettings["ReCaptcha:SiteKey"];
            UserModel viewModel;
            viewModel = (UserModel)TempData["userModel"] ?? new UserModel();
            return View(viewModel);
        }

        //Checks if there exists a user with the Username and after that Password
        [HttpPost]
        public ActionResult Enter(UserModel model)
        {
            if (new UserService().Get(model.ID).Password == ComputeHash(model.Password))
            {
                return View();
            }

            //If there doesn't exist one, go back to the login screen
            ViewBag.ErrorMessage = "Username or Password was incorrect. Try again.";
            ViewBag.RecaptchaLastErrors = ReCaptcha.GetLastErrors(HttpContext);
            ViewBag.publicKey = ConfigurationManager.AppSettings["ReCaptcha:SiteKey"];
            TempData["userModel"] = model;
            return RedirectToAction("LoginUser");
        }


        //Lists all Users
        [HttpGet]
        public ActionResult List()
        {
            List<UserModel> users = new List<UserModel>();
            foreach (UserDTO dto in new UserService().GetAll())
            {
                users.Add(Convert(dto));
            }
            return View(users);
        }

        //Gives a View for editing a user by a specified ID
        [HttpGet]
        public ActionResult Edit(int id)
        {
            UserModel model = Convert(new UserService().Get(id));
            return View(model);
        }

        //Outputs change to the database via UserService
        [HttpPost]
        public ActionResult Edit(UserModel viewModel)
        {
            if (ModelState.IsValid)
            {
                new UserService().Edit(Convert(viewModel));

                return RedirectToAction("Users");
            }
            else
                return RedirectToAction("Edit", new { username = viewModel.Username });
        }

        //Deletes a User selected by a specified ID
        [HttpGet]
        public ActionResult Delete(int id)
        {
            new UserService().Delete(id);
            return RedirectToAction("Users");
        }

        #region Mappers
        private UserModel Convert(UserDTO dto)
        => new UserModel
        {
            About = dto.About,
            Email = dto.Email,
            Gender = (Gender)Enum.Parse(typeof(Gender), dto.Gender),
            Agreement=true,
            SecretQuestion = dto.SecretQuestion,
            Username = dto.Username,
            ID = dto.ID
        };

        private UserDTO Convert(UserModel viewModel)
        => new UserDTO
        {
            About = viewModel.About,
            Email = viewModel.Email,
            Gender = viewModel.Gender.ToString(),
            SecretQuestion = viewModel.SecretQuestion,
            Password=ComputeHash(viewModel.Password),
            SecretAnswer=ComputeHash(viewModel.SecretAnswer),
            Username = viewModel.Username,
            ID = viewModel.ID
        };
        #endregion

        //Hash Creator
        private string ComputeHash(string input)
        {
            byte[] hash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder builder = new StringBuilder(256);
            foreach (byte element in hash) builder.Append(element.ToString("x2"));

            return builder.ToString();
        }
    }
}