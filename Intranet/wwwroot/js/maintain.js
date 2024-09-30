$('#collapseMaintainMenu').on('show.bs.collapse', function () {
    $('#btnMaintainMenu').addClass('active');
    $('.btnArrowMaintainMenu').css({ 'transition': 'all 0.4s ease', 'transform': 'rotateZ(-180deg)' });

    if ($('#collapseMaintainComplaint').hasClass('show')) {
        var collapseMaintainComplaint = new bootstrap.Collapse($('#collapseMaintainComplaint'), {
            show: false
        });
    }
    else if ($('#collapseMaintainLocation').hasClass('show')) {
        var collapseMaintainLocation = new bootstrap.Collapse($('#collapseMaintainLocation'), {
            show: false
        });
    }
});

$('#collapseMaintainMenu').on('hide.bs.collapse', function () {
    $('#btnMaintainMenu').removeClass('active');
    $('.btnArrowMaintainMenu').css({ 'transition': 'all 0.4s ease', 'transform': 'rotateZ(0)' });
});

$('#collapseMaintainComplaint').on('show.bs.collapse', function () {
    $('#btnMaintainComplaint').addClass('active');
    $('.btnArrowMaintainComplaint').css({ 'transition': 'all 0.4s ease', 'transform': 'rotateZ(-180deg)' });

    if ($('#collapseMaintainMenu').hasClass('show')) {
        var collapseMaintainMenu = new bootstrap.Collapse($('#collapseMaintainMenu'), {
            show: false
        });
    }
    else if ($('#collapseMaintainLocation').hasClass('show')) {
        var collapseMaintainLocation = new bootstrap.Collapse($('#collapseMaintainLocation'), {
            show: false
        });
    }
});

$('#collapseMaintainComplaint').on('hide.bs.collapse', function () {
    $('#btnMaintainComplaint').removeClass('active');
    $('.btnArrowMaintainComplaint').css({ 'transition': 'all 0.4s ease', 'transform': 'rotateZ(0)' });
});

$('#collapseMaintainLocation').on('show.bs.collapse', function () {
    $('#btnMaintainLocation').addClass('active');
    $('.btnArrowMaintainLocation').css({ 'transition': 'all 0.4s ease', 'transform': 'rotateZ(-180deg)' });

    if ($('#collapseMaintainMenu').hasClass('show')) {
        var collapseMaintainMenu = new bootstrap.Collapse($('#collapseMaintainMenu'), {
            show: false
        });
    }
    else if ($('#collapseMaintainComplaint').hasClass('show')) {
        var collapseMaintainComplaint = new bootstrap.Collapse($('#collapseMaintainComplaint'), {
            show: false
        });
    }
});

$('#collapseMaintainLocation').on('hide.bs.collapse', function () {
    $('#btnMaintainLocation').removeClass('active');
    $('.btnArrowMaintainLocation').css({ 'transition': 'all 0.4s ease', 'transform': 'rotateZ(0)' });
});



/*--********** MaintainModal **********--*/
$('.ClickMaintainMenuGroupModal').on('click', function () {
    $('#MaintainMenuGroupModal').show();
    $('#MaintainUserMenuGroupModal').hide();
    $('#MaintainCategoryComplaintModal').hide();
    $('#MaintainComplaintFromModal').hide();
    $('#MaintainLocationGroupModal').hide();
    $('#MaintainLocationModal').hide();
    $('#MaintainDepartment1Modal').hide();
    $('#MaintainDepartment2Modal').hide();
    $('#MaintainSectionModal').hide();
    $('#MaintainShopModal').hide();
    $('#MaintainShopGroupModal').hide();
    $('#MaintainShopPositionGroupModal').hide();
    $('#MaintainPositionModal').hide();
});


$('.ClickMaintainUserMenuGroupModal').on('click', function () {
    $('#MaintainMenuGroupModal').hide();
    $('#MaintainUserMenuGroupModal').show();
    $('#MaintainCategoryComplaintModal').hide();
    $('#MaintainComplaintFromModal').hide();
    $('#MaintainLocationGroupModal').hide();
    $('#MaintainLocationModal').hide();
    $('#MaintainDepartment1Modal').hide();
    $('#MaintainDepartment2Modal').hide();
    $('#MaintainSectionModal').hide();
    $('#MaintainShopModal').hide();
    $('#MaintainShopGroupModal').hide();
    $('#MaintainShopPositionGroupModal').hide();
    $('#MaintainPositionModal').hide();
});

$('.ClickMaintainCategoryComplaintModal').on('click', function () {
    $('#MaintainMenuGroupModal').hide();
    $('#MaintainUserMenuGroupModal').hide();
    $('#MaintainCategoryComplaintModal').show();
    $('#MaintainComplaintFromModal').hide();
    $('#MaintainLocationGroupModal').hide();
    $('#MaintainLocationModal').hide();
    $('#MaintainDepartment1Modal').hide();
    $('#MaintainDepartment2Modal').hide();
    $('#MaintainSectionModal').hide();
    $('#MaintainShopModal').hide();
    $('#MaintainShopGroupModal').hide();
    $('#MaintainShopPositionGroupModal').hide();
    $('#MaintainPositionModal').hide();
});

$('.ClickMaintainComplaintFromModal').on('click', function () {
    $('#MaintainMenuGroupModal').hide();
    $('#MaintainUserMenuGroupModal').hide();
    $('#MaintainCategoryComplaintModal').hide();
    $('#MaintainComplaintFromModal').show();
    $('#MaintainLocationGroupModal').hide();
    $('#MaintainLocationModal').hide();
    $('#MaintainDepartment1Modal').hide();
    $('#MaintainDepartment2Modal').hide();
    $('#MaintainSectionModal').hide();
    $('#MaintainShopModal').hide();
    $('#MaintainShopGroupModal').hide();
    $('#MaintainShopPositionGroupModal').hide();
    $('#MaintainPositionModal').hide();
});

$('.ClickMaintainLocationGroupModal').on('click', function () {
    $('#MaintainMenuGroupModal').hide();
    $('#MaintainUserMenuGroupModal').hide();
    $('#MaintainCategoryComplaintModal').hide();
    $('#MaintainComplaintFromModal').hide();
    $('#MaintainLocationGroupModal').show();
    $('#MaintainLocationModal').hide();
    $('#MaintainDepartment1Modal').hide();
    $('#MaintainDepartment2Modal').hide();
    $('#MaintainSectionModal').hide();
    $('#MaintainShopModal').hide();
    $('#MaintainShopGroupModal').hide();
    $('#MaintainShopPositionGroupModal').hide();
    $('#MaintainPositionModal').hide();
});


$('.ClickMaintainLocationModal').on('click', function () {
    $('#MaintainMenuGroupModal').hide();
    $('#MaintainUserMenuGroupModal').hide();
    $('#MaintainCategoryComplaintModal').hide();
    $('#MaintainComplaintFromModal').hide();
    $('#MaintainLocationGroupModal').hide();
    $('#MaintainLocationModal').show();
    $('#MaintainDepartment1Modal').hide();
    $('#MaintainDepartment2Modal').hide();
    $('#MaintainSectionModal').hide();
    $('#MaintainShopModal').hide();
    $('#MaintainShopGroupModal').hide();
    $('#MaintainShopPositionGroupModal').hide();
    $('#MaintainPositionModal').hide();
});


$('.ClickMaintainDepartment1Modal').on('click', function () {
    $('#MaintainMenuGroupModal').hide();
    $('#MaintainUserMenuGroupModal').hide();
    $('#MaintainCategoryComplaintModal').hide();
    $('#MaintainComplaintFromModal').hide();
    $('#MaintainLocationGroupModal').hide();
    $('#MaintainLocationModal').hide();
    $('#MaintainDepartment1Modal').show();
    $('#MaintainDepartment2Modal').hide();
    $('#MaintainSectionModal').hide();
    $('#MaintainShopModal').hide();
    $('#MaintainShopGroupModal').hide();
    $('#MaintainShopPositionGroupModal').hide();
    $('#MaintainPositionModal').hide();
});


$('.ClickMaintainDepartment2Modal').on('click', function () {
    $('#MaintainMenuGroupModal').hide();
    $('#MaintainUserMenuGroupModal').hide();
    $('#MaintainCategoryComplaintModal').hide();
    $('#MaintainComplaintFromModal').hide();
    $('#MaintainLocationGroupModal').hide();
    $('#MaintainLocationModal').hide();
    $('#MaintainDepartment1Modal').hide();
    $('#MaintainDepartment2Modal').show();
    $('#MaintainSectionModal').hide();
    $('#MaintainShopModal').hide();
    $('#MaintainShopGroupModal').hide();
    $('#MaintainShopPositionGroupModal').hide();
    $('#MaintainPositionModal').hide();
});


$('.ClickMaintainSectionModal').on('click', function () {
    $('#MaintainMenuGroupModal').hide();
    $('#MaintainUserMenuGroupModal').hide();
    $('#MaintainCategoryComplaintModal').hide();
    $('#MaintainComplaintFromModal').hide();
    $('#MaintainLocationGroupModal').hide();
    $('#MaintainLocationModal').hide();
    $('#MaintainDepartment1Modal').hide();
    $('#MaintainDepartment2Modal').hide();
    $('#MaintainSectionModal').show();
    $('#MaintainShopModal').hide();
    $('#MaintainShopGroupModal').hide();
    $('#MaintainShopPositionGroupModal').hide();
    $('#MaintainPositionModal').hide();
});


