var isFetchingData = false;

async function UserConnection() {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/UserConnection',
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            UserConnection().then(resolve).catch(reject);
        }, 1000);
    }
}

async function ListNotificationId(id, checkread, skipid) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/ListNotificationId?id=' + id + '&checkread=' + checkread + '&skipid=' + skipid,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            ListNotificationId(id, checkread, skipid).then(resolve).catch(reject);
        }, 1000);
    }

}

async function ListMenuGroupId(name) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/ListMenuGroupId?name=' + name,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            ListMenuGroupId(name).then(resolve).catch(reject);
        }, 1000);
    }
}

async function MenuGroupId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/MenuGroupId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            MenuGroupId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function UserMenuGroupId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/UserMenuGroupId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
        isFetchingData = false;
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            UserMenuGroupId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function CreatePost(data, filefromdata) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            filefromdata.append('data', data);

            $.ajax({
                url: '/api/BlogpostApi/CreatePost',
                type: 'GET',
                data: filefromdata,
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            CreatePost(data, filefromdata).then(resolve).catch(reject);
        }, 1000);
    }
}

async function EditPost(id, data, mediavalue, filefromdata) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            filefromdata.append('data', data);

            $.ajax({
                url: '/api/BlogpostApi/EditPost?editpostid=' + id + '?mediapostvalue=' + mediavalue,
                type: 'GET',
                contentType: 'application/json',
                data: filefromdata,
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            EditPost(id, data, mediavalue, filefromdata).then(resolve).catch(reject);
        }, 1000);
    }
}

async function DeletePost(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/DeletePost?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            DeletePost(id).then(resolve).catch(reject);
        }, 1000);
    }
};

async function ListPostId(id, skipid) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/ListPostId?id=' + id + '&skipid=' + skipid,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            ListPostId(id, skipid).then(resolve).catch(reject);
        }, 1000);
    }
}

async function PostId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/PostId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            PostId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function CheckActionPostId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/CheckActionPostId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            CheckActionPostId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function ListMediaId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/ListMediaId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            ListMediaId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function CreateLike(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/CreateLike?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            CreateLike(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function LikeId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/LikeId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            LikeId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function CheckActionLikeId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/CheckActionLikeId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            CheckActionLikeId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function CreateComment(id, data) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/CreateComment?id=' + id,
                type: 'GET',
                data: data,
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            CreateComment(id, data).then(resolve).catch(reject);
        }, 1000);
    }
}

async function EditComment(id, data) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/EditComment?id=' + id,
                type: 'GET',
                data: data,
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            EditComment(id, data).then(resolve).catch(reject);
        }, 1000);
    }
}

async function DeleteComment(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/DeleteComment?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            DeleteComment(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function ListCommentId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/ListCommentId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            ListCommentId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function CheckActionCommentId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/CheckActionCommentId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            CheckActionCommentId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function CommentId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/CommentId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            CommentId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function UserId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/UserId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            UserId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function CreateNotification(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/CreateNotification?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            CreateNotification(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function EditNotification(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/EditNotification?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            EditNotification(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function NotificationId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/NotificationId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            NotificationId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function CheckActionNotificationId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/CheckActionNotificationId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            CheckActionNotificationId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function UnreadNotificationId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/UnreadNotificationId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            UnreadNotificationId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function CheckUserRoleId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/CheckUserRoleId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            CheckUserRoleId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function SearchPost(data, menugroupid) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/SearchPost?menugroupid=' + menugroupid,
                type: 'GET',
                data: data,
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            SearchPost(data, menugroupid).then(resolve).catch(reject);
        }, 1000);
    }
}

async function UploadFile(id, filefromdata) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/UploadFile?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                data: filefromdata,
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            UploadFile(id, filefromdata).then(resolve).catch(reject);
        }, 1000);
    }
}

async function DeleteFile(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/DeleteFile?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            DeleteFile(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function ListAttachmentId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/ListAttachmentId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            ListAttachmentId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function AttachmentId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/AttachmentId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            AttachmentId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function CheckActionAttachmentId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/CheckActionAttachmentId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            CheckActionAttachmentId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function ListDirectoryGroupId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/ListDirectoryGroupId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            ListDirectoryGroupId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function CreateFolder(id, name) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/CreateFolder?id=' + id,
                type: 'GET',
                data: name,
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            CreateFolder(id, name).then(resolve).catch(reject);
        }, 1000);
    }
}

async function DeleteFolder(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/DeleteFolder?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            DeleteFolder(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function FolderId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/FolderId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            FolderId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function ListFolderId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/ListFolderId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            ListFolderId(id).then(resolve).catch(reject);
        }, 1000);
    }
}

async function ListPreviewFolderId(id, url) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/ListPreviewFolderId?id=' + id + '?url=' + url,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            ListPreviewFolderId(id, url).then(resolve).catch(reject);
        }, 1000);
    }
}

async function TitleFolderId(id, title) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/TitleFolderId?id=' + id + '?title=' + title,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            TitleFolderId(id, title).then(resolve).catch(reject);
        }, 1000);
    }
}

async function CheckActionFolderId(id) {
    if (!isFetchingData) {
        isFetchingData = true;
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/api/BlogpostApi/CheckActionFolderId?id=' + id,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    isFetchingData = false;
                    resolve(response);
                },
                error: function (error) {
                    isFetchingData = false;
                    console.error('Request failed:', error);
                    reject(error);
                }
            });
        });
    }
    else {
        console.log("API is already being fetched. Please wait...");
        setTimeout(() => {
            CheckActionFolderId(id).then(resolve).catch(reject);
        }, 1000);
    }
}
