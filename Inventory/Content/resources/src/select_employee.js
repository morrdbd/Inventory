$(function () {
    $('#employee_search_form').submit(function (e, param) {
        e.preventDefault();
        Metronic.blockUI({
            target: '#list_div',
            animate: true
        });
        var data = $('#employee_search_form').serializeJSON();
        var json = JSON.stringify(data);
        //Url defined in razor file
        var url = searchEmployeeUrl;
        $.ajax({
            url: url,
            type: 'POST',
            data: json,
            contentType: 'application/json',
            success: function (response) {
                $('#employee_table > tbody').empty();
                response.forEach(function (emp) {
                    var selectBtn = '<button class="btn btn-xs btn-primary btn_select_employee" data-id="' + emp.EmployeeID + '">' + lblPleaseSelect + '</button>';
                    $('#employee_table > tbody').append('<tr><td>' + emp.Name + '</td><td>' + emp.FatherName + '</td><td>' + emp.Occupation + '</td><td>' + emp.DepartmentName + '</td><td>' + selectBtn + '</td></tr>');
                })
                Metronic.unblockUI('#list_div');
            }
        })
    });

    $('body').on('click', '.btn_select_employee', function (e) {
        e.preventDefault();
        //popup_employee
        var tr = $(this).closest('tr');
        var EmployeeID = $(this).data().id;
        var Name = $(tr).find('td:first').text();
        var FatherName = $(tr).find('td:nth-child(2)').text();
        var Occpation = $(tr).find('td:nth-child(3)').text();
        var Department = $(tr).find('td:nth-child(4)').text();
        $('#EmployeeID').val(EmployeeID);
        $('#EmpName').val(Name);
        $('#EmpFatherName').val(FatherName);
        $('#EmpOccupation').val(Occpation);
        $('#EmpDepartmentName').val(Department);
        $('#popup_employee').modal('hide');
        // $('#employee_search_form').reset();
        $("#employee_search_form").trigger("reset");
    })
})