$('.ClickMaintainShopModal').on('click', function () {
    $('#MaintainMenuGroupModal').hide();
    $('#MaintainUserMenuGroupModal').hide();
    $('#MaintainCategoryComplaintModal').hide();
    $('#MaintainComplaintFromModal').hide();
    $('#MaintainLocationGroupModal').hide();
    $('#MaintainLocationModal').hide();
    $('#MaintainDepartment1Modal').hide();
    $('#MaintainDepartment2Modal').hide();
    $('#MaintainSectionModal').hide();
    $('#MaintainShopModal').show();
    $('#MaintainShopGroupModal').hide();
    $('#MaintainShopPositionGroupModal').hide();
    $('#MaintainPositionModal').hide();
});


$('.ClickMaintainShopGroupModal').on('click', function () {
    $('#MaintainMenuGroupModal').hide();
    $('#MaintainUserMenuGroupModal').hide();
    $('#MaintainCategoryComplaintModal').hide();
    $('#MaintainComplaintFromModal').hide();
    $('#MaintainLocationGroupModal').hide();
    $('#MaintainLocationModal').hide();
    $('#MaintainDepartment1Modal').hide();
    $('#MaintainDepartment2Modal').hide();
    $('#MaintainSectionModal').hide();
    $('#MaintainShopModal').hide();
    $('#MaintainShopGroupModal').show();
    $('#MaintainShopPositionGroupModal').hide();
    $('#MaintainPositionModal').hide();
});


$('.ClickMaintainShopPositionGroupModal').on('click', function () {
    $('#MaintainMenuGroupModal').hide();
    $('#MaintainUserMenuGroupModal').hide();
    $('#MaintainCategoryComplaintModal').hide();
    $('#MaintainComplaintFromModal').hide();
    $('#MaintainLocationGroupModal').hide();
    $('#MaintainLocationModal').hide();
    $('#MaintainDepartment1Modal').hide();
    $('#MaintainDepartment2Modal').hide();
    $('#MaintainSectionModal').hide();
    $('#MaintainShopModal').hide();
    $('#MaintainShopGroupModal').hide();
    $('#MaintainShopPositionGroupModal').show();
    $('#MaintainPositionModal').hide();
});


$('.ClickMaintainPositionModal').on('click', function () {
    $('#MaintainMenuGroupModal').hide();
    $('#MaintainUserMenuGroupModal').hide();
    $('#MaintainCategoryComplaintModal').hide();
    $('#MaintainComplaintFromModal').hide();
    $('#MaintainLocationGroupModal').hide();
    $('#MaintainLocationModal').hide();
    $('#MaintainDepartment1Modal').hide();
    $('#MaintainDepartment2Modal').hide();
    $('#MaintainSectionModal').hide();
    $('#MaintainShopModal').hide();
    $('#MaintainShopGroupModal').hide();
    $('#MaintainShopPositionGroupModal').hide();
    $('#MaintainPositionModal').show();
});


/*--********** CreateMaintainModal **********--*/
$('.BtnCreateMaintainMenuGroup').on('click', function () {
    $('#CreateMaintainMenuGroupModal').show();
    $('#CreateMaintainUserMenuGroupModal').hide();
    $('#CreateMaintainCategoryComplaintModal').hide();
    $('#CreateMaintainComplaintFromModal').hide();
    $('#CreateMaintainLocationGroupModal').hide();
    $('#CreateMaintainLocationModal').hide();
    $('#CreateMaintainDepartment1Modal').hide();
    $('#CreateMaintainDepartment2Modal').hide();
    $('#CreateMaintainSectionModal').hide();
    $('#CreateMaintainShopModal').hide();
    $('#CreateMaintainShopGroupModal').hide();
    $('#CreateMaintainShopPositionGroupModal').hide();
    $('#CreateMaintainPositionModal').hide();
});


$('.BtnCreateMaintainUserMenuGroup').on('click', function () {
    $('#CreateMaintainMenuGroupModal').hide();
    $('#CreateMaintainUserMenuGroupModal').show();
    $('#CreateMaintainCategoryComplaintModal').hide();
    $('#CreateMaintainComplaintFromModal').hide();
    $('#CreateMaintainLocationGroupModal').hide();
    $('#CreateMaintainLocationModal').hide();
    $('#CreateMaintainDepartment1Modal').hide();
    $('#CreateMaintainDepartment2Modal').hide();
    $('#CreateMaintainSectionModal').hide();
    $('#CreateMaintainShopModal').hide();
    $('#CreateMaintainShopGroupModal').hide();
    $('#CreateMaintainShopPositionGroupModal').hide();
    $('#CreateMaintainPositionModal').hide();
});

$('.BtnCreateMaintainCategoryComplaint').on('click', function () {
    $('#CreateMaintainMenuGroupModal').hide();
    $('#CreateMaintainUserMenuGroupModal').hide();
    $('#CreateMaintainCategoryComplaintModal').show();
    $('#CreateMaintainComplaintFromModal').hide();
    $('#CreateMaintainLocationGroupModal').hide();
    $('#CreateMaintainLocationModal').hide();
    $('#CreateMaintainDepartment1Modal').hide();
    $('#CreateMaintainDepartment2Modal').hide();
    $('#CreateMaintainSectionModal').hide();
    $('#CreateMaintainShopModal').hide();
    $('#CreateMaintainShopGroupModal').hide();
    $('#CreateMaintainShopPositionGroupModal').hide();
    $('#CreateMaintainPositionModal').hide();
});

$('.BtnCreateMaintainComplaintFrom').on('click', function () {
    $('#CreateMaintainMenuGroupModal').hide();
    $('#CreateMaintainUserMenuGroupModal').hide();
    $('#CreateMaintainCategoryComplaintModal').hide();
    $('#CreateMaintainComplaintFromModal').show();
    $('#CreateMaintainLocationGroupModal').hide();
    $('#CreateMaintainLocationModal').hide();
    $('#CreateMaintainDepartment1Modal').hide();
    $('#CreateMaintainDepartment2Modal').hide();
    $('#CreateMaintainSectionModal').hide();
    $('#CreateMaintainShopModal').hide();
    $('#CreateMaintainShopGroupModal').hide();
    $('#CreateMaintainShopPositionGroupModal').hide();
    $('#CreateMaintainPositionModal').hide();
});

$('.BtnCreateMaintainLocationGroup').on('click', function () {
    $('#CreateMaintainMenuGroupModal').hide();
    $('#CreateMaintainUserMenuGroupModal').hide();
    $('#CreateMaintainCategoryComplaintModal').hide();
    $('#CreateMaintainComplaintFromModal').hide();
    $('#CreateMaintainLocationGroupModal').show();
    $('#CreateMaintainLocationModal').hide();
    $('#CreateMaintainDepartment1Modal').hide();
    $('#CreateMaintainDepartment2Modal').hide();
    $('#CreateMaintainSectionModal').hide();
    $('#CreateMaintainShopModal').hide();
    $('#CreateMaintainShopGroupModal').hide();
    $('#CreateMaintainShopPositionGroupModal').hide();
    $('#CreateMaintainPositionModal').hide();
});


$('.BtnCreateMaintainLocation').on('click', function () {
    $('#CreateMaintainMenuGroupModal').hide();
    $('#CreateMaintainUserMenuGroupModal').hide();
    $('#CreateMaintainCategoryComplaintModal').hide();
    $('#CreateMaintainComplaintFromModal').hide();
    $('#CreateMaintainLocationGroupModal').hide();
    $('#CreateMaintainLocationModal').show();
    $('#CreateMaintainDepartment1Modal').hide();
    $('#CreateMaintainDepartment2Modal').hide();
    $('#CreateMaintainSectionModal').hide();
    $('#CreateMaintainShopModal').hide();
    $('#CreateMaintainShopGroupModal').hide();
    $('#CreateMaintainShopPositionGroupModal').hide();
    $('#CreateMaintainPositionModal').hide();
});


$('.BtnCreateMaintainDepartment1').on('click', function () {
    $('#CreateMaintainMenuGroupModal').hide();
    $('#CreateMaintainUserMenuGroupModal').hide();
    $('#CreateMaintainCategoryComplaintModal').hide();
    $('#CreateMaintainComplaintFromModal').hide();
    $('#CreateMaintainLocationGroupModal').hide();
    $('#CreateMaintainLocationModal').hide();
    $('#CreateMaintainDepartment1Modal').show();
    $('#CreateMaintainDepartment2Modal').hide();
    $('#CreateMaintainSectionModal').hide();
    $('#CreateMaintainShopModal').hide();
    $('#CreateMaintainShopGroupModal').hide();
    $('#CreateMaintainShopPositionGroupModal').hide();
    $('#CreateMaintainPositionModal').hide();
});


$('.BtnCreateMaintainDepartment2').on('click', function () {
    $('#CreateMaintainMenuGroupModal').hide();
    $('#CreateMaintainUserMenuGroupModal').hide();
    $('#CreateMaintainCategoryComplaintModal').hide();
    $('#CreateMaintainComplaintFromModal').hide();
    $('#CreateMaintainLocationGroupModal').hide();
    $('#CreateMaintainLocationModal').hide();
    $('#CreateMaintainDepartment1Modal').hide();
    $('#CreateMaintainDepartment2Modal').show();
    $('#CreateMaintainSectionModal').hide();
    $('#CreateMaintainShopModal').hide();
    $('#CreateMaintainShopGroupModal').hide();
    $('#CreateMaintainShopPositionGroupModal').hide();
    $('#CreateMaintainPositionModal').hide();
});


