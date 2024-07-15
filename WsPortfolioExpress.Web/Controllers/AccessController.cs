using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using WsPortfolioExpress.Common.Entities;
using WsPortfolioExpress.Web.Models;
using WsPortfolioExpress.Web.Repositories.Interfaces;
using WsPortfolioExpress.Web.Services;

namespace WsPortfolioExpress.Web.Controllers
{
    public class AccessController : BaseController
    {
        private readonly IUsersRepository _ctRepo;
        private readonly IWebHostEnvironment _environment;

        public AccessController(IUsersRepository ctRepo, IWebHostEnvironment environment)
        {
            _ctRepo = ctRepo;
            _environment = environment;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string correo, string clave)
        {
            User user = _ctRepo.GetUserLogin(correo, clave);

            if (user != null)
            {
                if (!user.Confirmed)
                {
                    ViewBag.Mensaje = $"Falta confirmar su cuenta. Se le envio un correo a {correo}";
                }
                else if (user.Restore)
                {
                    ViewBag.Mensaje = $"Se ha solicitado restablecer su cuenta, favor revise su bandeja del correo {correo}";
                }
                else
                {
                    GetUserLoginInfo(user);
                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                ViewBag.Mensaje = "No se encontraron coincidencias";
            }


            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserViewModel usuario)
        {
            if (usuario.Password != usuario.ConfirmPassword)
            {
                ViewBag.Name = usuario.Name;
                ViewBag.Email = usuario.Email;
                ViewBag.Message = "Las contraseñas no coinciden";
                return View();
            }

            if (_ctRepo.GetUserByEmail(usuario.Email) == null)
            {
                usuario.Password = Utilities.ConvertToSHA256(usuario.Password);
                usuario.Token = Utilities.GenerateToken();
                usuario.Restore = false;
                usuario.Confirmed = true;
                usuario.CreateDate = DateTime.Now;

                User _user = SetUserCreate(usuario);
                bool respuesta = _ctRepo.CreateUser(_user);

                if (respuesta)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "email_template/");

                    string uniqueFileName = "confirm.html";
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    string content = System.IO.File.ReadAllText(filePath);
                    string url = string.Format("{0}://{1}{2}", Request.Scheme, Request.Headers["host"], "/Access/Confirm?token=" + _user.Token);

                    string htmlBody = string.Format(content, _user.Name, url);

                    ViewBag.Creado = true;
                    ViewBag.Mensaje = $"Su cuenta ha sido creada. Hemos enviado un mensaje al correo {_user.Email} para confirmar su cuenta";
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo crear su cuenta";
                }
            }
            else
            {
                ViewBag.Mensaje = "El correo ya se encuentra registrado";
            }

            return View();
        }

        public ActionResult Restore()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Restore(string email)
        {
            User _user = _ctRepo.GetUserByEmail(email);
            ViewBag.Email = email;
            if (_user != null)
            {
                bool respuesta = _ctRepo.UpdateUser(_user);

                if (respuesta)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "email_template/");

                    string uniqueFileName = "restore.html";
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    string content = System.IO.File.ReadAllText(filePath);
                    string url = string.Format("{0}://{1}{2}", Request.Scheme, Request.Headers["host"], "/Access/Restore?token=" + _user.Token);

                    string htmlBody = string.Format(content, _user.Name, url);

                    EmailViewModel _email = new EmailViewModel()
                    {
                        Para = email,
                        Asunto = "Restablecer cuenta",
                        Contenido = htmlBody
                    };

                    //bool enviado = EmailServices.Enviar(_email);
                    ViewBag.Restablecido = true;
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo restablecer la cuenta";
                }

            }
            else
            {
                ViewBag.Mensaje = "No se encontraron coincidencias con el correo";
            }

            return View();
        }

        public ActionResult Update(string token)
        {
            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        public ActionResult Update(UserViewModel userVm) //(string token, string clave, string confirmarClave)
        {
            ViewBag.Token = userVm.Token;
            if (userVm.Password != userVm.ConfirmPassword)
            {
                ViewBag.Mensaje = "Las contraseñas no coinciden";
                return View();
            }

            User _user = _ctRepo.GetUserByEmail(userVm.Email);
            _user.Password = Utilities.ConvertToSHA256(userVm.Password);
            _user.Token = userVm.Token;

            bool respuesta = _ctRepo.UpdateUser(_user);

            if (respuesta)
                ViewBag.Restablecido = true;
            else
                ViewBag.Mensaje = "No se pudo actualizar";

            return View();
        }

        private User SetUserCreate(UserViewModel userView)
        {
            User user = new User() 
            {
                Id = userView.Id,
                Name = userView.Name,
                Email = userView.Email,
                Password = userView.Password,
                Restore = userView.Restore.Value,
                Confirmed = userView.Confirmed.Value,
                Token = userView.Token
            };

            return user;
        }

        public ActionResult LogOut()
        {
            UserService.UserLogin = null;
            return RedirectToAction("Login", "Access");
        }
    }
}
