$(document).ready(function () {
    $('#PaycheckList, #EmployeeList').DataTable();

    if ($("[name$='isEmployerPackagePaid']").length > 0) {
        $("#employerExpenses").show();
    } else {
        $("#employerExpenses").hide();
    }

    $("[name$='isEmployerPackagePaid']").on("change", function (e) {
        if (this.checked) {
            $("#employerExpenses").show();
        } else {
            $("#employerExpenses").hide();
        }
    });

    $('#formEmployee').submit(function () {
        if ($("#isEmployerPackagePaid").attr("checked") && $("#AnnualPackageExpense").val() == 0) {
            $("#calculateExpensesButton").trigger('click');
        }
        return true;
    });

    $("#addDependents").on("click", function (e) {
        id = "dependentsList[@i].Name"

        var number = $("#currentDependentsValue").val();
        let html = "<div><label>Dependent's Name</label><input class='form-control'"
            + "' id='employee.Dependents[" + number + "].Name' name='employee.Dependents[" + number + "].Name' value=''></div>";

        $("#addDependents").parent().append(html);

        var newNumber = parseInt(number) + 1;

        $("#currentDependentsValue").val(newNumber);
    });

    $("#calculateExpensesButton").on("click", function (e) {
        var data = $("#formEmployee").find("input").serialize();

        $.ajax({
            type: "POST",
            url: "/Employee/CalculateEmployerExpenses",
            data: data,
            success: function (data) {
                if (data) {
                    $("#AnnualPackageExpense").val(data.annualPackageExpense);
                    $("#PackageExpensePerPaycheck").val(data.packageExpensePerPaycheck);
                    $("#NetPay").val(data.netPay);
                    $("#NetPayPerPaycheck").val(data.netPayPerPaycheck);
                }
            },
            error: function (data) {
                console.log(data);
            }
        });
    });
});