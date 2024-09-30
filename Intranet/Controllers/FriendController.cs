using Intranet.Data;
using Intranet.Models;
using Intranet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

namespace Intranet.Controllers
{
    public class FriendController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public FriendController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
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

        public void SendMailChangePassword(string password, string firstnameTH, string lastnameTH, string usermail)
        {
            var FromMail = "system@moshimoshi.co.th";
            var FromPasswordMail = "AdminMoshi!2019";
            var ToMail = usermail;

            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress(ToMail));
            message.From = new MailAddress(FromMail);
            message.Subject = "[Intranet] สร้างรหัสผ่านครั้งแรก";
            message.Body = "<b>" + "เรียน คุณ" + firstnameTH + " " + lastnameTH + "</b>" + "<br>" + "<br>" +
                           "&emsp;&emsp;" + "การสร้างรหัสผ่านบัญชี Intranet ของคุณ" + "<br>" +
                           "&emsp;&emsp;" + "คุณจะต้องใช้รหัสผ่านนี้  " + "<b>" + password + "</b>" + "  เพื่อเข้าถึงบัญชีและสร้างรหัสผ่านใหม่" + "<br>" + "<br>" +
                           "&emsp;&emsp;" + "<a href=\"https://Intranet.moshimoshijp.co.th:8048\" style=\"font-weight:bold\">" + "เข้าสู่หน้าเว็บไซต์" + "</a>" + "<br>" + "<br>" +
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