$('.BtnCreateMaintainSection').on('click', function () {
    $('#CreateMaintainMenuGroupModal').hide();
    $('#CreateMaintainUserMenuGroupModal').hide();
    $('#CreateMaintainCategoryComplaintModal').hide();
    $('#CreateMaintainComplaintFromModal').hide();
    $('#CreateMaintainLocationGroupModal').hide();
    $('#CreateMaintainLocationModal').hide();
    $('#CreateMaintainDepartment1Modal').hide();
    $('#CreateMaintainDepartment2Modal').hide();
    $('#CreateMaintainSectionModal').show();
    $('#CreateMaintainShopModal').hide();
    $('#CreateMaintainShopGroupModal').hide();
    $('#CreateMaintainShopPositionGroupModal').hide();
    $('#CreateMaintainPositionModal').hide();
});


$('.BtnCreateMaintainShop').on('click', function () {
    $('#CreateMaintainMenuGroupModal').hide();
    $('#CreateMaintainUserMenuGroupModal').hide();
    $('#CreateMaintainCategoryComplaintModal').hide();
    $('#CreateMaintainComplaintFromModal').hide();
    $('#CreateMaintainLocationGroupModal').hide();
    $('#CreateMaintainLocationModal').hide();
    $('#CreateMaintainDepartment1Modal').hide();
    $('#CreateMaintainDepartment2Modal').hide();
    $('#CreateMaintainSectionModal').hide();
    $('#CreateMaintainShopModal').show();
    $('#CreateMaintainShopGroupModal').hide();
    $('#CreateMaintainShopPositionGroupModal').hide();
    $('#CreateMaintainPositionModal').hide();
});


$('.BtnCreateMaintainShopGroup').on('click', function () {
    $('#CreateMaintainMenuGroupModal').hide();
    $('#CreateMaintainUserMenuGroupModal').hide();
    $('#CreateMaintainCategoryComplaintModal').hide();
    $('#CreateMaintainComplaintFromModal').hide();
    $('#CreateMaintainLocationGroupModal').hide();
    $('#CreateMaintainLocationModal').hide();
    $('#CreateMaintainDepartment1Modal').hide();
    $('#CreateMaintainDepartment2Modal').hide();
    $('#CreateMaintainSectionModal').hide();
    $('#CreateMaintainShopModal').hide();
    $('#CreateMaintainShopGroupModal').show();
    $('#CreateMaintainShopPositionGroupModal').hide();
    $('#CreateMaintainPositionModal').hide();
});


$('.BtnCreateMaintainShopPositionGroup').on('click', function () {
    $('#CreateMaintainMenuGroupModal').hide();
    $('#CreateMaintainUserMenuGroupModal').hide();
    $('#CreateMaintainCategoryComplaintModal').hide();
    $('#CreateMaintainComplaintFromModal').hide();
    $('#CreateMaintainLocationGroupModal').hide();
    $('#CreateMaintainLocationModal').hide();
    $('#CreateMaintainDepartment1Modal').hide();
    $('#CreateMaintainDepartment2Modal').hide();
    $('#CreateMaintainSectionModal').hide();
    $('#CreateMaintainShopModal').hide();
    $('#CreateMaintainShopGroupModal').hide();
    $('#CreateMaintainShopPositionGroupModal').show();
    $('#CreateMaintainPositionModal').hide();
});


$('.BtnCreateMaintainPosition').on('click', function () {
    $('#CreateMaintainMenuGroupModal').hide();
    $('#CreateMaintainUserMenuGroupModal').hide();
    $('#CreateMaintainCategoryComplaintModal').hide();
    $('#CreateMaintainComplaintFromModal').hide();
    $('#CreateMaintainLocationGroupModal').hide();
    $('#CreateMaintainLocationModal').hide();
    $('#CreateMaintainDepartment1Modal').hide();
    $('#CreateMaintainDepartment2Modal').hide();
    $('#CreateMaintainSectionModal').hide();
    $('#CreateMaintainShopModal').hide();
    $('#CreateMaintainShopGroupModal').hide();
    $('#CreateMaintainShopPositionGroupModal').hide();
    $('#CreateMaintainPositionModal').show();
});



/*--********** EditMaintainModal **********--*/
$('.BtnEditMaintainMenuGroup').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    $('#EditMaintainMenuGroupModal').show();
    $('#EditMaintainUserMenuGroupModal').hide();
    $('#EditMaintainCategoryComplaintModal').hide();
    $('#EditMaintainComplaintFromModal').hide();
    $('#EditMaintainLocationGroupModal').hide();
    $('#EditMaintainLocationModal').hide();
    $('#EditMaintainDepartment1Modal').hide();
    $('#EditMaintainDepartment2Modal').hide();
    $('#EditMaintainSectionModal').hide();
    $('#EditMaintainShopModal').hide();
    $('#EditMaintainShopGroupModal').hide();
    $('#EditMaintainShopPositionGroupModal').hide();
    $('#EditMaintainPositionModal').hide();
    $('#EditMaintainMenuGroupId').val(id);
    $('#EditMaintainMenuGroupName').val(name);
});


$('.BtnEditMaintainUserMenuGroup').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var menugroupid = $(this).parent().find('#menugroupid').val();
    var userid = $(this).parent().find('#userid').val();
    $('#EditMaintainMenuGroupModal').hide();
    $('#EditMaintainUserMenuGroupModal').show();
    $('#EditMaintainCategoryComplaintModal').hide();
    $('#EditMaintainComplaintFromModal').hide();
    $('#EditMaintainLocationGroupModal').hide();
    $('#EditMaintainLocationModal').hide();
    $('#EditMaintainDepartment1Modal').hide();
    $('#EditMaintainDepartment2Modal').hide();
    $('#EditMaintainSectionModal').hide();
    $('#EditMaintainShopModal').hide();
    $('#EditMaintainShopGroupModal').hide();
    $('#EditMaintainShopPositionGroupModal').hide();
    $('#EditMaintainPositionModal').hide();
    $('#EditMaintainUserMenuGroupId').val(id);
    $('#EditMaintainUserMenuGroupMenuGroupId').val(menugroupid);
    $('#EditMaintainUserMenuGroupUserId').val(userid);
});

$('.BtnEditMaintainCategoryComplaint').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    var email = $(this).parent().find('#email').val();
    $('#EditMaintainMenuGroupModal').hide();
    $('#EditMaintainUserMenuGroupModal').hide();
    $('#EditMaintainCategoryComplaintModal').show();
    $('#EditMaintainComplaintFromModal').hide();
    $('#EditMaintainLocationGroupModal').hide();
    $('#EditMaintainLocationModal').hide();
    $('#EditMaintainDepartment1Modal').hide();
    $('#EditMaintainDepartment2Modal').hide();
    $('#EditMaintainSectionModal').hide();
    $('#EditMaintainShopModal').hide();
    $('#EditMaintainShopGroupModal').hide();
    $('#EditMaintainShopPositionGroupModal').hide();
    $('#EditMaintainPositionModal').hide();
    $('#EditMaintainCategoryComplaintId').val(id);
    $('#EditMaintainCategoryComplaintName').val(name);
    $('#EditMaintainCategoryComplaintEmail').val(email);
});

$('.BtnEditMaintainComplaintFrom').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var email = $(this).parent().find('#email').val();
    var password = $(this).parent().find('#password').val();
    $('#EditMaintainMenuGroupModal').hide();
    $('#EditMaintainUserMenuGroupModal').hide();
    $('#EditMaintainCategoryComplaintModal').hide();
    $('#EditMaintainComplaintFromModal').show();
    $('#EditMaintainLocationGroupModal').hide();
    $('#EditMaintainLocationModal').hide();
    $('#EditMaintainDepartment1Modal').hide();
    $('#EditMaintainDepartment2Modal').hide();
    $('#EditMaintainSectionModal').hide();
    $('#EditMaintainShopModal').hide();
    $('#EditMaintainShopGroupModal').hide();
    $('#EditMaintainShopPositionGroupModal').hide();
    $('#EditMaintainPositionModal').hide();
    $('#EditMaintainComplaintFromId').val(id);
    $('#EditMaintainComplaintFromEmail').val(email);
    $('#EditMaintainComplaintFromPassword').val(password);
});



$('.BtnEditMaintainLocationGroup').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    $('#EditMaintainMenuGroupModal').hide();
    $('#EditMaintainUserMenuGroupModal').hide();
    $('#EditMaintainCategoryComplaintModal').hide();
    $('#EditMaintainComplaintFromModal').hide();
    $('#EditMaintainLocationGroupModal').show();
    $('#EditMaintainLocationModal').hide();
    $('#EditMaintainDepartment1Modal').hide();
    $('#EditMaintainDepartment2Modal').hide();
    $('#EditMaintainSectionModal').hide();
    $('#EditMaintainShopModal').hide();
    $('#EditMaintainShopGroupModal').hide();
    $('#EditMaintainShopPositionGroupModal').hide();
    $('#EditMaintainPositionModal').hide();
    $('#EditMaintainLocationGroupId').val(id);
    $('#EditMaintainLocationGroupName').val(name);
});


