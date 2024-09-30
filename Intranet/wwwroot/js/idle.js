function Form_Logout() {
    $('#Form_Logout').trigger('submit');
}

var logout = 'ออกจากระบบ';
$("#countnumberidle").html(logout);
var timeout;
var interval;


$('#IdleModal').on('shown.bs.modal', function () {
    var counter = 61;
    interval = setInterval(function () {
        counter--;
        var text = 'ออกจากระบบ (' + counter + ')';
        $("#countnumberidle").html(text);
        if (counter == 0) {
            $("#countnumberidle").html("กำลังออกจากระบบ");
            clearInterval(interval);
            $(".countreset").trigger('click');
        }
    }, 1000);

    $('.countstop').on('click', function () {
        clearInterval(interval);
        var text = 'ออกจากระบบ';
        $("#countnumberidle").html(text);
    });
});

$(document).on('mousemove', function () {
    clearTimeout(timeout);
    timeout = setTimeout(function () {
        $('#IdleModal').modal('show');
    }, 900000); /*900000*/ /*1000 = 1 msec 1000 * 60 = 60000 msec,1 sec  60000 * 15 = 900000 msec,15 min*/
});


