/*--********** ErrorUserManageModal **********--*/
$(window).on('load', function () {
    $('#ErrorUserManageModal').modal('show');
    $('.mainbody').addClass('d-grid');
});

/*Function*/
function Department2Id(id) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/Friend/Department2Id?id=' + id,
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

function SectionId(id1, id2) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/Friend/SectionId?id1=' + id1 + '&id2=' + id2,
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

function ShopId(id) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/Friend/ShopId?id=' + id,
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

function PositionId(id1, id2) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/Friend/PositionId?id1=' + id1 + '&id2=' + id2,
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

/*--********** SelectCreateModal **********--*/
$('.SelectCreateDepartment1UserManage').on('change', function () {
    var id = $(this).val();
    $('.SelectCreateDepartment2UserManage select').empty();
    $('.SelectCreateDepartment2UserManage select').append('<option value="">เลือกฝ่าย 2</option>');

    Department2Id(id)
        .then(function (Department2IdFunction) {
            if (Department2IdFunction.length > 0) {
                $.each(Department2IdFunction, function (i, department2iddata) {
                    $('.SelectCreateSectionUserManage').attr('hidden', true);
                    $('.SelectCreateSectionUserManage select').attr('disabled', true);
                    $('.SelectCreateDepartment2UserManage select').append('<option value=' + department2iddata.id + '>' + department2iddata.name + '</option>');
                    $('.SelectCreateDepartment2UserManage').attr('hidden', false);
                    $('.SelectCreateDepartment2UserManage select').attr('disabled', false);
                });
            }
            else {
                $('.SelectCreateDepartment2UserManage').attr('hidden', true);
                $('.SelectCreateDepartment2UserManage select').attr('disabled', true);
                $('.SelectCreateSectionUserManage select').empty();
                $('.SelectCreateSectionUserManage select').append('<option value="">เลือกแผนก</option>');

                SectionId(id)
                    .then(function (SectionIdFunction) {
                        if (SectionIdFunction.length > 0) {
                            $.each(SectionIdFunction, function (i, sectioniddata) {
                                $('.SelectCreateSectionUserManage select').append('<option value=' + sectioniddata.id + '>' + sectioniddata.name + '</option>');
                                $('.SelectCreateSectionUserManage').attr('hidden', false);
                                $('.SelectCreateSectionUserManage select').attr('disabled', false);
                            });
                        }
                        else {
                            $('.SelectCreateSectionUserManage').attr('hidden', true);
                            $('.SelectCreateSectionUserManage select').attr('disabled', true);
                        }
                    })
                    .catch(function (error) {
                        console.error(error);
                        throw error;
                    });
            }

            $('.SelectCreateShopUserManage').attr('hidden', true);
            $('.SelectCreateShopUserManage select').attr('disabled', true);
            $('.SelectCreatePositionUserManage').attr('hidden', true);
            $('.SelectCreatePositionUserManage select').attr('disabled', true);
        })
        .catch(function (error) {
            console.error(error);
            throw error;
        });
});


$('.SelectCreateDepartment2UserManage Select').on('change', function () {
    var id1 = $('.SelectCreateDepartment1UserManage').val();
    var id2 = $(this).val();
    $('.SelectCreateSectionUserManage select').empty();
    $('.SelectCreateSectionUserManage select').append('<option value="">เลือกแผนก</option>');

    SectionId(id1, id2)
        .then(function (SectionIdFunction) {
            if (SectionIdFunction.length > 0) {
                $.each(SectionIdFunction, function (i, sectioniddata) {
                    $('.SelectCreateSectionUserManage select').append('<option value=' + sectioniddata.id + '>' + sectioniddata.name + '</option>');
                    $('.SelectCreateSectionUserManage').attr('hidden', false);
                    $('.SelectCreateSectionUserManage select').attr('disabled', false);
                });
            }
            else {
                $('.SelectCreateSectionUserManage').attr('hidden', true);
                $('.SelectCreateSectionUserManage select').attr('disabled', true);
            }

            $('.SelectCreateShopUserManage').attr('hidden', true);
            $('.SelectCreateShopUserManage select').attr('disabled', true);
            $('.SelectCreatePositionUserManage').attr('hidden', true);
            $('.SelectCreatePositionUserManage select').attr('disabled', true);
        })
        .catch(function (error) {
            console.error(error);
            throw error;
        });
});