$('.BtnEditMaintainLocation').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    var locationlocationgroupid = $(this).parent().find('#locationlocationgroupid').val();
    $('#EditMaintainMenuGroupModal').hide();
    $('#EditMaintainUserMenuGroupModal').hide();
    $('#EditMaintainCategoryComplaintModal').hide();
    $('#EditMaintainComplaintFromModal').hide();
    $('#EditMaintainLocationGroupModal').hide();
    $('#EditMaintainLocationModal').show();
    $('#EditMaintainDepartment1Modal').hide();
    $('#EditMaintainDepartment2Modal').hide();
    $('#EditMaintainSectionModal').hide();
    $('#EditMaintainShopModal').hide();
    $('#EditMaintainShopGroupModal').hide();
    $('#EditMaintainShopPositionGroupModal').hide();
    $('#EditMaintainPositionModal').hide();
    $('#EditMaintainLocationId').val(id);
    $('#EditMaintainLocationName').val(name);
    $('#EditMaintainLocationLocationGroupId').val(locationlocationgroupid);
});


$('.BtnEditMaintainDepartment1').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    var department1locationid = $(this).parent().find('#department1locationid').val();
    $('#EditMaintainMenuGroupModal').hide();
    $('#EditMaintainUserMenuGroupModal').hide();
    $('#EditMaintainCategoryComplaintModal').hide();
    $('#EditMaintainComplaintFromModal').hide();
    $('#EditMaintainLocationGroupModal').hide();
    $('#EditMaintainLocationModal').hide();
    $('#EditMaintainDepartment1Modal').show();
    $('#EditMaintainDepartment2Modal').hide();
    $('#EditMaintainSectionModal').hide();
    $('#EditMaintainShopModal').hide();
    $('#EditMaintainShopGroupModal').hide();
    $('#EditMaintainShopPositionGroupModal').hide();
    $('#EditMaintainPositionModal').hide();
    $('#EditMaintainDepartment1Id').val(id);
    $('#EditMaintainDepartment1Name').val(name);
    $('#EditMaintainDepartment1LocationId').val(department1locationid);
});


$('.BtnEditMaintainDepartment2').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    var department2department1id = $(this).parent().find('#department2department1id').val();
    $('#EditMaintainMenuGroupModal').hide();
    $('#EditMaintainUserMenuGroupModal').hide();
    $('#EditMaintainCategoryComplaintModal').hide();
    $('#EditMaintainComplaintFromModal').hide();
    $('#EditMaintainLocationGroupModal').hide();
    $('#EditMaintainLocationModal').hide();
    $('#EditMaintainDepartment1Modal').hide();
    $('#EditMaintainDepartment2Modal').show();
    $('#EditMaintainSectionModal').hide();
    $('#EditMaintainShopModal').hide();
    $('#EditMaintainShopGroupModal').hide();
    $('#EditMaintainShopPositionGroupModal').hide();
    $('#EditMaintainPositionModal').hide();
    $('#EditMaintainDepartment2Id').val(id);
    $('#EditMaintainDepartment2Name').val(name);
    $('#EditMaintainDepartment2Department1Id').val(department2department1id);
});


$('.BtnEditMaintainSection').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    var sectiondepartment1id = $(this).parent().find('#sectiondepartment1id').val();
    var sectiondepartment2id = $(this).parent().find('#sectiondepartment2id').val();
    $('#EditMaintainMenuGroupModal').hide();
    $('#EditMaintainUserMenuGroupModal').hide();
    $('#EditMaintainCategoryComplaintModal').hide();
    $('#EditMaintainComplaintFromModal').hide();
    $('#EditMaintainLocationGroupModal').hide();
    $('#EditMaintainLocationModal').hide();
    $('#EditMaintainDepartment1Modal').hide();
    $('#EditMaintainDepartment2Modal').hide();
    $('#EditMaintainSectionModal').show();
    $('#EditMaintainShopModal').hide();
    $('#EditMaintainShopGroupModal').hide();
    $('#EditMaintainShopPositionGroupModal').hide();
    $('#EditMaintainPositionModal').hide();
    $('#EditMaintainSectionId').val(id);
    $('#EditMaintainSectionName').val(name);
    $('#EditMaintainSectionDepartment1Id').val(sectiondepartment1id);
    if (sectiondepartment2id.length > 0) {
        $('.SelectEditSectionDepartment2 select').empty();
        $('.SelectEditSectionDepartment2 select').append('<option value="">เลือกฝ่าย 2</option>');
        $.ajax({
            url: '/Admin/Department2?id=' + sectiondepartment1id,
            success: function (result) {
                if (!result) {
                    $('.SelectEditSectionDepartment2').attr('hidden', true);
                    $('.SelectEditSectionDepartment2 select').attr('disabled', true);
                }
                else {
                    $.each(result, function (i, data) {
                        $('.SelectEditSectionDepartment2 select').append('<option value=' + data.id + '>' + data.name + '</option>');
                        $('#EditMaintainSectionDepartment2Id').val(sectiondepartment2id);
                        $('.SelectEditSectionDepartment2').attr('hidden', false);
                        $('.SelectEditSectionDepartment2 select').attr('disabled', false);
                    });
                }
            }
        });

    }
    else {
        $('.SelectEditSectionDepartment2').attr('hidden', true);
        $('.SelectEditSectionDepartment2 select').attr('disabled', true);
    }
});


$('.BtnEditMaintainShop').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    var shopsectionid = $(this).parent().find('#shopsectionid').val();
    var shoprequestshopid = $(this).parent().find('#shoprequestshopid').val();
    var shopshoppositiongroupid = $(this).parent().find('#shopshoppositiongroupid').val();
    var shopbranch = $(this).parent().find('#shopbranch').val();
    $('#EditMaintainMenuGroupModal').hide();
    $('#EditMaintainUserMenuGroupModal').hide();
    $('#EditMaintainCategoryComplaintModal').hide();
    $('#EditMaintainComplaintFromModal').hide();
    $('#EditMaintainLocationGroupModal').hide();
    $('#EditMaintainLocationModal').hide();
    $('#EditMaintainDepartment1Modal').hide();
    $('#EditMaintainDepartment2Modal').hide();
    $('#EditMaintainSectionModal').hide();
    $('#EditMaintainShopModal').show();
    $('#EditMaintainShopGroupModal').hide();
    $('#EditMaintainShopPositionGroupModal').hide();
    $('#EditMaintainPositionModal').hide();
    $('#EditMaintainShopId').val(id);
    $('#EditMaintainShopName').val(name);
    $('#EditMaintainShopSectionId').val(shopsectionid);
    $('#EditMaintainShopRequestShopId').val(shoprequestshopid);
    $('#EditMaintainShopShopPositionGroupId').val(shopshoppositiongroupid);
    $('#EditMaintainShopBranchName').val(shopbranch);
});


$('.BtnEditMaintainShopGroup').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    $('#EditMaintainMenuGroupModal').hide();
    $('#EditMaintainUserMenuGroupModal').hide();
    $('#EditMaintainCategoryComplaintModal').hide();
    $('#EditMaintainComplaintFromModal').hide();
    $('#EditMaintainLocationGroupModal').hide();
    $('#EditMaintainLocationModal').hide();
    $('#EditMaintainDepartment1Modal').hide();
    $('#EditMaintainDepartment2Modal').hide();
    $('#EditMaintainSectionModal').hide();
    $('#EditMaintainShopModal').hide();
    $('#EditMaintainShopGroupModal').show();
    $('#EditMaintainShopPositionGroupModal').hide();
    $('#EditMaintainPositionModal').hide();
    $('#EditMaintainShopGroupId').val(id);
    $('#EditMaintainShopGroupName').val(name);
});

$('.BtnEditMaintainShopPositionGroup').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    $('#EditMaintainMenuGroupModal').hide();
    $('#EditMaintainUserMenuGroupModal').hide();
    $('#EditMaintainCategoryComplaintModal').hide();
    $('#EditMaintainComplaintFromModal').hide();
    $('#EditMaintainLocationGroupModal').hide();
    $('#EditMaintainLocationModal').hide();
    $('#EditMaintainDepartment1Modal').hide();
    $('#EditMaintainDepartment2Modal').hide();
    $('#EditMaintainSectionModal').hide();
    $('#EditMaintainShopModal').hide();
    $('#EditMaintainShopGroupModal').hide();
    $('#EditMaintainShopPositionGroupModal').show();
    $('#EditMaintainPositionModal').hide();
    $('#EditMaintainShopPositionGroupId').val(id);
    $('#EditMaintainShopPositionGroupName').val(name);
});


