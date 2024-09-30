$('.BtnDeleteProfile').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var profilefirstnameTH = $(this).parent().find('#profilefirstnameTH').val();
    var profilelastnameTH = $(this).parent().find('#profilelastnameTH').val();
    var deleteprofilename = "ลบบัญชีผู้ใช้ " + profilefirstnameTH + " " + profilelastnameTH;
    $('#DeleteProfileLabel').html(deleteprofilename);
    $('#DeleteProfileId').val(id);
});

$('.BtnExtendProfile').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var profilefirstnameTH = $(this).parent().find('#profilefirstnameTH').val();
    var profilelastnameTH = $(this).parent().find('#profilelastnameTH').val();
    var extendprofilename = "ขยายสิทธิ์บัญชีผู้ใช้ " + profilefirstnameTH + " " + profilelastnameTH;
    $('#ExtendProfileLabel').html(extendprofilename);
    $('#ExtendProfileId').val(id);

});

var collapseDescriptionProfile = new bootstrap.Collapse($('#collapseDescriptionProfile'), {
    show: true
});
$('.btnArrowDescriptionProfile').css({ 'transition': 'all 0.4s ease', 'transform': 'rotateZ(90deg)' });

$('#collapseDescriptionProfile').on('show.bs.collapse', function () {
    $('.btnArrowDescriptionProfile').css({ 'transition': 'all 0.4s ease', 'transform': 'rotateZ(90deg)' });
});

$('#collapseDescriptionProfile').on('hide.bs.collapse', function () {
    $('.btnArrowDescriptionProfile').css({ 'transition': 'all 0.4s ease', 'transform': 'rotateZ(0)' });
});


var collapseRoleProfile = new bootstrap.Collapse($('#collapseRoleProfile'), {
    show: true
});
$('.btnArrowRoleProfile').css({ 'transition': 'all 0.4s ease', 'transform': 'rotateZ(90deg)' });

$('#collapseRoleProfile').on('show.bs.collapse', function () {
    $('.btnArrowRoleProfile').css({ 'transition': 'all 0.4s ease', 'transform': 'rotateZ(90deg)' });
});

$('#collapseRoleProfile').on('hide.bs.collapse', function () {
    $('.btnArrowRoleProfile').css({ 'transition': 'all 0.4s ease', 'transform': 'rotateZ(0)' });
});

/*Function*/
function Department2Id(id) {
    var value;
    $.ajax({
        url: '/Account/Department2Id?id=' + id,
        async: false,
        success: function (result) {
            value = result;
        }
    });
    return value;
}

function SectionId(id1,id2) {
    var value;
    $.ajax({
        url: '/Account/SectionId?id1=' + id1 + '&id2=' + id2,
        async: false,
        success: function (result) {
            value = result;
        }
    });
    return value;
}

function ShopId(id) {
    var value;
    $.ajax({
        url: '/Account/ShopId?id=' + id,
        async: false,
        success: function (result) {
            value = result;
        }
    });
    return value;
}

function PositionId(id1, id2) {
    var value;
    $.ajax({
        url: '/Account/PositionId?id1=' + id1 + '&id2=' + id2,
        async: false,
        success: function (result) {
            value = result;
        }
    });
    return value;
}

function AvatarImageProfile() {
    var value;
    $.ajax({
        url: '/Account/AvatarImageProfile',
        async: false,
        success: function (result) {
            value = result;
        }
    });
    return value;
}

function AvatarBackgroundProfile() {
    var value;
    $.ajax({
        url: '/Account/AvatarBackgroundProfile',
        async: false,
        success: function (result) {
            value = result;
        }
    });
    return value;
}

