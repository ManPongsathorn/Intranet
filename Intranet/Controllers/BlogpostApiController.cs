using Intranet.Data;
using Intranet.Hubs;
using Intranet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogpostApiController : ControllerBase
    {
        private readonly IHubContext<ConnectionHub> _hubContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;


        public BlogpostApiController(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IHubContext<ConnectionHub> hubContext)
        {
            _userManager = userManager;
            _db = db;
            _hubContext = hubContext;
        }

        [HttpGet("UserConnection")]
        public async Task<IActionResult> UserConnection()
        {
            var activeUserIds = await _db.HubConnections.Where(connection => connection.UserId != null).Select(connection => connection.UserId).Distinct().ToListAsync();
            var activeUsers = await _userManager.Users.Where(user => activeUserIds.Contains(user.Id) && user.Active).ToListAsync();
            return Ok(activeUsers);
        }


        [HttpGet("MenuGroupId")]
        public async Task<IActionResult> MenuGroupId(int id)
        {
            var menugroupid = await _db.MenuGroups.Where(a => a.Id == id && a.Active).ToListAsync();
            return Ok(menugroupid);
        }

        [HttpGet("ListMenuGroupId")]
        public async Task<IActionResult> ListMenuGroupId(string name)
        {
            var listmenugroupid = await _db.MenuGroups.Where(a => a.Name == name && a.Active).ToListAsync();
            return Ok(listmenugroupid);
        }

        [HttpGet("UserMenuGroupId")]
        public async Task<IActionResult> UserMenuGroupId(string id)
        {
            var usermenugroupid = await _db.UserMenuGroups.Where(a => a.UserId == id && a.Active).FirstAsync();
            return Ok(usermenugroupid);
        }

        [HttpGet("PostId")]
        public async Task<IActionResult> PostId(int id)
        {
            var postid = await _db.Posts.Where(a => a.Id == id && a.Active).ToListAsync();
            return Ok(postid);
        }

        [HttpGet("ListPostId")]
        public async Task<IActionResult> ListPostId(int id, int skipid)
        {
            IQueryable<Post> query = _db.Posts.Where(a => a.Active);

            if (id > 0)
            {
                query = query.Where(a => a.MenuGroupId == id);
            }

            if (skipid > 0)
            {
                query = query.Where(a => a.Id < skipid);
            }

            var listpostid = await query.OrderByDescending(o => o.Id).Take(10).ToListAsync();
            return Ok(listpostid);
        }

        [HttpGet("CheckActionPostId")]
        public async Task<IActionResult> CheckActionPostId(int id)
        {
            var checkactionpostid = await _db.Log_Posts.Where(a => a.PostId == id).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            return Ok(checkactionpostid);
        }

        [HttpGet("UserId")]
        public async Task<IActionResult> UserId(string id)
        {
            var userid = await _userManager.Users.Where(a => a.Id == id && a.Active).ToListAsync();
            return Ok(userid);
        }

        [HttpGet("LikeId")]
        public async Task<IActionResult> LikeId(int id)
        {
            var likeid = await _db.Likes.Where(a => a.PostId == id && a.CheckLike == true && a.Active).ToListAsync();
            return Ok(likeid);
        }

        [HttpGet("CheckActionLikeId")]
        public async Task<IActionResult> CheckActionLikeId(int id)
        {
            var checkactionlikeid = await _db.Log_Likes.Where(a => a.LikeId == id && a.Active).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            return Ok(checkactionlikeid);
        }

        [HttpGet("CommentId")]
        public async Task<IActionResult> CommentId(int id)
        {
            var commentid = await _db.Comments.Where(a => a.Id == id && a.Active).ToListAsync();
            return Ok(commentid);
        }

        [HttpGet("ListCommentId")]
        public async Task<IActionResult> ListCommentId(int id)
        {
            var listcommentid = await _db.Comments.Where(a => a.PostId == id && a.Active).ToListAsync();
            return Ok(listcommentid);
        }

        [HttpGet("CheckActionCommentId")]
        public async Task<IActionResult> CheckActionCommentId(int id)
        {
            var checkactioncommentid = await _db.Log_Comments.Where(a => a.CommentId == id).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            return Ok(checkactioncommentid);
        }

        [HttpGet("ListMediaId")]
        public async Task<IActionResult> ListMediaId(int id)
        {
            var listmediaid = await _db.Medias.Where(a => a.PostId == id && a.Active).OrderBy(o => o.Id).ToListAsync();
            return Ok(listmediaid);
        }

        [HttpGet("CheckUserRoleId")]
        public async Task<IActionResult> CheckUserRoleId(string id)
        {
            var userid = await _userManager.Users.Where(a => a.Id == id && a.Active).FirstOrDefaultAsync();
            var userroles = await _db.UserRoles.Where(a => a.UserId == userid!.Id).FirstOrDefaultAsync();
            var roles = await _db.Roles.Where(a => a.Id == userroles!.RoleId).FirstOrDefaultAsync();
            return Ok(roles);
        }

        [HttpGet("SearchPost")]
        public async Task<IActionResult> SearchPost(int menugroupid, [FromBody] string data)
        {
            IQueryable<Post> query = _db.Posts.Where(a => a.Description!.Contains(data) && a.Active);

            if (menugroupid > 0)
            {
                query = query.Where(a => a.MenuGroupId == menugroupid);
            }

            var searchpost = await query.ToListAsync();

            return Ok(searchpost);
        }

        [HttpGet("AllNotificationId")]
        public async Task<IActionResult> AllNotificationId(string id)
        {
            var allnotification = await _db.Notifications.Where(a => a.UserId == id && a.Active).OrderByDescending(o => o.Id).Take(10).ToListAsync();
            return Ok(allnotification);
        }

        [HttpGet("NotificationId")]
        public async Task<IActionResult> NotificationId(int id)
        {
            var notification = await _db.Notifications.Where(a => a.Id == id && a.Active).OrderBy(o => o.Id).FirstOrDefaultAsync();
            return Ok(notification);
        }

        [HttpGet("UnreadNotificationId")]
        public async Task<IActionResult> UnreadNotificationId(string id)
        {
            var unreadnotification = await _db.Notifications.Where(a => a.UserId == id && a.Read == false && a.Active).OrderByDescending(o => o.Id).ToListAsync();
            return Ok(unreadnotification);
        }

        [HttpGet("ListNotificationId")]
        public async Task<IActionResult> ListNotificationId(string id, string checkread, int skipid)
        {
            var listnotification = new List<Notification>();

            if (checkread == "All")
            {
                if (skipid > 0)
                {
                    listnotification = await _db.Notifications.Where(a => a.UserId == id && a.Id < skipid && a.Active).OrderByDescending(o => o.Id).Take(10).ToListAsync();
                }
                else
                {
                    listnotification = await _db.Notifications.Where(a => a.UserId == id && a.Active).OrderByDescending(o => o.Id).Take(10).ToListAsync();
                }
            }
            else
            {
                if (skipid > 0)
                {
                    listnotification = await _db.Notifications.Where(a => a.UserId == id && a.Read == false && a.Id < skipid && a.Active).OrderByDescending(o => o.Id).Take(10).ToListAsync();
                }
                else
                {
                    listnotification = await _db.Notifications.Where(a => a.UserId == id && a.Read == false && a.Active).OrderByDescending(o => o.Id).Take(10).ToListAsync();
                }
            }
            return Ok(listnotification);
        }

        [HttpGet("CheckActionNotificationId")]
        public async Task<IActionResult> CheckActionNotificationId(int id)
        {
            var checkactionnotificationid = await _db.Log_Notifications.Where(a => a.NotificationId == id).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            return Ok(checkactionnotificationid);
        }

        [HttpGet("ListAttachmentId")]
        public async Task<IActionResult> ListAttachmentId(int id, int skipid)
        {
            var listattachmentid = await _db.Attachments.Where(a => a.DirectoryGroupId == id && a.Active).OrderBy(o => o.Name).ToListAsync();
            return Ok(listattachmentid);
        }

        [HttpGet("AttachmentId")]
        public async Task<IActionResult> AttachmentId(int id)
        {
            var attachmentid = await _db.Attachments.Where(a => a.Id == id && a.Active).ToListAsync();
            return Ok(attachmentid);
        }

        [HttpGet("CheckActionAttachmentId")]
        public async Task<IActionResult> CheckActionAttachmentId(int id)
        {
            var checkactionattachmentid = await _db.Log_Attachments.Where(a => a.AttachmentId == id).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            return Ok(checkactionattachmentid);
        }

        [HttpGet("ListDirectoryGroupId")]
        public async Task<IActionResult> ListDirectoryGroupId(int id)
        {
            var listdirectorygroupid = await _db.DirectoryGroups.Where(a => a.Id == id && a.Active).ToListAsync();
            return Ok(listdirectorygroupid);
        }

        [HttpGet("FolderId")]
        public async Task<IActionResult> FolderId(int id)
        {
            var folderid = await _db.DirectoryGroups.Where(a => a.Id == id && a.Active).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            return Ok(folderid);
        }

        [HttpGet("ListFolderId")]
        public async Task<IActionResult> ListFolderId(int id)
        {
            var listfolderid = await _db.DirectoryGroups.Where(a => a.MenuGroupId == id && a.Active).OrderBy(o => o.Name).ToListAsync();
            return Ok(listfolderid);
        }

        [HttpGet("ListPreviewFolderId")]
        public async Task<IActionResult> ListPreviewFolderId(int id, string url)
        {
            var listfolderid = new List<DirectoryGroup>();

            if (url != null)
            {
                var urlfull = "wwwroot\\Attachment\\" + url;
                listfolderid = await _db.DirectoryGroups.Where(a => a.MenuGroupId == id && a.Url.Contains(urlfull) && a.Active).OrderBy(o => o.Id).ToListAsync();
            }
            else
            {
                var urlfull = "wwwroot\\Attachment\\";
                listfolderid = await _db.DirectoryGroups.Where(a => a.MenuGroupId == id && a.Url.Contains(urlfull) && a.Active).OrderBy(o => o.Id).ToListAsync();
            }
            return Ok(listfolderid);
        }

        [HttpGet("TitleFolderId")]
        public async Task<IActionResult> TitleFolderId(int id, string title)
        {
            var titlefolderid = new DirectoryGroup();

            if (title != null)
            {
                var urlfull = "wwwroot\\Attachment\\" + title;
                titlefolderid = await _db.DirectoryGroups.Where(a => a.MenuGroupId == id && a.Url == urlfull && a.Active).OrderBy(o => o.Id).FirstOrDefaultAsync();
            }
            return Ok(titlefolderid);
        }

        [HttpGet("CheckActionFolderId")]
        public async Task<IActionResult> CheckActionFolderId(int id)
        {
            var checkactionfolderid = await _db.Log_DirectoryGroups.Where(a => a.DirectoryGroupId == id).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            return Ok(checkactionfolderid);
        }

        [HttpGet("ListCategoryComplaint")]
        public async Task<IActionResult> ListCategoryComplaint()
        {
            var listcategorycomplaint = await _db.CategoryComplaints.Where(a => a.Active).ToListAsync();
            return Ok(listcategorycomplaint);
        }
    }
}
