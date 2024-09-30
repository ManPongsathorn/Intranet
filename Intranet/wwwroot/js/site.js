const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));

$('[data-bs-toggle="tooltip"]').on('click', function () {
    const tooltip = bootstrap.Tooltip.getOrCreateInstance(this);
    tooltip.hide();
})

function toThaiDateString(date) {
    let dayNames = ["อาทิตย์", "จันทร์", "อังคาร", "พุธ", "พฤหัส", "ศุกร์", "เสาร์"];
    let monthNames = ["มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม.", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม"];

    let year = date.getFullYear();
    let month = monthNames[date.getMonth()];
    let numOfDay = date.getDate();
    let day = dayNames[date.getDay()];

    let hour = date.getHours().toString().padStart(2, "0");
    let minutes = date.getMinutes().toString().padStart(2, "0");

    return `วัน${day}ที่ ${numOfDay} ${month} ${year} ` + `เวลา ${hour}:${minutes} น.`;
}

async function DisplayGeneralUserConnection() {
    const UserConnectionFunction = await UserConnection();

    if (UserConnectionFunction) {
        $('.ListUserConnectionLayout').empty();
        $('.labelheaduserconnection_layout').html(`ผู้ใช้งาน (${UserConnectionFunction.length} คน)`);

        if (UserConnectionFunction.length > 0) {
            $('.ListUserConnectionLayout').attr('hidden', true);
            $('.UserLoadConnectionLayout').attr('hidden', false);

            for (const [indexuserconnectiondata, userconnectiondata] of UserConnectionFunction.entries()) {
                const profileImage = userconnectiondata.imageUser ? userconnectiondata.imageUser : 'ProfileUser_None.jpg';
                const profileLink = `/Account/Profile?id=${userconnectiondata.id}`;
                const fullName = `${userconnectiondata.firstNameTH} ${userconnectiondata.lastNameTH}`;

                const html = `
                    <div class="mt-3 d-flex flex-row align-items-center" id="${userconnectiondata.id}">
                        <a href="${profileLink}"><img class="userimageprofile_layout" src="/Image/users/profile/${profileImage}"/></a>
                        <div class="ms-2 d-flex flex-column">
                            <a class="usernameprofile_layout fw-bold text-decoration-none" href="${profileLink}">${fullName}</a>
                            <label class="labelestatus_layout fw-bold">กำลังใช้งาน</label>
                        </div>
                    </div>
                `;

                $('.ListUserConnectionLayout').append(html);
            }

            setTimeout(() => {
                $('.ListUserConnectionLayout').attr('hidden', false);
                $('.UserLoadConnectionLayout').attr('hidden', true);
            }, 1500);
        }
    }
}

$('.btnonetime').on('click', function () {
    $(this).attr('disabled', true);
});