using Intranet.Data;
using Intranet.Models;
using Intranet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Mime;
using Microsoft.Extensions.FileProviders;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http.Extensions;
using System.IO;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver.Core.Connections;

namespace Intranet.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public string GeneratePassword()
        {
            Random rand = new();
            int length = 8;
            int remainingGroups = 4;

            string[] allowedLowerChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z".Split(',');
            string[] allowedUpperChars = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z".Split(',');
            string[] allowedNumbers = "1,2,3,4,5,6,7,8,9,0".Split(',');
            string[] allowedSpecialChars = "!,@,#,$,%,&,?".Split(',');

            var password = allowedLowerChars.OrderBy(c => rand.Next()).Take(rand.Next(1, length - remainingGroups--)).ToList();
            password.AddRange(allowedUpperChars.OrderBy(c => rand.Next()).Take(rand.Next(1, length - password.Count - remainingGroups--)).ToList());
            password.AddRange(allowedNumbers.OrderBy(c => rand.Next()).Take(rand.Next(1, length - password.Count - remainingGroups--)).ToList());
            password.AddRange(allowedSpecialChars.OrderBy(c => rand.Next()).Take(length - password.Count).ToList());
            password = password.OrderBy(c => rand.Next()).ToList(); // randomize groups

            return string.Join("", password);
        }

        public void ChangePasswordUser()
        {

            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;

            var OldPasswordUserList = _db.PasswordHistories.Where(a => a.UserId == UserId.Id && a.Active == true).OrderByDescending(o => o.CountPassword);

            if (OldPasswordUserList.Any())
            {
                var OldPasswordUserLast = OldPasswordUserList.FirstOrDefault();
                var PasswordExpires = OldPasswordUserList.Where(a => a.ExpiresOn <= DateTime.Now.Date && a.PasswordLock == false).OrderByDescending(o => o.CountPassword).FirstOrDefault();
                var ResetPassword = OldPasswordUserList.Where(a => a.ResetPassword == true && a.PasswordLock == false).OrderByDescending(o => o.CountPassword).FirstOrDefault();

                if (PasswordExpires != null)
                {
                    var CountPassword = PasswordExpires.CountPassword;
                    CountPassword++;

                    PasswordHistory passwordHistory = new()
                    {
                        UserId = UserId.Id,
                        PasswordHash = UserId.PasswordHash,
                        CountPassword = CountPassword,
                        ResetPassword = false,
                        PasswordLock = false,
                        CreateBy = UserId.Id,
                        CreateOn = DateTime.Now,
                        ExpiresOn = DateTime.Now.AddDays(90),
                        Active = true
                    };
                    _db.PasswordHistories.Add(passwordHistory);
                    _db.SaveChanges();

                    PasswordExpires.PasswordLock = true;
                    _db.PasswordHistories.Update(PasswordExpires);
                    _db.SaveChanges();

                    var OldPasswordLockUserList = _db.PasswordHistories.Where(a => a.UserId == UserId.Id && a.PasswordLock == true && a.Active == true).OrderBy(o => o.CountPassword).Take(5);

                    if (OldPasswordLockUserList.Count() == 5)
                    {
                        var OldPasswordLockUserLast = OldPasswordLockUserList.FirstOrDefault();

                        if (OldPasswordLockUserLast != null)
                        {
                            OldPasswordLockUserLast.Active = false;
                            _db.PasswordHistories.Update(OldPasswordLockUserLast);
                            _db.SaveChanges();
                        }
                        else
                        {

                        }

                    }
                    else
                    {

                    }
                }
                else if (ResetPassword != null)
                {
                    var CountPassword = ResetPassword.CountPassword;
                    CountPassword++;

                    PasswordHistory passwordHistory = new()
                    {
                        UserId = UserId.Id,
                        PasswordHash = UserId.PasswordHash,
                        CountPassword = CountPassword,
                        ResetPassword = false,
                        PasswordLock = false,
                        CreateBy = UserId.Id,
                        CreateOn = DateTime.Now,
                        ExpiresOn = DateTime.Now.AddDays(90),
                        Active = true
                    };
                    _db.PasswordHistories.Add(passwordHistory);
                    _db.SaveChanges();

                    ResetPassword.PasswordLock = true;
                    _db.PasswordHistories.Update(ResetPassword);
                    _db.SaveChanges();

                    var OldPasswordLockUserList = _db.PasswordHistories.Where(a => a.UserId == UserId.Id && a.PasswordLock == true && a.Active == true).OrderBy(o => o.CountPassword).Take(5);

                    if (OldPasswordLockUserList.Count() == 5)
                    {
                        var OldPasswordLockUserLast = OldPasswordLockUserList.FirstOrDefault();

                        if (OldPasswordLockUserLast != null)
                        {
                            OldPasswordLockUserLast.Active = false;
                            _db.PasswordHistories.Update(OldPasswordLockUserLast);
                            _db.SaveChanges();
                        }
                        else
                        {

                        }

                    }
                    else
                    {

                    }
                }
            }
            else
            {
                PasswordHistory passwordHistory = new()
                {
                    UserId = UserId.Id,
                    PasswordHash = UserId.PasswordHash,
                    CountPassword = 1,
                    ResetPassword = false,
                    PasswordLock = false,
                    CreateBy = UserId.Id,
                    CreateOn = DateTime.Now,
                    ExpiresOn = DateTime.Now.AddDays(90),
                    Active = true
                };
                _db.PasswordHistories.Add(passwordHistory);
                _db.SaveChanges();
            }
        }
        private AlternateView GetEmbeddedImage(String filePath)
        {
            LinkedResource res = new LinkedResource(filePath);
            res.ContentId = Guid.NewGuid().ToString();
            string htmlBody = @"<img src='cid:" + res.ContentId + @"'/>";
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(res);
            return alternateView;
        }

        public void SendMailChangePassword(string password, string firstnameTH, string lastnameTH, string usermail)
        {
            var FromMail = "system@moshimoshi.co.th";
            var FromPasswordMail = "AdminMoshi!2019";
            var ToMail = usermail;

            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress(ToMail));
            message.From = new MailAddress(FromMail);
            message.Subject = "[Intranet] รีเซ็ตรหัสผ่าน";
            message.Body = "<b>" + "เรียน คุณ" + firstnameTH + " " + lastnameTH + "</b>" + "<br>" + "<br>" +
                           "&emsp;&emsp;" + "รหัสผ่านบัญชี Intranet ของคุณถูกรีเซ็ต" + "<br>" +
                           "&emsp;&emsp;" + "คุณจะต้องใช้รหัสผ่านนี้  " + "<b>" + password + "</b>" + "  เพื่อเข้าถึงบัญชีและสร้างรหัสผ่านใหม่" + "<br>" + "<br>" +
                           "&emsp;&emsp;" + "<a href=\"https://Intranet.moshimoshijp.co.th:8048/\" style=\"font-weight:bold\">" + "เข้าสู่หน้าเว็บไซต์" + "</a>" + "<br>" + "<br>" +
                           "<img src =\"cid:imgpath\">" + "<br>" +
                           "Information Technology" + "<br>" +
                           "Moshi Moshi Retail Corporation Pub Co., Ltd." + "<br>" +
                           "26/18 Moo 10 Ekachai Rd. Bangkhuntien, Jomthong, Bangkok 10150";

            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(message.Body, null, "text/html");
            LinkedResource linkedResource = new LinkedResource("wwwroot\\Image\\Logo_MoshiMoshi_ForMail.jpg", new ContentType("image/jpeg"));
            linkedResource.ContentId = "imgpath";
            alternateView.LinkedResources.Add(linkedResource);
            message.AlternateViews.Add(alternateView);

            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(FromMail, FromPasswordMail),
                EnableSsl = true
            };

            smtpClient.Send(message);
            smtpClient.Dispose();
        }

        public JsonResult Department2Id(int id)
        {
            var listdepartment2id = _db.Department2s.Where(i => i.Department1Id == id && i.Active == true).OrderBy(o => o.Name).ToList();
            return new JsonResult(listdepartment2id);
        }

        public JsonResult SectionId(int id1, int id2)
        {
            var listsectionid = new List<Section>();

            if (id2 != 0)
            {
                listsectionid = _db.Sections.Where(i => i.Department1Id == id1 && i.Department2Id == id2 && i.Active == true).OrderBy(o => o.Name).ToList();
            }
            else
            {
                listsectionid = _db.Sections.Where(i => i.Department1Id == id1 && i.Department2Id == null && i.Active == true).OrderBy(o => o.Name).ToList();

            }
            return new JsonResult(listsectionid);
        }

        public JsonResult ShopId(int id)
        {
            var shopsid = _db.Shops.Where(i => i.SectionId == id && i.Active == true).OrderBy(o => o.Branch).ToList();

            List<object> listshopid = new List<object>();
            foreach (var item in shopsid)
            {
                var shopbranch = shopsid.Where(a => a.Id == item.Id).Select(s => s.Branch).FirstOrDefault();

                listshopid.Add(new
                {
                    item.Id,
                    Name = shopbranch + " : " + item.Name
                });
            }

            return new JsonResult(listshopid);
        }

        public JsonResult PositionId(int id1, int id2)
        {
            var listpositionid = new List<Position>();

            if (id2 != 0)
            {
                var listshoppositiongroup = _db.Shops.Where(i => i.Id == id2 && i.SectionId == id1 && i.Active == true).Select(s => s.ShopPositionGroupId).FirstOrDefault();
                listpositionid = _db.Positions.Where(i => i.SectionId == id1 && i.ShopPositionGroupId == listshoppositiongroup && i.Active == true).OrderBy(o => o.Name).ToList();
            }
            else
            {
                listpositionid = _db.Positions.Where(i => i.SectionId == id1 && i.ShopPositionGroupId == null && i.Active == true).OrderBy(o => o.Name).ToList();

            }
            return new JsonResult(listpositionid);
        }

        public JsonResult AvatarImageProfile()
        {
            var ProfilePath = new DirectoryInfo(@"wwwroot\Image\users\profile").GetFiles("*.*").OrderBy(o => o.Name);

            List<string> AvatarImageProfile = new List<string>();

            foreach (FileInfo item in ProfilePath)
            {
                if (item.Name.Contains("Avatar"))
                {
                    AvatarImageProfile.Add(item.Name);
                }
            }

            return new JsonResult(AvatarImageProfile.OrderBy(o => Convert.ToInt32(o.Replace("Avatar", "").Replace(".jpg", ""))));
        }

        public JsonResult AvatarBackgroundProfile()
        {
            var BackgroundPath = new DirectoryInfo(@"wwwroot\Image\users\background").GetFiles("*.*").OrderBy(o => o.Name);

            List<string> AvatarBackgroundProfile = new List<string>();

            foreach (FileInfo item in BackgroundPath)
            {
                if (item.Name.Contains("BgColor"))
                {
                    AvatarBackgroundProfile.Add(item.Name);
                }
            }
            return new JsonResult(AvatarBackgroundProfile.OrderBy(o => Convert.ToInt32(o.Replace("BgColor", "").Replace(".jpg", ""))));
        }

        private async Task<List<string>> GetRolesList(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminLogin()
        {
            return View();
        }

        public IActionResult Login()
        {
            var returnUrl = Request.Query["returnUrl"].ToString();

            if (!string.IsNullOrEmpty(returnUrl))
            {
                TempData["returnUrl"] = returnUrl;
            }
            else
            {
                TempData["returnUrl"] = null;
            }

            ViewBag.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version!.ToString();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel data)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(data.Email, data.Password, isPersistent: false, lockoutOnFailure: true);
                var UserEmail = _userManager.Users.Where(a => a.UserName == data.Email && a.Active == true || a.NormalizedUserName == data.Email && a.Active == true).Select(s => s.Id).FirstOrDefault();

                if (UserEmail != null)
                {
                    if (result.IsLockedOut)
                    {
                        var UserId = _userManager.Users.Where(a => a.UserName == data.Email && a.Active == true || a.NormalizedUserName == data.Email && a.Active == true).Select(s => s.Id).FirstOrDefault();
                        var PasswordUser = _db.PasswordHistories.Where(a => a.UserId == UserId && a.Active == true).OrderByDescending(o => o.CountPassword).FirstOrDefault();

                        if (PasswordUser != null)
                        {
                            PasswordUser.ResetPassword = true;
                            _db.PasswordHistories.Update(PasswordUser);
                            _db.SaveChanges();

                            ViewBag.ErrorLogin = "Error";
                            ViewBag.ErrorTextLogin = "ErrorPasswordLock";
                            return View(data);
                        }
                        else
                        {
                            ViewBag.ErrorLogin = "Error";
                            ViewBag.ErrorTextLogin = "ErrorPasswordLock";
                            return View(data);
                        }

                    }
                    else if (result.Succeeded)
                    {
                        return RedirectToAction("ChangePassword");
                    }
                    else
                    {
                        var UserIdAccessFailedCount = _userManager.Users.Where(a => a.UserName == data.Email && a.Active == true || a.NormalizedUserName == data.Email && a.Active == true).Select(s => s.AccessFailedCount).FirstOrDefault();

                        ViewBag.ErrorLogin = "Error";
                        ViewBag.ErrorTextLogin = "ErrorPasswordCount";
                        ViewBag.ErrorPasswordCount = UserIdAccessFailedCount;
                        return View(data);
                    }
                }
                else
                {
                    ViewBag.ErrorLogin = "Error";
                    ViewBag.ErrorTextLogin = "ErrorUserEmail";
                    return View(data);
                }
            }
            else
            {
            }
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("Username");
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            var PasswordUser = _db.PasswordHistories.Where(a => a.UserId == UserId.Id && a.Active == true).ToList();

            if (PasswordUser.Any())
            {
                var PasswordExpires = PasswordUser.Where(a => a.ExpiresOn <= DateTime.Now.Date && a.PasswordLock == false).OrderByDescending(o => o.CountPassword).FirstOrDefault();
                var ResetPassword = PasswordUser.Where(a => a.ResetPassword == true && a.PasswordLock == false).OrderByDescending(o => o.CountPassword).FirstOrDefault();

                if (PasswordExpires != null )
                {
                    ViewBag.ErrorChangePassword = "Error";
                    ViewBag.ErrorTextChangePassword = "ErrorPasswordExpires";
                    return View();
                }
                else if (ResetPassword != null)
                {
                    ViewBag.ErrorChangePassword = "Error";
                    ViewBag.ErrorTextChangePassword = "ErrorResetPasswor";
                    return View();
                }
                else
                {
                    HttpContext.Session.SetString("UserName", UserId.UserName);

                    if (User.IsInRole("Admin"))
                    {
                        var returnUrl = TempData["returnUrl"];

                        if (returnUrl != null)
                        {
                            var LocalreturnUrl = returnUrl.ToString();
                            return LocalRedirect(LocalreturnUrl!);
                        }
                        else
                        {
                            return RedirectToAction("Home", "Blogpost");
                        }
                    }
                    else
                    {
                        var returnUrl = TempData["returnUrl"];

                        if (returnUrl != null)
                        {
                            var LocalreturnUrl = returnUrl.ToString();
                            return LocalRedirect(LocalreturnUrl!);
                        }
                        else
                        {
                            return RedirectToAction("Home", "Blogpost");
                        }
                    }
                }
            }
            else
            {
                ViewBag.ErrorChangePassword = "Error";
                ViewBag.ErrorTextChangePassword = "ErrorFirstPassword";
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel data)
        {
            if (ModelState.IsValid)
            {
                var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;

                var CheckOldPassword = _userManager.PasswordHasher.VerifyHashedPassword(UserId, UserId.PasswordHash, data.OldPassword);

                if (CheckOldPassword == PasswordVerificationResult.Success)
                {
                    if (data.NewPassword == data.ConfirmPassword)
                    {
                        var OldPasswordUser = _db.PasswordHistories.Where(a => a.UserId == UserId.Id && a.Active == true).OrderByDescending(o => o.CountPassword).Take(5);

                        foreach (var item in OldPasswordUser)
                        {
                            var CheckNewPassword = _userManager.PasswordHasher.VerifyHashedPassword(UserId, item.PasswordHash, data.NewPassword);

                            if (CheckNewPassword == PasswordVerificationResult.Success)
                            {
                                ViewBag.ErrorChangePassword = "Error";
                                ViewBag.ErrorTextChangePassword = "ErrorRepeatPassword";
                                return View(data);
                            }
                            else
                            {

                            }
                        }

                        var result = await _userManager.ChangePasswordAsync(UserId, data.OldPassword, data.NewPassword);

                        if (result.Succeeded)
                        {
                            ChangePasswordUser();

                            ViewBag.ErrorChangePassword = "Error";
                            ViewBag.ErrorTextChangePassword = "SuccessChangePassword";

                            return View(data);
                        }
                        else
                        {
                            foreach (var item in result.Errors)
                            {
                                if (item.Code == "PasswordRequiresDigit")
                                {
                                    ViewBag.ErrorPasswordRequiresDigit = "Error";
                                }
                                else if (item.Code == "PasswordRequiresLower")
                                {
                                    ViewBag.ErrorPasswordRequiresLower = "Error";
                                }
                                else if (item.Code == "PasswordRequiresNonAlphanumeric")
                                {
                                    ViewBag.ErrorPasswordRequiresNonAlphanumeric = "Error";
                                }
                                else if (item.Code == "PasswordRequiresUpper")
                                {
                                    ViewBag.ErrorPasswordRequiresUpper = "Error";
                                }
                                else if (item.Code == "PasswordTooShort")
                                {
                                    ViewBag.ErrorPasswordTooShort = "Error";
                                }
                                else
                                {

                                }
                            }
                            ViewBag.ErrorTextChangePassword = "ErrorIncorrectPassword";
                            ViewBag.ErrorChangePassword = "Error";
                            return View(data);
                        }
                    }
                    else
                    {
                        ViewBag.ErrorChangePassword = "Error";
                        ViewBag.ErrorTextChangePassword = "ErrorPasswordNotMatch";
                        return View(data);
                    }
                }
                else
                {
                    ViewBag.ErrorChangePassword = "Error";
                    ViewBag.ErrorTextChangePassword = "ErrorOldPassword";
                    return View(data);
                }
            }
            else
            {

            }
            return View(data);
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel data)
        {
            if (ModelState.IsValid)
            {

                var UserId = _userManager.Users.Where(a => a.UserName == data.Email && a.Active == true || a.NormalizedUserName == data.Email && a.Active == true).Select(s => s.Id).FirstOrDefault();

                if (UserId != null)
                {
                    var User = _userManager.FindByIdAsync(UserId).Result;
                    var password = GeneratePassword();

                    var token = await _userManager.GeneratePasswordResetTokenAsync(User);
                    var result = await _userManager.ResetPasswordAsync(User, token, password);

                    if (result.Succeeded)
                    {
                        User.LockoutEnd = null;

                        var resultUser = await _userManager.UpdateAsync(User);

                        if (resultUser.Succeeded)
                        {
                            var PasswordUser = _db.PasswordHistories.Where(a => a.UserId == UserId && a.Active == true).OrderByDescending(o => o.CountPassword).FirstOrDefault();

                            if (PasswordUser != null)
                            {
                                PasswordUser.ResetPassword = true;
                                _db.PasswordHistories.Update(PasswordUser);
                                _db.SaveChanges();

                                SendMailChangePassword(password, User.FirstNameTH, User.LastNameTH, User.UserName);

                                ViewBag.ErrorForgotPassword = "Error";
                                ViewBag.ErrorTextForgotPassword = "SuccessUserEMail";
                                return View(data);
                            }
                            else
                            {
                                SendMailChangePassword(password, User.FirstNameTH, User.LastNameTH, User.UserName);

                                ViewBag.ErrorForgotPassword = "Error";
                                ViewBag.ErrorTextForgotPassword = "SuccessUserEMail";
                                return View(data);
                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    ViewBag.ErrorForgotPassword = "Error";
                    ViewBag.ErrorTextForgotPassword = "ErrorUserEMail";
                    return View(data);
                }
            }
            return View(data);
        }

        [Authorize]
        public async Task<IActionResult> Profile(string id)
        {
            

            var UserId = new ApplicationUser();

            if (id != null)
            {
                UserId = _userManager.FindByIdAsync(id).Result;
            }
            else
            {
                UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            }

            var users = _userManager.Users.Where(a => a.Id == UserId.Id).OrderBy(o => o.UserName).ToList();
            var locationGroups = _db.LocationGroups.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var locations = _db.Locations.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var department1s = _db.Department1s.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var department2s = _db.Department2s.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var sections = _db.Sections.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var shops = _db.Shops.Where(a => a.Active == true).OrderBy(o => o.Branch).ToList();
            var shopPositionGroups = _db.ShopPositionGroups.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var positions = _db.Positions.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var roles = _roleManager.Roles.OrderBy(o => o.Name);

            var UserIdProfile = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;

            ViewBag.ImageProfileUesr = UserIdProfile.ImageUser;
            ViewBag.UserId = UserIdProfile.Id;
            ViewBag.UserName = UserIdProfile.FirstNameTH + " " + UserIdProfile.LastNameTH;

            ViewData["LocationGroupsList"] = new SelectList(locationGroups, "Id", "Name");
            ViewData["LocationsList"] = new SelectList(locations, "Id", "Name");
            ViewData["ShopPositionGroupsList"] = new SelectList(shopPositionGroups, "Id", "Name");
            ViewData["RolesList"] = new SelectList(roles, "Id", "Name");

            List<object> listdepartment1 = new List<object>();
            foreach (var item in department1s)
            {
                var department1location = locations.Where(a => a.Id == item.LocationId).Select(s => s.Name).FirstOrDefault();

                listdepartment1.Add(new
                {
                    item.Id,
                    Name = item.Name + " (" + department1location + ")"
                });
            }
            ViewData["Department1sList"] = new SelectList(listdepartment1, "Id", "Name");


            List<object> listsection = new List<object>();
            foreach (var item in sections)
            {
                var sectiondepartment1 = department1s.Where(a => a.Id == item.Department1Id).Select(s => s.LocationId).FirstOrDefault();
                var sectionlocation = locations.Where(a => a.Id == sectiondepartment1).Select(s => s.Name).FirstOrDefault();

                listsection.Add(new
                {

                    item.Id,
                    Name = item.Name + " (" + sectionlocation + ")"
                });
            }
            ViewData["SectionsList"] = new SelectList(listsection, "Id", "Name");


            var positionsection = positions.Where(a => a.SectionId > 0);
            List<object> listsectionposition = new List<object>();
            foreach (var item in positionsection)
            {
                var sectionposition = sections.Where(a => a.Id == item.SectionId).FirstOrDefault();

                if (sectionposition != null)
                {
                    var sectiondepartment1 = department1s.Where(a => a.Id == sectionposition.Department1Id).Select(s => s.LocationId).FirstOrDefault();
                    var sectionlocation = locations.Where(a => a.Id == sectiondepartment1).Select(s => s.Name).FirstOrDefault();

                    listsectionposition.Add(new
                    {
                        sectionposition.Id,
                        Name = sectionposition.Name + " (" + sectionlocation + ")"
                    });
                }
                else
                {

                }
            };
            ViewData["SectionsPositionList"] = new SelectList(listsectionposition.Distinct().ToList(), "Id", "Name");


            var ProfilePath = new DirectoryInfo(@"wwwroot\Image\users\profile").GetFiles("*.*");

            List<string> AvatarImageProfile = new List<string>().OrderBy(a => a).ToList();

            foreach (FileInfo item in ProfilePath) {
                if (item.Name.Contains("Avatar"))
                {
                    AvatarImageProfile.Add(item.Name);
                } 
            }

            var BgPath = new DirectoryInfo(@"wwwroot\Image\users\background").GetFiles("*.*");

            List<string> BgColor = new List<string>().OrderBy(a => a).ToList();

            foreach (FileInfo item in ProfilePath)
            {
                if (item.Name.Contains("BgColor"))
                {
                    BgColor.Add(item.Name);
                }
            }

            var userRoles = new List<UserRoleViewModel>();

            var viewModel = new ProfileVieweModel();

            foreach (var item in users)
            {
                var userRole = new UserRoleViewModel
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    EmployeeId = item.EmployeeId,
                    FirstNameEN = item.FirstNameEN,
                    LastNameEN = item.LastNameEN,
                    FirstNameTH = item.FirstNameTH,
                    LastNameTH = item.LastNameTH,
                    NickName = item.NickName,
                    ImageUser = item.ImageUser,
                    BgUser = item.BgUser,
                    InternalPhoneNumber = item.InternalPhoneNumber,
                    PhoneNumber = item.PhoneNumber,
                    ShopId = item.ShopId,
                    Shop = item.Shop,
                    PositionId = item.PositionId,
                    Position = item.Position,
                    CreateBy = item.CreateBy,
                    CreateOn = item.CreateOn,
                    Active = item.Active,
                    Roles = await _userManager.GetRolesAsync(item)
                };

                userRoles.Add(userRole);
            }
            viewModel.User = UserId;
            viewModel.UserRoleViewModelsList = userRoles;
            viewModel.SelectRoles = new SelectList(roles);
            viewModel.Department1s = department1s;
            viewModel.Department2s = department2s;
            viewModel.Sections = sections;
            viewModel.Shops = shops;
            viewModel.Positions = positions;
            viewModel.AvatarImageProfile = AvatarImageProfile;
            viewModel.BgColor = BgColor;

            ViewBag.ErrorMaintain = TempData["ErrorMaintain"];
            ViewBag.ErrorTextMaintain = TempData["ErrorTextMaintain"];

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditProfile(ProfileVieweModel data, string id, IFormFile imageuser, IFormFile bguser, string valueimageuser, string valuebguser, string valueavatarimageuser, string valueavatarbguser)
        {
            var userStore = new UserStore<ApplicationUser>(_db);
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            var ProfileId = _userManager.FindByIdAsync(id).Result;
            var log_ApplicationUsers = _db.Log_ApplicationUsers.ToList();

            var filePathImageUserDatabase = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "users", "profile")).Root + $@"\{ProfileId.ImageUser}";
            FileInfo fileInfoImageUserDatabase = new(filePathImageUserDatabase);
            var filePathBgUserDatabase = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "users", "background")).Root + $@"\{ProfileId.BgUser}";
            FileInfo fileInfoBgUserDatabase = new(filePathBgUserDatabase);


            if (ProfileId != null)
            {
                if (valueavatarimageuser != null)
                {
                    if (fileInfoImageUserDatabase.Exists)
                    {
                        if (fileInfoImageUserDatabase.Name.Contains("Avatar"))
                        {
                            ProfileId.ImageUser = null;
                        }
                        else
                        {
                            fileInfoImageUserDatabase.Delete();
                            ProfileId.ImageUser = null;
                        } 
                    }
                    ProfileId.ImageUser = valueavatarimageuser;
                }
                else if (valueimageuser == null)
                {
                    if (imageuser != null)
                    {
                        if (fileInfoImageUserDatabase.Exists)
                        {
                            if (fileInfoImageUserDatabase.Name.Contains("Avatar"))
                            {
                                ProfileId.ImageUser = null;
                            }
                            else
                            {
                                fileInfoImageUserDatabase.Delete();
                                ProfileId.ImageUser = null;
                            }
                        }

                        var fileNameImageUser = Path.GetFileName(imageuser.FileName);
                        var fileExtImageUser = Path.GetExtension(fileNameImageUser);

                        var tmpNameImageUser = Guid.NewGuid().ToString();
                        var newFileNameImageUser = string.Concat(tmpNameImageUser, fileExtImageUser);
                        var filePathImageUser = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "users", "profile")).Root + $@"\{newFileNameImageUser}";

                        using (FileStream fs = System.IO.File.Create(filePathImageUser))
                        {
                            imageuser.CopyTo(fs);
                            fs.Flush();
                        }
                        ProfileId.ImageUser = newFileNameImageUser.Trim();
                    }
                }
                else
                {
                    if (fileInfoImageUserDatabase.Exists)
                    {
                        if (fileInfoImageUserDatabase.Name.Contains("Avatar"))
                        {
                            ProfileId.ImageUser = null;
                        }
                        else
                        {
                            fileInfoImageUserDatabase.Delete();
                            ProfileId.ImageUser = null;
                        }
                    }
                }

                if (valueavatarbguser != null)
                {
                    if (fileInfoBgUserDatabase.Exists)
                    {
                        if (fileInfoBgUserDatabase.Name.Contains("BgColor"))
                        {
                            ProfileId.BgUser = null;
                        }
                        else
                        {
                            fileInfoBgUserDatabase.Delete();
                            ProfileId.BgUser = null;
                        }
                    }
                    ProfileId.BgUser = valueavatarbguser;
                }
                else if (valuebguser == null)
                {
                    if (bguser != null)
                    {
                        if (fileInfoBgUserDatabase.Name.Contains("BgColor"))
                        {
                            ProfileId.BgUser = null;
                        }
                        else
                        {
                            fileInfoBgUserDatabase.Delete();
                            ProfileId.BgUser = null;
                        }

                        var fileNameBgUser = Path.GetFileName(bguser.FileName);
                        var fileExtBgUser = Path.GetExtension(fileNameBgUser);

                        var tmpNameBgUser = Guid.NewGuid().ToString();
                        var newFileNameBgUser = string.Concat(tmpNameBgUser, fileExtBgUser);
                        var filePathBgUser = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "users", "background")).Root + $@"\{newFileNameBgUser}";

                        using (FileStream fs = System.IO.File.Create(filePathBgUser))
                        {
                            bguser.CopyTo(fs);
                            fs.Flush();
                        }
                        ProfileId.BgUser = newFileNameBgUser.Trim();
                    }
                }
                else
                {
                    if (fileInfoBgUserDatabase.Exists)
                    {
                        if (fileInfoBgUserDatabase.Name.Contains("BgColor"))
                        {
                            ProfileId.BgUser = null;
                        }
                        else
                        {
                            fileInfoBgUserDatabase.Delete();
                            ProfileId.BgUser = null;
                        }
                    }
                }

                if (data.UserRoleViewModel != null)
                {
                    ProfileId.UserName = data.UserRoleViewModel.UserName;
                    ProfileId.EmployeeId = data.UserRoleViewModel.EmployeeId;
                    ProfileId.FirstNameEN = data.UserRoleViewModel.FirstNameEN;
                    ProfileId.LastNameEN = data.UserRoleViewModel.LastNameEN;
                    ProfileId.FirstNameTH = data.UserRoleViewModel.FirstNameTH;
                    ProfileId.LastNameTH = data.UserRoleViewModel.LastNameTH;
                    ProfileId.NickName = data.UserRoleViewModel.NickName;
                    ProfileId.ShopId = data.UserRoleViewModel.ShopId;
                    ProfileId.PositionId = data.UserRoleViewModel.PositionId;
                    ProfileId.InternalPhoneNumber = data.UserRoleViewModel.InternalPhoneNumber;
                    ProfileId.PhoneNumber = data.UserRoleViewModel.PhoneNumber;
                }
                

                await _userManager.UpdateAsync(ProfileId);
                var ctx = userStore.Context;
                await ctx.SaveChangesAsync();

                var Role = await _userManager.GetRolesAsync(ProfileId);

                if (data.UserRoleViewModel != null)
                {
                    if (string.Join(",", Role.ToList()) != data.UserRoleViewModel.NewRole)
                    {
                        var removeRole = await _userManager.RemoveFromRolesAsync(ProfileId, Role.ToArray());

                        if (removeRole.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(ProfileId, data.UserRoleViewModel.NewRole);
                        }
                    }
                }
               
                var EditProfileDatabase = _userManager.FindByIdAsync(id).Result;

                if (EditProfileDatabase != null)
                {
                    Log_ApplicationUser SaveLog = new()
                    {
                        UserId = EditProfileDatabase.Id,
                        UserName = EditProfileDatabase.UserName,
                        EmployeeId = EditProfileDatabase.EmployeeId,
                        FirstNameEN = EditProfileDatabase.FirstNameEN,
                        LastNameEN = EditProfileDatabase.LastNameEN,
                        FirstNameTH = EditProfileDatabase.FirstNameTH,
                        LastNameTH = EditProfileDatabase.LastNameTH,
                        NickName = EditProfileDatabase.NickName,
                        ImageUser = EditProfileDatabase.ImageUser,
                        BgUser = EditProfileDatabase.BgUser,
                        ShopId = EditProfileDatabase.ShopId,
                        PositionId = EditProfileDatabase.PositionId,
                        InternalPhoneNumber = EditProfileDatabase.InternalPhoneNumber,
                        PhoneNumber = EditProfileDatabase.PhoneNumber,
                        CreateBy = EditProfileDatabase.CreateBy,
                        CreateOn = EditProfileDatabase.CreateOn,
                        Active = EditProfileDatabase.Active,
                        Action = "Edit",
                        ActionBy = UserId.Id,
                        ActionByFirstName = UserId.FirstNameTH,
                        ActionByLastName = UserId.LastNameTH,
                        ActionOn = DateTime.Now,
                    };
                    _db.Log_ApplicationUsers.Add(SaveLog);
                    _db.SaveChanges();
                }
                return RedirectToAction("Profile", new { id = id });
            }
            else
            {

            } 
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteProfile(string id)
        {
            var userStore = new UserStore<ApplicationUser>(_db);
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            var ProfileId = _userManager.FindByIdAsync(id).Result;

            if (ProfileId != null)
            {
                
                ProfileId.Active = false;

                await _userManager.UpdateAsync(ProfileId);
                var ctx = userStore.Context;
                await ctx.SaveChangesAsync();

                var EditProfileDatabase = _userManager.FindByIdAsync(id).Result;

                if (EditProfileDatabase != null)
                {
                    Log_ApplicationUser SaveLog = new()
                    {
                        UserId = EditProfileDatabase.Id,
                        UserName = EditProfileDatabase.UserName,
                        EmployeeId = EditProfileDatabase.EmployeeId,
                        FirstNameEN = EditProfileDatabase.FirstNameEN,
                        LastNameEN = EditProfileDatabase.LastNameEN,
                        FirstNameTH = EditProfileDatabase.FirstNameTH,
                        LastNameTH = EditProfileDatabase.LastNameTH,
                        NickName = EditProfileDatabase.NickName,
                        ImageUser = EditProfileDatabase.ImageUser,
                        BgUser = EditProfileDatabase.BgUser,
                        ShopId = EditProfileDatabase.ShopId,
                        PositionId = EditProfileDatabase.PositionId,
                        InternalPhoneNumber = EditProfileDatabase.InternalPhoneNumber,
                        PhoneNumber = EditProfileDatabase.PhoneNumber,
                        CreateBy = EditProfileDatabase.CreateBy,
                        CreateOn = EditProfileDatabase.CreateOn,
                        Active = EditProfileDatabase.Active,
                        Action = "Delete",
                        ActionBy = UserId.Id,
                        ActionByFirstName = UserId.FirstNameTH,
                        ActionByLastName = UserId.LastNameTH,
                        ActionOn = DateTime.Now,
                    };
                    _db.Log_ApplicationUsers.Add(SaveLog);
                    _db.SaveChanges();
                }
                return RedirectToAction("Profile", new { id = id });
            }
            else
            {

            }
            return RedirectToAction("Profile", new { id = id }); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ExtendProfile(string id)
        {
            var userStore = new UserStore<ApplicationUser>(_db);
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            var ProfileId = _userManager.FindByIdAsync(id).Result;

            if (ProfileId != null)
            {

                ProfileId.Active = true;

                await _userManager.UpdateAsync(ProfileId);
                var ctx = userStore.Context;
                await ctx.SaveChangesAsync();

                var EditProfileDatabase = _userManager.FindByIdAsync(id).Result;

                if (EditProfileDatabase != null)
                {
                    Log_ApplicationUser SaveLog = new()
                    {
                        UserId = EditProfileDatabase.Id,
                        UserName = EditProfileDatabase.UserName,
                        EmployeeId = EditProfileDatabase.EmployeeId,
                        FirstNameEN = EditProfileDatabase.FirstNameEN,
                        LastNameEN = EditProfileDatabase.LastNameEN,
                        FirstNameTH = EditProfileDatabase.FirstNameTH,
                        LastNameTH = EditProfileDatabase.LastNameTH,
                        ImageUser = EditProfileDatabase.ImageUser,
                        BgUser = EditProfileDatabase.BgUser,
                        ShopId = EditProfileDatabase.ShopId,
                        PositionId = EditProfileDatabase.PositionId,
                        InternalPhoneNumber = EditProfileDatabase.InternalPhoneNumber,
                        PhoneNumber = EditProfileDatabase.PhoneNumber,
                        CreateBy = EditProfileDatabase.CreateBy,
                        CreateOn = EditProfileDatabase.CreateOn,
                        Active = EditProfileDatabase.Active,
                        Action = "Extend",
                        ActionBy = UserId.Id,
                        ActionByFirstName = UserId.FirstNameTH,
                        ActionByLastName = UserId.LastNameTH,
                        ActionOn = DateTime.Now,
                    };
                    _db.Log_ApplicationUsers.Add(SaveLog);
                    _db.SaveChanges();
                }
                return RedirectToAction("Profile", new { id = id });
            }
            else
            {

            }
            return RedirectToAction("Profile", new { id = id }); ;
        }

        [Authorize]
        public IActionResult Menu()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            return View();
        }

        [Authorize]
        public IActionResult Help()
        {
            return View();
        }

        [Authorize]
        public IActionResult Setting()
        {
            return View();
        }
    }
}
