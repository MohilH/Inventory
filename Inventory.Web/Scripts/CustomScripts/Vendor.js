

///Valid the NewVendorInfo Form/////
$(document).ready(function () {
    debugger

    ValidateForm("frm_NewVendor")

    ///For Show/Hide the tab
    var splitedURL = document.location.href;
    var getVendorID = splitedURL.split('=')[1]
    if (getVendorID != null) {
        $('#li_vendorProduct,#li_orderHistory,#li_paymentHistory').show();
    }
    else {
        $('#li_vendorProduct,#li_orderHistory,#li_paymentHistory').hide();
    }

    ///Get the Current balance in Payment History Tab////
    $.get("/Vendor/getLatestPaymentRecord?vendorId=" + getVendorID, function (data) {
        debugger;
        $('#txt_CurrentBalance').val(data.CurrentBal.toFixed(2));
        //$('#txt_DueBalance').val(data.CurrentBal.toFixed(2));
        //$('#txt_filterDays').val(data.Balance.toFixed(2));
       
    });
    //$.get("/Vendor/getVendorOrderStatus?vendorId=" + getVendorID, function (data) {
    //    debugger;
       
    //    $('#txt_filterDays').val(data.Balance.toFixed(2));
    //});

});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/

///Function for unique name validation in vendor info/////
$(document).on('blur', '#txt_VendorName', function (e) {         ////////////17052017(D)////////
    e.preventDefault();
    $.get("/Vendor/IsVendorNameExists?VendorName=" + $('#txt_VendorName').val(), function (data) {
        if (data.usercheck == 1) {
            $('#txt_VendorName').css('border-color', 'red');
            bootbox.alert("Vendor name already exist!");
        }
        else {
            $('#txt_VendorName').css('border-color', 'green');
            //bootbox.alert("Vendor name does not exist!");
        }
    })
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/
///Function for unique name validation in vendor products tab(Grid)/////
$(document).on('blur', '#btn_ItemName', function (e) {         ////////////17052017(D)////////
    e.preventDefault();
    debugger;
    $.get("/Vendor/IsVendorItemNameExists?vendorItemName=" + $('#btn_ItemName').val(), function (data) {
        if (data.usercheck == 1) {
            $('#btn_ItemName').css('border-color', 'red');
            bootbox.alert("This Item Name already exist!");
        }
        else {
            $('#btn_ItemName').css('border-color', 'green');
            //bootbox.alert("Vendor name does not exist!");
        }
    })
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/

///Function for Display the notification messages/////

$(document).on('click', '#btn_Save', function () {
    debugger;
    if ($("#frm_NewVendor").valid()) {
        var model = {
            VendorId: $("#hdnVendorID").val(),
            Name: $('#txt_VendorName').val(),
            Balance: $('#txt_Balance').val(),
            Credit: $('#txt_Credit').val(),
            Address1: $('#txt_Address').val(),
            ContactName: $('#txt_ContactName').val(),
            Phone: $('#txt_PhoneNo').val(),
            Fax: $('#txt_Fax').val(),
            Email: $('#txt_Email').val(),
            Website: $('#txt_Website').val(),
            PaymentTerms: $('#ddl_PaymentTerms option:selected').val(),
            Discount: $('#txt_Discount').val(),
            Currency: $('#ddl_Currency option:selected').val(),
            TaxingScheme: $('#ddl_TaxingScheme option:selected').val(),
            Remarks: $('#txt_Remarks').val()
        };

        $.ajax({
            url: "/Vendor/SaveNewVendorDetails",
            type: 'POST',
            data: { VendorModel: model },
            dataType: "json",
            processData: true,
            success: function (data) {
                debugger;
                if (data.success == true) {
                    $("#hdnVendorID").val(data.vendorid)
                    debugger;
                    //$.toast({
                    //    text: data.Successmessage,
                    //    beforeHide: function () {
                    $.toaster({ priority: 'success', title: 'Notice', message: 'Your information successfully save.' });
                    debugger;
                    window.location.href = "/Vendor/VendorList"
                }
            }
        });
    }
    else {
        $("#txt_VendorName").tooltip('show');
    }
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/

//////function for only numeric values fill in Fax Text Box//////
$(document).on('keypress', '#txt_Fax', function (event) {
    var inputValue = event.charCode;
    if (!(inputValue >= 47 && inputValue <= 58) && (inputValue != 32 && inputValue != 0)) {
        event.preventDefault();
    }
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/

//////function for only numeric values fill in Credit Text Box//////
$(document).on('keypress', '#txt_Credit', function (event) {
    var inputValue = event.charCode;
    if (!(inputValue >= 47 && inputValue <= 58) && (inputValue != 32 && inputValue != 0)) {
        event.preventDefault();
    }
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/

//////function for only numeric values fill in Discount Text Box//////
$(document).on('keypress', '#txt_Discount', function (event) {
    var inputValue = event.charCode;
    if (!(inputValue >= 47 && inputValue <= 58) && (inputValue != 32 && inputValue != 0)) {
        event.preventDefault();
    }
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/

//////function for only numeric values fill in Balance Text Box//////
$(document).on('keypress', '#txt_Balance', function (event) {
    var inputValue = event.charCode;
    if (!(inputValue >= 47 && inputValue <= 58) && (inputValue != 32 && inputValue != 0)) {
        event.preventDefault();
    }
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/

$(document).on('change', '#endDate', function () {
    debugger;
    var VendorId = $("#hdnVendorID").val();
    var startDate = $('#txt_startDate').val();
    var endDate = $('#txt_endDate').val();

    if ($('#txt_startDate').val != "") {
        debugger;
        $.get('/Vendor/getPaymentHistoryByDate?StartDate=' + startDate + "&EndDate=" + endDate + "&VendorId=" + VendorId, function (data) {
            debugger;
            var totalBalance = 0;
            var table = $('#VendorPaymentHistory').DataTable();
            table.clear().draw();
            $.each(data.data, function (index, value) {
                debugger;
                var dataSource = [];
                debugger;             
                    var OrderDate = (value.OrderDate);
                    var DueDate = (value.DueDate);
                    var PaymentStatus = (value.PaymentStatus);
                    var amountPaid = (value.AmountPaid);
                    //var creditBalance = (value.creditBalance);
                    //var AdvDate = new Date(parseInt(value.OrderDate));
                    var balance = (value.Balance);
                    var orderDate = new Date(parseInt(value.OrderDate.replace("/Date(", "").replace(")/", ""), 10));
                    var dateOfOrder = orderDate.getDate() + "/" + (orderDate.getMonth() + 1) + "/" + orderDate.getFullYear();
                    var dueDate = new Date(parseInt(value.DueDate.replace("/Date(", "").replace(")/", ""), 10));
                    var dateOfDue = dueDate.getDate() + "/" + (dueDate.getMonth() + 1) + "/" + dueDate.getFullYear();

                    dataSource.push({ Text: dateOfOrder, Value: dateOfOrder });
                    dataSource.push({ Text: dateOfDue, Value: dateOfDue });
                    $('#VendorPaymentHistory').dataTable().fnAddData([dateOfOrder, dateOfDue, PaymentStatus, amountPaid, balance.toFixed(2)]);
            })
            for (var i = 0; i < data.data.length; i++)
            {
                debugger;
                totalBalance += data.data[i].Balance;
            }
            $('#txt_filterDays').val(totalBalance.toFixed(2));
        })
    }
    else {
        alert("Please select first of all start date");
    }
});



//--------------------------------------------------------------------------------------------------------------------------------------------------------/
$(document).on("click", "#btn_Clear", function () {
    debugger;
    $("#txt_startDate").val("");
    $("#txt_endDate").val("");
    history.go(0);
    //$("#li_paymentHistory").addClass('active');
});



//--------------------------------------------------------------------------------------------------------------------------------------------------------/
/////use for convert number into deicmal form in decimal field///
//$(document).on("blur", "#txt_Credit", function (event) {
//    var creditVal = parseFloat($("#txt_Credit").val()).toFixed(2);
//    if ("NaN" != creditVal) {
//        $("#txt_Credit").val(creditVal);
//    }
//});
/////use for convert number into deicmal form in Discount field///
//$(document).on("blur", "#txt_Discount", function (event) {
//    var creditVal = parseFloat($("#txt_Discount").val()).toFixed(2);
//    if ("NaN" != creditVal) {
//        $("#txt_Discount").val(creditVal);
//    }
//});

/////use for convert number into deicmal form in Balance field///
//$(document).on("blur", "#txt_Balance", function (event) {
//    var creditVal = parseFloat($("#txt_Balance").val()).toFixed(2);
//    if ("NaN" != creditVal) {
//        $("#txt_Balance").val(creditVal);
//    }
//});



