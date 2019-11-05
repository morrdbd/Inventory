
var ObjectToFormData = function (obj, form, prepend) {
    prepend = prepend || "";
    var fd = form || new FormData();

    for (var key in obj) {
        if (Array.isArray(obj[key])) {
            for (var i = 0; i < obj[key].length; i++) {
                if (typeof (obj[key][i]) == 'object' && !(obj[key][i] instanceof File)) {
                    _prepend = (prepend) ? prepend + "." : "";
                    ObjectToFormData(obj[key][i], fd, _prepend + key + "[" + i + "]");
                }
                else {
                    _prepend = (prepend) ? prepend + "." : "";
                    fd.append(_prepend + key + "[" + i + "]", obj[key][i]);
                }
            }
        }
        else if (typeof (obj[key]) == 'object' && !(obj[key] instanceof File)) {
            ObjectToFormData(obj[key], fd, key);
        }
        else {
            _prepend = (prepend) ? prepend + "." : "";
            fd.append(_prepend + key, obj[key]);
        }
    }
    return fd;
}

jQuery(document).ready(function () {
    $('#change_pass_form').validate({
        rules: {
            CurrPass: {
                remote: {
                    url: mainvars.check_pass_url,
                    type: 'post',
                    data: {
                        password: function () {
                            return $('#CurrPass').val();
                        }
                    }
                }
            }
        },
        submitHandler: function (form) {
            var url = mainvars.change_pass_url;
            var data = $(form).serialize();
            $.post(url, data, function (response) {
                $('#change_pass_div').modal('hide');
                bootbox.alert(mainvars.pass_change_suc, function () {
                    window.location.href = mainvars.logout_url;
                });
            })
        }
    })

    $('#change_pass_link').click(function () {
        $('#change_pass_form').get(0).reset();
        $('#change_pass_form input').tooltip('destroy')
        $('.error').removeClass('error')
    })

    //-----------------------------------------------------------------------

    $.validator.addMethod("regex", function (value, element, regexp) {
        var re = new RegExp(regexp);
        return this.optional(element) || re.test(value);
    });

    $.validator.addMethod("notEqual", function (value, element, param) {
        return this.optional(element) || value != param;
    });

    String.prototype.toEnglishDigits = function () {
        var charCodeZero = '۰'.charCodeAt(0);
        return this.replace(/[۰-۹]/g, function (w) {
            return w.charCodeAt(0) - charCodeZero;
        });
    }

    $('body').on('keyup', 'input[dir=ltr]', function (e) {
        if (e.which != 9 && e.which != 8) {
            var value = $(this).val()
            $(this).val(value.toEnglishDigits())
        }
    })

    $(document).ajaxError(function (e, xhr) {
        if (xhr.status === 401) {
            bootbox.alert("<p style='color:red'>Oops, You are not authorized to do this!</p>", function () {
                window.location.href = mainvars.home_url;
            })
        }
        else if (xhr.status === 500) {
            bootbox.alert("<p style='color:red'>Oops, There was an internal server error!</p>")
        }
    });

    if (mainvars.pass_needs_change == 'True') {
        $('#change_pass_div').modal('show');
    }

    $('body').on('click', '.page-sidebar-menu a.disabled', function (e) {
        e.preventDefault();
    })

    Metronic.init();
    Layout.init();

    $('.datepicker, .input-daterange').datepicker({
        format: 'yyyy-mm-dd',
        endDate: new Date(),
        autoclose: true,
    })
    .on('changeDate', function (e) {
        $(this).removeClass('error');
    })

    if (mainvars.lang != 'en') {
        bootbox.addLocale('dr', {
            OK: mainvars.yes,
            CANCEL: mainvars.no,
            CONFIRM: mainvars.yes
        })
        bootbox.setLocale('dr');
    }

    var collapseFlag = true;
    $('#collapse').click(function () {
        $(collapseFlag == true ? '.collapse' : '.expand').click();
        $(this).text(collapseFlag == true ? mainvars.expand_panels : mainvars.collapse_panels);
        collapseFlag = !collapseFlag;
    })

    //var cookie = $.cookie("ARISv2.0");
    //if (cookie == undefined) {
    //    bootbox.alert("YOU MUST LOGIN FIRST", function () {
    //        window.location.href = "/";
    //    });
    //}
});