/*--********** SelectEditModal **********--*/
$('.SelectEditDepartment1Profile').on('change', function () {
    var id = $(this).val();
    $('.SelectEditDepartment2Profile select').empty();
    $('.SelectEditDepartment2Profile select').append('<option value="">เลือกฝ่าย 2</option>');

    var Department2IdFunction = Department2Id(id);

    if (Department2IdFunction.length > 0) {
        $.each(Department2IdFunction, function (i, department2iddata) {
            $('.SelectEditSectionProfile').attr('hidden', true);
            $('.SelectEditSectionProfile select').attr('disabled', true);
            $('.SelectEditDepartment2Profile select').append('<option value=' + department2iddata.id + '>' + department2iddata.name + '</option>');
            $('.SelectEditDepartment2Profile').attr('hidden', false);
            $('.SelectEditDepartment2Profile select').attr('disabled', false);
        });
    }
    else {
        $('.SelectEditDepartment2Profile').attr('hidden', true);
        $('.SelectEditDepartment2Profile select').attr('disabled', true);
        $('.SelectEditSectionProfile select').empty();
        $('.SelectEditSectionProfile select').append('<option value="">เลือกแผนก</option>');

        var SectionIdFunction = SectionId(id);

        if (SectionIdFunction.length > 0) {
            $.each(SectionIdFunction, function (i, sectioniddata) {
                $('.SelectEditSectionProfile select').append('<option value=' + sectioniddata.id + '>' + sectioniddata.name + '</option>');
                $('.SelectEditSectionProfile').attr('hidden', false);
                $('.SelectEditSectionProfile select').attr('disabled', false);
            });
        }
        else {
            $('.SelectEditSectionProfile').attr('hidden', true);
            $('.SelectEditSectionProfile select').attr('disabled', true);
        }
    }
});

$('.SelectEditDepartment2Profile Select').on('change', function () {
    var id1 = $('.SelectEditDepartment1Profile').val();
    var id2 = $(this).val();
    $('.SelectEditSectionProfile select').empty();
    $('.SelectEditSectionProfile select').append('<option value="">เลือกแผนก</option>');

    var SectionIdFunction = SectionId(id1, id2);

    if (SectionIdFunction.length > 0) {
        $.each(SectionIdFunction, function (i, sectioniddata) {
            $('.SelectEditSectionProfile select').append('<option value=' + sectioniddata.id + '>' + sectioniddata.name + '</option>');
            $('.SelectEditSectionProfile').attr('hidden', false);
            $('.SelectEditSectionProfile select').attr('disabled', false);
        });
    }
    else {
        $('.SelectEditSectionProfile').attr('hidden', true);
        $('.SelectEditSectionProfile select').attr('disabled', true);
    }

    $('.SelectEditShopProfile').attr('hidden', true);
    $('.SelectEditShopProfile select').attr('disabled', true);
    $('.SelectEditPositionProfile').attr('hidden', true);
    $('.SelectEditPositionProfile select').attr('disabled', true);
});

$('.SelectEditSectionProfile Select').on('change', function () {
    var id = $(this).val();
    $('.SelectEditShopProfile select').empty();
    $('.SelectEditShopProfile select').append('<option value="">เลือกสาขา</option>');

    var ShopIdFunction = ShopId(id);

    if (ShopIdFunction.length > 0) {
        $.each(ShopIdFunction, function (i, shopiddata) {
            $('.SelectEditSectionProfile select').append('<option value=' + sectioniddata.id + '>' + sectioniddata.name + '</option>');
            $('.SelectEditSectionProfile').attr('hidden', false);
            $('.SelectEditSectionProfile select').attr('disabled', false);
        });
    }
    else {
        $('.SelectEditShopProfile').attr('hidden', true);
        $('.SelectEditShopProfile select').attr('disabled', true);
        $('.SelectEditPositionProfile select').empty();
        $('.SelectEditPositionProfile select').append('<option value="">เลือกตำแหน่ง</option>');

        var PositionIdFunction = PositionId(id);

        if (PositionIdFunction.length > 0) {
            $.each(PositionIdFunction, function (i, positioniddata) {
                $('.SelectEditPositionProfile select').append('<option value=' + positioniddata.id + '>' + positioniddata.name + '</option>');
                $('.SelectEditPositionProfile').attr('hidden', false);
                $('.SelectEditPositionProfile select').attr('disabled', false);
            });
        }
        else {
            $('.SelectEditPositionProfile').attr('hidden', true);
            $('.SelectEditPositionProfile select').attr('disabled', true);
        }
    }
});

$('.SelectEditShopProfile Select').on('change', function () {
    var id1 = $('.SelectEditSectionProfile select').val();
    var id2 = $(this).val();
    $('.SelectEditPositionProfile select').empty();
    $('.SelectEditPositionProfile select').append('<option value="">เลือกตำแหน่ง</option>');

    var PositionIdFunction = PositionId(id1,id2);

    if (PositionIdFunction.length > 0) {
        $.each(PositionIdFunction, function (i, positioniddata) {
            $('.SelectEditPositionProfile select').append('<option value=' + positioniddata.id + '>' + positioniddata.name + '</option>');
            $('.SelectEditPositionProfile').attr('hidden', false);
            $('.SelectEditPositionProfile select').attr('disabled', false);
        });
    }
    else {
        $('.SelectEditPositionProfile').attr('hidden', true);
        $('.SelectEditPositionProfile select').attr('disabled', true);
    }
});

