////////////////////////////////////////////////// Global Variables //////////////////////////////////////////////


var pLoopflag;
var aLoopflag;
var deviceInfos;
var selectedDeviceIndex = 0;

var cb_EncryptOpt = 0;
var txt_EncryptKey;
var cb_ExtractExMode = 0;

var flagStatus;
var flagFingerOn;
var gSensorValid = "false";
var gIsCapturing = "false";
var gSensorOn = "false";
var gIsFingeOn = "false";
var gPreviewFaileCount = 0;
var gUserSelected = -1;

var captureClicked = "false";
var deviceReady = "false";

var urlStr = 'http://localhost:8084';
var pageID = 0;

function Init() {
    jQuery.ajax({
        type: "GET",
        url: urlStr + "/api/initDevice?dummy=" + Math.random(),
        dataType: "json",
        success: function (msg) {
            if (msg.retValue == 0) {
                deviceInfos = msg.ScannerInfos;
                CheckStatusLoop();
                PreviewOnChecked();
                SendParameter();
                deviceReady = "true";
                notify('device is ready now - click start capture', 'green');
                $('#btn_fp_start_capture').prop('disabled', false);
            }
        },
        error: function (request, status, error) {
            console.log(request, status, error);
        }
    });
}

function UnInit() {
    jQuery.ajax({
        type: "GET",
        url: urlStr + "/api/uninitDevice?dummy=" + Math.random(),
        dataType: "json",
        success: function (msg) {
            if (msg.retValue == 0) {
                deviceReady = "false";
                notify('device is not ready - click initialize', 'red');
                $('#btn_fp_start_capture').prop('disabled', true);
            }
        },
        error: function (request, status, error) {
            console.log(request, status, error)
        }
    });
}

function CheckStatusLoop() {
    if ($("#slt_ScannerList option:selected").attr("value") == undefined)
        selectedDeviceIndex = 0;
    else
        selectedDeviceIndex = $("#slt_ScannerList option:selected").attr("value");

    jQuery.ajax({
        type: "GET",
        url: urlStr + "/api/getScannerStatus?dummy=" + Math.random(),
        dataType: "json",
        data: {
            sHandle: deviceInfos[selectedDeviceIndex].DeviceHandle
        },
        success: function (msg) {
            if (msg.retValue == 0) {
                gSensorValid = msg.SensorValid;
                gIsCapturing = msg.IsCapturing;
                gSensorOn = msg.SensorOn;
                gIsFingeOn = msg.IsFingerOn;

                if (gIsCapturing != 'true' && captureClicked == 'true') {
                    notify('capturing stoped', 'blue');
                    $('#btn_fp_add').prop('disabled', false);
                    captureClicked = 'false';
                }
            }
        },
        error: function (request, status, error) {
            console.log(request, status, error)
        }
    });
    flagStatus = setTimeout(CheckStatusLoop, 1000);
}

function CheckStatus() {
    selectedDeviceIndex = $("#slt_ScannerList option:selected").attr("value");
    jQuery.ajax({
        type: "GET",
        url: urlStr + "/api/getScannerStatus?dummy=" + Math.random(),
        dataType: "json",
        data: {
            sHandle: deviceInfos[selectedDeviceIndex].DeviceHandle
        },
        success: function (msg) {
            if (msg.retValue == 0) {
                gSensorValid = msg.SensorValid;
                gIsCapturing = msg.IsCapturing;
                gSensorOn = msg.SensorOn;
            }
        },
        error: function (request, status, error) {
            console.log(request, status, error)
        }
    });
}

function isExistScannerHandle() {
    if (deviceInfos[selectedDeviceIndex].DeviceHandle == 0) {
        return false;
    } else {
        return true;
    }
}

function StartCapture() {
    if (isExistScannerHandle() == false) {
        return;
    }
    var delayVal = 30000;

    jQuery.ajax({
        type: "GET",
        url: urlStr + "/api/startCapturing?dummy=" + Math.random(),
        dataType: "json",
        data: {
            sHandle: deviceInfos[selectedDeviceIndex].DeviceHandle,
            id: pageID,
            resetTimer: delayVal
        },
        success: function (msg) {
            if (msg.retValue == 0) {
                if (gSensorOn == 'true' && deviceReady == 'true') {
                    notify('capturing started - put the finger on device', 'green');
                    captureClicked = "true";
                }
                CheckStatus();
                PreviewLoop();
            }
        },
        error: function (request, status, error) {
            console.log(request, status, error)
        }
    });
}