$('.BtnEditMaintainPosition').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    var positionsectionid = $(this).parent().find('#positionsectionid').val();
    var positionshoppositiongroupid = $(this).parent().find('#positionshoppositiongroupid').val();
    $('#EditMaintainMenuGroupModal').hide();
    $('#EditMaintainUserMenuGroupModal').hide();
    $('#EditMaintainCategoryComplaintModal').hide();
    $('#EditMaintainComplaintFromModal').hide();
    $('#EditMaintainLocationGroupModal').hide();
    $('#EditMaintainLocationModal').hide();
    $('#EditMaintainDepartment1Modal').hide();
    $('#EditMaintainDepartment2Modal').hide();
    $('#EditMaintainSectionModal').hide();
    $('#EditMaintainShopModal').hide();
    $('#EditMaintainShopGroupModal').hide();
    $('#EditMaintainShopPositionGroupModal').hide();
    $('#EditMaintainPositionModal').show();
    $('#EditMaintainPositionId').val(id);
    $('#EditMaintainPositionName').val(name);
    $('#EditMaintainPositionSectionId').val(positionsectionid);
    $('#EditMaintainPositionShopPositionGroupId').val(positionshoppositiongroupid);

    if (positionshoppositiongroupid.length > 0) {
        $('.EditSelectPositionShopPositionGroup').attr({ 'hidden': false, 'disabled': false });
        $('#EditSwitchPositionShopPositionGroup').prop('checked', true);
    }
    else {
        $('.EditSelectPositionShopPositionGroup').attr({ 'hidden': true, 'disabled': true });
        $('#EditSwitchPositionShopPositionGroup').prop('checked', false);
    }
});



/*--********** DeleteMaintainModal **********--*/
$('.BtnDeleteMaintainMenuGroup').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    var deletename = ('"' + name + '"');
    $('#DeleteMaintainMenuGroupModal').show();
    $('#DeleteMaintainUserMenuGroupModal').hide();
    $('#DeleteMaintainCategoryComplaintModal').hide();
    $('#DeleteMaintainComplaintFromModal').hide();
    $('#DeleteMaintainLocationGroupModal').hide();
    $('#DeleteMaintainLocationModal').hide();
    $('#DeleteMaintainDepartment1Modal').hide();
    $('#DeleteMaintainDepartment2Modal').hide();
    $('#DeleteMaintainSectionModal').hide();
    $('#DeleteMaintainShopModal').hide();
    $('#DeleteMaintainShopGroupModal').hide();
    $('#DeleteMaintainShopPositionGroupModal').hide();
    $('#DeleteMaintainPositionModal').hide();
    $('#DeleteMaintainMenuGroupId').val(id);
    $('#DeleteMaintainMenuGroupName').html(deletename);
});


$('.BtnDeleteMaintainUserMenuGroup').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var menugroupname = $(this).parent().find('#menugroupname').val();
    var username = $(this).parent().find('#username').val();
    var deletename = ('"' + menugroupname + "และ" + username + '"');
    $('#DeleteMaintainMenuGroupModal').hide();
    $('#DeleteMaintainUserMenuGroupModal').show();
    $('#DeleteMaintainCategoryComplaintModal').hide();
    $('#DeleteMaintainComplaintFromModal').hide();
    $('#DeleteMaintainLocationGroupModal').hide();
    $('#DeleteMaintainLocationModal').hide();
    $('#DeleteMaintainDepartment1Modal').hide();
    $('#DeleteMaintainDepartment2Modal').hide();
    $('#DeleteMaintainSectionModal').hide();
    $('#DeleteMaintainShopModal').hide();
    $('#DeleteMaintainShopGroupModal').hide();
    $('#DeleteMaintainShopPositionGroupModal').hide();
    $('#DeleteMaintainPositionModal').hide();
    $('#DeleteMaintainUserMenuGroupId').val(id);
    $('#DeleteMaintainUserMenuGroupName').html(deletename);
});

$('.BtnDeleteMaintainCategoryComplaint').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    var deletename = ('"' + name + '"');
    $('#DeleteMaintainMenuGroupModal').hide();
    $('#DeleteMaintainUserMenuGroupModal').hide();
    $('#DeleteMaintainCategoryComplaintModal').show();
    $('#DeleteMaintainComplaintFromModal').hide();
    $('#DeleteMaintainLocationGroupModal').hide();
    $('#DeleteMaintainLocationModal').hide();
    $('#DeleteMaintainDepartment1Modal').hide();
    $('#DeleteMaintainDepartment2Modal').hide();
    $('#DeleteMaintainSectionModal').hide();
    $('#DeleteMaintainShopModal').hide();
    $('#DeleteMaintainShopGroupModal').hide();
    $('#DeleteMaintainShopPositionGroupModal').hide();
    $('#DeleteMaintainPositionModal').hide();
    $('#DeleteMaintainCategoryComplaintId').val(id);
    $('#DeleteMaintainCategoryComplaintName').html(deletename);
});

$('.BtnDeleteMaintainComplaintFrom').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var email = $(this).parent().find('#email').val();
    var deleteemail = ('"' + email + '"');
    $('#DeleteMaintainMenuGroupModal').hide();
    $('#DeleteMaintainUserMenuGroupModal').hide();
    $('#DeleteMaintainCategoryComplaintModal').hide();
    $('#DeleteMaintainComplaintFromModal').show();
    $('#DeleteMaintainLocationGroupModal').hide();
    $('#DeleteMaintainLocationModal').hide();
    $('#DeleteMaintainDepartment1Modal').hide();
    $('#DeleteMaintainDepartment2Modal').hide();
    $('#DeleteMaintainSectionModal').hide();
    $('#DeleteMaintainShopModal').hide();
    $('#DeleteMaintainShopGroupModal').hide();
    $('#DeleteMaintainShopPositionGroupModal').hide();
    $('#DeleteMaintainPositionModal').hide();
    $('#DeleteMaintainComplaintFromId').val(id);
    $('#DeleteMaintainComplaintFromEmail').html(deleteemail);
});


$('.BtnDeleteMaintainLocationGroup').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    var deletename = ('"' + name + '"');
    $('#DeleteMaintainMenuGroupModal').hide();
    $('#DeleteMaintainUserMenuGroupModal').hide();
    $('#DeleteMaintainCategoryComplaintModal').hide();
    $('#DeleteMaintainComplaintFromModal').hide();
    $('#DeleteMaintainLocationGroupModal').show();
    $('#DeleteMaintainLocationModal').hide();
    $('#DeleteMaintainDepartment1Modal').hide();
    $('#DeleteMaintainDepartment2Modal').hide();
    $('#DeleteMaintainSectionModal').hide();
    $('#DeleteMaintainShopModal').hide();
    $('#DeleteMaintainShopGroupModal').hide();
    $('#DeleteMaintainShopPositionGroupModal').hide();
    $('#DeleteMaintainPositionModal').hide();
    $('#DeleteMaintainLocationGroupId').val(id);
    $('#DeleteMaintainLocationGroupName').html(deletename);
});


$('.BtnDeleteMaintainLocation').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    var deletename = ('"' + name + '"');
    $('#DeleteMaintainMenuGroupModal').hide();
    $('#DeleteMaintainUserMenuGroupModal').hide();
    $('#DeleteMaintainCategoryComplaintModal').hide();
    $('#DeleteMaintainComplaintFromModal').hide();
    $('#DeleteMaintainLocationGroupModal').hide();
    $('#DeleteMaintainLocationModal').show();
    $('#DeleteMaintainDepartment1Modal').hide();
    $('#DeleteMaintainDepartment2Modal').hide();
    $('#DeleteMaintainSectionModal').hide();
    $('#DeleteMaintainShopModal').hide();
    $('#DeleteMaintainShopPositionGroupModal').hide();
    $('#DeleteMaintainPositionModal').hide();
    $('#DeleteMaintainLocationId').val(id);
    $('#DeleteMaintainLocationName').html(deletename);
});


$('.BtnDeleteMaintainDepartment1').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    var deletename = ('"' + name + '"');
    $('#DeleteMaintainMenuGroupModal').hide();
    $('#DeleteMaintainUserMenuGroupModal').hide();
    $('#DeleteMaintainCategoryComplaintModal').hide();
    $('#DeleteMaintainComplaintFromModal').hide();
    $('#DeleteMaintainLocationGroupModal').hide();
    $('#DeleteMaintainLocationModal').hide();
    $('#DeleteMaintainDepartment1Modal').show();
    $('#DeleteMaintainDepartment2Modal').hide();
    $('#DeleteMaintainSectionModal').hide();
    $('#DeleteMaintainShopModal').hide();
    $('#DeleteMaintainShopGroupModal').hide();
    $('#DeleteMaintainShopPositionGroupModal').hide();
    $('#DeleteMaintainPositionModal').hide();
    $('#DeleteMaintainDepartment1Id').val(id);
    $('#DeleteMaintainDepartment1Name').html(deletename);
});


$('.BtnDeleteMaintainDepartment2').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    var deletename = ('"' + name + '"');
    $('#DeleteMaintainMenuGroupModal').hide();
    $('#DeleteMaintainUserMenuGroupModal').hide();
    $('#DeleteMaintainCategoryComplaintModal').hide();
    $('#DeleteMaintainComplaintFromModal').hide();
    $('#DeleteMaintainLocationGroupModal').hide();
    $('#DeleteMaintainLocationModal').hide();
    $('#DeleteMaintainDepartment1Modal').hide();
    $('#DeleteMaintainDepartment2Modal').show();
    $('#DeleteMaintainSectionModal').hide();
    $('#DeleteMaintainShopModal').hide();
    $('#DeleteMaintainShopGroupModal').hide();
    $('#DeleteMaintainShopPositionGroupModal').hide();
    $('#DeleteMaintainPositionModal').hide();
    $('#DeleteMaintainDepartment2Id').val(id);
    $('#DeleteMaintainDepartment2Name').html(deletename);
});


