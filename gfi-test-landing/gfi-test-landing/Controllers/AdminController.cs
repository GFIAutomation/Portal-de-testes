using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using gfi_test_landing.Models;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Net;
using System.Threading;

namespace gfi_test_landing.Controllers
{

   

    [Authorize]
    public class AdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private testLandingEntities db = new testLandingEntities();

        private void changeLanguage(string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
        }

        //
        //GET: /Admin/UserList
        [Authorize]
        public ActionResult UserList()
        {
            var userList = db.AspNetUsers.Select(t => t);
            return View(userList.ToList());
            
        }

        // GET: AspNetUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,PhoneNumber,ImageUrl,FirstName,LastName")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                AspNetUsers userRow  = db.AspNetUsers.Single(u => u.Id == aspNetUsers.Id);
                userRow.Email = aspNetUsers.Email;
                userRow.PhoneNumber = aspNetUsers.PhoneNumber;
                userRow.ImageUrl = aspNetUsers.ImageUrl;
                userRow.FirstName = aspNetUsers.FirstName;
                userRow.LastName = aspNetUsers.LastName;
                db.Entry(userRow).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("UserList");
            }
            return View(aspNetUsers);
        }

        // GET: AspNetUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUsers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    

    //
    // GET: /Admin/Register
    [Authorize]
        public ActionResult Register()
        {

            if (ViewBag.roleList == null && ViewBag.projectList == null)
            {
                getRolesAndProjectsDropDown();
            }


            return View();
        }

        //
        // POST: /Admin/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if(ViewBag.roleList==null && ViewBag.projectList == null)
            {
                getRolesAndProjectsDropDown();
            }
            

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber, FirstName = model.FirstName, LastName = model.LastName };
                var result = await UserManager.CreateAsync(user, model.Password);
               
           
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                   
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    //return RedirectToAction("Dashboard", "Home");
                    //var userId = User.Identity.GetUserId();
                    //var userId = (from s in db.AspNetUsers
                    //            where s.Email == model.Email
                    //             select s.Id).ToString();
                   // await this.UserManager.AddToRolesAsync(user.Id,model.NameRole);

                    saveUserProject(model, user);


                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private void getRolesAndProjectsDropDown()
        {
            var roleIDs = db.AspNetRoles.Select(x => x);
            var projectIDs = db.Project.Select(x => x);
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var t in roleIDs)
            {
                SelectListItem s = new SelectListItem();
                s.Text = t.Name.ToString();
                s.Value = t.Id.ToString();
                items.Add(s);
            }
            ViewBag.roleList = items;

            List<SelectListItem> itemsProject = new List<SelectListItem>();
            foreach (var p in projectIDs)
            {
                SelectListItem a = new SelectListItem();
                a.Text = p.name.ToString();
                a.Value = p.id.ToString();
                itemsProject.Add(a);
            }
            ViewBag.roleList = items;
            ViewBag.projectList = itemsProject;
        }

        private void saveUserProject(RegisterViewModel model, ApplicationUser user)
        {
            UserRole userRole = new UserRole();

            userRole.id_project = model.IdProject;
            userRole.UserId = user.Id;
            userRole.RoleId = model.NameRole;
            userRole.date = DateTime.Now;

            db.UserRole.Add(userRole);

            db.SaveChanges();
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Dashboard", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}