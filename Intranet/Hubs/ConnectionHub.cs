using Amazon.Runtime.Internal;
using Intranet.Controllers;
using Intranet.Data;
using Intranet.Models;
using Intranet.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver.Core.Connections;
using System.Data.Odbc;
using ZstdSharp.Unsafe;

namespace Intranet.Hubs
{
    public class ConnectionHub : Hub
    {
        private readonly ApplicationDbContext _db;

        public ConnectionHub(ApplicationDbContext db) 
        {
            _db = db;
        }

        public async Task SendPost(string Description)
        {
            using (var db = new ApplicationDbContext())
            {          
                var post = new Post {
                    MenuGroupId = 1,
                    Description = Description,
                    CreatedBy = "A",
                    CreatedOn = DateTime.Now,
                    Active = true,                    
                };

                db.Posts.Add(post);
                await db.SaveChangesAsync();
            }

            await Clients.All.SendAsync("ReceivePostTest", Description);
        }

        public async Task SendNotificationToAll(int postid)
        {
            await Clients.All.SendAsync("ReceivedNotification", postid);
        }

        public async Task SendPostToAll(int postid)
        {
            await Clients.All.SendAsync("ReceivedPost", postid);
        }

        public async Task SendCommentToAll(int commentid)
        {
            await Clients.All.SendAsync("ReceivedComment", commentid);
        }

        public async Task SendLikeToAll(int likeid)
        {
            await Clients.All.SendAsync("ReceivedLike", likeid);
        }

        public async Task SendAttachmentToAll(int attachmentid)
        {
            await Clients.All.SendAsync("ReceivedAttachment", attachmentid);
        }

        public async Task SendDirectoryGroupToAll(int directorygroupid)
        {
            await Clients.All.SendAsync("ReceivedDirectoryGroup", directorygroupid);
        }

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("OnConnected");
            return base.OnConnectedAsync();
        }

        public async Task SaveUserConnection(string userid)
        {
            if (userid != null)
            {
                var HubConnectionDatabase = await _db.HubConnections.Where(a => a.ConnectionOn.Date < DateTime.Now.Date || a.UserId == userid).ToListAsync();

                if (HubConnectionDatabase.Any())
                {
                    _db.HubConnections.RemoveRange(HubConnectionDatabase);
                    await _db.SaveChangesAsync();
                }

                var connectionId = Context.ConnectionId;
                HubConnection hubConnection = new HubConnection
                {
                    ConnectionId = connectionId,
                    UserId = userid,
                    ConnectionOn = DateTime.Now
                };

                _db.HubConnections.Add(hubConnection);
                var result = await _db.SaveChangesAsync();

                if (result > 0)
                {
                    Log_UserConnection log_UserConnection = new Log_UserConnection
                    {
                        UserId = hubConnection.UserId,
                        ConnectionOn = hubConnection.ConnectionOn
                    };

                    _db.Log_UserConnections.Add(log_UserConnection);
                    await _db.SaveChangesAsync();
                }
            }
            await Clients.All.SendAsync("UserConnected");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var hubConnection = _db.HubConnections.FirstOrDefault(a => a.ConnectionId == Context.ConnectionId);
            if (hubConnection != null)
            {
                _db.HubConnections.Remove(hubConnection);
                await _db.SaveChangesAsync();
            }
            await Clients.All.SendAsync("UserDisconnected");
            await base.OnDisconnectedAsync(exception);
        }
    }
}