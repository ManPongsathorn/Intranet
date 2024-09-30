let editorcomplaint;
var countCharactersComplaint;

CKEDITOR.ClassicEditor.create(document.getElementById("Editor_Complaint"), {
    toolbar: {
        items: ['bold', 'italic', 'underline', '|', 'outdent', 'indent', '|', 'undo', 'redo',],
        shouldNotGroupWhenFull: true
    },
    language: 'th',
    placeholder: 'กรุณาใส่รายละเอียด',
    fontFamily: {
        options: [
            'Kanit', 'sans-serif'
        ],
        supportAllValues: true
    },
    removePlugins: [
        'CKBox',
        'CKFinder',
        'EasyImage',
        'RealTimeCollaborativeComments',
        'RealTimeCollaborativeTrackChanges',
        'RealTimeCollaborativeRevisionHistory',
        'PresenceList',
        'Comments',
        'TrackChanges',
        'TrackChangesData',
        'RevisionHistory',
        'Pagination',
        'WProofreader',
        'MathType',
        'SlashCommand',
        'Template',
        'DocumentOutline',
        'FormatPainter',
        'TableOfContents',
        'PasteFromOfficeEnhanced'
    ],
    wordCount: {
        onUpdate: stats => {
            countCharactersComplaint = stats.characters;
        }
    }
}).then(newEditor => {
    editorcomplaint = newEditor;
    editorcomplaint.model.document.on('change:data', () => {
        if (countCharactersComplaint > 0) {
            if (editorcomplaint.getData() != '') {
                var categorycomplaintid = $('.selectcategorycomplaint_complaint').find(':selected').val();

                if (categorycomplaintid > 0) {
                    $('.btn_complaint').attr('disabled', false);
                }
                else {
                    $('.btn_complaint').attr('disabled', true);
                }
            }
            else {
                $('.btn_complaint').attr('disabled', true);
            }
        }
        else {
            $('.btn_complaint').attr('disabled', true);
        }


    });
}).catch(error => {
    console.error(error);
});

/*Function*/
function CreateComplaint(categorycomplaintid, editorcomplaintdata) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/Blogpost/CreateComplaint?id=' + categorycomplaintid + '&data=' + editorcomplaintdata,
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

function ListCategoryComplaint() {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/Blogpost/ListCategoryComplaint',
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

function DisplayGeneralSelectCategoryComplaint() {
    $('.selectcategorycomplaint_complaint').empty();
    $('.selectcategorycomplaint_complaint').append('<option select val="0">เลือกหัวข้อการร้องเรียน</option>');

    ListCategoryComplaint()
        .then(function (ListCategoryComplaintFunction) {
            if (ListCategoryComplaintFunction.length > 0) {
                $.each(ListCategoryComplaintFunction, function (i, listcategorycomplaintdata) {
                    $('.selectcategorycomplaint_complaint').append($('<option>', {
                        value: listcategorycomplaintdata.id,
                        text: listcategorycomplaintdata.name
                    }));
                });
            }
        })
        .catch(function (error) {
            console.error(error);
            throw error;
        });
}

/*First*/
DisplayGeneralSelectCategoryComplaint();

$('.selectcategorycomplaint_complaint').on('change', function () {
    const editorcomplaintdata = editorcomplaint.getData();
    if (editorcomplaintdata != '') {
        $('.btn_complaint').attr('disabled', false);
    }
    else {
        $('.btn_complaint').attr('disabled', true);
    }
});

$('.btn_complaint').on('click', function () {
    const editorcomplaintdata = editorcomplaint.getData();
    var categorycomplaintid = $('.selectcategorycomplaint_complaint').find(':selected').val();
    if (editorcomplaintdata != '') {

        CreateComplaint(categorycomplaintid, editorcomplaintdata)
            .then(function (CreateComplaintFunction) {
                if (CreateComplaintFunction != null) {
                    editorcomplaint.setData('');
                    DisplayGeneralSelectCategoryComplaint();
                    $('#SuccessBlogPost').modal('show');
                    $('.headlabelsuccess_successblogpostmodal').html('สำเร็จ!');
                    $('.labelsuccess_successblogpostmodal').html('คุณได้ส่งคำร้องเรียนเรียบร้อยแล้ว');
                    $('.btnsuccess_successblogpostmodal').attr({ 'data-bs-dismiss': 'modal' });
                }
            })
            .catch(function (error) {
                console.error(error);
                throw error;
            });
    }
});