using Intranet.Data;
using Intranet.Models;
using Intranet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Collections;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Xml.Linq;

namespace Intranet.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public JsonResult Department2(int id)
        {
            var listdepartment2 = _db.Department2s.Where(i => i.Department1Id == id && i.Active == true).OrderBy(o => o.Name).ToList();
            return new JsonResult(listdepartment2);
        }

        public JsonResult Section(int id1,int id2)
        {
            var listsection =  new List<Section>();
            
            if (id2 != 0)
            {
                listsection = _db.Sections.Where(i => i.Department1Id == id1 && i.Department2Id == id2 && i.Active == true).OrderBy(o => o.Name).ToList();
            }
            else
            {
                listsection = _db.Sections.Where(i => i.Department1Id == id1 && i.Department2Id == null && i.Active == true).OrderBy(o => o.Name).ToList();
                
            }
            return new JsonResult(listsection);
        }

        public JsonResult Shop(int id)
        {
            var shops = _db.Shops.Where(i => i.SectionId == id && i.Active == true).OrderBy(o => o.Branch).ToList();

            List<object> listshop = new List<object>();
            foreach (var item in shops)
            {
                var shopbranch = shops.Where(a => a.Id == item.Id).Select(s => s.Branch).FirstOrDefault();

                listshop.Add(new
                {
                    item.Id,
                    Name = shopbranch + " : " + item.Name
                });
            }

            return new JsonResult(listshop);
        }

        public JsonResult Position(int id1,int id2)
        {  
            var listposition = new List<Position>();

            if (id2 != 0)
            {
                var listshoppositiongroup = _db.Shops.Where(i => i.Id == id2 && i.SectionId == id1 && i.Active == true).Select(s => s.ShopPositionGroupId).FirstOrDefault();
                listposition = _db.Positions.Where(i => i.SectionId == id1 && i.ShopPositionGroupId == listshoppositiongroup && i.Active == true).OrderBy(o => o.Name).ToList();
            }
            else
            {
                listposition = _db.Positions.Where(i => i.SectionId == id1 && i.ShopPositionGroupId == null && i.Active == true).OrderBy(o => o.Name).ToList();

            }
            return new JsonResult(listposition);         
        }

        public JsonResult PositionMaintain(int id)
        {
            var listpositionmaintain = new List<Position>();

            listpositionmaintain = _db.Positions.Where(i => i.SectionId == id && i.Active == true).OrderBy(o => o.Name).ToList();

            return new JsonResult(listpositionmaintain);
        }


        private async Task<List<string>> GetRolesList(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Maintain()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            var users = _userManager.Users.Where(a => a.Active == true).OrderBy(o => o.UserName).ToList();
            var menuGroups = _db.MenuGroups.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var userMenuGroups = _db.UserMenuGroups.Where(a => a.Active == true).OrderBy(o => o.MenuGroupId).ToList();
            var categoryComplaints = _db.CategoryComplaints.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var complaintFroms = _db.ComplaintFroms.Where(a => a.Active == true).OrderBy(o => o.Id).ToList();
            var shopGroups = _db.ShopGroups.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();           
            var locationGroups = _db.LocationGroups.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var locations = _db.Locations.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var department1s = _db.Department1s.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var department2s = _db.Department2s.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var sections = _db.Sections.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var shops = _db.Shops.Where(a => a.Active == true).OrderBy(o => o.Branch).ToList();
            var shopPositionGroups = _db.ShopPositionGroups.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var positions = _db.Positions.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var usersRole = _userManager.GetUsersInRoleAsync("Admin").Result;

            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;

            ViewData["LocationGroupsList"] = new SelectList(locationGroups, "Id", "Name");
            ViewData["LocationsList"] = new SelectList(locations, "Id", "Name");
            ViewData["ShopPositionGroupsList"] = new SelectList(shopPositionGroups, "Id", "Name");
            ViewData["ShopGroupsList"] = new SelectList(shopGroups, "Id", "Name");

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

            List<object> listmultipledocumentsandticketsuseradmins = new List<object>();

            foreach (var item in usersRole)
            {
                if (item.UserName != "System@moshimoshi.co.th")
                {
                    var userid = _userManager.Users.Where(a => a.Id == item.Id && a.Active == true).FirstOrDefault();

                    if (userid != null)
                    {
                        listmultipledocumentsandticketsuseradmins.Add(new
                        {
                            userid.Id,
                            Name = userid.FirstNameTH + " " + userid.LastNameTH
                        });
                    }
                }
            }

            ViewData["MultipleDocumentsandTicketsUserAdminsList"] = new SelectList(listmultipledocumentsandticketsuseradmins.Distinct().ToList(), "Id", "Name");

            ViewData["MenuGroupsList"] = new SelectList(menuGroups, "Id", "Name");

            List<object> listuser = new List<object>();

            foreach (var item in users.OrderBy(o => o.FirstNameTH))
            {
                listuser.Add(new
                {
                    item.Id,
                    Name = item.FirstNameTH + " " + item.LastNameTH
                });
            }

            ViewData["UsersList"] = new SelectList(listuser, "Id", "Name");

            var viewModel = new MaintainViewModel()
            {
                MenuGroups = menuGroups,
                UserMenuGroups = userMenuGroups,
                CategoryComplaints = categoryComplaints,
                ComplaintFroms = complaintFroms,
                LocationGroups = locationGroups,
                Locations = locations,
                Department1s = department1s,
                Department2s = department2s,
                Sections = sections,
                Shops = shops,
                ShopGroups = shopGroups,
                ShopPositionGroups = shopPositionGroups,
                Positions = positions,
                Users = users,
            };

            ViewBag.ErrorMaintain = TempData["ErrorMaintain"];
            ViewBag.ErrorTextMaintain = TempData["ErrorTextMaintain"];
            ViewBag.ErrorCreateExcelCountMaintain = TempData["ErrorCreateExcelCountMaintain"];
            ViewBag.ErrorCreateExcelSystemCountMaintain = TempData["ErrorCreateExcelSystemCountMaintain"];

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CreateMaintainAsync(MaintainViewModel data, string MaintainData, IFormFile FileCreateMaintain)
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
            var menuGroups = _db.MenuGroups.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var userMenuGroups = _db.UserMenuGroups.Where(a => a.Active == true).OrderBy(o => o.Id).ToList();
            var categoryComplaints = _db.CategoryComplaints.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var complaintFroms = _db.ComplaintFroms.Where(a => a.Active == true).OrderBy(o => o.Id).ToList();


            if (MaintainData == "MenuGroup")
            {
                if (FileCreateMaintain != null)
                {
                    var listmaintain = new List<MenuGroup>();
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;

                    using (var stream = new MemoryStream())
                    {
                        await FileCreateMaintain.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowcount = worksheet.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                listmaintain.Add(new MenuGroup
                                {
                                    Name = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                });
                            }
                        }
                    }

                    foreach (var item in listmaintain)
                    {
                        var checkdata = _db.MenuGroups.Where(n => n.Name == item.Name && n.Active == true);

                        if (checkdata.Any())
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "CreateError";
                            return RedirectToAction("Maintain");
                        }
                    }

                    if (listmaintain.Count > 0)
                    {
                        var countcreatemaintain = 0;

                        foreach (var itemlistmaintain in listmaintain)
                        {
                            MenuGroup menuGroup = new()
                            {
                                Name = itemlistmaintain.Name,
                                Active = true
                            };

                            _db.MenuGroups.Add(menuGroup);
                            var result = _db.SaveChanges();

                            var MenuGroupDatabase = _db.MenuGroups.Where(a => a.Name == menuGroup.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                            if (result > 0 && MenuGroupDatabase != null)
                            {
                                Log_MenuGroup SaveLog = new()
                                {
                                    MenuGroupId = MenuGroupDatabase.Id,
                                    Name = MenuGroupDatabase.Name,
                                    Active = MenuGroupDatabase.Active,
                                    Action = "Create",
                                    ActionBy = UserId.Id,
                                    ActionByFirstName = UserId.FirstNameTH,
                                    ActionByLastName = UserId.LastNameTH,
                                    ActionOn = DateTime.Now,
                                };
                                _db.Log_MenuGroups.Add(SaveLog);
                                _db.SaveChanges();

                                countcreatemaintain++;
                            }
                            else
                            {
                                TempData["ErrorMaintain"] = "Error";
                                TempData["ErrorTextMaintain"] = "LogError";
                                return RedirectToAction("Maintain");
                            }
                        }

                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "ErrorCreateMaintainExcel";
                        TempData["ErrorCreateExcelCountMaintain"] = listmaintain.Count();
                        TempData["ErrorCreateExcelSystemCountMaintain"] = countcreatemaintain;
                    }
                }
                else
                {
                    var checkdata = _db.MenuGroups.Where(n => n.Name == data.MenuGroup.Name && n.Active == true);

                    if (checkdata.Any())
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "CreateError";
                        return RedirectToAction("Maintain");
                    }
                    else
                    {
                        data.MenuGroup.Active = true;
                        _db.MenuGroups.Add(data.MenuGroup);
                        var result = _db.SaveChanges();

                        var MenuGroupDatabase = _db.MenuGroups.Where(a => a.Name == data.MenuGroup.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && MenuGroupDatabase != null)
                        {
                            Log_MenuGroup SaveLog = new()
                            {
                                MenuGroupId = MenuGroupDatabase.Id,
                                Name = MenuGroupDatabase.Name,
                                Active = MenuGroupDatabase.Active,
                                Action = "Create",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                            };
                            _db.Log_MenuGroups.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }

                        return RedirectToAction("Maintain");
                    }
                }
            }
            else if (MaintainData == "UserMenuGroup")
            {
                if (FileCreateMaintain != null)
                {
                    var listmaintain = new List<UserMenuGroup>();
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;

                    using (var stream = new MemoryStream())
                    {
                        await FileCreateMaintain.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowcount = worksheet.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                listmaintain.Add(new UserMenuGroup
                                {
                                    MenuGroupId = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                                    UserId = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                });
                            }
                        }
                    }

                    foreach (var item in listmaintain)
                    {
                        var checkdata = _db.UserMenuGroups.Where(n => n.MenuGroupId == item.MenuGroupId && n.UserId == item.UserId && n.Active == true);

                        if (checkdata.Any())
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "CreateError";
                            return RedirectToAction("Maintain");
                        }
                    }

                    if (listmaintain.Count > 0)
                    {
                        var countcreatemaintain = 0;

                        foreach (var itemlistmaintain in listmaintain)
                        {
                            UserMenuGroup userMenuGroup = new()
                            {
                                MenuGroupId = itemlistmaintain.MenuGroupId,
                                UserId = itemlistmaintain.UserId,
                                Active = true
                            };

                            _db.UserMenuGroups.Add(userMenuGroup);
                            var result = _db.SaveChanges();

                            var UserMenuGroupDatabase = _db.UserMenuGroups.Where(a => a.MenuGroupId == userMenuGroup.MenuGroupId && a.UserId == userMenuGroup.UserId).OrderByDescending(o => o.Id).FirstOrDefault();

                            if (result > 0 && UserMenuGroupDatabase != null)
                            {
                                Log_UserMenuGroup SaveLog = new()
                                {
                                    UserMenuGroupId = UserMenuGroupDatabase.Id,
                                    MenuGroupId = UserMenuGroupDatabase.MenuGroupId,
                                    UserId = UserMenuGroupDatabase.UserId,
                                    Active = UserMenuGroupDatabase.Active,
                                    Action = "Create",
                                    ActionBy = UserId.Id,
                                    ActionByFirstName = UserId.FirstNameTH,
                                    ActionByLastName = UserId.LastNameTH,
                                    ActionOn = DateTime.Now,
                                };
                                _db.Log_UserMenuGroups.Add(SaveLog);
                                _db.SaveChanges();

                                countcreatemaintain++;
                            }
                            else
                            {
                                TempData["ErrorMaintain"] = "Error";
                                TempData["ErrorTextMaintain"] = "LogError";
                                return RedirectToAction("Maintain");
                            }
                        }

                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "ErrorCreateMaintainExcel";
                        TempData["ErrorCreateExcelCountMaintain"] = listmaintain.Count();
                        TempData["ErrorCreateExcelSystemCountMaintain"] = countcreatemaintain;
                    }
                }
                else
                {
                    var checkdata = _db.UserMenuGroups.Where(n => n.MenuGroupId == data.UserMenuGroup.MenuGroupId && n.UserId == data.UserMenuGroup.UserId && n.Active == true);

                    if (checkdata.Any())
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "CreateError";
                        return RedirectToAction("Maintain");
                    }
                    else
                    {
                        data.UserMenuGroup.Active = true;
                        _db.UserMenuGroups.Add(data.UserMenuGroup);
                        var result = _db.SaveChanges();

                        var UserMenuGroupDatabase = _db.UserMenuGroups.Where(a => a.MenuGroupId == data.UserMenuGroup.MenuGroupId && a.UserId == data.UserMenuGroup.UserId).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && UserMenuGroupDatabase != null)
                        {
                            Log_UserMenuGroup SaveLog = new()
                            {
                                UserMenuGroupId = UserMenuGroupDatabase.Id,
                                MenuGroupId = UserMenuGroupDatabase.MenuGroupId,
                                UserId = UserMenuGroupDatabase.UserId,
                                Active = UserMenuGroupDatabase.Active,
                                Action = "Create",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                            };
                            _db.Log_UserMenuGroups.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }

                        return RedirectToAction("Maintain");
                    }
                }
            }
            else if (MaintainData == "CategoryComplaint")
            {
                if (FileCreateMaintain != null)
                {
                    var listmaintain = new List<CategoryComplaint>();
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;

                    using (var stream = new MemoryStream())
                    {
                        await FileCreateMaintain.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowcount = worksheet.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                listmaintain.Add(new CategoryComplaint
                                {
                                    Name = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                    Email = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                });
                            }
                        }
                    }

                    foreach (var item in listmaintain)
                    {
                        var checkdata = _db.CategoryComplaints.Where(n => n.Name == item.Name && n.Active == true);

                        if (checkdata.Any())
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "CreateError";
                            return RedirectToAction("Maintain");
                        }
                    }

                    if (listmaintain.Count > 0)
                    {
                        var countcreatemaintain = 0;

                        foreach (var itemlistmaintain in listmaintain)
                        {
                            CategoryComplaint CategoryComplaint = new()
                            {
                                Name = itemlistmaintain.Name,
                                Email = itemlistmaintain.Email,
                                Active = true
                            };

                            _db.CategoryComplaints.Add(CategoryComplaint);
                            var result = _db.SaveChanges();

                            var CategoryComplaintDatabase = _db.CategoryComplaints.Where(a => a.Name == CategoryComplaint.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                            if (result > 0 && CategoryComplaintDatabase != null)
                            {
                                Log_CategoryComplaint SaveLog = new()
                                {
                                    CategoryComplaintId = CategoryComplaintDatabase.Id,
                                    Name = CategoryComplaintDatabase.Name,
                                    Email = CategoryComplaintDatabase.Email,
                                    Active = CategoryComplaintDatabase.Active,
                                    Action = "Create",
                                    ActionBy = UserId.Id,
                                    ActionByFirstName = UserId.FirstNameTH,
                                    ActionByLastName = UserId.LastNameTH,
                                    ActionOn = DateTime.Now,

                                   
                                };
                                _db.Log_CategoryComplaints.Add(SaveLog);
                                _db.SaveChanges();

                                countcreatemaintain++;
                            }
                            else
                            {
                                TempData["ErrorMaintain"] = "Error";
                                TempData["ErrorTextMaintain"] = "LogError";
                                return RedirectToAction("Maintain");
                            }
                        }

                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "ErrorCreateMaintainExcel";
                        TempData["ErrorCreateExcelCountMaintain"] = listmaintain.Count();
                        TempData["ErrorCreateExcelSystemCountMaintain"] = countcreatemaintain;
                    }
                }
                else
                {
                    var checkdata = _db.CategoryComplaints.Where(n => n.Name == data.CategoryComplaint.Name && n.Active == true);

                    if (checkdata.Any())
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "CreateError";
                        return RedirectToAction("Maintain");
                    }
                    else
                    {
                        data.CategoryComplaint.Active = true;
                        _db.CategoryComplaints.Add(data.CategoryComplaint);
                        var result = _db.SaveChanges();

                        var CategoryComplaintDatabase = _db.CategoryComplaints.Where(a => a.Name == data.CategoryComplaint.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && CategoryComplaintDatabase != null)
                        {
                            Log_CategoryComplaint SaveLog = new()
                            {
                                CategoryComplaintId = CategoryComplaintDatabase.Id,
                                Name = CategoryComplaintDatabase.Name,
                                Email = CategoryComplaintDatabase.Email,
                                Active = CategoryComplaintDatabase.Active,
                                Action = "Create",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                            };
                            _db.Log_CategoryComplaints.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }

                        return RedirectToAction("Maintain");
                    }
                }
            }
            else if (MaintainData == "ComplaintFrom")
            {
                if (FileCreateMaintain != null)
                {
                    var listmaintain = new List<ComplaintFrom>();
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;

                    using (var stream = new MemoryStream())
                    {
                        await FileCreateMaintain.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowcount = worksheet.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                listmaintain.Add(new ComplaintFrom
                                {
                                    Email = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                    Password = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                });
                            }
                        }
                    }

                    foreach (var item in listmaintain)
                    {
                        var checkdata = _db.ComplaintFroms.Where(n => n.Email == item.Email && n.Active == true);

                        if (checkdata.Any())
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "CreateError";
                            return RedirectToAction("Maintain");
                        }
                    }

                    if (listmaintain.Count > 0)
                    {
                        var countcreatemaintain = 0;

                        foreach (var itemlistmaintain in listmaintain)
                        {
                            ComplaintFrom ComplaintFrom = new()
                            {
                                Email = itemlistmaintain.Email,
                                Password = itemlistmaintain.Password,
                                Active = true
                            };

                            _db.ComplaintFroms.Add(ComplaintFrom);
                            var result = _db.SaveChanges();

                            var ComplaintFromDatabase = _db.ComplaintFroms.Where(a => a.Email == ComplaintFrom.Email).OrderByDescending(o => o.Id).FirstOrDefault();

                            if (result > 0 && ComplaintFromDatabase != null)
                            {
                                Log_ComplaintFrom SaveLog = new()
                                {
                                    ComplaintFromId = ComplaintFromDatabase.Id,
                                    Email = ComplaintFromDatabase.Email,
                                    Password = ComplaintFromDatabase.Password,
                                    Active = ComplaintFromDatabase.Active,
                                    Action = "Create",
                                    ActionBy = UserId.Id,
                                    ActionByFirstName = UserId.FirstNameTH,
                                    ActionByLastName = UserId.LastNameTH,
                                    ActionOn = DateTime.Now,
                                };
                                _db.log_ComplaintFroms.Add(SaveLog);
                                _db.SaveChanges();

                                countcreatemaintain++;
                            }
                            else
                            {
                                TempData["ErrorMaintain"] = "Error";
                                TempData["ErrorTextMaintain"] = "LogError";
                                return RedirectToAction("Maintain");
                            }
                        }

                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "ErrorCreateMaintainExcel";
                        TempData["ErrorCreateExcelCountMaintain"] = listmaintain.Count();
                        TempData["ErrorCreateExcelSystemCountMaintain"] = countcreatemaintain;
                    }
                }
                else
                {
                    var checkdata = _db.ComplaintFroms.Where(n => n.Email == data.ComplaintFrom.Email && n.Active == true);

                    if (checkdata.Any())
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "CreateError";
                        return RedirectToAction("Maintain");
                    }
                    else
                    {
                        data.ComplaintFrom.Active = true;
                        _db.ComplaintFroms.Add(data.ComplaintFrom);
                        var result = _db.SaveChanges();

                        var ComplaintFromDatabase = _db.ComplaintFroms.Where(a => a.Email == data.ComplaintFrom.Email).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && ComplaintFromDatabase != null)
                        {
                            Log_ComplaintFrom SaveLog = new()
                            {
                                ComplaintFromId = ComplaintFromDatabase.Id,
                                Email = ComplaintFromDatabase.Email,
                                Password = ComplaintFromDatabase.Password,
                                Active = ComplaintFromDatabase.Active,
                                Action = "Create",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                            };
                            _db.log_ComplaintFroms.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }

                        return RedirectToAction("Maintain");
                    }
                }
            }
            else if (MaintainData == "LocationGroup")
            {
                if (FileCreateMaintain != null)
                {
                    var listmaintain = new List<LocationGroup>();
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;

                    using (var stream = new MemoryStream())
                    {
                        await FileCreateMaintain.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowcount = worksheet.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                listmaintain.Add(new LocationGroup
                                {
                                    Name = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                });
                            }
                        }
                    }

                    foreach (var item in listmaintain)
                    {
                        var checkdata = _db.LocationGroups.Where(n => n.Name == item.Name && n.Active == true);

                        if (checkdata.Any())
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "CreateError";
                            return RedirectToAction("Maintain");
                        }
                    }

                    if (listmaintain.Count > 0)
                    {
                        var countcreatemaintain = 0;

                        foreach (var itemlistmaintain in listmaintain)
                        {
                            LocationGroup locationGroup = new()
                            {
                                Name = itemlistmaintain.Name,
                                Active = true
                            };

                            _db.LocationGroups.Add(locationGroup);
                            var result = _db.SaveChanges();

                            var LocationGroupDatabase = _db.LocationGroups.Where(a => a.Name == locationGroup.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                            if (result > 0 && LocationGroupDatabase != null)
                            {
                                Log_LocationGroup SaveLog = new()
                                {
                                    LocationGroupId = LocationGroupDatabase.Id,
                                    Name = LocationGroupDatabase.Name,
                                    Active = LocationGroupDatabase.Active,
                                    Action = "Create",
                                    ActionBy = UserId.Id,
                                    ActionByFirstName = UserId.FirstNameTH,
                                    ActionByLastName = UserId.LastNameTH,
                                    ActionOn = DateTime.Now,
                                };
                                _db.Log_LocationGroups.Add(SaveLog);
                                _db.SaveChanges();

                                countcreatemaintain++;
                            }
                            else
                            {
                                TempData["ErrorMaintain"] = "Error";
                                TempData["ErrorTextMaintain"] = "LogError";
                                return RedirectToAction("Maintain");
                            }
                        }

                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "ErrorCreateMaintainExcel";
                        TempData["ErrorCreateExcelCountMaintain"] = listmaintain.Count();
                        TempData["ErrorCreateExcelSystemCountMaintain"] = countcreatemaintain;
                    }
                }
                else
                {
                    var checkdata = _db.LocationGroups.Where(n => n.Name == data.LocationGroup.Name && n.Active == true);

                    if (checkdata.Any())
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "CreateError";
                        return RedirectToAction("Maintain");
                    }
                    else
                    {
                        data.LocationGroup.Active = true;
                        _db.LocationGroups.Add(data.LocationGroup);
                        var result = _db.SaveChanges();

                        var LocationGroupDatabase = _db.LocationGroups.Where(a => a.Name == data.LocationGroup.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && LocationGroupDatabase != null)
                        {
                            Log_LocationGroup SaveLog = new()
                            {
                                LocationGroupId = LocationGroupDatabase.Id,
                                Name = LocationGroupDatabase.Name,
                                Active = LocationGroupDatabase.Active,
                                Action = "Create",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                            };
                            _db.Log_LocationGroups.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }

                        return RedirectToAction("Maintain");
                    }
                }
            }
            else if (MaintainData == "Location")
            {
                if (FileCreateMaintain != null)
                {
                    var listmaintain = new List<Models.Location>();
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;

                    using (var stream = new MemoryStream())
                    {
                        await FileCreateMaintain.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowcount = worksheet.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                listmaintain.Add(new Models.Location
                                {
                                    LocationGroupId = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                                    Name = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                });
                            }
                        }
                    }

                    foreach (var item in listmaintain)
                    {
                        var checkdata = _db.Locations.Where(n => n.Name == item.Name && n.Active == true);

                        if (checkdata.Any())
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "CreateError";
                            return RedirectToAction("Maintain");
                        }
                    }

                    if (listmaintain.Count > 0)
                    {
                        var countcreatemaintain = 0;

                        foreach (var itemlistmaintain in listmaintain)
                        {
                            Models.Location location = new()
                            {
                                LocationGroupId = itemlistmaintain.LocationGroupId,
                                Name = itemlistmaintain.Name,
                                Active = true
                            };

                            _db.Locations.Add(location);
                            var result = _db.SaveChanges();

                            var LocationDatabase = _db.Locations.Where(a => a.Name == location.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                            if (result > 0 && LocationDatabase != null)
                            {
                                Log_Location SaveLog = new()
                                {
                                    LocationId = LocationDatabase.Id,
                                    LocationGroupId = LocationDatabase.LocationGroupId,
                                    Name = LocationDatabase.Name,
                                    Active = LocationDatabase.Active,
                                    Action = "Create",
                                    ActionBy = UserId.Id,
                                    ActionByFirstName = UserId.FirstNameTH,
                                    ActionByLastName = UserId.LastNameTH,
                                    ActionOn = DateTime.Now,
                                };
                                _db.Log_Locations.Add(SaveLog);
                                _db.SaveChanges();

                                countcreatemaintain++;
                            }
                            else
                            {
                                TempData["ErrorMaintain"] = "Error";
                                TempData["ErrorTextMaintain"] = "LogError";
                                return RedirectToAction("Maintain");
                            }
                        }

                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "ErrorCreateMaintainExcel";
                        TempData["ErrorCreateExcelCountMaintain"] = listmaintain.Count();
                        TempData["ErrorCreateExcelSystemCountMaintain"] = countcreatemaintain;
                    }
                }
                else
                {
                    var checkdata = _db.Locations.Where(n => n.Name == data.Location.Name && n.Active == true);

                    if (checkdata.Any())
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "CreateError";
                        return RedirectToAction("Maintain");
                    }
                    else
                    {
                        data.Location.Active = true;
                        _db.Locations.Add(data.Location);
                        var result = _db.SaveChanges();

                        var LocationDatabase = _db.Locations.Where(a => a.Name == data.Location.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && LocationDatabase != null)
                        {
                            Log_Location SaveLog = new()
                            {
                                LocationId = LocationDatabase.Id,
                                LocationGroupId = LocationDatabase.LocationGroupId,
                                Name = LocationDatabase.Name,
                                Active = LocationDatabase.Active,
                                Action = "Create",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                            };
                            _db.Log_Locations.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }

                        return RedirectToAction("Maintain");
                    }
                }
            }
            else if (MaintainData == "Department1")
            {
                if (FileCreateMaintain != null)
                {
                    var listmaintain = new List<Department1>();
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;

                    using (var stream = new MemoryStream())
                    {
                        await FileCreateMaintain.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowcount = worksheet.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                listmaintain.Add(new Department1
                                {
                                    LocationId = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                                    Name = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                });
                            }
                        }
                    }

                    foreach (var item in listmaintain)
                    {
                        var checkdata = _db.Department1s.Where(n => n.Name == item.Name && n.LocationId == item.LocationId && n.Active == true);

                        if (checkdata.Any())
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "CreateError";
                            return RedirectToAction("Maintain");
                        }
                    }

                    if (listmaintain.Count > 0)
                    {
                        var countcreatemaintain = 0;

                        foreach (var itemlistmaintain in listmaintain)
                        {
                            Department1 department1 = new()
                            {
                                LocationId = itemlistmaintain.LocationId,
                                Name = itemlistmaintain.Name,
                                Active = true
                            };

                            _db.Department1s.Add(department1);
                            var result = _db.SaveChanges();

                            var Department1Database = _db.Department1s.Where(a => a.Name == department1.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                            if (result > 0 && Department1Database != null)
                            {
                                Log_Department1 SaveLog = new()
                                {
                                    Department1Id = Department1Database.Id,
                                    LocationId = Department1Database.LocationId,
                                    Name = Department1Database.Name,
                                    Active = Department1Database.Active,
                                    Action = "Create",
                                    ActionBy = UserId.Id,
                                    ActionByFirstName = UserId.FirstNameTH,
                                    ActionByLastName = UserId.LastNameTH,
                                    ActionOn = DateTime.Now,
                                };
                                _db.Log_Department1s.Add(SaveLog);
                                _db.SaveChanges();

                                countcreatemaintain++;
                            }
                            else
                            {
                                TempData["ErrorMaintain"] = "Error";
                                TempData["ErrorTextMaintain"] = "LogError";
                                return RedirectToAction("Maintain");
                            }
                        }

                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "ErrorCreateMaintainExcel";
                        TempData["ErrorCreateExcelCountMaintain"] = listmaintain.Count();
                        TempData["ErrorCreateExcelSystemCountMaintain"] = countcreatemaintain;
                    }
                }
                else
                {
                    var checkdata = _db.Department1s.Where(n => n.Name == data.Department1.Name && n.LocationId == data.Department1.LocationId && n.Active == true);

                    if (checkdata.Any())
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "CreateError";
                        return RedirectToAction("Maintain");
                    }
                    else
                    {
                        data.Department1.Active = true;
                        _db.Department1s.Add(data.Department1);
                        var result = _db.SaveChanges();

                        var Department1Database = _db.Department1s.Where(a => a.Name == data.Department1.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && Department1Database != null)
                        {
                            Log_Department1 SaveLog = new()
                            {
                                Department1Id = Department1Database.Id,
                                LocationId = Department1Database.LocationId,
                                Name = Department1Database.Name,
                                Active = Department1Database.Active,
                                Action = "Create",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                            };
                            _db.Log_Department1s.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }

                        return RedirectToAction("Maintain");
                    }
                }
            }
            else if (MaintainData == "Department2")
            {
                if (FileCreateMaintain != null)
                {
                    var listmaintain = new List<Department2>();
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;

                    using (var stream = new MemoryStream())
                    {
                        await FileCreateMaintain.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowcount = worksheet.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                listmaintain.Add(new Department2
                                {
                                    Department1Id = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                                    Name = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                });
                            }
                        }
                    }

                    foreach (var item in listmaintain)
                    {
                        var checkdata = _db.Department2s.Where(n => n.Name == item.Name && n.Department1Id == item.Department1Id && n.Active == true);

                        if (checkdata.Any())
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "CreateError";
                            return RedirectToAction("Maintain");
                        }
                    }

                    if (listmaintain.Count > 0)
                    {
                        var countcreatemaintain = 0;

                        foreach (var itemlistmaintain in listmaintain)
                        {
                            Department2 department2 = new()
                            {
                                Department1Id = itemlistmaintain.Department1Id,
                                Name = itemlistmaintain.Name,
                                Active = true
                            };

                            _db.Department2s.Add(department2);
                            var result = _db.SaveChanges();

                            var Department2Database = _db.Department2s.Where(a => a.Name == department2.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                            if (result > 0 && Department2Database != null)
                            {
                                Log_Department2 SaveLog = new()
                                {
                                    Department2Id = Department2Database.Id,
                                    Department1Id = Department2Database.Department1Id,
                                    Name = Department2Database.Name,
                                    Active = Department2Database.Active,
                                    Action = "Create",
                                    ActionBy = UserId.Id,
                                    ActionByFirstName = UserId.FirstNameTH,
                                    ActionByLastName = UserId.LastNameTH,
                                    ActionOn = DateTime.Now,
                                };
                                _db.Log_Department2s.Add(SaveLog);
                                _db.SaveChanges();

                                countcreatemaintain++;
                            }
                            else
                            {
                                TempData["ErrorMaintain"] = "Error";
                                TempData["ErrorTextMaintain"] = "LogError";
                                return RedirectToAction("Maintain");
                            }
                        }

                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "ErrorCreateMaintainExcel";
                        TempData["ErrorCreateExcelCountMaintain"] = listmaintain.Count();
                        TempData["ErrorCreateExcelSystemCountMaintain"] = countcreatemaintain;
                    }
                }
                else
                {
                    var checkdata = _db.Department2s.Where(n => n.Name == data.Department2.Name && n.Department1Id == data.Department2.Department1Id && n.Active == true);

                    if (checkdata.Any())
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "CreateError";
                        return RedirectToAction("Maintain");
                    }
                    else
                    {
                        data.Department2.Active = true;
                        _db.Department2s.Add(data.Department2);
                        var result = _db.SaveChanges();

                        var Department2Database = _db.Department2s.Where(a => a.Name == data.Department2.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && Department2Database != null)
                        {
                            Log_Department2 SaveLog = new()
                            {
                                Department2Id = Department2Database.Id,
                                Department1Id = Department2Database.Department1Id,
                                Name = Department2Database.Name,
                                Active = Department2Database.Active,
                                Action = "Create",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                            };
                            _db.Log_Department2s.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }

                        return RedirectToAction("Maintain");
                    }
                }
            }
            else if (MaintainData == "Section")
            {
                if (FileCreateMaintain != null)
                {
                    var listmaintain = new List<Section>();
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;

                    using (var stream = new MemoryStream())
                    {
                        await FileCreateMaintain.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowcount = worksheet.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                int? Department2IdValue = 0;

                                if (worksheet.Cells[row, 2].Value?.ToString()?.Trim() != null)
                                {
                                    Department2IdValue = Convert.ToInt32(worksheet.Cells[row, 2].Value);
                                }
                                else
                                {
                                    Department2IdValue = null;
                                }

                                listmaintain.Add(new Section
                                {
                                    Department1Id = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                                    Department2Id = Department2IdValue,
                                    Name = worksheet.Cells[row, 3].Value.ToString().Trim(),
                                });
                            }
                        }
                    }

                    foreach (var item in listmaintain)
                    {
                        var checkdata = _db.Sections.Where(n => n.Name == item.Name && n.Department1Id == item.Department1Id && n.Department2Id == item.Department2Id && n.Active == true);

                        if (checkdata.Any())
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "CreateError";
                            return RedirectToAction("Maintain");
                        }
                    }

                    if (listmaintain.Count > 0)
                    {
                        var countcreatemaintain = 0;

                        foreach (var itemlistmaintain in listmaintain)
                        {
                            Section section = new()
                            {
                                Department1Id = itemlistmaintain.Department1Id,
                                Department2Id = itemlistmaintain.Department2Id,
                                Name = itemlistmaintain.Name,
                                Active = true
                            };

                            _db.Sections.Add(section);
                            var result = _db.SaveChanges();

                            var SectionDatabase = _db.Sections.Where(a => a.Name == section.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                            if (result > 0 && SectionDatabase != null)
                            {
                                Log_Section SaveLog = new()
                                {
                                    SectionId = SectionDatabase.Id,
                                    Department1Id = SectionDatabase.Department1Id,
                                    Department2Id = SectionDatabase.Department2Id,
                                    Name = SectionDatabase.Name,
                                    Active = SectionDatabase.Active,
                                    Action = "Create",
                                    ActionBy = UserId.Id,
                                    ActionByFirstName = UserId.FirstNameTH,
                                    ActionByLastName = UserId.LastNameTH,
                                    ActionOn = DateTime.Now,
                                };
                                _db.Log_Sections.Add(SaveLog);
                                _db.SaveChanges();

                                countcreatemaintain++;
                            }
                            else
                            {
                                TempData["ErrorMaintain"] = "Error";
                                TempData["ErrorTextMaintain"] = "LogError";
                                return RedirectToAction("Maintain");
                            }
                        }

                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "ErrorCreateMaintainExcel";
                        TempData["ErrorCreateExcelCountMaintain"] = listmaintain.Count();
                        TempData["ErrorCreateExcelSystemCountMaintain"] = countcreatemaintain;
                    }
                }
                else
                {
                    var checkdata = _db.Sections.Where(n => n.Name == data.Section.Name && n.Department1Id == data.Section.Department1Id && n.Department2Id == data.Section.Department2Id && n.Active == true);

                    if (checkdata.Any())
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "CreateError";
                        return RedirectToAction("Maintain");
                    }
                    else
                    {
                        data.Section.Active = true;
                        _db.Sections.Add(data.Section);
                        var result = _db.SaveChanges();

                        var SectionDatabase = _db.Sections.Where(a => a.Name == data.Section.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && SectionDatabase != null)
                        {
                            Log_Section SaveLog = new()
                            {
                                SectionId = SectionDatabase.Id,
                                Department1Id = SectionDatabase.Department1Id,
                                Department2Id = SectionDatabase.Department2Id,
                                Name = SectionDatabase.Name,
                                Active = SectionDatabase.Active,
                                Action = "Create",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                            };
                            _db.Log_Sections.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }

                        return RedirectToAction("Maintain");
                    }
                }
            }
            else if (MaintainData == "Shop")
            {
                if (FileCreateMaintain != null)
                {
                    var listmaintain = new List<Shop>();
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;

                    using (var stream = new MemoryStream())
                    {
                        await FileCreateMaintain.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowcount = worksheet.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                listmaintain.Add(new Shop
                                {
                                    SectionId = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                                    ShopGroupId = Convert.ToInt32(worksheet.Cells[row, 2].Value),
                                    ShopPositionGroupId = Convert.ToInt32(worksheet.Cells[row, 3].Value),
                                    Branch = worksheet.Cells[row, 4].Value.ToString().Trim(),
                                    Name = worksheet.Cells[row, 5].Value.ToString().Trim(),
                                });
                            }
                        }
                    }

                    foreach (var item in listmaintain)
                    {
                        var checkdata = _db.Shops.Where(n => n.Name == item.Name && n.Branch == item.Branch && n.Active == true);

                        if (checkdata.Any())
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "CreateError";
                            return RedirectToAction("Maintain");
                        }
                    }

                    if (listmaintain.Count > 0)
                    {
                        var countcreatemaintain = 0;

                        foreach (var itemlistmaintain in listmaintain)
                        {
                            Shop shop = new()
                            {
                                SectionId = itemlistmaintain.SectionId,
                                ShopGroupId = itemlistmaintain.ShopGroupId,
                                ShopPositionGroupId = itemlistmaintain.ShopPositionGroupId,
                                Branch = itemlistmaintain.Branch,
                                Name = itemlistmaintain.Name,
                                Active = true
                            };

                            _db.Shops.Add(shop);
                            var result = _db.SaveChanges();

                            var ShopDatabase = _db.Shops.Where(a => a.Name == shop.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                            if (result > 0 && ShopDatabase != null)
                            {
                                Log_Shop SaveLog = new()
                                {
                                    ShopId = ShopDatabase.Id,
                                    Branch = ShopDatabase.Branch,
                                    ShopGroupId = ShopDatabase.ShopGroupId,
                                    SectionId = ShopDatabase.SectionId,
                                    ShopPositionGroupId = ShopDatabase.ShopPositionGroupId,
                                    Name = ShopDatabase.Name,
                                    Active = ShopDatabase.Active,
                                    Action = "Create",
                                    ActionBy = UserId.Id,
                                    ActionByFirstName = UserId.FirstNameTH,
                                    ActionByLastName = UserId.LastNameTH,
                                    ActionOn = DateTime.Now,
                                };
                                _db.Log_Shops.Add(SaveLog);
                                _db.SaveChanges();

                                countcreatemaintain++;
                            }
                            else
                            {
                                TempData["ErrorMaintain"] = "Error";
                                TempData["ErrorTextMaintain"] = "LogError";
                                return RedirectToAction("Maintain");
                            }
                        }

                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "ErrorCreateMaintainExcel";
                        TempData["ErrorCreateExcelCountMaintain"] = listmaintain.Count();
                        TempData["ErrorCreateExcelSystemCountMaintain"] = countcreatemaintain;
                    }
                }
                else
                {
                    var checkdata = _db.Shops.Where(n => n.Name == data.Shop.Name && n.Branch == data.Shop.Branch && n.Active == true);

                    if (checkdata.Any())
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "CreateError";
                        return RedirectToAction("Maintain");
                    }
                    else
                    {
                        data.Shop.Active = true;
                        _db.Shops.Add(data.Shop);
                        var result = _db.SaveChanges();

                        var ShopDatabase = _db.Shops.Where(a => a.Name == data.Shop.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && ShopDatabase != null)
                        {
                            Log_Shop SaveLog = new()
                            {
                                ShopId = ShopDatabase.Id,
                                Branch = ShopDatabase.Branch,
                                ShopGroupId = ShopDatabase.ShopGroupId,
                                SectionId = ShopDatabase.SectionId,
                                ShopPositionGroupId = ShopDatabase.ShopPositionGroupId,
                                Name = ShopDatabase.Name,
                                Active = ShopDatabase.Active,
                                Action = "Create",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                            };
                            _db.Log_Shops.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }

                        return RedirectToAction("Maintain");
                    }
                }
            }
            else if (MaintainData == "ShopGroup")
            {
                if (FileCreateMaintain != null)
                {
                    var listmaintain = new List<ShopGroup>();
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;

                    using (var stream = new MemoryStream())
                    {
                        await FileCreateMaintain.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowcount = worksheet.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                listmaintain.Add(new ShopGroup
                                {
                                    Name = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                });
                            }
                        }
                    }

                    foreach (var item in listmaintain)
                    {
                        var checkdata = _db.ShopGroups.Where(n => n.Name == item.Name && n.Active == true);

                        if (checkdata.Any())
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "CreateError";
                            return RedirectToAction("Maintain");
                        }
                    }

                    if (listmaintain.Count > 0)
                    {
                        var countcreatemaintain = 0;

                        foreach (var itemlistmaintain in listmaintain)
                        {
                            ShopGroup shopGroup = new()
                            {
                                Name = itemlistmaintain.Name,
                                Active = true
                            };

                            _db.ShopGroups.Add(shopGroup);
                            var result = _db.SaveChanges();

                            var ShopGroupDatabase = _db.ShopGroups.Where(a => a.Name == shopGroup.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                            if (result > 0 && ShopGroupDatabase != null)
                            {
                                Log_ShopGroup SaveLog = new()
                                {
                                    ShopGroupId = ShopGroupDatabase.Id,
                                    Name = ShopGroupDatabase.Name,
                                    Active = ShopGroupDatabase.Active,
                                    Action = "Create",
                                    ActionBy = UserId.Id,
                                    ActionByFirstName = UserId.FirstNameTH,
                                    ActionByLastName = UserId.LastNameTH,
                                    ActionOn = DateTime.Now,
                                };
                                _db.log_ShopGroups.Add(SaveLog);
                                _db.SaveChanges();

                                countcreatemaintain++;
                            }
                            else
                            {
                                TempData["ErrorMaintain"] = "Error";
                                TempData["ErrorTextMaintain"] = "LogError";
                                return RedirectToAction("Maintain");
                            }
                        }

                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "ErrorCreateMaintainExcel";
                        TempData["ErrorCreateExcelCountMaintain"] = listmaintain.Count();
                        TempData["ErrorCreateExcelSystemCountMaintain"] = countcreatemaintain;
                    }
                }
                else
                {
                    var checkdata = _db.ShopGroups.Where(n => n.Name == data.ShopGroup.Name && n.Active == true);

                    if (checkdata.Any())
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "CreateError";
                        return RedirectToAction("Maintain");
                    }
                    else
                    {
                        data.ShopGroup.Active = true;
                        _db.ShopGroups.Add(data.ShopGroup);
                        var result = _db.SaveChanges();

                        var ShopGroupDatabase = _db.ShopGroups.Where(a => a.Name == data.ShopGroup.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && ShopGroupDatabase != null)
                        {
                            Log_ShopGroup SaveLog = new()
                            {
                                ShopGroupId = ShopGroupDatabase.Id,
                                Name = ShopGroupDatabase.Name,
                                Active = ShopGroupDatabase.Active,
                                Action = "Create",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                            };
                            _db.log_ShopGroups.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }

                        return RedirectToAction("Maintain");
                    }
                }
            }
            else if (MaintainData == "ShopPositionGroup")
            {
                if (FileCreateMaintain != null)
                {
                    var listmaintain = new List<ShopPositionGroup>();
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;

                    using (var stream = new MemoryStream())
                    {
                        await FileCreateMaintain.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowcount = worksheet.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                listmaintain.Add(new ShopPositionGroup
                                {
                                    Name = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                });
                            }
                        }
                    }

                    foreach (var item in listmaintain)
                    {
                        var checkdata = _db.ShopPositionGroups.Where(n => n.Name == item.Name && n.Active == true);

                        if (checkdata.Any())
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "CreateError";
                            return RedirectToAction("Maintain");
                        }
                    }

                    if (listmaintain.Count > 0)
                    {
                        var countcreatemaintain = 0;

                        foreach (var itemlistmaintain in listmaintain)
                        {
                            ShopPositionGroup shopPositionGroup = new()
                            {
                                Name = itemlistmaintain.Name,
                                Active = true
                            };

                            _db.ShopPositionGroups.Add(shopPositionGroup);
                            var result = _db.SaveChanges();

                            var ShopPositionGroupDatabase = _db.ShopPositionGroups.Where(a => a.Name == shopPositionGroup.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                            if (result > 0 && ShopPositionGroupDatabase != null)
                            {
                                Log_ShopPositionGroup SaveLog = new()
                                {
                                    ShopPositionGroupId = ShopPositionGroupDatabase.Id,
                                    Name = ShopPositionGroupDatabase.Name,
                                    Active = ShopPositionGroupDatabase.Active,
                                    Action = "Create",
                                    ActionBy = UserId.Id,
                                    ActionByFirstName = UserId.FirstNameTH,
                                    ActionByLastName = UserId.LastNameTH,
                                    ActionOn = DateTime.Now,
                                };
                                _db.Log_ShopPositionGroups.Add(SaveLog);
                                _db.SaveChanges();

                                countcreatemaintain++;
                            }
                            else
                            {
                                TempData["ErrorMaintain"] = "Error";
                                TempData["ErrorTextMaintain"] = "LogError";
                                return RedirectToAction("Maintain");
                            }
                        }

                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "ErrorCreateMaintainExcel";
                        TempData["ErrorCreateExcelCountMaintain"] = listmaintain.Count();
                        TempData["ErrorCreateExcelSystemCountMaintain"] = countcreatemaintain;
                    }
                }
                else
                {
                    var checkdata = _db.ShopPositionGroups.Where(n => n.Name == data.ShopPositionGroup.Name && n.Active == true);

                    if (checkdata.Any())
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "CreateError";
                        return RedirectToAction("Maintain");
                    }
                    else
                    {
                        data.ShopPositionGroup.Active = true;
                        _db.ShopPositionGroups.Add(data.ShopPositionGroup);
                        var result = _db.SaveChanges();

                        var ShopPositionGroupDatabase = _db.ShopPositionGroups.Where(a => a.Name == data.ShopPositionGroup.Name).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && ShopPositionGroupDatabase != null)
                        {
                            Log_ShopPositionGroup SaveLog = new()
                            {
                                ShopPositionGroupId = ShopPositionGroupDatabase.Id,
                                Name = ShopPositionGroupDatabase.Name,
                                Active = ShopPositionGroupDatabase.Active,
                                Action = "Create",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                            };
                            _db.Log_ShopPositionGroups.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }

                        return RedirectToAction("Maintain");
                    }
                }
            }
            else if (MaintainData == "Position")
            {
                if (FileCreateMaintain != null)
                {
                    var listmaintain = new List<Position>();
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;

                    using (var stream = new MemoryStream())
                    {
                        await FileCreateMaintain.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowcount = worksheet.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                int? ShopPositionGroupIdValue = 0;

                                if (worksheet.Cells[row, 2].Value?.ToString()?.Trim() != null)
                                {
                                    ShopPositionGroupIdValue = Convert.ToInt32(worksheet.Cells[row, 2].Value);
                                }
                                else
                                {
                                    ShopPositionGroupIdValue = null;
                                }

                                listmaintain.Add(new Position
                                {
                                    SectionId = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                                    ShopPositionGroupId = ShopPositionGroupIdValue,
                                    Name = worksheet.Cells[row, 3].Value.ToString().Trim(),
                                });
                            }
                        }
                    }

                    foreach (var item in listmaintain)
                    {
                        var checkdata = _db.Positions.Where(n => n.Name == item.Name && n.SectionId == item.SectionId && n.Active == true);

                        if (checkdata.Any())
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "CreateError";
                            return RedirectToAction("Maintain");
                        }
                    }

                    if (listmaintain.Count > 0)
                    {
                        var countcreatemaintain = 0;

                        foreach (var itemlistmaintain in listmaintain)
                        {
                            Position position = new()
                            {
                                SectionId = itemlistmaintain.SectionId,
                                ShopPositionGroupId = itemlistmaintain.ShopPositionGroupId,
                                Name = itemlistmaintain.Name,
                                Active = true
                            };

                            _db.Positions.Add(position);
                            var result = _db.SaveChanges();

                            var PositionDatabase = _db.Positions.Where(a => a.Name == position.Name && a.SectionId == position.SectionId).OrderByDescending(o => o.Id).FirstOrDefault();

                            if (result > 0 && PositionDatabase != null)
                            {
                                Log_Position SaveLog = new()
                                {
                                    PositionId = PositionDatabase.Id,
                                    SectionId = PositionDatabase.SectionId,
                                    ShopPositionGroupId = PositionDatabase.ShopPositionGroupId,
                                    Name = PositionDatabase.Name,
                                    Active = PositionDatabase.Active,
                                    Action = "Create",
                                    ActionBy = UserId.Id,
                                    ActionByFirstName = UserId.FirstNameTH,
                                    ActionByLastName = UserId.LastNameTH,
                                    ActionOn = DateTime.Now,
                                };
                                _db.Log_Positions.Add(SaveLog);
                                _db.SaveChanges();

                                countcreatemaintain++;
                            }
                            else
                            {
                                TempData["ErrorMaintain"] = "Error";
                                TempData["ErrorTextMaintain"] = "LogError";
                                return RedirectToAction("Maintain");
                            }
                        }

                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "ErrorCreateMaintainExcel";
                        TempData["ErrorCreateExcelCountMaintain"] = listmaintain.Count();
                        TempData["ErrorCreateExcelSystemCountMaintain"] = countcreatemaintain;
                    }
                }
                else
                {
                    var checkdata = _db.Positions.Where(n => n.Name == data.Position.Name && n.SectionId == data.Position.SectionId && n.Active == true);

                    if (checkdata.Any())
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "CreateError";
                        return RedirectToAction("Maintain");
                    }
                    else
                    {
                        data.Position.Active = true;
                        _db.Positions.Add(data.Position);
                        var result = _db.SaveChanges();

                        var PositionDatabase = _db.Positions.Where(a => a.Name == data.Position.Name && a.SectionId == data.Position.SectionId).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && PositionDatabase != null)
                        {
                            Log_Position SaveLog = new()
                            {
                                PositionId = PositionDatabase.Id,
                                SectionId = PositionDatabase.SectionId,
                                ShopPositionGroupId = PositionDatabase.ShopPositionGroupId,
                                Name = PositionDatabase.Name,
                                Active = PositionDatabase.Active,
                                Action = "Create",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                            };
                            _db.Log_Positions.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }

                        return RedirectToAction("Maintain");
                    }
                }
            }
            else
            {

            }
            return RedirectToAction("Maintain");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult EditMaintain(MaintainViewModel data, int MaintainId, string MaintainData)
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;

            if (MaintainData == "MenuGroup")
            {
                var checkdata = _db.MenuGroups.Where(n => n.Name == data.MenuGroup.Name && n.Id != MaintainId && n.Active == true);

                if (checkdata.Any())
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "EditError";
                    return RedirectToAction("Maintain");
                }
                else
                {
                    var EditMenuGroup = _db.MenuGroups.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                    if (EditMenuGroup != null)
                    {
                        EditMenuGroup.Name = data.MenuGroup.Name;
                        _db.MenuGroups.Update(EditMenuGroup);
                        var result = _db.SaveChanges();

                        var EditMenuGroupDatabase = _db.MenuGroups.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && EditMenuGroupDatabase != null)
                        {
                            Log_MenuGroup SaveLog = new()
                            {
                                MenuGroupId = EditMenuGroupDatabase.Id,
                                Name = EditMenuGroupDatabase.Name,
                                Active = EditMenuGroupDatabase.Active,
                                Action = "Edit",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                                Remark = data.Remark
                            };
                            _db.Log_MenuGroups.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "EditFail";
                        return RedirectToAction("Maintain");
                    }
                }
            }
            else if (MaintainData == "UserMenuGroup")
            {
                var checkdata = _db.UserMenuGroups.Where(n => n.MenuGroupId == data.UserMenuGroup.MenuGroupId && n.UserId == data.UserMenuGroup.UserId && n.Id != MaintainId && n.Active == true);

                if (checkdata.Any())
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "EditError";
                    return RedirectToAction("Maintain");
                }
                else
                {
                    var EditUserMenuGroup = _db.UserMenuGroups.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                    if (EditUserMenuGroup != null)
                    {
                        EditUserMenuGroup.MenuGroupId = data.UserMenuGroup.MenuGroupId;
                        EditUserMenuGroup.UserId = data.UserMenuGroup.UserId;
                        _db.UserMenuGroups.Update(EditUserMenuGroup);
                        var result = _db.SaveChanges();

                        var EditUserMenuGroupDatabase = _db.UserMenuGroups.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && EditUserMenuGroupDatabase != null)
                        {
                            Log_UserMenuGroup SaveLog = new()
                            {
                                UserMenuGroupId = EditUserMenuGroupDatabase.Id,
                                MenuGroupId = EditUserMenuGroupDatabase.MenuGroupId,
                                UserId = EditUserMenuGroupDatabase.UserId,
                                Active = EditUserMenuGroupDatabase.Active,
                                Action = "Edit",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                                Remark = data.Remark
                            };
                            _db.Log_UserMenuGroups.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "EditFail";
                        return RedirectToAction("Maintain");
                    }
                }
            }
            else if (MaintainData == "CategoryComplaint")
            {
                var checkdata = _db.CategoryComplaints.Where(n => n.Name == data.CategoryComplaint.Name && n.Id != MaintainId && n.Active == true);


                if (checkdata.Any())
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "EditError";
                    return RedirectToAction("Maintain");
                }
                else
                {
                    var EditCategoryComplaint = _db.CategoryComplaints.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                    if (EditCategoryComplaint != null)
                    {
                        EditCategoryComplaint.Name = data.CategoryComplaint.Name;
                        EditCategoryComplaint.Email = data.CategoryComplaint.Email;
                        _db.CategoryComplaints.Update(EditCategoryComplaint);
                        var result = _db.SaveChanges();

                        var EditCategoryComplaintDatabase = _db.CategoryComplaints.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && EditCategoryComplaintDatabase != null)
                        {
                            Log_CategoryComplaint SaveLog = new()
                            {
                                CategoryComplaintId = EditCategoryComplaintDatabase.Id,
                                Name = EditCategoryComplaintDatabase.Name,
                                Email = EditCategoryComplaintDatabase.Email,
                                Active = EditCategoryComplaintDatabase.Active,
                                Action = "Edit",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                                Remark = data.Remark
                            };
                            _db.Log_CategoryComplaints.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "EditFail";
                        return RedirectToAction("Maintain");
                    }

                }
            }
            else if (MaintainData == "ComplaintFrom")
            {
                var checkdata = _db.ComplaintFroms.Where(n => n.Email == data.ComplaintFrom.Email && n.Id != MaintainId && n.Active == true);


                if (checkdata.Any())
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "EditError";
                    return RedirectToAction("Maintain");
                }
                else
                {
                    var EditComplaintFrom = _db.ComplaintFroms.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                    if (EditComplaintFrom != null)
                    {
                        EditComplaintFrom.Email = data.ComplaintFrom.Email;
                        EditComplaintFrom.Password = data.ComplaintFrom.Password;
                        _db.ComplaintFroms.Update(EditComplaintFrom);
                        var result = _db.SaveChanges();

                        var EditComplaintFromDatabase = _db.ComplaintFroms.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && EditComplaintFromDatabase != null)
                        {
                            Log_ComplaintFrom SaveLog = new()
                            {
                                ComplaintFromId = EditComplaintFromDatabase.Id,
                                Email = EditComplaintFromDatabase.Email,
                                Password = EditComplaintFromDatabase.Password,
                                Active = EditComplaintFromDatabase.Active,
                                Action = "Edit",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                                Remark = data.Remark
                            };
                            _db.log_ComplaintFroms.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "EditFail";
                        return RedirectToAction("Maintain");
                    }

                }
            }
            else if (MaintainData == "LocationGroup")
            {
                var checkdata = _db.LocationGroups.Where(n => n.Name == data.LocationGroup.Name && n.Id != MaintainId && n.Active == true);


                if (checkdata.Any())
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "EditError";
                    return RedirectToAction("Maintain");
                }
                else
                {
                    var EditLocationGroup = _db.LocationGroups.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                    if (EditLocationGroup != null)
                    {
                        EditLocationGroup.Name = data.LocationGroup.Name;
                        _db.LocationGroups.Update(EditLocationGroup);
                        var result = _db.SaveChanges();

                        var EditLocationGroupDatabase = _db.LocationGroups.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && EditLocationGroupDatabase != null)
                        {
                            Log_LocationGroup SaveLog = new()
                            {
                                LocationGroupId = EditLocationGroupDatabase.Id,
                                Name = EditLocationGroupDatabase.Name,
                                Active = EditLocationGroupDatabase.Active,
                                Action = "Edit",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                                Remark = data.Remark
                            };
                            _db.Log_LocationGroups.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "EditFail";
                        return RedirectToAction("Maintain");
                    }

                }
            }
            else if (MaintainData == "Location")
            {
                var checkdata = _db.Locations.Where(n => n.Name == data.Location.Name && n.Id != MaintainId && n.Active == true);


                if (checkdata.Any())
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "EditError";
                    return RedirectToAction("Maintain");
                }
                else
                {
                    var EditLocation = _db.Locations.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                    if (EditLocation != null)
                    {
                        EditLocation.Name = data.Location.Name;
                        EditLocation.LocationGroupId = data.Location.LocationGroupId;
                        _db.Locations.Update(EditLocation);
                        var result = _db.SaveChanges();

                        var EditLocationDatabase = _db.Locations.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && EditLocationDatabase != null)
                        {
                            Log_Location SaveLog = new()
                            {
                                LocationId = EditLocationDatabase.Id,
                                LocationGroupId = EditLocationDatabase.LocationGroupId,
                                Name = EditLocationDatabase.Name,
                                Active = EditLocationDatabase.Active,
                                Action = "Edit",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                                Remark = data.Remark
                            };
                            _db.Log_Locations.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "EditFail";
                        return RedirectToAction("Maintain");
                    }

                }
            }
            else if (MaintainData == "Department1")
            {
                var checkdata = _db.Department1s.Where(n => n.Name == data.Department1.Name && n.LocationId == data.Department1.LocationId && n.Id != MaintainId && n.Active == true);


                if (checkdata.Any())
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "EditError";
                    return RedirectToAction("Maintain");
                }
                else
                {
                    var EditDepartment1 = _db.Department1s.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                    if (EditDepartment1 != null)
                    {
                        EditDepartment1.Name = data.Department1.Name;
                        EditDepartment1.LocationId = data.Department1.LocationId;
                        _db.Department1s.Update(EditDepartment1);
                        var result = _db.SaveChanges();

                        var EditDepartment1Database = _db.Department1s.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && EditDepartment1Database != null)
                        {
                            Log_Department1 SaveLog = new()
                            {
                                Department1Id = EditDepartment1Database.Id,
                                LocationId = EditDepartment1Database.LocationId,
                                Name = EditDepartment1Database.Name,
                                Active = EditDepartment1Database.Active,
                                Action = "Edit",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                                Remark = data.Remark
                            };
                            _db.Log_Department1s.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "EditFail";
                        return RedirectToAction("Maintain");
                    }

                }
            }
            else if (MaintainData == "Department2")
            {
                var checkdata = _db.Department2s.Where(n => n.Name == data.Department2.Name && n.Department1Id == data.Department2.Department1Id && n.Id != MaintainId && n.Active == true);


                if (checkdata.Any())
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "EditError";
                    return RedirectToAction("Maintain");
                }
                else
                {
                    var EditDepartment2 = _db.Department2s.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                    if (EditDepartment2 != null)
                    {
                        EditDepartment2.Name = data.Department2.Name;
                        EditDepartment2.Department1Id = data.Department2.Department1Id;
                        _db.Department2s.Update(EditDepartment2);
                        var result = _db.SaveChanges();

                        var EditDepartment2Database = _db.Department2s.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && EditDepartment2Database != null)
                        {
                            Log_Department2 SaveLog = new()
                            {
                                Department2Id = EditDepartment2Database.Id,
                                Department1Id = EditDepartment2Database.Department1Id,
                                Name = EditDepartment2Database.Name,
                                Active = EditDepartment2Database.Active,
                                Action = "Edit",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                                Remark = data.Remark
                            };
                            _db.Log_Department2s.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "EditFail";
                        return RedirectToAction("Maintain");
                    }

                }
            }
            else if (MaintainData == "Section")
            {
                var checkdata = _db.Sections.Where(n => n.Name == data.Section.Name && n.Department1Id == data.Section.Department1Id && n.Department2Id == data.Section.Department2Id && n.Id != MaintainId && n.Active == true);


                if (checkdata.Any())
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "EditError";
                    return RedirectToAction("Maintain");
                }
                else
                {
                    var EditSection = _db.Sections.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                    if (EditSection != null)
                    {
                        EditSection.Name = data.Section.Name;
                        EditSection.Department1Id = data.Section.Department1Id;
                        EditSection.Department2Id = data.Section.Department2Id;
                        _db.Sections.Update(EditSection);
                        var result = _db.SaveChanges();

                        var EditSectionDatabase = _db.Sections.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && EditSectionDatabase != null)
                        {
                            Log_Section SaveLog = new()
                            {
                                SectionId = EditSectionDatabase.Id,
                                Department1Id = EditSectionDatabase.Department1Id,
                                Department2Id = EditSectionDatabase.Department2Id,
                                Name = EditSectionDatabase.Name,
                                Active = EditSectionDatabase.Active,
                                Action = "Edit",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                                Remark = data.Remark
                            };
                            _db.Log_Sections.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "EditFail";
                        return RedirectToAction("Maintain");
                    }

                }
            }
            else if (MaintainData == "Shop")
            {
                var checkdata = _db.Shops.Where(n => n.Name == data.Shop.Name && n.Branch == data.Shop.Branch && n.Id != MaintainId && n.Active == true);


                if (checkdata.Any())
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "EditError";
                    return RedirectToAction("Maintain");
                }
                else
                {
                    var EditShop = _db.Shops.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                    if (EditShop != null)
                    {
                        EditShop.Name = data.Shop.Name;
                        EditShop.SectionId = data.Shop.SectionId;
                        EditShop.ShopGroupId = data.Shop.ShopGroupId;
                        EditShop.ShopPositionGroupId = data.Shop.ShopPositionGroupId;
                        EditShop.Branch = data.Shop.Branch;
                        _db.Shops.Update(EditShop);
                        var result = _db.SaveChanges();

                        var EditShopDatabase = _db.Shops.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && EditShopDatabase != null)
                        {
                            Log_Shop SaveLog = new()
                            {
                                ShopId = EditShopDatabase.Id,
                                Branch = EditShopDatabase.Branch,
                                ShopGroupId = EditShopDatabase.ShopGroupId,
                                SectionId = EditShopDatabase.SectionId,
                                ShopPositionGroupId = EditShopDatabase.ShopPositionGroupId,
                                Name = EditShopDatabase.Name,
                                Active = EditShopDatabase.Active,
                                Action = "Edit",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                                Remark = data.Remark
                            };
                            _db.Log_Shops.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "EditFail";
                        return RedirectToAction("Maintain");
                    }

                }
            }
            else if (MaintainData == "ShopGroup")
            {
                var checkdata = _db.ShopGroups.Where(n => n.Name == data.ShopGroup.Name && n.Id != MaintainId && n.Active == true);


                if (checkdata.Any())
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "EditError";
                    return RedirectToAction("Maintain");
                }
                else
                {
                    var EditShopGroup = _db.ShopGroups.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                    if (EditShopGroup != null)
                    {
                        EditShopGroup.Name = data.ShopGroup.Name;
                        _db.ShopGroups.Update(EditShopGroup);
                        var result = _db.SaveChanges();

                        var EditShopGroupDatabase = _db.ShopGroups.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && EditShopGroupDatabase != null)
                        {
                            Log_ShopGroup SaveLog = new()
                            {
                                ShopGroupId = EditShopGroupDatabase.Id,
                                Name = EditShopGroupDatabase.Name,
                                Active = EditShopGroupDatabase.Active,
                                Action = "Edit",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                                Remark = data.Remark
                            };
                            _db.log_ShopGroups.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "EditFail";
                        return RedirectToAction("Maintain");
                    }

                }
            }
            else if (MaintainData == "ShopPositionGroup")
            {
                var checkdata = _db.ShopPositionGroups.Where(n => n.Name == data.ShopPositionGroup.Name && n.Id != MaintainId && n.Active == true);


                if (checkdata.Any())
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "EditError";
                    return RedirectToAction("Maintain");
                }
                else
                {
                    var EditShopPositionGroup = _db.ShopPositionGroups.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                    if (EditShopPositionGroup != null)
                    {
                        EditShopPositionGroup.Name = data.ShopPositionGroup.Name;
                        _db.ShopPositionGroups.Update(EditShopPositionGroup);
                        var result = _db.SaveChanges();

                        var EditShopPositionGroupDatabase = _db.ShopPositionGroups.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && EditShopPositionGroupDatabase != null)
                        {
                            Log_ShopPositionGroup SaveLog = new()
                            {
                                ShopPositionGroupId = EditShopPositionGroupDatabase.Id,
                                Name = EditShopPositionGroupDatabase.Name,
                                Active = EditShopPositionGroupDatabase.Active,
                                Action = "Edit",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                                Remark = data.Remark
                            };
                            _db.Log_ShopPositionGroups.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "EditFail";
                        return RedirectToAction("Maintain");
                    }

                }
            }
            else if (MaintainData == "Position")
            {
                var checkdata = _db.Positions.Where(n => n.Name == data.Position.Name && n.SectionId == data.Position.SectionId && n.Id != MaintainId && n.Active == true);


                if (checkdata.Any())
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "EditError";
                    return RedirectToAction("Maintain");
                }
                else
                {
                    var EditPosition = _db.Positions.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                    if (EditPosition != null)
                    {
                        EditPosition.Name = data.Position.Name;
                        EditPosition.SectionId = data.Position.SectionId;
                        EditPosition.ShopPositionGroupId = data.Position.ShopPositionGroupId;
                        _db.Positions.Update(EditPosition);
                        var result = _db.SaveChanges();

                        var EditPositionDatabase = _db.Positions.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                        if (result > 0 && EditPositionDatabase != null)
                        {
                            Log_Position SaveLog = new()
                            {
                                PositionId = EditPositionDatabase.Id,
                                SectionId = EditPositionDatabase.SectionId,
                                ShopPositionGroupId = EditPositionDatabase.ShopPositionGroupId,
                                Name = EditPositionDatabase.Name,
                                Active = EditPositionDatabase.Active,
                                Action = "Edit",
                                ActionBy = UserId.Id,
                                ActionByFirstName = UserId.FirstNameTH,
                                ActionByLastName = UserId.LastNameTH,
                                ActionOn = DateTime.Now,
                                Remark = data.Remark
                            };
                            _db.Log_Positions.Add(SaveLog);
                            _db.SaveChanges();
                        }
                        else
                        {
                            TempData["ErrorMaintain"] = "Error";
                            TempData["ErrorTextMaintain"] = "LogError";
                            return RedirectToAction("Maintain");
                        }
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "EditFail";
                        return RedirectToAction("Maintain");
                    }

                }
            }
            else
            {

            }
            return RedirectToAction("Maintain");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteMaintain(MaintainViewModel data, int MaintainId, string MaintainData)
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;

            if (MaintainData == "MenuGroup")
            {
                var DeleteMenuGroup = _db.MenuGroups.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                if (DeleteMenuGroup != null)
                {
                    DeleteMenuGroup.Active = false;
                    _db.MenuGroups.Update(DeleteMenuGroup);
                    var result = _db.SaveChanges();

                    var DeleteMenuGroupDatabase = _db.MenuGroups.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                    if (result > 0 && DeleteMenuGroupDatabase != null)
                    {
                        Log_MenuGroup SaveLog = new()
                        {
                            MenuGroupId = DeleteMenuGroupDatabase.Id,
                            Name = DeleteMenuGroupDatabase.Name,
                            Active = DeleteMenuGroupDatabase.Active,
                            Action = "Delete",
                            ActionBy = UserId.Id,
                            ActionByFirstName = UserId.FirstNameTH,
                            ActionByLastName = UserId.LastNameTH,
                            ActionOn = DateTime.Now,
                            Remark = data.Remark
                        };
                        _db.Log_MenuGroups.Add(SaveLog);
                        _db.SaveChanges();
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "LogError";
                        return RedirectToAction("Maintain");
                    }
                }
                else
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "DeleteError";
                    return RedirectToAction("Maintain");
                }
            }
            else if (MaintainData == "UserMenuGroup")
            {
                var DeleteUserMenuGroup = _db.UserMenuGroups.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                if (DeleteUserMenuGroup != null)
                {
                    DeleteUserMenuGroup.Active = false;
                    _db.UserMenuGroups.Update(DeleteUserMenuGroup);
                    var result = _db.SaveChanges();

                    var DeleteUserMenuGroupDatabase = _db.UserMenuGroups.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                    if (result > 0 && DeleteUserMenuGroupDatabase != null)
                    {
                        Log_UserMenuGroup SaveLog = new()
                        {
                            UserMenuGroupId = DeleteUserMenuGroupDatabase.Id,
                            MenuGroupId = DeleteUserMenuGroupDatabase.MenuGroupId,
                            UserId = DeleteUserMenuGroupDatabase.UserId,
                            Active = DeleteUserMenuGroupDatabase.Active,
                            Action = "Delete",
                            ActionBy = UserId.Id,
                            ActionByFirstName = UserId.FirstNameTH,
                            ActionByLastName = UserId.LastNameTH,
                            ActionOn = DateTime.Now,
                            Remark = data.Remark
                        };
                        _db.Log_UserMenuGroups.Add(SaveLog);
                        _db.SaveChanges();
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "LogError";
                        return RedirectToAction("Maintain");
                    }
                }
                else
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "DeleteError";
                    return RedirectToAction("Maintain");
                }
            }
            else if (MaintainData == "CategoryComplaint")
            {
                var DeleteCategoryComplaint = _db.CategoryComplaints.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                if (DeleteCategoryComplaint != null)
                {
                    DeleteCategoryComplaint.Active = false;
                    _db.CategoryComplaints.Update(DeleteCategoryComplaint);
                    var result = _db.SaveChanges();

                    var DeleteCategoryComplaintDatabase = _db.CategoryComplaints.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                    if (result > 0 && DeleteCategoryComplaintDatabase != null)
                    {
                        Log_CategoryComplaint SaveLog = new()
                        {
                            CategoryComplaintId = DeleteCategoryComplaintDatabase.Id,
                            Name = DeleteCategoryComplaintDatabase.Name,
                            Email = DeleteCategoryComplaintDatabase.Email,
                            Active = DeleteCategoryComplaintDatabase.Active,
                            Action = "Delete",
                            ActionBy = UserId.Id,
                            ActionByFirstName = UserId.FirstNameTH,
                            ActionByLastName = UserId.LastNameTH,
                            ActionOn = DateTime.Now,
                            Remark = data.Remark
                        };
                        _db.Log_CategoryComplaints.Add(SaveLog);
                        _db.SaveChanges();
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "LogError";
                        return RedirectToAction("Maintain");
                    }
                }
                else
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "DeleteError";
                    return RedirectToAction("Maintain");
                }
            }
            else if (MaintainData == "ComplaintFrom")
            {
                var DeleteComplaintFrom = _db.ComplaintFroms.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                if (DeleteComplaintFrom != null)
                {
                    DeleteComplaintFrom.Active = false;
                    _db.ComplaintFroms.Update(DeleteComplaintFrom);
                    var result = _db.SaveChanges();

                    var DeleteComplaintFromDatabase = _db.ComplaintFroms.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                    if (result > 0 && DeleteComplaintFromDatabase != null)
                    {
                        Log_ComplaintFrom SaveLog = new()
                        {
                            ComplaintFromId = DeleteComplaintFromDatabase.Id,
                            Email = DeleteComplaintFromDatabase.Email,
                            Password = DeleteComplaintFromDatabase.Password,
                            Active = DeleteComplaintFromDatabase.Active,
                            Action = "Delete",
                            ActionBy = UserId.Id,
                            ActionByFirstName = UserId.FirstNameTH,
                            ActionByLastName = UserId.LastNameTH,
                            ActionOn = DateTime.Now,
                            Remark = data.Remark
                        };
                        _db.log_ComplaintFroms.Add(SaveLog);
                        _db.SaveChanges();
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "LogError";
                        return RedirectToAction("Maintain");
                    }
                }
                else
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "DeleteError";
                    return RedirectToAction("Maintain");
                }
            }
            else if (MaintainData == "LocationGroup")
            {
                var DeleteLocationGroup = _db.LocationGroups.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                if (DeleteLocationGroup != null)
                {
                    DeleteLocationGroup.Active = false;
                    _db.LocationGroups.Update(DeleteLocationGroup);
                    var result = _db.SaveChanges();

                    var DeleteLocationGroupDatabase = _db.LocationGroups.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                    if (result > 0 && DeleteLocationGroupDatabase != null)
                    {
                        Log_LocationGroup SaveLog = new()
                        {
                            LocationGroupId = DeleteLocationGroupDatabase.Id,
                            Name = DeleteLocationGroupDatabase.Name,
                            Active = DeleteLocationGroupDatabase.Active,
                            Action = "Delete",
                            ActionBy = UserId.Id,
                            ActionByFirstName = UserId.FirstNameTH,
                            ActionByLastName = UserId.LastNameTH,
                            ActionOn = DateTime.Now,
                            Remark = data.Remark
                        };
                        _db.Log_LocationGroups.Add(SaveLog);
                        _db.SaveChanges();
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "LogError";
                        return RedirectToAction("Maintain");
                    }
                }
                else
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "DeleteError";
                    return RedirectToAction("Maintain");
                }
            }
            else if (MaintainData == "Location")
            {
                var DeleteLocation = _db.Locations.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                if (DeleteLocation != null)
                {
                    DeleteLocation.Active = false;
                    _db.Locations.Update(DeleteLocation);
                    var result = _db.SaveChanges();

                    var DeleteLocationDatabase = _db.Locations.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                    if (result > 0 && DeleteLocationDatabase != null)
                    {
                        Log_Location SaveLog = new()
                        {
                            LocationId = DeleteLocationDatabase.Id,
                            LocationGroupId = DeleteLocationDatabase.LocationGroupId,
                            Name = DeleteLocationDatabase.Name,
                            Active = DeleteLocationDatabase.Active,
                            Action = "Delete",
                            ActionBy = UserId.Id,
                            ActionByFirstName = UserId.FirstNameTH,
                            ActionByLastName = UserId.LastNameTH,
                            ActionOn = DateTime.Now,
                            Remark = data.Remark
                        };
                        _db.Log_Locations.Add(SaveLog);
                        _db.SaveChanges();
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "LogError";
                        return RedirectToAction("Maintain");
                    }
                }
                else
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "DeleteError";
                    return RedirectToAction("Maintain");
                }
            }
            else if (MaintainData == "Department1")
            {
                var DeleteDepartment1 = _db.Department1s.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                if (DeleteDepartment1 != null)
                {
                    DeleteDepartment1.Active = false;
                    _db.Department1s.Update(DeleteDepartment1);
                    var result = _db.SaveChanges();

                    var DeleteDepartment1Database = _db.Department1s.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                    if (result > 0 && DeleteDepartment1Database != null)
                    {
                        Log_Department1 SaveLog = new()
                        {
                            Department1Id = DeleteDepartment1Database.Id,
                            LocationId = DeleteDepartment1Database.LocationId,
                            Name = DeleteDepartment1Database.Name,
                            Active = DeleteDepartment1Database.Active,
                            Action = "Delete",
                            ActionBy = UserId.Id,
                            ActionByFirstName = UserId.FirstNameTH,
                            ActionByLastName = UserId.LastNameTH,
                            ActionOn = DateTime.Now,
                            Remark = data.Remark
                        };
                        _db.Log_Department1s.Add(SaveLog);
                        _db.SaveChanges();
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "LogError";
                        return RedirectToAction("Maintain");
                    }
                }
                else
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "DeleteError";
                    return RedirectToAction("Maintain");
                }
            }
            else if (MaintainData == "Department2")
            {
                var DeleteDepartment2 = _db.Department2s.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                if (DeleteDepartment2 != null)
                {
                    DeleteDepartment2.Active = false;
                    _db.Department2s.Update(DeleteDepartment2);
                    var result = _db.SaveChanges();

                    var DeleteDepartment2Database = _db.Department2s.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                    if (result > 0 && DeleteDepartment2Database != null)
                    {
                        Log_Department2 SaveLog = new()
                        {
                            Department2Id = DeleteDepartment2Database.Id,
                            Department1Id = DeleteDepartment2Database.Department1Id,
                            Name = DeleteDepartment2Database.Name,
                            Active = DeleteDepartment2Database.Active,
                            Action = "Delete",
                            ActionBy = UserId.Id,
                            ActionByFirstName = UserId.FirstNameTH,
                            ActionByLastName = UserId.LastNameTH,
                            ActionOn = DateTime.Now,
                            Remark = data.Remark
                        };
                        _db.Log_Department2s.Add(SaveLog);
                        _db.SaveChanges();
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "LogError";
                        return RedirectToAction("Maintain");
                    }
                }
                else
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "DeleteError";
                    return RedirectToAction("Maintain");
                }
            }
            else if (MaintainData == "Section")
            {
                var DeleteSection = _db.Sections.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                if (DeleteSection != null)
                {
                    DeleteSection.Active = false;
                    _db.Sections.Update(DeleteSection);
                    var result = _db.SaveChanges();

                    var DeleteSectionDatabase = _db.Sections.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                    if (result > 0 && DeleteSectionDatabase != null)
                    {
                        Log_Section SaveLog = new()
                        {
                            SectionId = DeleteSectionDatabase.Id,
                            Department1Id = DeleteSectionDatabase.Department1Id,
                            Department2Id = DeleteSectionDatabase.Department2Id,
                            Name = DeleteSectionDatabase.Name,
                            Active = DeleteSectionDatabase.Active,
                            Action = "Delete",
                            ActionBy = UserId.Id,
                            ActionByFirstName = UserId.FirstNameTH,
                            ActionByLastName = UserId.LastNameTH,
                            ActionOn = DateTime.Now,
                            Remark = data.Remark
                        };
                        _db.Log_Sections.Add(SaveLog);
                        _db.SaveChanges();
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "LogError";
                        return RedirectToAction("Maintain");
                    }
                }
                else
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "DeleteError";
                    return RedirectToAction("Maintain");
                }
            }
            else if (MaintainData == "Shop")
            {
                var DeleteShop = _db.Shops.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                if (DeleteShop != null)
                {
                    DeleteShop.Active = false;
                    _db.Shops.Update(DeleteShop);
                    var result = _db.SaveChanges();

                    var DeleteShopDatabase = _db.Shops.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                    if (result > 0 && DeleteShopDatabase != null)
                    {
                        Log_Shop SaveLog = new()
                        {
                            ShopId = DeleteShopDatabase.Id,
                            Branch = DeleteShopDatabase.Branch,
                            ShopGroupId = DeleteShopDatabase.ShopGroupId,
                            SectionId = DeleteShopDatabase.SectionId,
                            ShopPositionGroupId = DeleteShopDatabase.ShopPositionGroupId,
                            Name = DeleteShopDatabase.Name,
                            Active = DeleteShopDatabase.Active,
                            Action = "Delete",
                            ActionBy = UserId.Id,
                            ActionByFirstName = UserId.FirstNameTH,
                            ActionByLastName = UserId.LastNameTH,
                            ActionOn = DateTime.Now,
                            Remark = data.Remark
                        };
                        _db.Log_Shops.Add(SaveLog);
                        _db.SaveChanges();
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "LogError";
                        return RedirectToAction("Maintain");
                    }
                }
                else
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "DeleteError";
                    return RedirectToAction("Maintain");
                }
            }
            else if (MaintainData == "ShopGroup")
            {
                var DeleteShopGroup = _db.ShopGroups.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                if (DeleteShopGroup != null)
                {
                    DeleteShopGroup.Active = false;
                    _db.ShopGroups.Update(DeleteShopGroup);
                    var result = _db.SaveChanges();

                    var DeleteShopGroupDatabase = _db.ShopGroups.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                    if (result > 0 && DeleteShopGroupDatabase != null)
                    {
                        Log_ShopGroup SaveLog = new()
                        {
                            ShopGroupId = DeleteShopGroupDatabase.Id,
                            Name = DeleteShopGroupDatabase.Name,
                            Active = DeleteShopGroupDatabase.Active,
                            Action = "Delete",
                            ActionBy = UserId.Id,
                            ActionByFirstName = UserId.FirstNameTH,
                            ActionByLastName = UserId.LastNameTH,
                            ActionOn = DateTime.Now,
                            Remark = data.Remark
                        };
                        _db.log_ShopGroups.Add(SaveLog);
                        _db.SaveChanges();
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "LogError";
                        return RedirectToAction("Maintain");
                    }
                }
                else
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "DeleteError";
                    return RedirectToAction("Maintain");
                }
            }
            else if (MaintainData == "ShopPositionGroup")
            {
                var DeleteShopPositionGroup = _db.ShopPositionGroups.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                if (DeleteShopPositionGroup != null)
                {
                    DeleteShopPositionGroup.Active = false;
                    _db.ShopPositionGroups.Update(DeleteShopPositionGroup);
                    var result = _db.SaveChanges();

                    var DeleteShopPositionGroupDatabase = _db.ShopPositionGroups.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                    if (result > 0 && DeleteShopPositionGroupDatabase != null)
                    {
                        Log_ShopPositionGroup SaveLog = new()
                        {
                            ShopPositionGroupId = DeleteShopPositionGroupDatabase.Id,
                            Name = DeleteShopPositionGroupDatabase.Name,
                            Active = DeleteShopPositionGroupDatabase.Active,
                            Action = "Delete",
                            ActionBy = UserId.Id,
                            ActionByFirstName = UserId.FirstNameTH,
                            ActionByLastName = UserId.LastNameTH,
                            ActionOn = DateTime.Now,
                            Remark = data.Remark
                        };
                        _db.Log_ShopPositionGroups.Add(SaveLog);
                        _db.SaveChanges();
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "LogError";
                        return RedirectToAction("Maintain");
                    }
                }
                else
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "DeleteError";
                    return RedirectToAction("Maintain");
                }
            }
            else if (MaintainData == "Position")
            {
                var DeletePosition = _db.Positions.Where(i => i.Id == MaintainId && i.Active == true).FirstOrDefault();

                if (DeletePosition != null)
                {
                    DeletePosition.Active = false;
                    _db.Positions.Update(DeletePosition);
                    var result = _db.SaveChanges();

                    var DeletePositionDatabase = _db.Positions.Where(a => a.Id == MaintainId).OrderByDescending(o => o.Id).FirstOrDefault();

                    if (result > 0 && DeletePositionDatabase != null)
                    {
                        Log_Position SaveLog = new()
                        {
                            PositionId = DeletePositionDatabase.Id,
                            SectionId = DeletePositionDatabase.SectionId,
                            ShopPositionGroupId = DeletePositionDatabase.ShopPositionGroupId,
                            Name = DeletePositionDatabase.Name,
                            Active = DeletePositionDatabase.Active,
                            Action = "Delete",
                            ActionBy = UserId.Id,
                            ActionByFirstName = UserId.FirstNameTH,
                            ActionByLastName = UserId.LastNameTH,
                            ActionOn = DateTime.Now,
                            Remark = data.Remark
                        };
                        _db.Log_Positions.Add(SaveLog);
                        _db.SaveChanges();
                    }
                    else
                    {
                        TempData["ErrorMaintain"] = "Error";
                        TempData["ErrorTextMaintain"] = "LogError";
                        return RedirectToAction("Maintain");
                    }
                }
                else
                {
                    TempData["ErrorMaintain"] = "Error";
                    TempData["ErrorTextMaintain"] = "DeleteError";
                    return RedirectToAction("Maintain");
                }
            }
            else
            {

            }
            return RedirectToAction("Maintain");
        }
    }
}