        private async Task<List<string>> GetRolesList(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            
            var locationGroups = _db.LocationGroups.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var locations = _db.Locations.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var department1s = _db.Department1s.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var department2s = _db.Department2s.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var sections = _db.Sections.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var shops = _db.Shops.Where(a => a.Active == true).OrderBy(o => o.Branch).ToList();
            var shopPositionGroups = _db.ShopPositionGroups.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var positions = _db.Positions.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var roles = _roleManager.Roles.OrderBy(o => o.Name);
            var users = _userManager.Users.OrderBy(o => o.UserName).ToList();

            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;

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


            var userRoles = new List<UserRoleViewModel>();

            var viewModel = new UserManageViewModel();

            foreach (var item in users)
            {
                var userRole = new UserRoleViewModel
                {
                    Id = item.Id,
                    ImageUser = item.ImageUser,
                    BgUser = item.BgUser,
                    UserName = item.UserName,
                    EmployeeId = item.EmployeeId,
                    FirstNameEN = item.FirstNameEN,
                    LastNameEN = item.LastNameEN,
                    FirstNameTH = item.FirstNameTH,
                    LastNameTH = item.LastNameTH,
                    NickName = item.NickName,
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
            viewModel.UserRoleViewModelsList = userRoles;
            viewModel.SelectRoles = new SelectList(roles);
            viewModel.Department1s = department1s;
            viewModel.Department2s = department2s;
            viewModel.Sections = sections;
            viewModel.Shops = shops;
            viewModel.Positions = positions;           


            ViewBag.ErrorUserManage = TempData["ErrorUserManage"];
            ViewBag.ErrorTextUserManage = TempData["ErrorTextUserManage"];

            ViewBag.ErrorCreateExcelCountUserManage = TempData["ErrorCreateExcelCountUserManage"];
            ViewBag.ErrorCreateExcelSystemCountUserManage = TempData["ErrorCreateExcelSystemCountUserManage"];

            ViewBag.ErrorMaintain = TempData["ErrorMaintain"];
            ViewBag.ErrorTextMaintain = TempData["ErrorTextMaintain"];

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CreateUser(UserManageViewModel data, IFormFile FileCreateUser)
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;

            if (FileCreateUser != null)
            {
                var listuser = new List<UserRoleViewModel>();
                ExcelPackage.LicenseContext = LicenseContext.Commercial;

                using (var stream = new MemoryStream())
                {
                    await FileCreateUser.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowcount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowcount; row++)
                        {
                            int? ShopIdValue = 0;
                            int? PositionIdValue = 0;
                            var NickNameValue = "";
                            var InternalPhoneNumberValue = "";
                            var PhoneNumberValue = "";

                            if (worksheet.Cells[row, 7].Value?.ToString()?.Trim() != null)
                            {
                                NickNameValue = worksheet.Cells[row, 7].Value?.ToString()?.Trim();
                            }
                            else
                            {
                                NickNameValue = null;
                            }

                            if (worksheet.Cells[row, 8].Value?.ToString()?.Trim() != null)
                            {
                                ShopIdValue = Convert.ToInt32(worksheet.Cells[row, 8].Value);
                            }
                            else
                            {
                                ShopIdValue = null;
                            }

                            if (worksheet.Cells[row, 9].Value?.ToString()?.Trim() != null)
                            {
                                PositionIdValue = Convert.ToInt32(worksheet.Cells[row, 9].Value);
                            }
                            else
                            {
                                PositionIdValue = null;
                            }

                            if (worksheet.Cells[row, 10].Value?.ToString()?.Trim() != null)
                            {
                                InternalPhoneNumberValue = worksheet.Cells[row, 10].Value?.ToString()?.Trim();
                            }
                            else
                            {
                                InternalPhoneNumberValue = null;
                            }

                            if (worksheet.Cells[row, 11].Value?.ToString()?.Trim() != null)
                            {
                                PhoneNumberValue = worksheet.Cells[row, 11].Value?.ToString()?.Trim();
                            }
                            else
                            {
                                PhoneNumberValue = null;
                            }

                            listuser.Add(new UserRoleViewModel
                            {
                                UserName = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                EmployeeId = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                FirstNameEN = worksheet.Cells[row, 3].Value.ToString().Trim(),
                                LastNameEN = worksheet.Cells[row, 4].Value.ToString().Trim(),
                                FirstNameTH = worksheet.Cells[row, 5].Value?.ToString().Trim(),
                                LastNameTH = worksheet.Cells[row, 6].Value?.ToString()?.Trim(),
                                NickName = NickNameValue,
                                ShopId = ShopIdValue,
                                PositionId = PositionIdValue,
                                InternalPhoneNumber = InternalPhoneNumberValue,
                                PhoneNumber = PhoneNumberValue,
                                NewRole = worksheet.Cells[row, 12].Value.ToString().Trim()
                            });
                        }
                    }
                }

                if (listuser.Count > 0)
                {
                    var countcreateuser = 0;

                    foreach (var itemlistuser in listuser)
                    {
                        ApplicationUser user = new()
                        {
                            UserName = itemlistuser.UserName,
                            Email = itemlistuser.UserName,
                            EmployeeId = itemlistuser.EmployeeId,
                            FirstNameEN = itemlistuser.FirstNameEN,
                            LastNameEN = itemlistuser.LastNameEN,
                            FirstNameTH = itemlistuser.FirstNameTH,
                            LastNameTH = itemlistuser.LastNameTH,
                            NickName = itemlistuser.NickName,
                            ShopId = itemlistuser.ShopId,
                            PositionId = itemlistuser.PositionId,
                            InternalPhoneNumber = itemlistuser.InternalPhoneNumber,
                            PhoneNumber = itemlistuser.PhoneNumber,
                            CreateBy = UserId.Id,
                            CreateOn = DateTime.Now,
                            Active = true
                        };

                        var password = GeneratePassword();
                        var resultUser = await _userManager.CreateAsync(user, password);

                        if (resultUser.Succeeded)
                        {
                            var resultRole = await _userManager.AddToRoleAsync(user, itemlistuser.NewRole);

                            if (resultRole.Succeeded)
                            {
                                SendMailChangePassword(password, user.FirstNameTH, user.LastNameTH, user.UserName);

                                var UserDatabase = _userManager.FindByNameAsync(itemlistuser.UserName).Result;

                                if (UserDatabase != null)
                                {
                                    Log_ApplicationUser SaveLog = new()
                                    {
                                        UserId = UserDatabase.Id,
                                        UserName = UserDatabase.UserName,
                                        EmployeeId = UserDatabase.EmployeeId,
                                        FirstNameEN = UserDatabase.FirstNameEN,
                                        LastNameEN = UserDatabase.LastNameEN,
                                        FirstNameTH = UserDatabase.FirstNameTH,
                                        LastNameTH = UserDatabase.LastNameTH,
                                        NickName = UserDatabase.NickName,
                                        ImageUser = UserDatabase.ImageUser,
                                        BgUser = UserDatabase.BgUser,
                                        ShopId = UserDatabase.ShopId,
                                        PositionId = UserDatabase.PositionId,
                                        InternalPhoneNumber = UserDatabase.InternalPhoneNumber,
                                        PhoneNumber = UserDatabase.PhoneNumber,
                                        CreateBy = UserDatabase.CreateBy,
                                        CreateOn = UserDatabase.CreateOn,
                                        Active = UserDatabase.Active,
                                        Action = "Create",
                                        ActionBy = UserDatabase.Id,
                                        ActionByFirstName = UserId.FirstNameTH,
                                        ActionByLastName = UserId.LastNameTH,
                                        ActionOn = DateTime.Now,
                                    };
                                    _db.Log_ApplicationUsers.Add(SaveLog);
                                    _db.SaveChanges();

                                    countcreateuser++;
                                }
                                else
                                {

                                }
                            }
                            else
                            {
                                foreach (var item in resultRole.Errors)
                                {
                                    ModelState.AddModelError("", item.Description);
                                }
                            }
                        }
                        else
                        {
                            TempData["ErrorUserManage"] = "Error";
                            TempData["ErrorTextUserManage"] = "ErrorUserEmail";
                        }
                    }

                    TempData["ErrorUserManage"] = "Error";
                    TempData["ErrorTextUserManage"] = "ErrorCreateUserExcel";
                    TempData["ErrorCreateExcelCountUserManage"] = listuser.Count();
                    TempData["ErrorCreateExcelSystemCountUserManage"] = countcreateuser;
                }
            }
            else
            {
                if (data != null)
                {
                    ApplicationUser user = new()
                    {
                        UserName = data.UserRoleViewModel.UserName,
                        Email = data.UserRoleViewModel.UserName,
                        EmployeeId = data.UserRoleViewModel.EmployeeId,
                        FirstNameEN = data.UserRoleViewModel.FirstNameEN,
                        LastNameEN = data.UserRoleViewModel.LastNameEN,
                        FirstNameTH = data.UserRoleViewModel.FirstNameTH,
                        LastNameTH = data.UserRoleViewModel.LastNameTH,
                        NickName = data.UserRoleViewModel.NickName,
                        ShopId = data.UserRoleViewModel.ShopId,
                        PositionId = data.UserRoleViewModel.PositionId,
                        InternalPhoneNumber = data.UserRoleViewModel.InternalPhoneNumber,
                        PhoneNumber = data.UserRoleViewModel.PhoneNumber,
                        CreateBy = UserId.Id,
                        CreateOn = DateTime.Now,
                        Active = true
                    };


                    var password = GeneratePassword();
                    var resultUser = await _userManager.CreateAsync(user, password);

                    if (resultUser.Succeeded)
                    {
                        var resultRole = await _userManager.AddToRoleAsync(user, data.UserRoleViewModel.NewRole);

                        if (resultRole.Succeeded)
                        {
                            SendMailChangePassword(password, user.FirstNameTH, user.LastNameTH, user.UserName);

                            var UserDatabase = _userManager.FindByNameAsync(data.UserRoleViewModel.UserName).Result;

                            if (UserDatabase != null)
                            {
                                Log_ApplicationUser SaveLog = new()
                                {
                                    UserId = UserDatabase.Id,
                                    UserName = UserDatabase.UserName,
                                    EmployeeId = UserDatabase.EmployeeId,
                                    FirstNameEN = UserDatabase.FirstNameEN,
                                    LastNameEN = UserDatabase.LastNameEN,
                                    FirstNameTH = UserDatabase.FirstNameTH,
                                    LastNameTH = UserDatabase.LastNameTH,
                                    ImageUser = UserDatabase.ImageUser,
                                    NickName = UserDatabase.NickName,
                                    BgUser = UserDatabase.BgUser,
                                    ShopId = UserDatabase.ShopId,
                                    PositionId = UserDatabase.PositionId,
                                    InternalPhoneNumber = UserDatabase.InternalPhoneNumber,
                                    PhoneNumber = UserDatabase.PhoneNumber,
                                    CreateBy = UserDatabase.CreateBy,
                                    CreateOn = UserDatabase.CreateOn,
                                    Active = UserDatabase.Active,
                                    Action = "Create",
                                    ActionBy = UserDatabase.Id,
                                    ActionByFirstName = UserId.FirstNameTH,
                                    ActionByLastName = UserId.LastNameTH,
                                    ActionOn = DateTime.Now,
                                };
                                _db.Log_ApplicationUsers.Add(SaveLog);
                                _db.SaveChanges();
                            }
                            else
                            {

                            }

                            return RedirectToAction("Profile", "Account", new { UserDatabase!.Id });
                        }
                        else
                        {
                            foreach (var item in resultRole.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }
                    else
                    {
                        TempData["ErrorUserManage"] = "Error";
                        TempData["ErrorTextUserManage"] = "ErrorUserEmail";
                    }

                }
            }

            return RedirectToAction("Index");
        }
    }
}
