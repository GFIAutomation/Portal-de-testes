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
        //GET: /Admin/CreateProject
        [Authorize]
        public ActionResult createProject()
        {
            //Carregar todas as listas
            return View();
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



            //if (userRoleProjectModel != null)
            //{
            //    _RoleProjectByUser( roleUserByProj);
            //    //PartialView("~/Views/Shared/_ListProjectUser.cshtml", model);
            //}

            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }


            return View(aspNetUsers);
        }


        //GET  PartialView AspNetUsers/Details/5
        public ActionResult _RoleProjectByUser(string id)
        {
            IEnumerable<RoleProjectByUserModel> roleUserByProj = (from ur in db.UserRole
                                                                  join p in db.Project on ur.id_project equals p.id
                                                                  join r in db.AspNetRoles on ur.RoleId equals r.Id
                                                                  where ur.UserId == id
                                                                  select new RoleProjectByUserModel()
                                                                  {
                                                                      IdUser = id,
                                                                      IdRole = r.Id,
                                                                      IdProject = p.id,
                                                                      NameRole = r.Name,
                                                                      NameProject = p.name,
                                                                      DescriptionProject = p.description
                                                                  }).ToList();

            UserRoleProjectModel userRoleProjectModel = new UserRoleProjectModel
            {
                //Id = id,
                RoleProjetByUser = roleUserByProj
            };
            if (userRoleProjectModel == null)
            {
                //Then Error

            }
            return PartialView(roleUserByProj);

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
                AspNetUsers userRow = db.AspNetUsers.Single(u => u.Id == aspNetUsers.Id);
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

        //DELETE with modal
        public ActionResult _ModalDelete(String id, String actionName, String idUser, String idRole, String idProject)
        {
            if (actionName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (actionName == "UserList")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
                TempData["title"] = "User";
                TempData["Msg"] = "Do you want to delete the " + aspNetUsers.Email + " ?";
                return PartialView("_ModalDelete");
            }

            if (actionName == "roleProjectByUser")
            {
                if (idRole == null || idProject == null || idUser == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TempData["title"] = "Association";
                TempData["Msg"] = "Do you want to delete this Row?";
                return PartialView("_ModalDelete");
            }
            return PartialView();
        }


        //POST
        //_ModalDeleteConfirm
        [HttpPost, ActionName("_ModalDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult _ModalDeleteConfirmed(String id, String actionName, String idUser, String idRole, String idProject)
        {
            if (actionName == "UserList")
            {
                AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);

                db.AspNetUsers.Remove(aspNetUsers);
                db.SaveChanges();
            }
            if (actionName == "roleProjectByUser")
            {
                int idPro = int.Parse(idProject);
                UserRole userRole = db.UserRole.Where(ur => ur.UserId == idUser && ur.RoleId == idRole && ur.id_project == idPro).First();

                if (userRole == null)
                {
                    //Error
                }
                db.UserRole.Remove(userRole);
                db.SaveChanges();
                return RedirectToAction("Details", "Admin", new { id = id });
            }
            return RedirectToAction("UserList");
        }

        // GET: AspNetUsers/Edit/5
        public ActionResult EditRoleProjectByUser(string idUser, int idProject, string idRole)
        {
            ViewBag.idUser = idUser;
            ViewBag.idProject = idProject;
            ViewBag.idRole = idRole;

            if (idUser == null || idProject.ToString() == null || idRole == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ViewBag.roleList == null && ViewBag.projectList == null)
            {
                getRolesAndProjectsDropDownEdit(idProject, idRole);
            }



            if (ViewBag.DropRole == null)
            {
                //ERROR
            }
      
            IEnumerable<DropRoleProjectByUserModel> DropRoleProjetByUser = DropDownRoleProjetByUser(idUser, idProject, idRole);

            if (DropRoleProjetByUser == null)
            {
                return HttpNotFound();
            }

            return View(DropRoleProjetByUser);
        }


        // POST: /Admin/Register
        [HttpPost, ActionName("EditRoleProjectByUser")]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoleProjectByUser(DropRoleProjectByUserModel DropRoleProjetByUser, FormCollection form)
        {
            if (ViewBag.DropRole == null && ViewBag.DropProject == null)
            {
                getRolesAndProjectsDropDownEdit(DropRoleProjetByUser.IdProject, DropRoleProjetByUser.IdRole);
            }
        

            string idRoleChange = form["DropRole"].ToString();

            int idProjectChange = 0;
            if (form["DropProject"] !=null && form["DropProject"] !="")
            { 
                 idProjectChange = Int32.Parse(form["DropProject"]);
            }

             UserRole userRole = db.UserRole.Where(ur => ur.id_project == DropRoleProjetByUser.IdProject && ur.RoleId == DropRoleProjetByUser.IdRole &&  ur.UserId == DropRoleProjetByUser.IdUser).FirstOrDefault();

            DropDownRoleProjetByUser(DropRoleProjetByUser.IdUser, DropRoleProjetByUser.IdProject, DropRoleProjetByUser.IdRole);

            if (userRole != null)
            {
                db.UserRole.Remove(userRole);
                db.SaveChanges();
            }
            else
            {
                //ERROR
            }

            UserRole userRoleInsert = new UserRole();

            if (idRoleChange != DropRoleProjetByUser.IdRole || idProjectChange != DropRoleProjetByUser.IdProject || idRoleChange!="")
            {
                if ( idProjectChange==0)
                {
                    idProjectChange = DropRoleProjetByUser.IdProject;
                }
   
                if(DropRoleProjetByUser.IdRole != idRoleChange && idRoleChange !="")
                {
                    DropRoleProjetByUser.IdRole = idRoleChange;
                }
                
                userRoleInsert.RoleId = DropRoleProjetByUser.IdRole;
                userRoleInsert.id_project = idProjectChange;
                userRoleInsert.UserId = DropRoleProjetByUser.IdUser;
                userRoleInsert.date = DateTime.Now;

                db.UserRole.Add(userRoleInsert);

                db.SaveChanges();
            }

            return RedirectToAction("Details", "Admin", new { id = DropRoleProjetByUser.IdUser });

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
            if (ViewBag.roleList == null && ViewBag.projectList == null)
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

        private IEnumerable<DropRoleProjectByUserModel> DropDownRoleProjetByUser(string idUser, int idProject, string idRole){

            IEnumerable<DropRoleProjectByUserModel> DropRoleProjetByUser = (from ur in db.UserRole
                                                                            join p in db.Project on ur.id_project equals p.id
                                                                            join u in db.AspNetUsers on ur.UserId equals u.Id
                                                                            join r in db.AspNetRoles on ur.RoleId equals r.Id
                                                                            where (u.Id == idUser && p.id == idProject && r.Id == idRole)

                                                                            select new DropRoleProjectByUserModel()
                                                                            {
                                                                                Email = u.Email,
                                                                                FirstName = u.FirstName,
                                                                                LastName = u.LastName,
                                                                                ImageUser = u.ImageUrl,
                                                                                PhoneNumber = u.PhoneNumber,
                                                                                NameRole = r.Name,
                                                                                NameProject = p.name,
                                                                                IdProject = p.id,
                                                                                IdRole = r.Id,
                                                                                IdUser = u.Id

                                                                            }).ToList();

            return DropRoleProjetByUser;
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

        private void getRolesAndProjectsDropDownEdit(int idProject, string idRole)
        {
            var roleIDs = db.AspNetRoles.Select(x => x).Where(x=>x.Id !=idRole );
            var projectIDs = db.Project.Select(x => x).Where(x => x.id != idProject);
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

            ViewBag.DropRole = new SelectList(items.AsEnumerable(), "Value", "Text", idRole);
            ViewBag.DropProject = new SelectList(itemsProject.AsEnumerable(), "Value", "Text", idProject);
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