$('.BtnEditProfile').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var profileusername = $(this).parent().find('#profileusername').val();
    var profileemployeeid = $(this).parent().find('#profileemployeeid').val();
    var profilefirstnameEN = $(this).parent().find('#profilefirstnameEN').val();
    var profilelastnameEN = $(this).parent().find('#profilelastnameEN').val();
    var profilefirstnameTH = $(this).parent().find('#profilefirstnameTH').val();
    var profilelastnameTH = $(this).parent().find('#profilelastnameTH').val();
    var profilenickname = $(this).parent().find('#profilenickname').val();
    var profileinternalphonenumber = $(this).parent().find('#profileinternalphonenumber').val();
    var profilephonenumber = $(this).parent().find('#profilephonenumber').val();
    var profiledepartment1id = $(this).parent().find('#profiledepartment1id').val();
    var profiledepartment2id = $(this).parent().find('#profiledepartment2id').val();
    var profilesectionid = $(this).parent().find('#profilesectionid').val();
    var profileshopid = $(this).parent().find('#profileshopid').val();
    var profilepositionid = $(this).parent().find('#profilepositionid').val();
    var profilerole = $(this).parent().find('#profilerole').val();

    $('#EditProfileId').val(id);
    $('#EditUserNameProfile').val(profileusername);
    $('#EditEmpolyeeIdProfile').val(profileemployeeid);
    $('#EditFirstNameENProfile').val(profilefirstnameEN);
    $('#EditLastNameENProfile').val(profilelastnameEN);
    $('#EditFirstNameTHProfile').val(profilefirstnameTH);
    $('#EditLastNameTHProfile').val(profilelastnameTH);
    $('#EditNickNameProfile').val(profilenickname);
    $('#EditInternalPhoneNumberProfile').val(profileinternalphonenumber);
    $('#EditPhoneNumberProfile').val(profilephonenumber);
    $('#EditDepartment1Profile').val(profiledepartment1id);
    $('#EditRoleProfile').val(profilerole);


    if (profiledepartment2id != '') {
        $('.SelectEditDepartment2Profile select').empty();
        $('.SelectEditDepartment2Profile select').append('<option value="">เลือกฝ่าย 2</option>');

        var Department2IdFunction = Department2Id(profiledepartment1id);

        if (Department2IdFunction.length > 0) {
            $.each(Department2IdFunction, function (i, department2iddata) {
                $('.SelectEditDepartment2Profile select').append('<option value=' + department2iddata.id + '>' + department2iddata.name + '</option>');
                $('#EditDepartment2Profile').val(profiledepartment2id);
                $('.SelectEditDepartment2Profile').attr('hidden', false);
                $('.SelectEditDepartment2Profile select').attr('disabled', false);
            });
        }
        else {
            $('.SelectEditDepartment2Profile').attr('hidden', true);
            $('.SelectEditDepartment2Profile select').attr('disabled', true);
        }
    }
    else {
        $('.SelectEditDepartment2Profile').attr('hidden', true);
        $('.SelectEditDepartment2Profile select').attr('disabled', true);
    }


    if (profilesectionid != '') {
        var id1 = profiledepartment1id;
        var id2 = profiledepartment2id;
        $('.SelectEditSectionProfile select').empty();
        $('.SelectEditSectionProfile select').append('<option value="">เลือกแผนก</option>');

        var SectionIdFunction = SectionId(id1, id2);

        if (SectionIdFunction.length > 0) {
            $.each(SectionIdFunction, function (i, sectioniddata) {
                $('.SelectEditSectionProfile select').append('<option value=' + sectioniddata.id + '>' + sectioniddata.name + '</option>');
                $('#EditSectionProfile').val(profilesectionid);
                $('.SelectEditSectionProfile').attr('hidden', false);
                $('.SelectEditSectionProfile select').attr('disabled', false);
            });
        }
        else {
            $('.SelectEditSectionProfile').attr('hidden', true);
            $('.SelectEditSectionProfile select').attr('disabled', true);
        }
    }
    else {
        $('.SelectEditSectionProfile').attr('hidden', true);
        $('.SelectEditSectionProfile select').attr('disabled', true);
    }


    if (profileshopid != '') {
        $('.SelectEditShopProfile select').empty();
        $('.SelectEditShopProfile select').append('<option value="">เลือกสาขา</option>');

        var ShopIdFunction = ShopId(profilesectionid);

        if (ShopIdFunction.length > 0) {
            $.each(ShopIdFunction, function (i, shopiddata) {
                $('.SelectEditShopProfile select').append('<option value=' + shopiddata.id + '>' + shopiddata.name + '</option>');
                $('#EditShopProfile').val(profileshopid);
                $('.SelectEditShopProfile').attr('hidden', false);
                $('.SelectEditShopProfile select').attr('disabled', false);
            });
        }
        else {
            $('.SelectEditShopProfile').attr('hidden', true);
            $('.SelectEditShopProfile select').attr('disabled', true);
        }
    }
    else {
        $('.SelectEditShopProfile').attr('hidden', true);
        $('.SelectEditShopProfile select').attr('disabled', true);
    }

    if (profilepositionid != '') {
        var id1 = profilesectionid;
        var id2 = profileshopid;
        $('.SelectEditPositionProfile select').empty();
        $('.SelectEditPositionProfile select').append('<option value="">เลือกตำแหน่ง</option>');

        var PositionIdFunction = PositionId(id1, id2);

        if (PositionIdFunction.length > 0) {
            $.each(PositionIdFunction, function (i, positioniddata) {
                $('.SelectEditPositionProfile select').append('<option value=' + positioniddata.id + '>' + positioniddata.name + '</option>');
                $('#EditPositionProfile').val(profilepositionid);
                $('.SelectEditPositionProfile').attr('hidden', false);
                $('.SelectEditPositionProfile select').attr('disabled', false);
            });
        }
        else {
            $('.SelectEditPositionProfile').attr('hidden', true);
            $('.SelectEditPositionProfile select').attr('disabled', true);
        }
    }
    else {
        $('.SelectEditPositionProfile').attr('hidden', true);
        $('.SelectEditPositionProfile select').attr('disabled', true);
    }

});

