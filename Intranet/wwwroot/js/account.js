/*--********** ErrorLgoinModal **********--*/
$(window).on('load', function () {
    $('#ErrorLoginModal').modal('show');
});

/*--********** MaintananceModal **********--*/
$(window).on('load', function () {
    $('#MaintananceModal').modal('show');
});

/*--********** ErrorAccountModal **********--*/
$(window).on('load', function () {
    $('#ErrorAccountModal').modal('show');
});


/*--********** ErrorForgotPasswordModal **********--*/
$(window).on('load', function () {
    $('#ErrorForgotPasswordModal').modal('show');
});


/*--********** LoginShowPassword **********--*/
$("#LoginShowPassword button").on('click', function (event) {
    event.preventDefault();
    if ($('#LoginShowPassword input').attr("type") == "text") {
        $('#LoginShowPassword input').attr('type', 'password');
        $('#LoginShowPassword button i').addClass("bi bi-eye-slash-fill");
        $('#LoginShowPassword button i').removeClass("bi bi-eye-fill");
    } else if ($('#LoginShowPassword input').attr("type") == "password") {
        $('#LoginShowPassword input').attr('type', 'text');
        $('#LoginShowPassword button i').removeClass("bi bi-eye-slash-fill");
        $('#LoginShowPassword button i').addClass("bi bi-eye-fill");
    }
});

/*--********** ChangeOldPasswordShowPassword **********--*/
$("#ChangeOldPasswordShowPassword button").on('click', function (event) {
    event.preventDefault();
    if ($('#ChangeOldPasswordShowPassword input').attr("type") == "text") {
        $('#ChangeOldPasswordShowPassword input').attr('type', 'password');
        $('#ChangeOldPasswordShowPassword button i').addClass("bi bi-eye-slash-fill");
        $('#ChangeOldPasswordShowPassword button i').removeClass("bi bi-eye-fill");
    } else if ($('#ChangeOldPasswordShowPassword input').attr("type") == "password") {
        $('#ChangeOldPasswordShowPassword input').attr('type', 'text');
        $('#ChangeOldPasswordShowPassword button i').removeClass("bi bi-eye-slash-fill");
        $('#ChangeOldPasswordShowPassword button i').addClass("bi bi-eye-fill");
    }
});

/*--********** ChangeNewPasswordShowPassword **********--*/
$("#ChangeNewPasswordShowPassword button").on('click', function (event) {
    event.preventDefault();
    if ($('#ChangeNewPasswordShowPassword input').attr("type") == "text") {
        $('#ChangeNewPasswordShowPassword input').attr('type', 'password');
        $('#ChangeNewPasswordShowPassword button i').addClass("bi bi-eye-slash-fill");
        $('#ChangeNewPasswordShowPassword button i').removeClass("bi bi-eye-fill");
    } else if ($('#ChangeNewPasswordShowPassword input').attr("type") == "password") {
        $('#ChangeNewPasswordShowPassword input').attr('type', 'text');
        $('#ChangeNewPasswordShowPassword button i').removeClass("bi bi-eye-slash-fill");
        $('#ChangeNewPasswordShowPassword button i').addClass("bi bi-eye-fill");
    }
});

/*--********** ChangeConfirmPasswordShowPassword **********--*/
$("#ChangeConfirmPasswordShowPassword button").on('click', function (event) {
    event.preventDefault();
    if ($('#ChangeConfirmPasswordShowPassword input').attr("type") == "text") {
        $('#ChangeConfirmPasswordShowPassword input').attr('type', 'password');
        $('#ChangeConfirmPasswordShowPassword button i').addClass("bi bi-eye-slash-fill");
        $('#ChangeConfirmPasswordShowPassword button i').removeClass("bi bi-eye-fill");
    } else if ($('#ChangeConfirmPasswordShowPassword input').attr("type") == "password") {
        $('#ChangeConfirmPasswordShowPassword input').attr('type', 'text');
        $('#ChangeConfirmPasswordShowPassword button i').removeClass("bi bi-eye-slash-fill");
        $('#ChangeConfirmPasswordShowPassword button i').addClass("bi bi-eye-fill");
    }
});


$('#password').on('keyup', function () {
    var password = $(this)[0].value.replace(/\s/g, "");
    $(this).val(password);
});

$('#oldpassword').on('keyup', function () {
    var password = $(this)[0].value.replace(/\s/g, "");
    $(this).val(password);
});