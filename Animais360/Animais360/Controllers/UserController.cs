using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Animais360.Filters;
using Animais360.Models;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Animais360.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class UserController : Controller
    {
        private Animais360Context db = new Animais360Context();

        public ActionResult Index()
        {
            int id = Convert.ToInt32(Membership.GetUser().ProviderUserKey.ToString());
            User us = db.Users.Find(id);
            Session["User"] = us;

            ViewBag.IMAGE = us.Avatar;
            ViewBag.IdUser = id;
            ViewBag.Tipo = us.Tipo;
            ViewBag.Users = db.Users.ToList();
            ViewBag.Classificacoes = db.Classificacoes.ToList();

            return View(us);
        }

        
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase[] files)
        {
            foreach (HttpPostedFileBase file in files)
            {
                string picture = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Animais360/Images"), picture);
                string[] paths = path.Split('.');
                string time = DateTime.UtcNow.ToString();
                time = time.Replace(" ", "-");
                time = time.Replace(":", "-");
                file.SaveAs(paths[0] + "-" + time + ".jpg");
            }
            ViewBag.Message = "File(s) uploaded successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Perfil(int id = -9999) {
            if (id == -9999)
                return HttpNotFound();
            User u = db.Users.Find(id);

            IList<Jogo> jogos = db.Jogos.Where(j => (j.User.UserId == id)).ToList();

            int idx = Convert.ToInt32(Membership.GetUser().ProviderUserKey.ToString());
            ViewBag.IdUser = idx;
            User us = db.Users.Find(idx);
            ViewBag.Tipo = us.Tipo;
            ViewBag.Jogos = jogos;
            ViewBag.TabelaUA = db.UserAreaProtegidas.ToList();

            //ViewBag.Tipo = u.Tipo;

            return View(u);
        }

        public ActionResult Regras()
        {
            ViewBag.IdUser = Convert.ToInt32(Membership.GetUser().ProviderUserKey.ToString());
            return View();
        }

        public ActionResult Gerir()
        {
            int id = Convert.ToInt32(Membership.GetUser().ProviderUserKey.ToString());
            User us = db.Users.Find(id);

            Session["User"] = us;
            ViewBag.IdUser = id;
            ViewBag.Tipo = us.Tipo;

            return View(db.Users.ToList());
        }

        public ActionResult Edit(int id = 0)
        {
            User us = db.Users.Find(id);
            if (us == null)
            {
                return HttpNotFound();
            }

            int idx = Convert.ToInt32(Membership.GetUser().ProviderUserKey.ToString());
            User u = db.Users.Find(idx);
            ViewBag.Tipo = u.Tipo;

            return View(us);
        }

        [HttpPost]
        public ActionResult EditUser(User u)
        {
            User newUser = db.Users.Find(u.UserId);
            newUser.UserName = u.UserName;
            newUser.Email = u.Email;
            newUser.Descricao = u.Descricao;
            db.Entry(newUser).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Gerir");
        }

        [HttpPost]
        public ActionResult EditProfile(User u, HttpPostedFileBase fileinput_thumb = null)
        {
            User newUser = db.Users.Find(u.UserId);
            if (fileinput_thumb != null && fileinput_thumb.ContentLength > 0)
            {
                string fileName = Path.GetFileName(fileinput_thumb.FileName);
                string path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                fileinput_thumb.SaveAs(path);
                newUser.Avatar = "/../images/"+fileName;
            }
            newUser.Email = u.Email;
            newUser.Descricao = u.Descricao;
            db.Entry(newUser).State = EntityState.Modified;
            db.SaveChanges();

            Session["User"] = newUser;
            return RedirectToAction("Perfil", new { id = u.UserId });
        }

        public ActionResult Editar(int id = -9999)
        {

            if (id == -9999)
                return HttpNotFound();

            User u = db.Users.Find(id);

            if (ModelState.IsValid)
            {

                if (u.Estado == 1) u.Estado = 0;
                else u.Estado = 1;
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Gerir", "User");
            }
            return View(u);
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new
                {
                    Email = model.Email,
                    Avatar = "/../images/default.jpg",
                    Descricao = "Sou eu que mando nisto tudo",
                    NrVoltas = 0,
                    NrJogos = 0,
                    Estado = 0,
                    DataRegisto = DateTime.Now,
                    Tipo = 1
                });
                return RedirectToAction("Gerir");
            }
            return View(model);
        }


        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: false))
            {
                //return RedirectToLocal(returnUrl);

                return RedirectToAction("Index", "User");
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new
                    {
                        Email = model.Email,
                        Avatar = "/../images/default.jpg",
                        Descricao = "Escreve aqui alguma coisa sobre ti...",
                        NrVoltas = 0,
                        NrJogos = 0,
                        Estado = 0,
                        DataRegisto = DateTime.Now,
                        Tipo = 0
                    });
                    WebSecurity.Login(model.UserName, model.Password);
                    return RedirectToAction("Index", "User");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new { Message = message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditModel model)
        {

            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Gerir", "User");
            }
            return View(model);
        }

        public ActionResult Create() {
            int id = Convert.ToInt32(Membership.GetUser().ProviderUserKey.ToString());
            User us = db.Users.Find(id);
            ViewBag.Tipo = us.Tipo;

            return View();
        }

        //
        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", String.Format("Unable to create local account. An account with the name \"{0}\" may already exist.", User.Identity.Name));
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