$('#EditChooseImageUserProfile').click(function () {
    $('#EditImageUserProfile').click();
});

$('#EditChooseBgUserProfile').click(function () {
    $('#EditBgUserProfile').click();
});

function previewAvatarImageUserProfile(src) {
    var arr = src.split('/');
    var file = arr[arr.length - 1];
    previewimageuserprofile.src = src;
    $('#EditValueAvatarImageUser').val(file);
    $('#EditValueImageUser').val(null);
}

function previewImageUserProfile() {
    previewimageuserprofile.src = URL.createObjectURL(event.target.files[0]);
    $('#EditValueAvatarImageUser').val(null);
    $('#EditValueImageUser').val(null);
}

function clearImageUserProfile() {
    $('#EditValueAvatarImageUser').val(null);
    $('#EditImageUserProfile').val(null);
    previewimageuserprofile.src = "/Image/users/profile/" + "ProfileUser_None.jpg";
    $('#EditValueImageUser').val("delete");
}

function previewAvatarBgUserProfile(src) {
    var arr = src.split('/');
    var file = arr[arr.length - 1];
    previewbackgrounduserprofile.src = src;
    $('#EditValueAvatarBgUser').val(file);
    $('#EditValueBgUser').val(null);
}

function previewBgUserProfile() {
    previewbackgrounduserprofile.src = URL.createObjectURL(event.target.files[0]);
    $('#EditValueAvatarBgUser').val(null);
    $('#EditValueBgUser').val(null);
}

function clearBgUserProfile() {
    $('#EditValueAvatarBgUser').val(null);
    $('#EditBgUserProfile').val(null);
    previewbackgrounduserprofile.src = "/Image/users/background/" + "BgUser_None.jpg";
    $('#EditValueBgUser').val("delete");
}