function PreviewLoop() {
    var sessionData = "&shandle=" + deviceInfos[selectedDeviceIndex].DeviceHandle + "&id=" + pageID;

    if (gIsCapturing == "true" && gSensorOn == "true" && gSensorValid == "true") {
        $("#Fpimg").removeAttr();
        var imgUrl = urlStr + "/img/PreviewImg.bmp?dummy=";
        $("#Fpimg").attr("src", imgUrl + Math.random() + sessionData);

        pLoopflag = setTimeout(PreviewLoop, 100);
        gPreviewFaileCount = 0;
    }
    else if (gPreviewFaileCount < 60) {
        pLoopflag = setTimeout(PreviewLoop, 1000);
        gPreviewFaileCount++;
    }
    else {
        gPreviewFaileCount = 0;
        $('#Fpimg').attr('src', 'data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///ywAAAAAAQABAAACAUwAOw==');
    }
}

function InitPage() {
    pageID = Math.random();
    jQuery.ajax({
        type: "GET",
        url: urlStr + "/api/createSessionID?dummy=" + Math.random(),
        dataType: "json",
        success: function (msg) {
            var current = new Date();
            var expires = new Date();
            expires.setTime(new Date(Date.parse(current) + 1000 * 60 * 60));
            var cookieStr = "username=" + msg.sessionId + "; expires=" + expires.toUTCString();
            document.cookie = cookieStr;
        },
        error: function (request, status, error) {
            console.log(request, status, error)
        }
    });
}

function DeletePage() {
    var current = new Date();
    document.cookie = "username=; expires=" + current.toUTCString();
    jQuery.ajax({
        type: "GET",
        async: false,
        url: urlStr + "/api/sessionClear?dummy=" + Math.random(),
        data: {
            id: pageID
        }
    });
}

function PreviewOnChecked() {
    var sessionData = "&shandle=" + deviceInfos[selectedDeviceIndex].DeviceHandle + "&id=" + pageID;
    var imgUrl = urlStr + "/img/PreviewStreaming.bmp?dummy=";
    document.getElementById('Fpimg').setAttribute("src", imgUrl + Math.random() + sessionData);
}

function SendParameter() {
    $(document).ready(function () {
        jQuery.ajax({
            type: "GET",
            url: urlStr + "/api/setParameters?dummy=" + Math.random(),
            dataType: "json",
            data: {
                templateType: 2002, // iso template
            },
            success: function (msg) {
                console.log("setParameters", msg.retString);
            },
            error: function (request, status, error) {
                console.log(request, status, error);
            }
        });
    });
}

/////////////////MY CODES///////////////////////////

function GetTmpAndImg() {
    if (isExistScannerHandle() == false) {
        return;
    }
    jQuery.when(
        ////////////// GET TEMPLATE ///////////////
        jQuery.ajax({
            type: "GET",
            url: urlStr + "/api/getTemplateData?dummy=" + Math.random(),
            dataType: "json",
            data: {
                sHandle: deviceInfos[selectedDeviceIndex].DeviceHandle,
                id: pageID,
                encrypt: cb_EncryptOpt,
                encryptKey: txt_EncryptKey,
                extractEx: cb_ExtractExMode,
                qualityLevel: 1,
            },
            success: function (msg) {
                if (msg.retValue == 0) {
                    $('#TMP').css('background-color', 'green');
                    FP_TMP = msg.templateBase64;
                    TMP_READY = true;
                }
            },
            error: function (request, status, error) {
                console.log(request, status, error)
            }
        }),
        /////////////////// GET IMAGE /////////////////
        jQuery.ajax({
            type: "GET",
            url: urlStr + "/api/getImageData?dummy=" + Math.random(),
            dataType: "json",
            data: {
                sHandle: deviceInfos[selectedDeviceIndex].DeviceHandle,
                id: pageID,
                fileType: 1,
                compressionRatio: 0.7,
            },
            success: function (msg) {
                if (msg.retValue == 0) {
                    $('#IMG').css('background-color', 'green');
                    IMG_READY = true;
                    FP_IMG = msg.imageBase64;
                }
            },
            error: function (request, status, error) {
                console.log(request, status, error)
            }
        })
    )
    .done(Extract_Done)
}

function Extract_Done() {
    if (TMP_READY == true && IMG_READY == true) {
        var FP_NAME = $(finger_obj).data().name;
        var data = {
            "Template": FP_TMP,
            "Image": FP_IMG,
            "Name": FP_NAME,
        };
        delete_finger(data.Name);
        finger_data.push(data);
        var img_src = 'data:image/bmp;base64,' + FP_IMG;
        $(finger_obj).attr('src', img_src).addClass('has_data').removeClass('hover')
        $('#capture_shadow').show();
        $('#btn_fp_add').prop('disabled', true);
        notify('fingerprint added - select another finger', 'green')
    } else {
        notify('unable to add fingerprint - click start capture', 'red')
    }
}

