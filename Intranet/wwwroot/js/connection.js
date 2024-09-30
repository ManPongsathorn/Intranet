"use strict";

var fetchingData = false;
var connection = new signalR.HubConnectionBuilder().withUrl("/connectionHub").build();

connection.start().then(function () {
    
}).catch(function (err) {
    console.error(err.toString());
});

//connection.on("UserConnected", (username) => {
//    console.log('connected');
//});

//connection.on("UserDisconnected", (username) => {
//    console.log('disconnected');
//});


//connection.on("OnConnected", async () => {
//    var userid = $('#userid_profilelayout').val();
//    try {
//        await connection.invoke("SaveUserConnection", userid);
//    } catch (err) {
//        console.error(err.toString());
//    }
//});

connection.on("UserConnected", async function () {
    await DisplayGeneralUserConnection();
});

//connection.on("UserDisconneced", async function () {
//    await DisplayGeneralUserConnection();
//});


//const startConnection = () => {
//    return
//};

/*export { connection, startConnection };*/


//connection.on("ReceivedNotification", async function (notificationid) {

//    var CheckActionNotificationIdFunction = await CheckActionNotificationId(notificationid);

//    if (UserConnectionFunction != null) {
//        if (CheckActionNotificationIdFunction != null) {
//            await DisplayGeneralFeedNotification('.listallnotification', 'All');

//            if (CheckActionNotificationIdFunction.action == 'Edit' || CheckActionNotificationIdFunction.action == 'Delete') {
//                await DisplayGeneralFeedNotification('.listunreadnotification', 'Unread');
//            }
//        }
//    }
//});

//connection.on("ReceivedPost", function (postid) {
//    setTimeout(function () {

//        CheckActionPostId(postid)
//            .then(function (CheckActionPostIdFucntion) {
//                if (CheckActionPostIdFucntion != null) {
//                    if (CheckActionPostIdFucntion.action == 'Create') {
//                        DisplayGeneralCreatePost(postid);
//                    }
//                    else if (CheckActionPostIdFucntion.action == 'Edit') {
//                        DisplayGeneralEditPost(postid);
//                    }
//                    else if (CheckActionPostIdFucntion.action == 'Delete') {
//                        DisplayGeneralDeletePost(postid);
//                    }
//                }
//            })
//            .catch(function (error) {
//                console.error(error);
//                throw error;
//            });
//    }, 1000);
//});

//connection.on("ReceivedComment", function (commentid) {
//    setTimeout(function () {

//        CheckActionCommentId(commentid)
//            .then(function (CheckActionCommentIdFunction) {
//                if (CheckActionCommentIdFunction != null) {
//                    if (CheckActionCommentIdFunction.action == 'Create') {
//                        DisplayGeneralCreateComment(commentid);
//                    }
//                    else if (CheckActionCommentIdFunction.action == 'Edit') {
//                        DisplayGeneralEditComment(commentid);
//                    }
//                    else if (CheckActionCommentIdFunction.action == 'Delete') {
//                        DisplayGeneralDeleteComment(commentid);
//                    }
//                }
//            })
//            .catch(function (error) {
//                console.error(error);
//                throw error;
//            });
//    }, 0);

//});

//connection.on("ReceivedLike", function (likeid) {
//    setTimeout(function () {
//        DisplayGeneralLike(likeid);
//    }, 1200);
//});

//connection.on("ReceivedAttachment", function (attachmentid) {
//    setTimeout(function () {

//        CheckActionAttachmentId(attachmentid)
//            .then(function (CheckActionAttachmentIdFunction) {
//                if (CheckActionAttachmentIdFunction != null) {
//                    if (CheckActionAttachmentIdFunction.action == 'Create') {
//                        DisplayGeneralCreateFileManage(attachmentid);
//                    }
//                    else if (CheckActionAttachmentIdFunction.action == 'Delete') {
//                        DisplayGeneralDeleteFileManage(attachmentid);
//                    }
//                }
//            })
//            .catch(function (error) {
//                console.error(error);
//                throw error;
//            });
//    }, 1300);

//});

//connection.on("ReceivedDirectoryGroup", function (directorygroupid) {
//    setTimeout(function () {

//        CheckActionFolderId(directorygroupid)
//            .then(function (CheckActionDirectoryGroupIdFunction) {
//                if (CheckActionDirectoryGroupIdFunction != null) {
//                    if (CheckActionDirectoryGroupIdFunction.action == 'Create') {
//                        DisplayGeneralCreateFolderFileManage(directorygroupid);
//                    }
//                    else if (CheckActionDirectoryGroupIdFunction.action == 'Edit') {

//                    }
//                    else if (CheckActionDirectoryGroupIdFunction.action == 'Delete') {
//                        DisplayGeneralDeleteFolderFileManage(directorygroupid)
//                    }
//                }
//            })
//            .catch(function (error) {
//                console.error(error);
//                throw error;
//            });
//    }, 1000);
//});