//$(function () {
//    var connection = $.hubConnection();
//    var hubProxy = connection.createHubProxy('ConnectionHub');

//    hubProxy.on('ReceivePostTest', function (message) {
//        $('#messagesList').append('<li>' + message + '</li>');
//    });

//    connection.start().done(function () {
//        $('#sendButton').on('click' , function () {
//            var message = $('#messageInput').val();
//            hubProxy.invoke('sendMessage', message);
//            $('#messageInput').val('');
//        });
//    });
//});