var FP_IMG;
var FP_TMP;
var TMP_READY = false;
var IMG_READY = false;

function delete_finger(fp_name) {
    finger_data.forEach(function (row, index, array) {
        if (row.Name == fp_name) {
            array.splice(index, 1);
        }
    })
}

function capture_finger() {
    $('.finger_icons img').removeClass('hover')
    $(finger_obj).addClass('hover');
    $('#capture_shadow').hide();

    $('.signal').css('background-color', 'red');
    TMP_READY = false;
    IMG_READY = false;

    if (gSensorOn == 'true' && deviceReady == 'true') {
        notify('device is ready - click start capture', 'green');
        $('#btn_fp_start_capture').prop('disabled', false);
    } else {
        notify('device is not ready - click initialize', 'red');
        $('#btn_fp_start_capture').prop('disabled', true);
    }
}

function CancelCapture() {
    $(finger_obj).removeClass('hover');
    $('#capture_shadow').show();
    notify('select the finger', 'blue')
}

var finger_data = [];

function notify(text, color) {
    $('#notif_p').text(text).removeClass().addClass(color);
}
var finger_obj;

$(function () {
    DeletePage()

    notify('select the finger', 'blue');

    $('.finger_icons img').click(function () {
        finger_obj = $(this);
        if ($(this).hasClass('has_data')) {
            $('#fp_popup').modal('show');
            $('#fp_big_img').attr('src', $(this).attr('src'));
        } else {
            capture_finger();
        }
    })

    $('body').on('click', '#btn_fp_delete', function (e) {
        e.preventDefault()
        $(this).confirmation('show');
    })

    $('body').on('confirmed.bs.confirmation', '#btn_fp_delete', function (e) {
        var fp_name = $(finger_obj).data().name;
        delete_finger(fp_name);
        $(finger_obj).attr('src', vars.fp_icon).removeClass('has_data');
        $('#fp_popup').modal('hide');
    })

    $('body').on('click', '#btn_fp_recapture', function (e) {
        e.preventDefault();
        capture_finger();
        $('#fp_popup').modal('hide');
    })

    function do_save_fingerprints(response) {
        if (response == true) {
            $('#btn_fp_submit').button('reset');
            bootbox.alert('Fingerprints were added successfully!', function () {
                if (temp_id == 0) {
                    window.location.href = vars.case_details_url_with_id;
                } else {
                    window.location.href = vars.case_details_url + '#id=' + temp_id;
                }
            })
        } else {
            toastr.error('Unable to save fingerprints!', 'Error');
        }
    }

    $('#btn_fp_submit').click(function (e) {
        e.preventDefault();
        if (finger_data.length == 0) {
            toastr.error('Error', 'There is no fingerprints!');
            return;
        }
        $('#btn_fp_submit').button('loading');
        var json_data = JSON.stringify(finger_data);

        if (temp_id == 0) {
            $.ajax({
                type: 'POST',
                url: vars.submit_url_with_id,
                data: json_data,
                contentType: 'application/json',
                success: do_save_fingerprints,
                error: function () {
                    toastr.error('Unable to save fingerprints!', 'Error');
                }
            })
        } else {
            open_indexdb(function () {
                insert_fingerprints(temp_id, person_index, finger_data, do_save_fingerprints)
            })
        }
    })

    var temp_id = get_url_var('id');
    var person_index = get_url_var('index');

    function do_load_data(data) {
        if (data != false) {
            $('.finger_icons img').each(function (index, obj) {
                var fp_name = $(obj).data().name;
                data.forEach(function (fp_obj) {
                    if (fp_obj.Name == fp_name) {
                        $(obj).attr('src', 'data:image/bmp;base64,' + fp_obj.Image).addClass('has_data');
                    }
                })
            })
            finger_data = data;
        }
    }

    if (temp_id == 0) {
        $.get(vars.get_fps_url, do_load_data)
    }

    // OFFLINE DATA

    if (temp_id != 0) {
        temp_id = parseInt(temp_id);
        person_index = parseInt(person_index);
        $('#js_case_link').attr('href', vars.case_details_url + '#id=' + temp_id);

        open_indexdb(function () {
            individual_details(temp_id, person_index, vars.lang, function (obj) {
                if (obj != undefined && obj != false) {
                    $('#js_forename').text(obj.Forename);
                    $('#js_father_name').text(obj.FatherName);
                    $('#js_gender').text(obj._gender);
                    $('#js_age').text(obj.Age);
                    $('#js_marital').text(obj._marital);

                    if (obj.Photo != undefined) {
                        $('#js_case_photo').attr('src', obj.Photo);
                    }
                }
            })
            get_fingerprints(temp_id, person_index, do_load_data)
        })
    }
})