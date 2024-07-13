function OpenAvatarInput() {
    $("#UserAvatar").click();
}

function UploadUserAvatar(url) {
    var avatarInput = document.getElementById("UserAvatar");
    if (avatarInput.files.length) {
        var file = avatarInput.files[0];
        var formData = new FormData();
        formData.append("UserAvatar", file);

        $.ajax({
            url: url,
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            beforeSend: function () {
            },
            success: function (response) {
                if (response.status === "Success") {
                    swal({
                        title: "اعلان",
                        text: "تصویر با موفقیت انجام شـد !",
                        icon: "success",
                        button: "باشه"
                    });
                    setTimeout(function () {
                        location.reload();
                    }, 1000);
                } else {
                    swal({
                        title: "خطا",
                        text: "#01 عملیات با خطا مواجه شد ! لطفا دوباره تلاش کنید .",
                        icon: "error",
                        button: "باشه"
                    });
                }
            },
            error: function () {
                swal({
                    title: "خطا",
                    text: "#02 عملیات با خطا مواجه شد ! لطفا دوباره تلاش کنید .",
                    icon: "error",
                    button: "باشه"
                });
            }
        });
    }
}

function AnswerQuestionFormDone(response) {
    if (response.status === "Success") {
        swal("اعلان", "پاسخ شما با موفقیت ثبت شد .", "success");
    } else if (response.status === "EmptyAnswer") {
        swal("هشدار", "متن پاسخ شما نمی تواند خالی  باشد .", "warning");
    } else if (response.status === "Error") {
        swal("خطا", "خطایی رخ داده است لطفا مجدد تلاش کنید .", "error");
    }
}

function LoadQuestionCategoryModal(id) {
    $.ajax({
        url: "/load-qc-modal",
        type: "get",
        data: {
            id: id
        },
        success: function (response) {
            $("#QuestionCategoryModal").html(response);
            $("#QuestionCategoryForm").data("validator", null);
            $.validate.unobtrusive.parse("#QuestionCategoryForm");
            $("#exampleModal").modal("#show");
        },
    });
}

