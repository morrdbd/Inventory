//When file select clicked it choose the file and preview it if it is an image else just show pdf icon image
$("#FileContent").change(function () {
    cleanPrview(false);
    var Files = this.files;
    if (Files && Files[0]) {
        cleanPrview(true);
        var fileType = Files[0].type;
        var reader = new FileReader;
        var image = new Image;
        reader.readAsDataURL(Files[0]);
        reader.onload = function (_file) {
            if (fileType == "application/pdf") {
                $("#ScanPreviewDiv").append('<img id="ScanPreview" src="/Content/resources/img/pdfPreivew.png" width="260px" height="160px" alt="@Labels.ScanPreview" />');
            } else {
                image.src = _file.target.result;
                image.onload = function () {
                    $("#ScanPreviewDiv").append('<img id="ScanPreview" src="' + _file.target.result + '" width="260px" height="160px" alt="@Labels.ScanPreview" />');
                }
            }

        }
    }
});
//When a recod show scan clicked it display it in viewScan div
$("body").on("click", ".showFile_row", function (e) {
    e.preventDefault();
    var filepath = $(this).data().filepath;
    $("#viewScan").empty();
    if (filepath.lastIndexOf(".pdf") != -1) {
        $("#printScan").hide();
        $("#viewScan").append('<object type="application/pdf" data="'+filepath+'?#zoom=100&view=FitH&toolbar=1" width="100%" height="600px"></object>');
    } else {
        $("#printScan").show();
        $("#viewScan").append('<img src="' + filepath + '" width="100%" height="100%" alt="' + lblScanPreviewv + '" />');
    }
})
//When the button with ID printScan click, it send the content of viewScan div to print preview
$("body").on("click", "#printScan", function (e) {
    e.preventDefault();
    var printContent = $("#viewScan").html();
    var newWin = window.open("");
    newWin.document.write(printContent);
    newWin.print();
    newWin.close();
});

//Hints for file preview when file not selected
function cleanPrview(haveImage) {
    if (haveImage) {
        $("#ScanPreviewDiv").empty();
    } else {
        $("#ScanPreviewDiv").empty();
        $("#ScanPreviewDiv").append('<div style="width:280px;height:190px; background-color: gray"> <p>' + lblScanPreviewv + '</p></div>');
    }
}

$("#btnNewRecord").on('click', function () {
    cleanPrview(false);
})
function PreviewScan(filePath)
{
    cleanPrview(true);
    $("#ScanPreviewDiv").append('<img id="ScanPreview" src="' + filePath + '" width="280px" height="190px" alt="' + lblScanPreviewv + '" />');
}
