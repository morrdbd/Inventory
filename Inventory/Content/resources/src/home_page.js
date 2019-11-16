$(function () {
    // LOGIN FORM
    $("#login_form").validate({
        invalidHandler: function (event, validator) {
            $('#err').text(pass_wrong).show().delay(1000).fadeOut()
        },
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        submitHandler: function (form) {
            $('#btn_login').button("loading");
            var url = $(form).attr("action");
            var data = $(form).serialize();
            $.ajax({
                url: url,
                data: data,
                type: "post",
                success: function (response) {
                    if (response != false) {
                        window.location.href = dashboard_url;
                    } else {
                        $('#err').text(pass_wrong).show().delay(1000).fadeOut()
                    }
                },
                complete: function () {
                    $('#btn_login').button("reset");
                }
            })
        }
    })
    /////////////////
    var cookie = $.cookie("Inventory");
    $('#login_link').text(cookie == undefined ? login_text : dashboard_text);
    $('#login_link').on('click', function (e) {
        e.preventDefault();
        if (cookie == undefined) {
            $('#login_div').modal('show');
        } else {
            window.location.href = dashboard_url;
        }
    })  
})