$('.SelectCreateSectionUserManage Select').on('change', function () {
    var id = $(this).val();
    $('.SelectCreateShopUserManage select').empty();
    $('.SelectCreateShopUserManage select').append('<option value="">เลือกสาขา</option>');

    ShopId(id)
        .then(function (ShopIdFunction) {
            if (ShopIdFunction.length > 0) {
                $.each(ShopIdFunction, function (i, shopiddata) {
                    $('.SelectCreateShopUserManage select').append('<option value=' + shopiddata.id + '>' + shopiddata.name + '</option>');
                    $('.SelectCreateShopUserManage').attr('hidden', false);
                    $('.SelectCreateShopUserManage select').attr('disabled', false);
                });
            }
            else {
                $('.SelectCreateShopUserManage').attr('hidden', true);
                $('.SelectCreateShopUserManage select').attr('disabled', true);
                $('.SelectCreatePositionUserManage select').empty();
                $('.SelectCreatePositionUserManage select').append('<option value="">เลือกตำแหน่ง</option>');

                var PositionIdFunction = PositionId(id);

                if (PositionIdFunction.length > 0) {
                    $.each(PositionIdFunction, function (i, positioniddata) {
                        $('.SelectCreatePositionUserManage select').append('<option value=' + positioniddata.id + '>' + positioniddata.name + '</option>');
                        $('.SelectCreatePositionUserManage').attr('hidden', false);
                        $('.SelectCreatePositionUserManage select').attr('disabled', false);
                    });
                }
                else {
                    $('.SelectCreatePositionUserManage').attr('hidden', true);
                    $('.SelectCreatePositionUserManage select').attr('disabled', true);
                }
            }
        })
        .catch(function (error) {
            console.error(error);
            throw error;
        });
});



$('.SelectCreateShopUserManage Select').on('change', function () {
    var id1 = $('.SelectCreateSectionUserManage Select').val();
    var id2 = $(this).val();
    $('.SelectCreatePositionUserManage select').empty();
    $('.SelectCreatePositionUserManage select').append('<option value="">เลือกตำแหน่ง</option>');

    PositionId(id1, id2)
        .then(function (PositionIdFunction) {
            if (PositionIdFunction.length > 0) {
                $.each(PositionIdFunction, function (i, positioniddata) {
                    $('.SelectCreatePositionUserManage select').append('<option value=' + positioniddata.id + '>' + positioniddata.name + '</option>');
                    $('.SelectCreatePositionUserManage').attr('hidden', false);
                    $('.SelectCreatePositionUserManage select').attr('disabled', false);
                });
            }
            else {
                $('.SelectCreatePositionUserManage').attr('hidden', true);
                $('.SelectCreatePositionUserManage select').attr('disabled', true);
            }
        })
        .catch(function (error) {
            console.error(error);
            throw error;
        });
});

/*DataTableUserManage*/
$.fn.DataTable.ext.pager.numbers_length = 5;

$('#TableUserManage').DataTable({
    dom: 'rtip',
    pagingType: 'full_numbers_no_ellipses',
    lengthMenu: [10],
    autoWidth: true,
    language: {
        emptyTable: '',
        info: 'แสดงรายการที่ _START_ - _END_ จากทั้งหมด _TOTAL_ รายการ',
        infoEmpty: 'แสดงรายการที่ 0 จากทั้งหมด 0 รายการ',
        infoFiltered: '',
        paginate: {
            first: 'หน้าแรก',
            last: 'หน้าสุดท้าย',
            next: '<i class="bi bi-chevron-right"></i>',
            previous: '<i class="bi bi-chevron-left"></i>'
        },
        zeroRecords: '',
    },

    initComplete: function (settings, json) {
        $('#TableUserManage').parents('.table-responsive').after('<div class="mt-3 d-flex justify-content-center OptionPaginateTableUserManage"></div>');
        $('#TableUserManage').parents('.table-responsive').after('<div class="mt-0 mt-md-4 mt-xl-0 d-flex justify-content-end OptionInfoTableUserManage"></div>');
        $('#TableUserManage_info').css({ 'color': 'var(--color7)' }).detach().appendTo('.OptionInfoTableUserManage');
        $('#TableUserManage_paginate').addClass('overflow-auto').detach().appendTo('.OptionPaginateTableUserManage');

    },
});

/*--********** SearchTicket **********--*/
$('#SearchUserManage').on('keyup', function () {

    var value = $(this).val().toLowerCase();

    $('#TableUserManage').DataTable().search(value).draw();

});

/*--********** SwitchExcelUserManageModal **********--*/
$('#SwitchExcelCreateUserManage').on('change', function () {
    if ($(this).is(':checked')) {
        $('.inputExcelCreateUserManage').attr({ 'hidden': false, 'disabled': false });
        $('.inputCreateUserManage').attr({ 'hidden': true, 'disabled': true });
    }
    else {
        $('.inputExcelCreateUserManage').attr({ 'hidden': true, 'disabled': true });
        $('.inputCreateUserManage').attr({ 'hidden': false, 'disabled': false });
    }
});
