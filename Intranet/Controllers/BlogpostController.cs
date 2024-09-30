using Amazon.Runtime.Internal;
using Amazon.Util.Internal;
using Intranet.Data;
using Intranet.Hubs;
using Intranet.Models;
using Intranet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using NuGet.Packaging.Core;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.Json;
using System.Xml.Linq;

namespace Intranet.Controllers
{
    public class BlogpostController : Controller
    {
        private readonly IHubContext<ConnectionHub> _hubContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        
        public BlogpostController(UserManager<ApplicationUser> userManager, ApplicationDbContext db,IHubContext<ConnectionHub> hubContext)
        {
            _userManager = userManager;
            _db = db;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> MenuGroupId(int id)
        {
            var menugroupid = await _db.MenuGroups.Where(a => a.Id == id && a.Active).ToListAsync();
            await _hubContext.Clients.All.SendAsync("MenuGroupId", menugroupid);
            return Ok();
        }
        public async Task<IActionResult> ListMenuGroupId(string name)
        {
            var listmenugroupid = await _db.MenuGroups.Where(a => a.Name == name && a.Active).ToListAsync();
            await _hubContext.Clients.All.SendAsync("ListMenuGroupId", listmenugroupid);
            return Ok();
        }

        public async Task<IActionResult> UserMenuGroupId(string id)
        {
            var usermenugroupid = await _db.UserMenuGroups.FirstAsync(a => a.UserId == id && a.Active);
            await _hubContext.Clients.All.SendAsync("UserMenuGroupId", usermenugroupid);
            return Ok();
        }

        public async Task<IActionResult> PostId(int id)
        {
            var postid = await _db.Posts.Where(a => a.Id == id && a.Active == true).ToListAsync();
            await _hubContext.Clients.All.SendAsync("PostId", postid);
            return Ok();
        }

        //[HttpGet]
        //[Route("api/[Blogpost]/ListPostId")]
        //public async Task<IActionResult> ListPostId(int id, int skipid)
        //{
        //    IQueryable<Post> query = _db.Posts.Where(a => a.Active == true);

        //    if (id > 0)
        //    {
        //        query = query.Where(a => a.MenuGroupId == id);
        //    }

        //    if (skipid > 0)
        //    {
        //        query = query.Where(a => a.Id < skipid);
        //    }

        //    var listpostid = await query.OrderByDescending(o => o.Id).Take(10).ToListAsync();

        //    //await _hubContext.Clients.All.SendAsync("ListPostId", listpostid);
        //    return Ok(listpostid);
        //}

        public async Task<IActionResult> CheckActionPostId(int id)
        {
            var checkactionpostid = await _db.Log_Posts.Where(a => a.PostId == id).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            await _hubContext.Clients.All.SendAsync("CheckActionPostId", checkactionpostid);
            return Ok();
        }

        public async Task<IActionResult> UserId(string id)
        {
            var userid = await _userManager.Users.Where(a => a.Id == id && a.Active == true).ToListAsync();
            await _hubContext.Clients.All.SendAsync("UserId", userid);
            return Ok();
        }

        public async Task<IActionResult> LikeId(int id)
        {
            var likeid = await _db.Likes.Where(a => a.PostId == id && a.CheckLike == true && a.Active == true).ToListAsync();
            await _hubContext.Clients.All.SendAsync("LikeId", likeid);
            return Ok();
        }

        public async Task<IActionResult> CheckActionLikeId(int id)
        {
            var checkactionlikeid = await _db.Log_Likes.Where(a => a.LikeId == id && a.Active == true).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            await _hubContext.Clients.All.SendAsync("CheckActionLikeId", checkactionlikeid);
            return Ok();
        }

        public async Task<IActionResult> CommentId(int id)
        {
            var commentid = await _db.Comments.Where(a => a.Id == id && a.Active == true).ToListAsync();
            await _hubContext.Clients.All.SendAsync("CommentId", commentid);
            return Ok();
        }

        public async Task<IActionResult> ListCommentId(int id)
        {
            var listcommentid = await _db.Comments.Where(a => a.PostId == id && a.Active == true).ToListAsync();
            await _hubContext.Clients.All.SendAsync("ListCommentId", listcommentid);
            return Ok();
        }
        public async Task<IActionResult> CheckActionCommentId(int id)
        {
            var checkactioncommentid = await _db.Log_Comments.Where(a => a.CommentId == id).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            await _hubContext.Clients.All.SendAsync("CheckActionCommentId", checkactioncommentid);
            return Ok();
        }

        public async Task<IActionResult> ListMediaId(int id)
        {
            var listmediaid = await _db.Medias.Where(a => a.PostId == id && a.Active == true).OrderBy(o => o.Id).ToListAsync();
            await _hubContext.Clients.All.SendAsync("ListMediaId", listmediaid);
            return Ok();
        }

        public async Task<IActionResult> CheckUserRoleId(string id)
        {
            var userid = await _userManager.Users.Where(a => a.Id == id && a.Active == true).FirstOrDefaultAsync();
            var userroles = await _db.UserRoles.Where(a => a.UserId == userid!.Id).FirstOrDefaultAsync();
            var roles = await _db.Roles.Where(a => a.Id == userroles!.RoleId).FirstOrDefaultAsync();
            await _hubContext.Clients.All.SendAsync("CheckUserRoleId", roles);
            return Ok();
        }

        public async Task<IActionResult> SearchPost(int menugroupid, IFormCollection form)
        {
            var searchpost = new List<Post>();
            var data = form["data"];

            if (menugroupid > 0)
            {
                searchpost = await _db.Posts.Where(a => a.Description!.Contains(data) && a.MenuGroupId == menugroupid && a.Active == true).ToListAsync();
            }
            else
            {
                searchpost = await _db.Posts.Where(a => a.Description!.Contains(data) && a.Active == true).ToListAsync();
            }

            await _hubContext.Clients.All.SendAsync("SearchPost", searchpost);
            return Ok();
        }

        public async Task<IActionResult> AllNotificationId(string id)
        {
            var allnotification = await _db.Notifications.Where(a => a.UserId == id && a.Active == true).OrderByDescending(o => o.Id).Take(10).ToListAsync();
            await _hubContext.Clients.All.SendAsync("AllNotificationId", allnotification);
            return Ok();
        }

        public async Task<IActionResult> NotificationId(int id)
        {
            var notification = await _db.Notifications.Where(a => a.Id == id && a.Active == true).OrderBy(o => o.Id).FirstOrDefaultAsync();
            await _hubContext.Clients.All.SendAsync("NotificationId", notification);
            return Ok();
        }

        public async Task<IActionResult> UnreadNotificationId(string id)
        {
            var unreadnotification = await _db.Notifications.Where(a => a.UserId == id && a.Read == false && a.Active == true).OrderByDescending(o => o.Id).ToListAsync();
            await _hubContext.Clients.All.SendAsync("UnreadNotificationId", unreadnotification);
            return Ok();
        }

        public async Task<IActionResult> ListNotificationId(string id, string checkread, int skipid)
        {
            var listnotification = new List<Notification>();

            if (checkread == "All")
            {
                if (skipid > 0)
                {
                    listnotification = await _db.Notifications.Where(a => a.UserId == id && a.Id < skipid && a.Active == true).OrderByDescending(o => o.Id).Take(10).ToListAsync();
                }
                else
                {
                    listnotification = await _db.Notifications.Where(a => a.UserId == id && a.Active == true).OrderByDescending(o => o.Id).Take(10).ToListAsync();
                }
            }
            else
            {
                if (skipid > 0)
                {
                    listnotification = await _db.Notifications.Where(a => a.UserId == id && a.Read == false && a.Id < skipid && a.Active == true).OrderByDescending(o => o.Id).Take(10).ToListAsync();
                }
                else
                {
                    listnotification = await _db.Notifications.Where(a => a.UserId == id && a.Read == false && a.Active == true).OrderByDescending(o => o.Id).Take(10).ToListAsync();
                }
            }
            await _hubContext.Clients.All.SendAsync("ListNotificationId", listnotification);
            return Ok();
        }

        public async Task<IActionResult> CheckActionNotificationId(int id)
        {
            var checkactionnotificationid = await _db.Log_Notifications.Where(a => a.NotificationId == id).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            await _hubContext.Clients.All.SendAsync("CheckActionNotificationId", checkactionnotificationid);
            return Ok();
        }

        public async Task<IActionResult> ListAttachmentId(int id, int skipid)
        {
            var listattachmentid = await _db.Attachments.Where(a => a.DirectoryGroupId == id && a.Active == true).OrderBy(o => o.Name).ToListAsync();
            await _hubContext.Clients.All.SendAsync("ListAttachmentId", listattachmentid);
            return Ok();
        }

        public async Task<IActionResult> AttachmentId(int id)
        {
            var attachmentid = await _db.Attachments.Where(a => a.Id == id && a.Active == true).ToListAsync();
            await _hubContext.Clients.All.SendAsync("AttachmentId", attachmentid);
            return Ok();
        }

        public async Task<IActionResult> CheckActionAttachmentId(int id)
        {
            var checkactionattachmentid = await _db.Log_Attachments.Where(a => a.AttachmentId == id).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            await _hubContext.Clients.All.SendAsync("CheckActionAttachmentId", checkactionattachmentid);
            return Ok();
        }

        public async Task<IActionResult> ListDirectoryGroupId(int id)
        {
            var listdirectorygroupid = await _db.DirectoryGroups.Where(a => a.Id == id && a.Active == true).ToListAsync();
            await _hubContext.Clients.All.SendAsync("ListDirectoryGroupId", listdirectorygroupid);
            return Ok();
        }

        public async Task<IActionResult> FolderId(int id)
        {
            var folderid = await _db.DirectoryGroups.Where(a => a.Id == id && a.Active == true).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            await _hubContext.Clients.All.SendAsync("FolderId", folderid);
            return Ok();
        }

        public async Task<IActionResult> ListFolderId(int id)
        {
            var listfolderid = await _db.DirectoryGroups.Where(a => a.MenuGroupId == id && a.Active == true).OrderBy(o => o.Name).ToListAsync();

            await _hubContext.Clients.All.SendAsync("ListFolderId", listfolderid);
            return Ok();
        }

        public async Task<IActionResult> ListPreviewFolderId(int id, string url)
        {
            var listfolderid = new List<DirectoryGroup>();

            if (url != null)
            {
                var urlfull = "wwwroot\\Attachment\\" + url;
                listfolderid = await _db.DirectoryGroups.Where(a => a.MenuGroupId == id && a.Url.Contains(urlfull) && a.Active == true).OrderBy(o => o.Id).ToListAsync();
            }
            else
            {
                var urlfull = "wwwroot\\Attachment\\";
                listfolderid = await _db.DirectoryGroups.Where(a => a.MenuGroupId == id && a.Url.Contains(urlfull) && a.Active == true).OrderBy(o => o.Id).ToListAsync();
            }

            await _hubContext.Clients.All.SendAsync("ListPreviewFolderId", listfolderid);
            return Ok();
        }

        public async Task<IActionResult> TitleFolderId(int id, string title)
        {
            var titlefolderid = new DirectoryGroup();

            if (title != null)
            {
                var urlfull = "wwwroot\\Attachment\\" + title;
                titlefolderid = await _db.DirectoryGroups.Where(a => a.MenuGroupId == id && a.Url == urlfull && a.Active == true).OrderBy(o => o.Id).FirstOrDefaultAsync();
            }

            await _hubContext.Clients.All.SendAsync("TitleFolderId", titlefolderid);
            return Ok();
        }
        public async Task<IActionResult> CheckActionFolderId(int id)
        {
            var checkactionfolderid = await _db.Log_DirectoryGroups.Where(a => a.DirectoryGroupId == id).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            await _hubContext.Clients.All.SendAsync("CheckActionFolderId", checkactionfolderid);
            return Ok();
        }

        public async Task<IActionResult> ListCategoryComplaint()
        {
            var listcategorycomplaint = await _db.CategoryComplaints.Where(a => a.Active == true).ToListAsync();
            await _hubContext.Clients.All.SendAsync("ListCategoryComplaint", listcategorycomplaint);
            return Ok();
        }

        public async Task<IActionResult> CreatePost()
        {
            try
            {
                var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
                var UserMenuGroup = await _db.UserMenuGroups.Where(a => a.UserId == UserId.Id && a.Active).FirstOrDefaultAsync();

                Post post = new()
                {
                    MenuGroupId = UserMenuGroup!.MenuGroupId,
                    CreatedBy = UserId.Id,
                    CreatedOn = DateTime.Now,
                    Active = true
                };

                if (Request.Form.Keys != null)
                {
                    foreach (string key in Request.Form.Keys)
                    {
                        if (key == "data")
                        {
                            post!.Description = Request.Form[key];
                        }
                    }
                }

                await _db.Posts.AddAsync(post);
                var result = await _db.SaveChangesAsync();

                var ListPostDatabase = await _db.Posts.Where(a => a.Id == post.Id && a.Active == true).OrderByDescending(o => o.Id).ToListAsync();
                var PostDatabase = ListPostDatabase.FirstOrDefault();
                if (result > 0 && PostDatabase != null)
                {
                    Log_Post log_Post = new()
                    {
                        PostId = PostDatabase.Id,
                        MenuGroupId = PostDatabase.MenuGroupId,
                        Description = PostDatabase.Description,
                        CreatedBy = PostDatabase.CreatedBy,
                        CreatedOn = PostDatabase.CreatedOn,
                        Active = PostDatabase.Active,
                        Action = "Create",
                        ActionBy = PostDatabase.CreatedBy,
                        ActionOn = PostDatabase.CreatedOn
                    };

                    await _db.Log_Posts.AddAsync(log_Post);
                    await _db.SaveChangesAsync();

                    if (Request.Form.Files != null)
                    {
                        if (Request.Form.Files.Count > 0)
                        {
                            foreach (var file in Request.Form.Files)
                            {
                                var fileName = Path.GetFileName(file.FileName);
                                var fileExt = Path.GetExtension(fileName);
                                var fileSize = file.Length;
                                var tmpName = Guid.NewGuid().ToString();
                                var newfileName = string.Concat(tmpName, fileExt);
                                var folderVideos = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "blogpost", "videos");
                                var folderImages = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "blogpost", "images");
                                var fileSavePath = Path.Combine();

                                if (fileExt == ".mp4")
                                {
                                    fileSavePath = Path.Combine(folderVideos, newfileName);
                                }
                                else
                                {
                                    fileSavePath = Path.Combine(folderImages, newfileName);
                                }

                                using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
                                {
                                    await file.CopyToAsync(stream);
                                }

                                Media media = new()
                                {
                                    PostId = PostDatabase.Id,
                                    Name = fileName,
                                    Url = newfileName,
                                    Type = fileExt,
                                    Size = fileSize,
                                    CreatedBy = UserId.Id,
                                    CreatedOn = DateTime.Now,
                                    Active = true
                                };

                                await _db.Medias.AddAsync(media);
                                var resultmedia = await _db.SaveChangesAsync();

                                var MediaDatabase = await _db.Medias.Where(a => a.Id == media.Id && a.Active == true).OrderByDescending(o => o.Id).FirstOrDefaultAsync();

                                if (result > 0 && MediaDatabase != null)
                                {
                                    Log_Media log_Media = new()
                                    {
                                        MediaId = MediaDatabase.Id,
                                        PostId = MediaDatabase.PostId,
                                        Name = MediaDatabase.Name,
                                        Url = MediaDatabase.Url,
                                        Type = MediaDatabase.Type,
                                        Size = MediaDatabase.Size,
                                        CreatedBy = MediaDatabase.CreatedBy,
                                        CreatedOn = MediaDatabase.CreatedOn,
                                        Active = MediaDatabase.Active,
                                        Action = "Create",
                                        ActionBy = MediaDatabase.CreatedBy,
                                        ActionOn = MediaDatabase.CreatedOn
                                    };

                                    await _db.Log_Medias.AddAsync(log_Media);
                                    await _db.SaveChangesAsync();
                                }
                            }
                        }
                    }
                }

                return new JsonResult(ListPostDatabase);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> EditPost(int editpostid, string mediapostvalue)
        {
            try
            {
                var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
                var CheckPostDatabase = await _db.Posts.Where(a => a.Id == editpostid && a.Active == true).OrderByDescending(o => o.Id).FirstAsync();

                if (Request.Form.Keys != null)
                {
                    foreach (string key in Request.Form.Keys)
                    {
                        if (key == "data")
                        {
                            CheckPostDatabase!.Description = Request.Form[key];
                        }
                    }
                }

                _db.Posts.Update(CheckPostDatabase!);
                var result = await _db.SaveChangesAsync();

                var ListPostDatabase = await _db.Posts.Where(a => a.Id == CheckPostDatabase!.Id && a.Active == true).OrderByDescending(o => o.Id).ToListAsync();
                var PostDatabase = ListPostDatabase.FirstOrDefault();
                if (result > 0 && PostDatabase != null)
                {
                    Log_Post log_Post = new()
                    {
                        PostId = PostDatabase.Id,
                        MenuGroupId = PostDatabase.MenuGroupId,
                        Description = PostDatabase.Description,
                        CreatedBy = PostDatabase.CreatedBy,
                        CreatedOn = PostDatabase.CreatedOn,
                        Active = PostDatabase.Active,
                        Action = "Edit",
                        ActionBy = UserId.Id,
                        ActionOn = DateTime.Now
                    };

                    await _db.Log_Posts.AddAsync(log_Post);
                    await _db.SaveChangesAsync();

                    if (Request.Form.Files != null)
                    {
                        if (Request.Form.Files.Count > 0)
                        {
                            var CheckMediaDatabase = await _db.Medias.Where(a => a.PostId == CheckPostDatabase.Id && a.Active == true).OrderByDescending(o => o.Id).ToListAsync();

                            if (CheckMediaDatabase != null)
                            {
                                foreach (var item in CheckMediaDatabase)
                                {
                                    var folderVideos = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "blogpost", "videos");
                                    var folderImages = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "blogpost", "images");
                                    var fileSavePath = Path.Combine();

                                    if (item.Type == ".mp4")
                                    {
                                        fileSavePath = Path.Combine(folderVideos, item.Url);
                                    }
                                    else
                                    {
                                        fileSavePath = Path.Combine(folderImages, item.Url);
                                    }

                                    FileInfo filedelete = new(fileSavePath);

                                    if (filedelete.Exists)//check for file exits or not
                                    {
                                        item.Active = false;
                                        _db.Medias.Update(item);
                                        await _db.SaveChangesAsync();

                                        var LogMediaDatabase = await _db.Medias.Where(a => a.Id == item.Id && a.Active == true).OrderByDescending(o => o.Id).FirstOrDefaultAsync();

                                        if (LogMediaDatabase != null)
                                        {
                                            Log_Media log_Media = new()
                                            {
                                                MediaId = LogMediaDatabase.Id,
                                                PostId = LogMediaDatabase.PostId,
                                                Name = LogMediaDatabase.Name,
                                                Url = LogMediaDatabase.Url,
                                                Type = LogMediaDatabase.Type,
                                                Size = LogMediaDatabase.Size,
                                                CreatedBy = LogMediaDatabase.CreatedBy,
                                                CreatedOn = LogMediaDatabase.CreatedOn,
                                                Active = LogMediaDatabase.Active,
                                                Action = "Delete",
                                                ActionBy = UserId.Id,
                                                ActionOn = DateTime.Now
                                            };

                                            await _db.Log_Medias.AddAsync(log_Media);
                                            await _db.SaveChangesAsync();
                                        }

                                        filedelete.Delete();
                                    }
                                }
                            }

                            foreach (var file in Request.Form.Files)
                            {
                                var fileName = Path.GetFileName(file.FileName);
                                var fileExt = Path.GetExtension(fileName);
                                var fileSize = file.Length;
                                var tmpName = Guid.NewGuid().ToString();
                                var newfileName = string.Concat(tmpName, fileExt);
                                var folderVideos = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "blogpost", "videos");
                                var folderImages = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "blogpost", "images");
                                var fileSavePath = Path.Combine();

                                if (fileExt == ".mp4")
                                {
                                    fileSavePath = Path.Combine(folderVideos, newfileName);
                                }
                                else
                                {
                                    fileSavePath = Path.Combine(folderImages, newfileName);
                                }

                                using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
                                {
                                    await file.CopyToAsync(stream);
                                }

                                Media media = new()
                                {
                                    PostId = PostDatabase.Id,
                                    Name = fileName,
                                    Url = newfileName,
                                    Type = fileExt,
                                    Size = fileSize,
                                    CreatedBy = UserId.Id,
                                    CreatedOn = DateTime.Now,
                                    Active = true
                                };

                                await _db.Medias.AddAsync(media);
                                var resultmedia = await _db.SaveChangesAsync();

                                var MediaDatabase = await _db.Medias.Where(a => a.Id == media.Id && a.Active == true).OrderByDescending(o => o.Id).FirstOrDefaultAsync();

                                if (result > 0 && MediaDatabase != null)
                                {
                                    Log_Media log_Media = new()
                                    {
                                        MediaId = MediaDatabase.Id,
                                        PostId = MediaDatabase.PostId,
                                        Name = MediaDatabase.Name,
                                        Url = MediaDatabase.Url,
                                        Type = MediaDatabase.Type,
                                        Size = MediaDatabase.Size,
                                        CreatedBy = MediaDatabase.CreatedBy,
                                        CreatedOn = MediaDatabase.CreatedOn,
                                        Active = MediaDatabase.Active,
                                        Action = "Edit",
                                        ActionBy = MediaDatabase.CreatedBy,
                                        ActionOn = MediaDatabase.CreatedOn
                                    };

                                    await _db.Log_Medias.AddAsync(log_Media);
                                    await _db.SaveChangesAsync();
                                }
                            }
                        }
                        else if (mediapostvalue == "deletemedia")
                        {
                            var CheckMediaDatabase = await _db.Medias.Where(a => a.PostId == CheckPostDatabase.Id && a.Active == true).OrderByDescending(o => o.Id).ToListAsync();

                            if (CheckMediaDatabase != null)
                            {
                                foreach (var item in CheckMediaDatabase)
                                {
                                    var folderVideos = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "blogpost", "videos");
                                    var folderImages = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "blogpost", "images");
                                    var fileSavePath = Path.Combine();

                                    if (item.Type == ".mp4")
                                    {
                                        fileSavePath = Path.Combine(folderVideos, item.Url);
                                    }
                                    else
                                    {
                                        fileSavePath = Path.Combine(folderImages, item.Url);
                                    }

                                    FileInfo filedelete = new(fileSavePath);

                                    if (filedelete.Exists)//check for file exits or not
                                    {
                                        item.Active = false;
                                        _db.Medias.Update(item);
                                        await _db.SaveChangesAsync();

                                        var LogMediaDatabase = await _db.Medias.Where(a => a.Id == item.Id && a.Active == true).OrderByDescending(o => o.Id).FirstOrDefaultAsync();

                                        if (LogMediaDatabase != null)
                                        {
                                            Log_Media log_Media = new()
                                            {
                                                MediaId = LogMediaDatabase.Id,
                                                PostId = LogMediaDatabase.PostId,
                                                Name = LogMediaDatabase.Name,
                                                Url = LogMediaDatabase.Url,
                                                Type = LogMediaDatabase.Type,
                                                Size = LogMediaDatabase.Size,
                                                CreatedBy = LogMediaDatabase.CreatedBy,
                                                CreatedOn = LogMediaDatabase.CreatedOn,
                                                Active = LogMediaDatabase.Active,
                                                Action = "Delete",
                                                ActionBy = UserId.Id,
                                                ActionOn = DateTime.Now
                                            };

                                            await _db.Log_Medias.AddAsync(log_Media);
                                            await _db.SaveChangesAsync();
                                        }

                                        filedelete.Delete();
                                    }
                                }
                            }
                        }
                    }
                }

                return new JsonResult(ListPostDatabase);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> DeletePost(int deletepostid)
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            var CheckPostDatabase = await _db.Posts.Where(a => a.Id == deletepostid && a.Active == true).OrderByDescending(o => o.Id).FirstOrDefaultAsync();

            CheckPostDatabase!.Active = false;

            _db.Posts.Update(CheckPostDatabase);
            var result = await _db.SaveChangesAsync();

            var ListPostDatabase = await _db.Posts.Where(a => a.Id == CheckPostDatabase.Id && a.Active == false).OrderByDescending(o => o.Id).ToListAsync();
            var PostDatabase = ListPostDatabase.FirstOrDefault();
            if (result > 0 && PostDatabase != null)
            {
                Log_Post log_Post = new()
                {
                    PostId = PostDatabase.Id,
                    MenuGroupId = PostDatabase.MenuGroupId,
                    Description = PostDatabase.Description,
                    CreatedBy = PostDatabase.CreatedBy,
                    CreatedOn = PostDatabase.CreatedOn,
                    Active = PostDatabase.Active,
                    Action = "Delete",
                    ActionBy = UserId.Id,
                    ActionOn = DateTime.Now
                };

                await _db.Log_Posts.AddAsync(log_Post);
                await _db.SaveChangesAsync();

                var CheckMediaDatabase = await _db.Medias.Where(a => a.PostId == CheckPostDatabase.Id && a.Active == true).OrderByDescending(o => o.Id).ToListAsync();

                if (CheckMediaDatabase != null)
                {
                    foreach (var item in CheckMediaDatabase)
                    {
                        var folderVideos = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "blogpost", "videos");
                        var folderImages = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "blogpost", "images");
                        var fileSavePath = Path.Combine();

                        if (item.Type == ".mp4")
                        {
                            fileSavePath = Path.Combine(folderVideos, item.Url);
                        }
                        else
                        {
                            fileSavePath = Path.Combine(folderImages, item.Url);
                        }

                        FileInfo filedelete = new(fileSavePath);

                        if (filedelete.Exists)//check for file exits or not
                        {
                            item.Active = false;
                            _db.Medias.Update(item);
                            await _db.SaveChangesAsync();

                            var LogMediaDatabase = await _db.Medias.Where(a => a.Id == item.Id && a.Active == true).OrderByDescending(o => o.Id).FirstOrDefaultAsync();

                            if (LogMediaDatabase != null)
                            {
                                Log_Media log_Media = new()
                                {
                                    MediaId = LogMediaDatabase.Id,
                                    PostId = LogMediaDatabase.PostId,
                                    Name = LogMediaDatabase.Name,
                                    Url = LogMediaDatabase.Url,
                                    Type = LogMediaDatabase.Type,
                                    Size = LogMediaDatabase.Size,
                                    CreatedBy = LogMediaDatabase.CreatedBy,
                                    CreatedOn = LogMediaDatabase.CreatedOn,
                                    Active = LogMediaDatabase.Active,
                                    Action = "Delete",
                                    ActionBy = UserId.Id,
                                    ActionOn = DateTime.Now
                                };

                                await _db.Log_Medias.AddAsync(log_Media);
                                await _db.SaveChangesAsync();
                            }

                            filedelete.Delete();
                        }
                    }
                }

                var CheckLikeDatabase = await _db.Likes.Where(a => a.PostId == CheckPostDatabase.Id && a.Active == true).OrderByDescending(o => o.Id).ToListAsync();

                if (CheckLikeDatabase != null)
                {
                    foreach (var item in CheckLikeDatabase)
                    {
                        item.Active = false;
                        _db.Likes.Update(item);
                        await _db.SaveChangesAsync();

                        var ListLikeDatabase = await _db.Likes.Where(a => a.Id == item.Id && a.Active == false).OrderByDescending(o => o.Id).FirstOrDefaultAsync();

                        if (ListLikeDatabase != null)
                        {
                            Log_Like log_Like = new()
                            {
                                LikeId = ListLikeDatabase.Id,
                                PostId = ListLikeDatabase.PostId,
                                CheckLike = ListLikeDatabase.CheckLike,
                                CreatedBy = ListLikeDatabase.CreatedBy,
                                CreatedOn = ListLikeDatabase.CreatedOn,
                                Active = ListLikeDatabase.Active,
                                Action = "Delete",
                                ActionBy = UserId.Id,
                                ActionOn = DateTime.Now
                            };

                            await _db.Log_Likes.AddAsync(log_Like);
                            await _db.SaveChangesAsync();
                        }
                    }
                }

                var CheckCommentDatabase = await _db.Comments.Where(a => a.PostId == CheckPostDatabase.Id && a.Active == true).OrderByDescending(o => o.Id).ToListAsync();

                if (CheckCommentDatabase != null)
                {
                    foreach (var item in CheckCommentDatabase)
                    {
                        item.Active = false;
                        _db.Comments.Update(item);
                        await _db.SaveChangesAsync();

                        var ListCommentDatabase = await _db.Comments.Where(a => a.Id == item.Id && a.Active == false).OrderByDescending(o => o.Id).FirstOrDefaultAsync();

                        if (ListCommentDatabase != null)
                        {
                            Log_Comment log_Comment = new()
                            {
                                PostId = ListCommentDatabase.Id,
                                Description = ListCommentDatabase.Description,
                                CreatedBy = ListCommentDatabase.CreatedBy,
                                CreatedOn = ListCommentDatabase.CreatedOn,
                                Active = ListCommentDatabase.Active,
                                Action = "Delete",
                                ActionBy = UserId.Id,
                                ActionOn = DateTime.Now
                            };

                            await _db.Log_Comments.AddAsync(log_Comment);
                            await _db.SaveChangesAsync();
                        }
                    }
                }

                var CheckNotificationDatabase = await _db.Notifications.Where(a => a.PostId == CheckPostDatabase.Id && a.Active == true).OrderByDescending(o => o.Id).ToListAsync();

                if (CheckNotificationDatabase != null)
                {
                    foreach (var item in CheckNotificationDatabase)
                    {
                        item.Active = false;
                        _db.Notifications.Update(item);
                        await _db.SaveChangesAsync();

                        var ListNotificationDatabase = await _db.Notifications.Where(a => a.Id == item.Id && a.Active == false).OrderByDescending(o => o.Id).FirstOrDefaultAsync();

                        if (ListNotificationDatabase != null)
                        {
                            Log_Notification log_Notification = new()
                            {
                                NotificationId = ListNotificationDatabase.Id,
                                PostId = ListNotificationDatabase.PostId,
                                Read = ListNotificationDatabase.Read,
                                UserId = ListNotificationDatabase.UserId,
                                Active = ListNotificationDatabase.Active,
                                Action = "Delete",
                                ActionBy = UserId.Id,
                                ActionOn = DateTime.Now
                            };

                            await _db.Log_Notifications.AddAsync(log_Notification);
                            await _db.SaveChangesAsync();
                        }
                    }
                }
            }

            return new JsonResult(ListPostDatabase);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification(int id)
        {
            var UserId = await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User));
            var Post = await _db.Posts.Where(a => a.Id == id).FirstAsync();
            var AllUser = await _userManager.Users.Where(a => a.Active == true).ToListAsync();

            List<Notification> notifications = AllUser.Select(user => new Notification
            {
                PostId = Post!.Id,
                Read = false,
                UserId = user.Id,
                Active = true
            }).ToList();

            await _db.Notifications.AddRangeAsync(notifications);
            var result = await _db.SaveChangesAsync();


            if (result > 0)
            {
                foreach (var notification in notifications)
                {
                    var notificationDatabase = await _db.Notifications.Where(a => a.Id == notification.Id && a.Active).OrderByDescending(o => o.Id).FirstAsync();

                    if (notificationDatabase != null)
                    {
                        Log_Notification log_Notification = new()
                        {
                            NotificationId = notificationDatabase.Id,
                            PostId = notificationDatabase.PostId,
                            Read = notificationDatabase.Read,
                            UserId = notificationDatabase.UserId,
                            Active = notificationDatabase.Active,
                            Action = "Create",
                            ActionBy = UserId.Id,
                            ActionOn = DateTime.Now
                        };

                        await _db.Log_Notifications.AddAsync(log_Notification);
                    }
                }

                await _db.SaveChangesAsync();
            }

            return new JsonResult(Post);
        }

        public async Task<IActionResult> EditNotification(int id)
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            var Notification = await _db.Notifications.Where(a => a.Id == id && a.Active == true).FirstOrDefaultAsync();

            if (Notification != null)
            {
                if (Notification.Read == false)
                {
                    Notification.Read = true;

                    _db.Notifications.Update(Notification);
                    var result = await _db.SaveChangesAsync();

                    var ListNotificationDatabase = await _db.Notifications.Where(a => a.Id == Notification.Id && a.Active == true).OrderByDescending(o => o.Id).ToListAsync();
                    var NotificationDatabase = ListNotificationDatabase.FirstOrDefault();
                    if (result > 0 && NotificationDatabase != null)
                    {
                        Log_Notification log_Notification = new()
                        {
                            NotificationId = NotificationDatabase.Id,
                            PostId = NotificationDatabase.PostId,
                            Read = NotificationDatabase.Read,
                            UserId = NotificationDatabase.UserId,
                            Active = NotificationDatabase.Active,
                            Action = "Edit",
                            ActionBy = UserId.Id,
                            ActionOn = DateTime.Now
                        };

                        await _db.Log_Notifications.AddAsync(log_Notification);
                        await _db.SaveChangesAsync();
                    }
                }
            }

            return new JsonResult(Notification);
        }

        public async Task<IActionResult> CreateLike(int id)
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            var Post = await _db.Posts.Where(a => a.Id == id && a.Active == true).FirstOrDefaultAsync();
            var LikeHistory = await _db.Likes.Where(a => a.PostId == Post!.Id && a.CreatedBy == UserId.Id && a.Active == true).FirstOrDefaultAsync();
            var ListLikeDatabase = new List<Like>();

            try
            {
                if (LikeHistory != null)
                {
                    if (LikeHistory.CheckLike == true)
                    {
                        LikeHistory.CheckLike = false;
                    }
                    else
                    {
                        LikeHistory.CheckLike = true;
                    }

                    _db.Likes.Update(LikeHistory);
                    var result = await _db.SaveChangesAsync();

                    ListLikeDatabase = await _db.Likes.Where(a => a.Id == LikeHistory.Id && a.Active == true).OrderByDescending(o => o.Id).ToListAsync();
                    var LikeDatabase = ListLikeDatabase.FirstOrDefault();

                    if (result > 0 && LikeDatabase != null)
                    {
                        Log_Like log_Like = new()
                        {
                            LikeId = LikeDatabase.Id,
                            PostId = LikeDatabase.PostId,
                            CheckLike = LikeDatabase.CheckLike,
                            CreatedBy = LikeDatabase.CreatedBy,
                            CreatedOn = LikeDatabase.CreatedOn,
                            Active = LikeDatabase.Active,
                            Action = "Edit",
                            ActionBy = UserId.Id,
                            ActionOn = DateTime.Now
                        };

                        await _db.Log_Likes.AddAsync(log_Like);
                        await _db.SaveChangesAsync();
                    }
                }
                else
                {
                    Like like = new()
                    {
                        PostId = Post!.Id,
                        CheckLike = true,
                        CreatedBy = UserId.Id,
                        CreatedOn = DateTime.Now,
                        Active = true
                    };

                    await _db.Likes.AddAsync(like);
                    var result = await _db.SaveChangesAsync();

                    ListLikeDatabase = await _db.Likes.Where(a => a.Id == like.Id && a.Active == true).OrderByDescending(o => o.Id).ToListAsync();
                    var LikeDatabase = ListLikeDatabase.FirstOrDefault();

                    if (result > 0 && LikeDatabase != null)
                    {
                        Log_Like log_Like = new()
                        {
                            LikeId = LikeDatabase.Id,
                            PostId = LikeDatabase.PostId,
                            CheckLike = LikeDatabase.CheckLike,
                            CreatedBy = LikeDatabase.CreatedBy,
                            CreatedOn = LikeDatabase.CreatedOn,
                            Active = LikeDatabase.Active,
                            Action = "Create",
                            ActionBy = LikeDatabase.CreatedBy,
                            ActionOn = LikeDatabase.CreatedOn
                        };

                        await _db.Log_Likes.AddAsync(log_Like);
                        await _db.SaveChangesAsync();
                    }
                }

                return new JsonResult(ListLikeDatabase);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateComment(int id)
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            var PostId = await _db.Posts.Where(a => a.Id == id && a.Active == true).FirstOrDefaultAsync();

            Comment comment = new()
            {
                PostId = PostId!.Id,
                CreatedBy = UserId.Id,
                CreatedOn = DateTime.Now,
                Active = true
            };

            if (Request.Form.Keys != null)
            {
                foreach (string key in Request.Form.Keys)
                {
                    if (key == "data")
                    {
                        comment!.Description = Request.Form[key];
                    }
                }
            }

            await _db.Comments.AddAsync(comment);
            var result = await _db.SaveChangesAsync();

            var ListCommentDatabase = await _db.Comments.Where(a => a.Id == comment.Id && a.Active == true).OrderByDescending(o => o.Id).ToListAsync();
            var CommentDatabase = ListCommentDatabase.FirstOrDefault();
            if (result > 0 && CommentDatabase != null)
            {
                Log_Comment log_Comment = new()
                {
                    CommentId = CommentDatabase.Id,
                    PostId = CommentDatabase.PostId,
                    Description = CommentDatabase.Description,
                    CreatedBy = CommentDatabase.CreatedBy,
                    CreatedOn = CommentDatabase.CreatedOn,
                    Active = CommentDatabase.Active,
                    Action = "Create",
                    ActionBy = CommentDatabase.CreatedBy,
                    ActionOn = CommentDatabase.CreatedOn
                };

                await _db.Log_Comments.AddAsync(log_Comment);
                await _db.SaveChangesAsync();
            }

            return new JsonResult(ListCommentDatabase);
        }

        public async Task<IActionResult> EditComment(int editcommentid)
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            var CheckCommentDatabase = await _db.Comments.Where(a => a.Id == editcommentid && a.Active == true).FirstAsync();

            if (Request.Form.Keys != null)
            {
                foreach (string key in Request.Form.Keys)
                {
                    if (key == "data")
                    {
                        CheckCommentDatabase!.Description = Request.Form[key];
                    }
                }
            }

            _db.Comments.Update(CheckCommentDatabase);
            var result = await _db.SaveChangesAsync();

            var ListCommentDatabase = await _db.Comments.Where(a => a.Id == CheckCommentDatabase.Id && a.Active == true).OrderByDescending(o => o.Id).ToListAsync();
            var CommentDatabase = ListCommentDatabase.FirstOrDefault();
            if (result > 0 && CommentDatabase != null)
            {
                Log_Comment log_Comment = new()
                {
                    CommentId = CommentDatabase.Id,
                    PostId = CommentDatabase.PostId,
                    Description = CommentDatabase.Description,
                    CreatedBy = CommentDatabase.CreatedBy,
                    CreatedOn = CommentDatabase.CreatedOn,
                    Active = CommentDatabase.Active,
                    Action = "Edit",
                    ActionBy = UserId.Id,
                    ActionOn = DateTime.Now
                };

                await _db.Log_Comments.AddAsync(log_Comment);
                await _db.SaveChangesAsync();
            }

            return new JsonResult(ListCommentDatabase);
        }

        public async Task<IActionResult> DeleteComment(int deletecommentid)
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            var CheckCommentDatabase = await _db.Comments.Where(a => a.Id == deletecommentid && a.Active == true).OrderByDescending(o => o.Id).FirstOrDefaultAsync();

            CheckCommentDatabase!.Active = false;

            _db.Comments.Update(CheckCommentDatabase);
            var result = await _db.SaveChangesAsync();

            var ListCommentDatabase = await _db.Comments.Where(a => a.Id == CheckCommentDatabase.Id && a.Active == false).OrderByDescending(o => o.Id).ToListAsync();
            var CommentDatabase = ListCommentDatabase.FirstOrDefault();
            if (result > 0 && CommentDatabase != null)
            {
                Log_Comment log_Comment = new()
                {
                    CommentId = CommentDatabase.Id,
                    PostId = CommentDatabase.PostId,
                    Description = CommentDatabase.Description,
                    CreatedBy = CommentDatabase.CreatedBy,
                    CreatedOn = CommentDatabase.CreatedOn,
                    Active = CommentDatabase.Active,
                    Action = "Delete",
                    ActionBy = UserId.Id,
                    ActionOn = DateTime.Now
                };

                await _db.Log_Comments.AddAsync(log_Comment);
                await _db.SaveChangesAsync();
            }

            return new JsonResult(ListCommentDatabase);
        }

        public async Task<IActionResult> UploadFile(int id)
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            var DirectoryGroup = await _db.DirectoryGroups.Where(a => a.Id == id && a.Active == true).FirstOrDefaultAsync();
            var AttachmentDatabase = new Models.Attachment();

            if (Request.Form.Files != null)
            {
                if (Request.Form.Files.Count > 0)
                {
                    foreach (var file in Request.Form.Files)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var fileExt = Path.GetExtension(fileName);
                        var fileSize = file.Length;
                        var tmpName = Guid.NewGuid().ToString();
                        var newfileName = string.Concat(tmpName, fileExt);
                        var filePath = Path.Combine();
                        var folderPath = Path.Combine();

                        if (DirectoryGroup != null)
                        {
                            filePath = Path.Combine(DirectoryGroup.Url, newfileName);
                            folderPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                        }
                        else
                        {
                            filePath = Path.Combine("wwwroot", "Attachment", newfileName);
                            folderPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                        }

                        using (FileStream stream = new FileStream(folderPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        Models.Attachment attachment = new()
                        {
                            DirectoryGroupId = DirectoryGroup!.Id,
                            Name = fileName,
                            Url = filePath,
                            Type = fileExt,
                            Size = fileSize,
                            CreatedBy = UserId.Id,
                            CreatedOn = DateTime.Now,
                            Active = true
                        };

                        await _db.Attachments.AddAsync(attachment);
                        var result = await _db.SaveChangesAsync();

                        AttachmentDatabase = await _db.Attachments.Where(a => a.Id == attachment.Id && a.Active == true).OrderByDescending(o => o.Id).FirstOrDefaultAsync();

                        if (result > 0 && AttachmentDatabase != null)
                        {
                            Log_Attachment log_Attachment = new()
                            {
                                AttachmentId = AttachmentDatabase.Id,
                                DirectoryGroupId = AttachmentDatabase.DirectoryGroupId,
                                Name = AttachmentDatabase.Name,
                                Url = AttachmentDatabase.Url,
                                Type = AttachmentDatabase.Type,
                                Size = AttachmentDatabase.Size,
                                CreatedBy = AttachmentDatabase.CreatedBy,
                                CreatedOn = AttachmentDatabase.CreatedOn,
                                Active = AttachmentDatabase.Active,
                                Action = "Create",
                                ActionBy = AttachmentDatabase.CreatedBy,
                                ActionOn = AttachmentDatabase.CreatedOn
                            };

                            await _db.Log_Attachments.AddAsync(log_Attachment);
                            await _db.SaveChangesAsync();
                        }
                    }
                }
            }
            return new JsonResult(AttachmentDatabase);
        }

        public async Task<IActionResult> DeleteFile(int deleteattachmentid)
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            var CheckAttachmentDatabase = await _db.Attachments.Where(a => a.Id == deleteattachmentid && a.Active == true).OrderByDescending(o => o.Id).ToListAsync();
            var LogAttachmentDatabase = new Models.Attachment();
            if (CheckAttachmentDatabase != null)
            {
                foreach (var item in CheckAttachmentDatabase)
                {
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), item.Url);

                    FileInfo filedelete = new(folderPath);

                    if (filedelete.Exists)//check for file exits or not
                    {
                        item.Active = false;
                        _db.Attachments.Update(item);
                        await _db.SaveChangesAsync();

                        LogAttachmentDatabase = await _db.Attachments.Where(a => a.Id == item.Id && a.Active == false).OrderByDescending(o => o.Id).FirstOrDefaultAsync();

                        if (LogAttachmentDatabase != null)
                        {
                            Log_Attachment log_Attachment = new()
                            {
                                AttachmentId = LogAttachmentDatabase.Id,
                                DirectoryGroupId = LogAttachmentDatabase.DirectoryGroupId,
                                Name = LogAttachmentDatabase.Name,
                                Url = LogAttachmentDatabase.Url,
                                Type = LogAttachmentDatabase.Type,
                                Size = LogAttachmentDatabase.Size,
                                CreatedBy = LogAttachmentDatabase.CreatedBy,
                                CreatedOn = LogAttachmentDatabase.CreatedOn,
                                Active = LogAttachmentDatabase.Active,
                                Action = "Delete",
                                ActionBy = UserId.Id,
                                ActionOn = DateTime.Now
                            };

                            await _db.Log_Attachments.AddAsync(log_Attachment);
                            await _db.SaveChangesAsync();
                        }

                        filedelete.Delete();
                    }
                }
            }

            return new JsonResult(LogAttachmentDatabase);
        }

        //public FileResult DownloadFile(int id)
        //{
        //    var attachment = _db.Attachments.Where(a => a.Id == id && a.Active == true).FirstOrDefault();
        //    var net = new WebClient();
        //    var data = net.DownloadData(attachment!.Url);
        //    var content = new System.IO.MemoryStream(data);
        //    var contentType = "APPLICATION/octet-stream";
        //    var fileName = attachment.Name;
        //    return File(content, contentType, fileName);
        //}
        public async Task<IActionResult> DownloadFile(int id)
        {
            var attachment = await _db.Attachments.FirstOrDefaultAsync(a => a.Id == id && a.Active);
            if (attachment == null)
            {
                return NotFound();
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(attachment.Url);
                if (response.IsSuccessStatusCode)
                {
                    var contentStream = await response.Content.ReadAsStreamAsync();
                    var contentType = "APPLICATION/octet-stream";
                    var fileName = attachment.Name;
                    return File(contentStream, contentType, fileName);
                }
                else
                {
                    return NotFound();
                }
            }
        }


        public async Task<IActionResult> CreateFolder(int id)
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            var UserMenuGroup = await _db.UserMenuGroups.Where(a => a.UserId == UserId.Id && a.Active == true).FirstOrDefaultAsync();
            var CheckDirectoryGroupDatabase = await _db.DirectoryGroups.Where(a => a.Id == id && a.Active == true).FirstOrDefaultAsync();
            var DirectoryGroupDatabase = new DirectoryGroup();
            var filePath = Path.Combine();
            var folderPath = Path.Combine();
            var name = "";

            if (Request.Form.Keys != null)
            {
                foreach (string key in Request.Form.Keys)
                {
                    if (key == "data")
                    {
                        name = Request.Form[key];
                    }
                }
            }

            if (CheckDirectoryGroupDatabase != null)
            {
                filePath = Path.Combine(CheckDirectoryGroupDatabase.Url, name);
                folderPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            }
            else
            {
                filePath = Path.Combine("wwwroot", "Attachment", name);
                folderPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            }

            if (!Directory.Exists(folderPath))
            {
                DirectoryGroup directoryGroup = new()
                {
                    MenuGroupId = UserMenuGroup!.MenuGroupId,
                    Name = name,
                    Url = filePath,
                    CreatedBy = UserId.Id,
                    CreatedOn = DateTime.Now,
                    Active = true
                };

                await _db.DirectoryGroups.AddAsync(directoryGroup);
                var result = await _db.SaveChangesAsync();

                DirectoryGroupDatabase = await _db.DirectoryGroups.Where(a => a.Id == directoryGroup.Id && a.Active == true).OrderByDescending(o => o.Id).FirstOrDefaultAsync();

                if (DirectoryGroupDatabase != null)
                {
                    Log_DirectoryGroup log_DirectoryGroup = new()
                    {
                        DirectoryGroupId = DirectoryGroupDatabase.Id,
                        MenuGroupId = DirectoryGroupDatabase.MenuGroupId,
                        Name = DirectoryGroupDatabase.Name,
                        Url = DirectoryGroupDatabase.Url,
                        CreatedBy = DirectoryGroupDatabase.CreatedBy,
                        CreatedOn = DirectoryGroupDatabase.CreatedOn,
                        Active = DirectoryGroupDatabase.Active,
                        Action = "Create",
                        ActionBy = UserId.Id,
                        ActionOn = DateTime.Now
                    };

                    await _db.Log_DirectoryGroups.AddAsync(log_DirectoryGroup);
                    await _db.SaveChangesAsync();

                    Directory.CreateDirectory(folderPath);
                }
            }

            return new JsonResult(DirectoryGroupDatabase);
        }

        public async Task<IActionResult> DeleteFolder(int deletefolderid)
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            var CheckDirectoryGroupDatabase = await _db.DirectoryGroups.Where(a => a.Id == deletefolderid && a.Active == true).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            var CheckListDirectoryGroupDatabase = await _db.DirectoryGroups.Where(a => a.Url.Contains(CheckDirectoryGroupDatabase!.Url) && a.Active == true).OrderByDescending(o => o.Id).ToListAsync();
            var CheckAttachmentDatabase = await _db.Attachments.Where(a => a.DirectoryGroupId == CheckDirectoryGroupDatabase!.Id && a.Active == true).OrderByDescending(o => o.Id).ToListAsync();
            var LogAttachmentDatabase = new Models.Attachment();
            var LogDirectoryGroupDatabase = new DirectoryGroup();
            if (CheckAttachmentDatabase != null)
            {
                foreach (var item in CheckAttachmentDatabase)
                {
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), item.Url);

                    FileInfo filedelete = new(folderPath);

                    if (filedelete.Exists)//check for file exits or not
                    {
                        item.Active = false;
                        _db.Attachments.Update(item);
                        await _db.SaveChangesAsync();

                        LogAttachmentDatabase = await _db.Attachments.Where(a => a.Id == item.Id && a.Active == false).OrderByDescending(o => o.Id).FirstOrDefaultAsync();

                        if (LogAttachmentDatabase != null)
                        {
                            Log_Attachment log_Attachment = new()
                            {
                                AttachmentId = LogAttachmentDatabase.Id,
                                DirectoryGroupId = LogAttachmentDatabase.DirectoryGroupId,
                                Name = LogAttachmentDatabase.Name,
                                Url = LogAttachmentDatabase.Url,
                                Type = LogAttachmentDatabase.Type,
                                Size = LogAttachmentDatabase.Size,
                                CreatedBy = LogAttachmentDatabase.CreatedBy,
                                CreatedOn = LogAttachmentDatabase.CreatedOn,
                                Active = LogAttachmentDatabase.Active,
                                Action = "Delete",
                                ActionBy = UserId.Id,
                                ActionOn = DateTime.Now
                            };

                            await _db.Log_Attachments.AddAsync(log_Attachment);
                            await _db.SaveChangesAsync();
                        }

                        filedelete.Delete();
                    }
                }
            }

            if (CheckListDirectoryGroupDatabase != null)
            {
                foreach (var item in CheckListDirectoryGroupDatabase)
                {
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), item.Url);

                    if (Directory.Exists(folderPath))//check for folder exits or not
                    {
                        var CheckAttachmentDirectoryGroupDatabase = await _db.Attachments.Where(a => a.DirectoryGroupId == item!.Id && a.Active == true).OrderByDescending(o => o.Id).ToListAsync();

                        if (CheckAttachmentDirectoryGroupDatabase != null)
                        {
                            foreach (var itemattachmentdirectorygroup in CheckAttachmentDirectoryGroupDatabase)
                            {
                                var folderPathAttachmentDirectoryGroup = Path.Combine(Directory.GetCurrentDirectory(), itemattachmentdirectorygroup.Url);

                                FileInfo filedelete = new(folderPathAttachmentDirectoryGroup);

                                if (filedelete.Exists)//check for file exits or not
                                {
                                    itemattachmentdirectorygroup.Active = false;
                                    _db.Attachments.Update(itemattachmentdirectorygroup);
                                    await _db.SaveChangesAsync();

                                    LogAttachmentDatabase = await _db.Attachments.Where(a => a.Id == itemattachmentdirectorygroup.Id && a.Active == false).OrderByDescending(o => o.Id).FirstOrDefaultAsync();

                                    if (LogAttachmentDatabase != null)
                                    {
                                        Log_Attachment log_Attachment = new()
                                        {
                                            AttachmentId = LogAttachmentDatabase.Id,
                                            DirectoryGroupId = LogAttachmentDatabase.DirectoryGroupId,
                                            Name = LogAttachmentDatabase.Name,
                                            Url = LogAttachmentDatabase.Url,
                                            Type = LogAttachmentDatabase.Type,
                                            Size = LogAttachmentDatabase.Size,
                                            CreatedBy = LogAttachmentDatabase.CreatedBy,
                                            CreatedOn = LogAttachmentDatabase.CreatedOn,
                                            Active = LogAttachmentDatabase.Active,
                                            Action = "Delete",
                                            ActionBy = UserId.Id,
                                            ActionOn = DateTime.Now
                                        };

                                        await _db.Log_Attachments.AddAsync(log_Attachment);
                                        await _db.SaveChangesAsync();
                                    }

                                    filedelete.Delete();
                                }
                            }
                        }

                        item.Active = false;
                        _db.DirectoryGroups.Update(item);
                        await _db.SaveChangesAsync();

                        LogDirectoryGroupDatabase = await _db.DirectoryGroups.Where(a => a.Id == item.Id && a.Active == false).OrderByDescending(o => o.Id).FirstOrDefaultAsync();

                        if (LogDirectoryGroupDatabase != null)
                        {
                            Log_DirectoryGroup log_DirectoryGroup = new()
                            {
                                DirectoryGroupId = LogDirectoryGroupDatabase.Id,
                                MenuGroupId = LogDirectoryGroupDatabase.MenuGroupId,
                                Name = LogDirectoryGroupDatabase.Name,
                                Url = LogDirectoryGroupDatabase.Url,
                                CreatedBy = LogDirectoryGroupDatabase.CreatedBy,
                                CreatedOn = LogDirectoryGroupDatabase.CreatedOn,
                                Active = LogDirectoryGroupDatabase.Active,
                                Action = "Delete",
                                ActionBy = UserId.Id,
                                ActionOn = DateTime.Now
                            };

                            await _db.Log_DirectoryGroups.AddAsync(log_DirectoryGroup);
                            await _db.SaveChangesAsync();
                        }

                        Directory.Delete(folderPath);
                    }
                }
            }

            return new JsonResult(LogAttachmentDatabase);
        }

        public async Task<IActionResult> CreateComplaint(int id)
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            //var locationGroups = _db.LocationGroups.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            //var locations = _db.Locations.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            //var department1s = _db.Department1s.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            //var department2s = _db.Department2s.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            //var sections = _db.Sections.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            //var shops = _db.Shops.Where(a => a.Active == true).OrderBy(o => o.Branch).ToList();
            //var shopPositionGroups = _db.ShopPositionGroups.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            //var positions = _db.Positions.Where(a => a.Active == true).OrderBy(o => o.Name).ToList();
            var CategoryComplaint = await _db.CategoryComplaints.Where(a => a.Id == id && a.Active == true).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            var ComplaintFrom = await _db.ComplaintFroms.Where(a => a.Active == true).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            var ComplaintDatabase = new Complaint();
            var data = "";

            if (Request.Form.Keys != null)
            {
                foreach (string key in Request.Form.Keys)
                {
                    if (key == "data")
                    {
                        data = Request.Form[key];
                    }
                }
            }

            if (CategoryComplaint != null)
            {
                var MailPhoneNumber = "-";
                if (UserId.PhoneNumber != null)
                {
                    MailPhoneNumber = UserId.PhoneNumber;
                }

                var MailFrom = ComplaintFrom!.Email;
                var MailFromPassword = ComplaintFrom.Password;
                var MailTo = CategoryComplaint.Email;
                var MailSubject = "[Intranet] หัวข้อการร้องเรียน : " + CategoryComplaint.Name;
                var MailDescription = "<label style='font-weight:bold'>" + "เรียน ผู้ที่เกี่ยวข้อง" + "</label>" + "<br>" + "<br>" +
                           "<label>" + "&emsp;&emsp;" + "หัวข้อการร้องเรียน : " + CategoryComplaint.Name + "</label>" +
                           "<div style='display:flex;'>" + "<div style='margin-top:1em;'>" + "&emsp;&emsp;" + "รายละเอียด :" + "</div>" + "<div style='margin-left:0.25rem;'>" + data + "</div>" + "</div>" +
                           "<label style='font-weight:bold'>" + "จากคุณ " + UserId.FirstNameTH + " " + UserId.LastNameTH + "</label>" + "<br>" +
                           "อีเมล : " + UserId.UserName + "<br>" +
                           "เบอร์โทรศัพท์ : " + MailPhoneNumber;

                var client = new SmtpClient("smtp-mail.outlook.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(MailFrom, MailFromPassword)
                };

                MailMessage message = new MailMessage();

                message.From = new MailAddress(MailFrom);
                message.To.Add(new MailAddress(MailTo));
                message.Subject = MailSubject;
                message.Body = MailDescription;
                message.IsBodyHtml = true;

                client.Send(message);

                Complaint complaint = new()
                {
                    CategoryComplaintId = CategoryComplaint.Id,
                    Description = data,
                    CreatedBy = UserId.Id,
                    CreatedOn = DateTime.Now,
                    Active = true
                };

                await _db.Complaints.AddAsync(complaint);
                var result = await _db.SaveChangesAsync();

                ComplaintDatabase = await _db.Complaints.Where(a => a.Id == complaint.Id && a.Active == true).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
                if (result > 0 && ComplaintDatabase != null)
                {
                    Log_Complaint log_Complaint = new()
                    {
                        ComplaintId = ComplaintDatabase.Id,
                        CategoryComplaintId = ComplaintDatabase.CategoryComplaintId,
                        Description = ComplaintDatabase.Description,
                        CreatedBy = ComplaintDatabase.CreatedBy,
                        CreatedOn = ComplaintDatabase.CreatedOn,
                        Active = ComplaintDatabase.Active,
                        Action = "Create",
                        ActionBy = ComplaintDatabase.CreatedBy,
                        ActionOn = ComplaintDatabase.CreatedOn
                    };

                    await _db.Log_Complaints.AddAsync(log_Complaint);
                    await _db.SaveChangesAsync();
                }
            }

            return new JsonResult(ComplaintDatabase);
        }

        [Authorize]
        public IActionResult CustomBlogpost(int id, string name)
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;
            ViewData["Title"] = name;
            return View(id);
        }

        [Authorize]
        public IActionResult Home()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;
            return View();
        }

        public IActionResult Corporate()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;
            return View();
        }

        [Authorize]
        public IActionResult HumanResources()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;
            return View();
        }

        [Authorize]
        public IActionResult Safety()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;
            return View();
        }

        [Authorize]
        public IActionResult IT()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;
            return View();
        }

        [Authorize]
        public IActionResult Accounting()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;
            return View();
        }

        [Authorize]
        public IActionResult Marketing()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;
            return View();
        }

        [Authorize]
        public IActionResult BD()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;
            return View();
        }

        [Authorize]
        public IActionResult Budgeting()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;
            return View();
        }

        [Authorize]
        public IActionResult InternalAudit()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;
            return View();
        }

        [Authorize]
        public IActionResult Merchandising()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;
            return View();
        }

        [Authorize]
        public IActionResult ProductDevelopment()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;
            return View();
        }

        [Authorize]
        public IActionResult Retail1()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;
            return View();
        }

        [Authorize]
        public IActionResult Retail2()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;
            return View();
        }

        [Authorize]
        public IActionResult SupplyChain()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;
            return View();
        }


        [Authorize]
        public IActionResult Warehouse()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;
            return View();
        }

        [Authorize]
        public IActionResult Complaint()
        {
            var UserId = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result;
            ViewBag.ImageProfileUesr = UserId.ImageUser;
            ViewBag.UserId = UserId.Id;
            ViewBag.UserName = UserId.FirstNameTH + " " + UserId.LastNameTH;
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}