$('.BtnDeleteMaintainSection').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    var deletename = ('"' + name + '"');
    $('#DeleteMaintainMenuGroupModal').hide();
    $('#DeleteMaintainUserMenuGroupModal').hide();
    $('#DeleteMaintainCategoryComplaintModal').hide();
    $('#DeleteMaintainComplaintFromModal').hide();
    $('#DeleteMaintainLocationGroupModal').hide();
    $('#DeleteMaintainLocationModal').hide();
    $('#DeleteMaintainDepartment1Modal').hide();
    $('#DeleteMaintainDepartment2Modal').hide();
    $('#DeleteMaintainSectionModal').show();
    $('#DeleteMaintainShopModal').hide();
    $('#DeleteMaintainShopGroupModal').hide();
    $('#DeleteMaintainShopPositionGroupModal').hide();
    $('#DeleteMaintainPositionModal').hide();
    $('#DeleteMaintainSectionId').val(id);
    $('#DeleteMaintainSectionName').html(deletename);
});


$('.BtnDeleteMaintainShop').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    var deletename = ('"' + name + '"');
    $('#DeleteMaintainMenuGroupModal').hide();
    $('#DeleteMaintainUserMenuGroupModal').hide();
    $('#DeleteMaintainCategoryComplaintModal').hide();
    $('#DeleteMaintainComplaintFromModal').hide();
    $('#DeleteMaintainLocationGroupModal').hide();
    $('#DeleteMaintainLocationModal').hide();
    $('#DeleteMaintainDepartment1Modal').hide();
    $('#DeleteMaintainDepartment2Modal').hide();
    $('#DeleteMaintainSectionModal').hide();
    $('#DeleteMaintainShopModal').show();
    $('#DeleteMaintainShopGroupModal').hide();
    $('#DeleteMaintainShopPositionGroupModal').hide();
    $('#DeleteMaintainPositionModal').hide();
    $('#DeleteMaintainShopId').val(id);
    $('#DeleteMaintainShopName').html(deletename);
});


$('.BtnDeleteMaintainShopGroup').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    var deletename = ('"' + name + '"');
    $('#DeleteMaintainMenuGroupModal').hide();
    $('#DeleteMaintainUserMenuGroupModal').hide();
    $('#DeleteMaintainCategoryComplaintModal').hide();
    $('#DeleteMaintainComplaintFromModal').hide();
    $('#DeleteMaintainLocationGroupModal').hide();
    $('#DeleteMaintainLocationModal').hide();
    $('#DeleteMaintainDepartment1Modal').hide();
    $('#DeleteMaintainDepartment2Modal').hide();
    $('#DeleteMaintainSectionModal').hide();
    $('#DeleteMaintainShopModal').hide();
    $('#DeleteMaintainShopGroupModal').show();
    $('#DeleteMaintainShopPositionGroupModal').hide();
    $('#DeleteMaintainPositionModal').hide();
    $('#DeleteMaintainShopGroupId').val(id);
    $('#DeleteMaintainShopGroupName').html(deletename);
});


$('.BtnDeleteMaintainShopPositionGroup').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    var deletename = ('"' + name + '"');
    $('#DeleteMaintainMenuGroupModal').hide();
    $('#DeleteMaintainUserMenuGroupModal').hide();
    $('#DeleteMaintainCategoryComplaintModal').hide();
    $('#DeleteMaintainComplaintFromModal').hide();
    $('#DeleteMaintainLocationGroupModal').hide();
    $('#DeleteMaintainLocationModal').hide();
    $('#DeleteMaintainDepartment1Modal').hide();
    $('#DeleteMaintainDepartment2Modal').hide();
    $('#DeleteMaintainSectionModal').hide();
    $('#DeleteMaintainShopModal').hide();
    $('#DeleteMaintainShopGroupModal').hide();
    $('#DeleteMaintainShopPositionGroupModal').show();
    $('#DeleteMaintainPositionModal').hide();
    $('#DeleteMaintainShopPositionGroupId').val(id);
    $('#DeleteMaintainShopPositionGroupName').html(deletename);
});


$('.BtnDeleteMaintainPosition').on('click', function () {
    var id = $(this).parent().find('#id').val();
    var name = $(this).parent().find('#name').val();
    var deletename = ('"' + name + '"');
    $('#DeleteMaintainMenuGroupModal').hide();
    $('#DeleteMaintainUserMenuGroupModal').hide();
    $('#DeleteMaintainCategoryComplaintModal').hide();
    $('#DeleteMaintainComplaintFromModal').hide();
    $('#DeleteMaintainLocationGroupModal').hide();
    $('#DeleteMaintainLocationModal').hide();
    $('#DeleteMaintainDepartment1Modal').hide();
    $('#DeleteMaintainDepartment2Modal').hide();
    $('#DeleteMaintainSectionModal').hide();
    $('#DeleteMaintainShopModal').hide();
    $('#DeleteMaintainShopGroupModal').hide();
    $('#DeleteMaintainShopPositionGroupModal').hide();
    $('#DeleteMaintainPositionModal').show();
    $('#DeleteMaintainPositionId').val(id);
    $('#DeleteMaintainPositionName').html(deletename);
});



/*--********** ErrorMaintainModal **********--*/
$(window).on('load', function () {
    $('#ErrorMaintainModal').modal('show');
});


/*--********** SearchMaintainModal **********--*/
$('#SearchMaintainMenuGroupModal').on('keyup', function () {
    var value = $(this).val().toLowerCase();
    $('.TableMaintainMenuGroupModal tr').each(function (index) {
        if (index !== 0) {

            $row = $(this);

            var id1 = $row.find('td:first').text().toLowerCase();

            if (id1.includes(value) != 1) {
                $row.hide();
            }
            else {
                $row.show();
            }
        }
    });
});


$('#SearchMaintainUserMenuGroupModal').on('keyup', function () {
    var value = $(this).val().toLowerCase();
    $('.TableMaintainUserMenuGroupModal tr').each(function (index) {
        if (index !== 0) {

            $row = $(this);

            var id1 = $row.find('td:first').text().toLowerCase();
            var id2 = $row.find('td:nth-child(2)').text().toLowerCase();

            if (id1.includes(value) != 1 && id2.includes(value) != 1) {
                $row.hide();
            }
            else {
                $row.show();
            }
        }
    });
});

$('#SearchMaintainCategoryComplaintModal').on('keyup', function () {
    var value = $(this).val().toLowerCase();
    $('.TableMaintainCategoryComplaintModal tr').each(function (index) {
        if (index !== 0) {

            $row = $(this);

            var id1 = $row.find('td:first').text().toLowerCase();
            var id2 = $row.find('td:nth-child(2)').text().toLowerCase();
            if (id1.includes(value) != 1 && id2.includes(value) != 1) {
                $row.hide();
            }
            else {
                $row.show();
            }
        }
    });
});

$('#SearchMaintainComplaintFromModal').on('keyup', function () {
    var value = $(this).val().toLowerCase();
    $('.TableMaintainComplaintFromModal tr').each(function (index) {
        if (index !== 0) {

            $row = $(this);

            var id1 = $row.find('td:first').text().toLowerCase();
            var id2 = $row.find('td:nth-child(2)').text().toLowerCase();
            if (id1.includes(value) != 1 && id2.includes(value) != 1) {
                $row.hide();
            }
            else {
                $row.show();
            }
        }
    });
});

$('#SearchMaintainLocationGroupModal').on('keyup', function () {
    var value = $(this).val().toLowerCase();
    $('.TableMaintainLocationGroupModal tr').each(function (index) {
        if (index !== 0) {

            $row = $(this);

            var id = $row.find('td:first').text().toLowerCase();

            if (id.includes(value) != 1) {
                $row.hide();
            }
            else {
                $row.show();
            }
        }
    });
});


$('#SearchMaintainLocationModal').on('keyup', function () {
    var value = $(this).val().toLowerCase();
    $('.TableMaintainLocationModal tr').each(function (index) {
        if (index !== 0) {

            $row = $(this);

            var id1 = $row.find('td:first').text().toLowerCase();
            var id2 = $row.find('td:nth-child(2)').text().toLowerCase();
            if (id1.includes(value) != 1 && id2.includes(value) != 1) {
                $row.hide();
            }
            else {
                $row.show();
            }
        }
    });
});


$('#SearchMaintainDepartment1Modal').on('keyup', function () {
    var value = $(this).val().toLowerCase();
    $('.TableMaintainDepartment1Modal tr').each(function (index) {
        if (index !== 0) {

            $row = $(this);

            var id1 = $row.find('td:first').text().toLowerCase();
            var id2 = $row.find('td:nth-child(2)').text().toLowerCase();
            if (id1.includes(value) != 1 && id2.includes(value) != 1) {
                $row.hide();
            }
            else {
                $row.show();
            }
        }
    });
});


$('#SearchMaintainDepartment2Modal').on('keyup', function () {
    var value = $(this).val().toLowerCase();
    $('.TableMaintainDepartment2Modal tr').each(function (index) {
        if (index !== 0) {

            $row = $(this);

            var id1 = $row.find('td:first').text().toLowerCase();
            var id2 = $row.find('td:nth-child(2)').text().toLowerCase();
            if (id1.includes(value) != 1 && id2.includes(value) != 1) {
                $row.hide();
            }
            else {
                $row.show();
            }
        }
    });
});