$('.clickavatarimgprofile').on('click', function () {
    $('.dropdownavatarimgprofile').empty();

    var AvatarImageProfileFunction = AvatarImageProfile();

    if (AvatarImageProfileFunction.length > 0) {
        var avatarimageprofilecount = 1;
        var avatarimageprofilelength = AvatarImageProfileFunction.length;
        $.each(AvatarImageProfileFunction, function (i, avatarimageprofiledata) {
            if (avatarimageprofilecount % 5 == 0) {
                if ((i + 5) >= avatarimageprofilelength) {
                    $('.dropdownavatarimgprofile').append('<div class="row mx-2 mb-2 maxwidthavatarimgprofile"><div class="col-3 p-0 text-center"><img class="avatarimgprofilemodal m-2" src="/Image/users/profile/' + avatarimageprofiledata + '"></div></div>');
                }
                else {
                    $('.dropdownavatarimgprofile').append('<div class="row mx-2 maxwidthavatarimgprofile"><div class="col-3 p-0 text-center"><img class="avatarimgprofilemodal m-2" src="/Image/users/profile/' + avatarimageprofiledata + '"></div></div>');
                }
                avatarimageprofilecount = 2;
            }
            else {
                if (avatarimageprofilecount == 1 && i == 0) {
                    $('.dropdownavatarimgprofile').append('<div class="row mx-2 mt-2 maxwidthavatarimgprofile"><div class="col-3 p-0 text-center"><img class="avatarimgprofilemodal m-2" src="/Image/users/profile/' + avatarimageprofiledata + '"></div></div>');
                }
                else {
                    $('.dropdownavatarimgprofile').find('.row').last().append('<div class="col-3 p-0 text-center"><img class="avatarimgprofilemodal m-2" src="/Image/users/profile/' + avatarimageprofiledata + '"></div>');
                }
                avatarimageprofilecount++;
            }
        });

        var rowCount = $('.dropdownavatarimgprofile').find('.row').last().find('.col-3').length;
        if (rowCount != 4) {
            rowCount = 4 - rowCount;
            for (var i = 0; i < rowCount; i++) {
                $('.dropdownavatarimgprofile').find('.row').last().append('<div class="col-3 p-0 text-center"><div class="avatarimgprofilemodal m-2"></div></div>');
            }
        }

        $('.avatarimgprofilemodal').on('click', function () {
            var src = $(this).attr('src');
            if (src != null) {
                previewAvatarImageUserProfile(src);
            }
        });
    }
});


$('.clickavatarbackgroundprofile').on('click', function () {
    $('.dropdownavatarbackgroundprofile').empty();

    var AvatarBackgroundProfileFunction = AvatarBackgroundProfile();

    if (AvatarBackgroundProfileFunction.length > 0) {
        var avatarbackgroundprofilecount = 1;
        var avatarbackgroundprofilelength = AvatarBackgroundProfileFunction.length;
        $.each(AvatarBackgroundProfileFunction, function (i, avatarbackgroundprofiledata) {
            if (avatarbackgroundprofilecount % 5 == 0) {
                if ((i + 5) >= avatarbackgroundprofilelength) {
                    $('.dropdownavatarbackgroundprofile').append('<div class="row mx-2 mb-2 maxwidthavatarimgprofile"><div class="col-3 p-0 text-center"><img class="avatarbackgroundprofilemodal m-2" src="/Image/users/background/' + avatarbackgroundprofiledata + '"></div></div>');
                }
                else {
                    $('.dropdownavatarbackgroundprofile').append('<div class="row mx-2 maxwidthavatarimgprofile"><div class="col-3 p-0 text-center"><img class="avatarbackgroundprofilemodal m-2" src="/Image/users/background/' + avatarbackgroundprofiledata + '"></div></div>');
                }
                avatarbackgroundprofilecount = 2;
            }
            else {
                if (avatarbackgroundprofilecount == 1 && i == 0) {
                    $('.dropdownavatarbackgroundprofile').append('<div class="row mx-2 mt-2 maxwidthavatarimgprofile"><div class="col-3 p-0 text-center"><img class="avatarbackgroundprofilemodal m-2" src="/Image/users/background/' + avatarbackgroundprofiledata + '"></div></div>');
                }
                else {
                    $('.dropdownavatarbackgroundprofile').find('.row').last().append('<div class="col-3 p-0 text-center"><img class="avatarbackgroundprofilemodal m-2" src="/Image/users/background/' + avatarbackgroundprofiledata + '"></div>');
                }
                avatarbackgroundprofilecount++;
            }
        });

        var rowCount = $('.dropdownavatarbackgroundprofile').find('.row').last().find('.col-3').length;
        if (rowCount != 4) {
            rowCount = 4 - rowCount;
            for (var i = 0; i < rowCount; i++) {
                $('.dropdownavatarbackgroundprofile').find('.row').last().append('<div class="col-3 p-0 text-center"><div class="avatarbackgroundprofilemodal m-2"></div></div>');
            }
        }

        $('.avatarbackgroundprofilemodal').on('click', function () {
            var src = $(this).attr('src');
            if (src != null) {
                previewAvatarBgUserProfile(src);
            }
        });
    }
});