$('#SearchMaintainSectionModal').on('keyup', function () {
    var value = $(this).val().toLowerCase();
    $('.TableMaintainSectionModal tr').each(function (index) {
        if (index !== 0) {

            $row = $(this);

            var id1 = $row.find('td:first').text().toLowerCase();
            var id2 = $row.find('td:nth-child(2)').text().toLowerCase();
            var id3 = $row.find('td:nth-child(3)').text().toLowerCase();
            if (id1.includes(value) != 1 && id2.includes(value) != 1 && id3.includes(value) != 1) {
                $row.hide();
            }
            else {
                $row.show();
            }
        }
    });
});


$('#SearchMaintainShopModal').on('keyup', function () {
    var value = $(this).val().toLowerCase();
    $('.TableMaintainShopModal tr').each(function (index) {
        if (index !== 0) {

            $row = $(this);

            var id1 = $row.find('td:first').text().toLowerCase();
            var id2 = $row.find('td:nth-child(2)').text().toLowerCase();
            var id3 = $row.find('td:nth-child(3)').text().toLowerCase();
            var id4 = $row.find('td:nth-child(4)').text().toLowerCase();
            var id5 = $row.find('td:nth-child(5)').text().toLowerCase();
            if (id1.includes(value) != 1 && id2.includes(value) != 1 && id3.includes(value) != 1 && id4.includes(value) != 1 && id5.includes(value) != 1) {
                $row.hide();
            }
            else {
                $row.show();
            }
        }
    });
});


$('#SearchMaintainShopGroupModal').on('keyup', function () {
    var value = $(this).val().toLowerCase();
    $('.TableMaintainShopGroupModal tr').each(function (index) {
        if (index !== 0) {

            $row = $(this);

            var id = $row.find('td:first').text().toLowerCase();
            if (id.includes(value) != 1) {
                $row.hide();
            }
            else {
                $row.show();
            }
        }
    });
});


$('#SearchMaintainShopPositionGroupModal').on('keyup', function () {
    var value = $(this).val().toLowerCase();
    $('.TableMaintainShopPositionGroupModal tr').each(function (index) {
        if (index !== 0) {

            $row = $(this);

            var id = $row.find('td:first').text().toLowerCase();
            if (id.includes(value) != 1) {
                $row.hide();
            }
            else {
                $row.show();
            }
        }
    });
});


$('#SearchMaintainPositionModal').on('keyup', function () {
    var value = $(this).val().toLowerCase();
    $('.TableMaintainPositionModal tr').each(function (index) {
        if (index !== 0) {

            $row = $(this);

            var id1 = $row.find('td:first').text().toLowerCase();
            var id2 = $row.find('td:nth-child(2)').text().toLowerCase();
            var id3 = $row.find('td:nth-child(3)').text().toLowerCase();
            if (id1.includes(value) != 1 && id2.includes(value) != 1 && id3.includes(value) != 1) {
                $row.hide();
            }
            else {
                $row.show();
            }
        }
    });
});

/*Function*/
function Department2Id(id) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/Admin/Department2?id=' + id,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (result) {
                resolve(result);
            },
            error: function (error) {
                reject(error);
            }
        });
    });
}

function PositionId(id) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/Admin/PositionMaintain?id=' + id,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (result) {
                resolve(result);
            },
            error: function (error) {
                reject(error);
            }
        });
    });
}

/*--********** SelectMaintainModal **********--*/
$('.SelectSectionDepartment1').on('change', function () {
    var id = $(this).val();
    $('.SelectSectionDepartment2 select').empty();
    $('.SelectSectionDepartment2 select').append('<option value="">เลือกฝ่าย 2</option>');

    Department2Id(id)
        .then(function (Department2IdFunction) {
            if (Department2IdFunction.length > 0) {
                $.each(Department2IdFunction, function (i, department2iddata) {
                    $('.SelectSectionDepartment2 select').append('<option value=' + department2iddata.id + '>' + department2iddata.name + '</option>');
                    $('.SelectSectionDepartment2').attr('hidden', false);
                    $('.SelectSectionDepartment2 select').attr('disabled', false);
                });
            }
            else {
                $('.SelectSectionDepartment2').attr('hidden', true);
                $('.SelectSectionDepartment2 select').attr('disabled', true);
            }
        })
        .catch(function (error) {
            console.error(error);
            throw error;
        });
});


$('.SelectApproveUserSection').on('change', function () {
    var id = $(this).val();
    $('.SelectApproveUserPosition select').empty();
    $('.SelectApproveUserPosition select').append('<option value="">เลือกตำแหน่ง</option>');

    PositionId(id)
        .then(function (PositionIdFunction) {
            if (PositionIdFunction.length > 0) {
                $.each(PositionIdFunction, function (i, positioniddata) {
                    $('.SelectApproveUserPosition select').append('<option value=' + positioniddata.id + '>' + positioniddata.name + '</option>');
                    $('.SelectApproveUserPosition').attr('hidden', false);
                    $('.SelectApproveUserPosition select').attr('disabled', false);
                });
            }
            else {
                $('.SelectApproveUserPosition').attr('hidden', true);
                $('.SelectApproveUserPosition select').attr('disabled', true);
            }
        })
        .catch(function (error) {
            console.error(error);
            throw error;
        });
});


$('.SelectRequestUserSection').on('change', function () {
    var id = $(this).val();
    $('.SelectRequestUserPosition select').empty();
    $('.SelectRequestUserPosition select').append('<option value="">เลือกตำแหน่ง</option>');

    PositionId(id)
        .then(function (PositionIdFunction) {
            if (PositionIdFunction.length > 0) {
                $.each(PositionIdFunction, function (i, positioniddata) {
                    $('.SelectRequestUserPosition select').append('<option value=' + positioniddata.id + '>' + positioniddata.name + '</option>');
                    $('.SelectRequestUserPosition').attr('hidden', false);
                    $('.SelectRequestUserPosition select').attr('disabled', false);
                });
            }
            else {
                $('.SelectRequestUserPosition').attr('hidden', true);
                $('.SelectRequestUserPosition select').attr('disabled', true);
            }
        })
        .catch(function (error) {
            console.error(error);
            throw error;
        });
});



/*--********** SelectEditMaintainModal **********--*/
$('.SelectEditSectionDepartment1').on('change', function () {
    var id = $(this).val();
    $('.SelectEditSectionDepartment2 select').empty();
    $('.SelectEditSectionDepartment2 select').append('<option value="">เลือกฝ่าย 2</option>');

    Department2Id(id)
        .then(function (Department2IdFunction) {
            if (Department2IdFunction.length > 0) {
                $.each(Department2IdFunction, function (i, department2iddata) {
                    $('.SelectEditSectionDepartment2 select').append('<option value=' + department2iddata.id + '>' + department2iddata.name + '</option>');
                    $('.SelectEditSectionDepartment2').attr('hidden', false);
                    $('.SelectEditSectionDepartment2 select').attr('disabled', false);
                });
            }
            else {
                $('.SelectEditSectionDepartment2').attr('hidden', true);
                $('.SelectEditSectionDepartment2 select').attr('disabled', true);
            }
        })
        .catch(function (error) {
            console.error(error);
            throw error;
        });
});


$('.SelectEditApproveUserSection').on('change', function () {
    var id = $(this).val();
    $('.SelectEditApproveUserPosition select').empty();
    $('.SelectEditApproveUserPosition select').append('<option value="">เลือกตำแหน่ง</option>');

    PositionId(id)
        .then(function (PositionIdFunction) {
            if (PositionIdFunction.length > 0) {
                $.each(PositionIdFunction, function (i, positioniddata) {
                    $('.SelectEditApproveUserPosition select').append('<option value=' + positioniddata.id + '>' + positioniddata.name + '</option>');
                    $('.SelectEditApproveUserPosition').attr('hidden', false);
                    $('.SelectEditApproveUserPosition select').attr('disabled', false);
                });
            }
            else {
                $('.SelectEditApproveUserPosition').attr('hidden', true);
                $('.SelectEditApproveUserPosition select').attr('disabled', true);
            }
        })
        .catch(function (error) {
            console.error(error);
            throw error;
        });
});


$('.SelectEditRequestUserSection').on('change', function () {
    var id = $(this).val();
    $('.SelectEditRequestUserPosition select').empty();
    $('.SelectEditRequestUserPosition select').append('<option value="">เลือกตำแหน่ง</option>');

    PositionId(id)
        .then(function (PositionIdFunction) {
            if (PositionIdFunction.length > 0) {
                $.each(PositionIdFunction, function (i, positioniddata) {
                    ('.SelectEditRequestUserPosition select').append('<option value=' + positioniddata.id + '>' + positioniddata.name + '</option>');
                    $('.SelectEditRequestUserPosition').attr('hidden', false);
                    $('.SelectEditRequestUserPosition select').attr('disabled', false);
                });
            }
            else {
                $('.SelectEditRequestUserPosition').attr('hidden', true);
                $('.SelectEditRequestUserPosition select').attr('disabled', true);
            }
        })
        .catch(function (error) {
            console.error(error);
            throw error;
        });
});



/*--********** SwitchMaintainModal **********--*/
$('#SwitchPositionShopPositionGroup').on('change', function () {
    if ($(this).is(':checked')) {
        $('.SelectPositionShopPositionGroup').attr({ 'hidden': false, 'disabled': false });
    }
    else {
        $('.SelectPositionShopPositionGroup').attr({ 'hidden': true, 'disabled': true });
    }
});



/*--********** SwitchEditMaintainModal **********--*/
$('#EditSwitchPositionShopPositionGroup').on('change', function () {
    if ($(this).is(':checked')) {
        $('.EditSelectPositionShopPositionGroup').attr({ 'hidden': false, 'disabled': false });
    }
    else {
        $('.EditSelectPositionShopPositionGroup').attr({ 'hidden': true, 'disabled': true });
    }
});



/*--********** RadioMaintainModal **********--*/
$('#createradiomaintainrequestuser').on('click', function () {
    if ($(this).is(':checked')) {
        $('.SelectApproveGroupRequestUser').attr('hidden', false);
        $('.SelectApproveGroupRequestUser select').attr('disabled', false);
        $('.SelectApproveGroupRequestShop').attr('hidden', true);
        $('.SelectApproveGroupRequestShop select').attr('disabled', true);
    }
    else {
        $('.SelectApproveGroupRequestUser').attr('hidden', true);
        $('.SelectApproveGroupRequestUser select').attr('disabled', true);
        $('.SelectApproveGroupRequestShop').attr('hidden', false);
        $('.SelectApproveGroupRequestShop select').attr('disabled', false);
    }
});


$('#createradiomaintainrequestshop').on('click', function () {
    if ($(this).is(':checked')) {
        $('.SelectApproveGroupRequestUser').attr('hidden', true);
        $('.SelectApproveGroupRequestUser select').attr('disabled', true);
        $('.SelectApproveGroupRequestShop').attr('hidden', false);
        $('.SelectApproveGroupRequestShop select').attr('disabled', false);
    }
    else {
        $('.SelectApproveGroupRequestUser').attr('hidden', false);
        $('.SelectApproveGroupRequestUser select').attr('disabled', false);
        $('.SelectApproveGroupRequestShop').attr('hidden', true);
        $('.SelectApproveGroupRequestShop select').attr('disabled', true);
    }
});




/*--********** SwitchExcelMaintainModal **********--*/
$('#SwitchExcelCreateMaintainMenuGroup').on('change', function () {
    if ($(this).is(':checked')) {
        $('.inputExcelCreateMaintainMenuGroup').attr({ 'hidden': false, 'disabled': false });
        $('.inputCreateMaintainMenuGroup').attr({ 'hidden': true, 'disabled': true });
    }
    else {
        $('.inputExcelCreateMaintainMenuGroup').attr({ 'hidden': true, 'disabled': true });
        $('.inputCreateMaintainMenuGroup').attr({ 'hidden': false, 'disabled': false });
    }
});

$('#SwitchExcelCreateMaintainUserMenuGroup').on('change', function () {
    if ($(this).is(':checked')) {
        $('.inputExcelCreateMaintainUserMenuGroup').attr({ 'hidden': false, 'disabled': false });
        $('.inputCreateMaintainUserMenuGroup').attr({ 'hidden': true, 'disabled': true });
    }
    else {
        $('.inputExcelCreateMaintainUserMenuGroup').attr({ 'hidden': true, 'disabled': true });
        $('.inputCreateMaintainUserMenuGroup').attr({ 'hidden': false, 'disabled': false });
    }
});

$('#SwitchExcelCreateMaintainCategoryComplaint').on('change', function () {
    if ($(this).is(':checked')) {
        $('.inputExcelCreateMaintainCategoryComplaint').attr({ 'hidden': false, 'disabled': false });
        $('.inputCreateMaintainCategoryComplaint').attr({ 'hidden': true, 'disabled': true });
    }
    else {
        $('.inputExcelCreateMaintainCategoryComplaint').attr({ 'hidden': true, 'disabled': true });
        $('.inputCreateMaintainCategoryComplaint').attr({ 'hidden': false, 'disabled': false });
    }
});

$('#SwitchExcelCreateMaintainComplaintFrom').on('change', function () {
    if ($(this).is(':checked')) {
        $('.inputExcelCreateMaintainComplaintFrom').attr({ 'hidden': false, 'disabled': false });
        $('.inputCreateMaintainComplaintFrom').attr({ 'hidden': true, 'disabled': true });
    }
    else {
        $('.inputExcelCreateMaintainComplaintFrom').attr({ 'hidden': true, 'disabled': true });
        $('.inputCreateMaintainComplaintFrom').attr({ 'hidden': false, 'disabled': false });
    }
});


$('#SwitchExcelCreateMaintainLocationGroup').on('change', function () {
    if ($(this).is(':checked')) {
        $('.inputExcelCreateMaintainLocationGroup').attr({ 'hidden': false, 'disabled': false });
        $('.inputCreateMaintainLocationGroup').attr({ 'hidden': true, 'disabled': true });
    }
    else {
        $('.inputExcelCreateMaintainLocationGroup').attr({ 'hidden': true, 'disabled': true });
        $('.inputCreateMaintainLocationGroup').attr({ 'hidden': false, 'disabled': false });
    }
});

$('#SwitchExcelCreateMaintainLocation').on('change', function () {
    if ($(this).is(':checked')) {
        $('.inputExcelCreateMaintainLocation').attr({ 'hidden': false, 'disabled': false });
        $('.inputCreateMaintainLocation').attr({ 'hidden': true, 'disabled': true });
    }
    else {
        $('.inputExcelCreateMaintainLocation').attr({ 'hidden': true, 'disabled': true });
        $('.inputCreateMaintainLocation').attr({ 'hidden': false, 'disabled': false });
    }
});

$('#SwitchExcelCreateMaintainDepartment1').on('change', function () {
    if ($(this).is(':checked')) {
        $('.inputExcelCreateMaintainDepartment1').attr({ 'hidden': false, 'disabled': false });
        $('.inputCreateMaintainDepartment1').attr({ 'hidden': true, 'disabled': true });
    }
    else {
        $('.inputExcelCreateMaintainDepartment1').attr({ 'hidden': true, 'disabled': true });
        $('.inputCreateMaintainDepartment1').attr({ 'hidden': false, 'disabled': false });
    }
});

$('#SwitchExcelCreateMaintainDepartment2').on('change', function () {
    if ($(this).is(':checked')) {
        $('.inputExcelCreateMaintainDepartment2').attr({ 'hidden': false, 'disabled': false });
        $('.inputCreateMaintainDepartment2').attr({ 'hidden': true, 'disabled': true });
    }
    else {
        $('.inputExcelCreateMaintainDepartment2').attr({ 'hidden': true, 'disabled': true });
        $('.inputCreateMaintainDepartment2').attr({ 'hidden': false, 'disabled': false });
    }
});

$('#SwitchExcelCreateMaintainSection').on('change', function () {
    if ($(this).is(':checked')) {
        $('.inputExcelCreateMaintainSection').attr({ 'hidden': false, 'disabled': false });
        $('.inputCreateMaintainSection').attr({ 'hidden': true, 'disabled': true });
    }
    else {
        $('.inputExcelCreateMaintainSection').attr({ 'hidden': true, 'disabled': true });
        $('.inputCreateMaintainSection').attr({ 'hidden': false, 'disabled': false });
    }
});

$('#SwitchExcelCreateMaintainShop').on('change', function () {
    if ($(this).is(':checked')) {
        $('.inputExcelCreateMaintainShop').attr({ 'hidden': false, 'disabled': false });
        $('.inputCreateMaintainShop').attr({ 'hidden': true, 'disabled': true });
    }
    else {
        $('.inputExcelCreateMaintainShop').attr({ 'hidden': true, 'disabled': true });
        $('.inputCreateMaintainShop').attr({ 'hidden': false, 'disabled': false });
    }
});

$('#SwitchExcelCreateMaintainShopGroup').on('change', function () {
    if ($(this).is(':checked')) {
        $('.inputExcelCreateMaintainShopGroup').attr({ 'hidden': false, 'disabled': false });
        $('.inputCreateMaintainShopGroup').attr({ 'hidden': true, 'disabled': true });
    }
    else {
        $('.inputExcelCreateMaintainShopGroup').attr({ 'hidden': true, 'disabled': true });
        $('.inputCreateMaintainShopGroup').attr({ 'hidden': false, 'disabled': false });
    }
});

$('#SwitchExcelCreateMaintainShopPositionGroup').on('change', function () {
    if ($(this).is(':checked')) {
        $('.inputExcelCreateMaintainShopPositionGroup').attr({ 'hidden': false, 'disabled': false });
        $('.inputCreateMaintainShopPositionGroup').attr({ 'hidden': true, 'disabled': true });
    }
    else {
        $('.inputExcelCreateMaintainShopPositionGroup').attr({ 'hidden': true, 'disabled': true });
        $('.inputCreateMaintainShopPositionGroup').attr({ 'hidden': false, 'disabled': false });
    }
});

$('#SwitchExcelCreateMaintainPosition').on('change', function () {
    if ($(this).is(':checked')) {
        $('.inputExcelCreateMaintainPosition').attr({ 'hidden': false, 'disabled': false });
        $('.inputCreateMaintainPosition').attr({ 'hidden': true, 'disabled': true });
    }
    else {
        $('.inputExcelCreateMaintainPosition').attr({ 'hidden': true, 'disabled': true });
        $('.inputCreateMaintainPosition').attr({ 'hidden': false, 'disabled': false });
    }
});