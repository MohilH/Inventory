


//var itemList = [];
//var AllItemsIds = [];
$(document).ready(function () {
    debugger;

    
    ValidateForm("frm_AddPurchaseOrder")
    if ($("#frm_AddPurchaseOrder").valid()) {
        $("#ddl_VendorId_PO").tooltip('hide');
    }
    else {
        $(document).on('click', '#PurchaseOrderDataSave', function () {
            $("#ddl_VendorId_PO").tooltip('show');
        });
    }

    $("#ddl_VendorId_PO").change(function () {

        $('#ddl_VendorId_PO').css('border-color', '');
    });

  
    var getCurrentActiveTabName = "";

    function getCurrentActiveTabNameFn() {

        getCurrentActiveTabName = $(".nav > li.active > a").attr('name');

    }
    var  PurchaseOrderId = $('#hdn_PurchaseOrderId_PO').val();

    if (PurchaseOrderId !== "") {
        debugger;
        $('#receiveItem_tabId,#returnItem_tabId,#unstockItem_tabId').show();
    }
    else {
        $('#receiveItem_tabId,#returnItem_tabId,#unstockItem_tabId').hide();
    }



    $(".PurchaseOrderDataSave").click(function () {

        
        debugger;
       
        var VendorId = $("#ddl_VendorId_PO option:selected").val();

        getCurrentActiveTabNameFn();

        if (VendorId !== undefined && VendorId !== "") {
            // var LocationId = $('#ddl_LocationId_PO option:selected').val();
            // var OrderNumberId = $("#txt_OrderNumber_PO").val();
            var purchaseOrderGridModel = [];
            var PurchaseOrderReceiveGridModel = [];
            var PurchaseOrderReturnGridModel = [];
            var PurchaseOrderUnStockGridModel = [];
            var ProductArray;
            if (getCurrentActiveTabName == "purchase") {

                if (PurchaseOrderId !== "")
                {
                    //***************************************************************//
                    // These line of code for get purchase Order Item GridTable data.
                    var oTablePurchaseOrderItemGrid = $('#purchaseOrderItemGridTable').dataTable();
                    var purchaseOrderItemGridTableArray = oTablePurchaseOrderItemGrid.fnGetData();

                    //***************************************************************//
                    // These line of code for get Grid row id for Update (this id only for Update time.)
                    var idArrayLength = $('#purchaseOrderItemGridTable > tbody  > tr ');
                    var tableBodyForGetId = $('#purchaseOrderItemGridTable').closest('table').find('tbody');

                    var getRowIdForUpdateInArray = [];
                    for (var i = 0; i < idArrayLength.length; i++) {
                        getRowIdForUpdateInArray.push(tableBodyForGetId.find('tr:eq(' + i + ')').attr('id'));
                    }

                    getRowIdForUpdateInArray.sort(function (x, y) {
                        return x - y;
                    })

                    // oTablePurchaseOrderItemGrid.fnGetPosition($("#336")[0])
                    //$('#purchaseOrderReceiveItemGridTable').dataTable().fnGetPosition($("#" + 335)[0]);
                    //  tableRowPosition.push(oTablePurchaseOrderItemGrid.fnGetPosition($("#" + getRowIdForUpdateInArray[j])[0]));

                    for (var i = 0; i < purchaseOrderItemGridTableArray.length; i++) {
                        ProductArray = purchaseOrderItemGridTableArray[i];
                        var getProIdArray = ProductArray[1];
                        var ProId = getProIdArray.substring(getProIdArray.indexOf('"') + 1, getProIdArray.lastIndexOf('"'));
                        ProductArray.splice(-2, 2)
                        purchaseOrderGridModel.push({
                            ProdId: ProId,
                            VendorItemCode: ProductArray[2],
                            Quantity: ProductArray[3],
                            UnitPrice: ProductArray[4],
                            Discount: ProductArray[5],
                            SubTotal: ProductArray[6],
                            PurchaseOrderLineId: getRowIdForUpdateInArray[i],
                            OrderStatus: 0
                        });
                    }
                }
                else
                {
                    //***************************************************************//
                    // These line of code for get purchase Order Item GridTable data.
                    var oTablePurchaseOrderItemGrid = $('#purchaseOrderAddNewItemGridTable').dataTable();
                    var purchaseOrderItemGridTableArray = oTablePurchaseOrderItemGrid.fnGetData();

                   // //***************************************************************//
                   // // These line of code for get Grid row id for Update (this id only for Update time.)
                    //var idArrayLength = $('#purchaseOrderItemGridTable > tbody  > tr ');
                    //var tableBodyForGetId = $('#purchaseOrderItemGridTable').closest('table').find('tbody');

                    //var getRowIdForUpdateInArray = [];
                    //for (var i = 0; i < idArrayLength.length; i++) {
                    //    getRowIdForUpdateInArray.push(tableBodyForGetId.find('tr:eq(' + i + ')').attr('id'));
                    //}

                    //getRowIdForUpdateInArray.sort(function (x, y) {
                    //    return x - y;
                    //})
 

                    for (var i = 0; i < purchaseOrderItemGridTableArray.length; i++) {
                        ProductArray = purchaseOrderItemGridTableArray[i];
                        var getProIdArray = ProductArray[0];
                        var ProId = getProIdArray.substring(getProIdArray.indexOf('"') + 1, getProIdArray.lastIndexOf('"'));
                        ProductArray.splice(-2, 2)
                        purchaseOrderGridModel.push({
                            ProdId: ProId,
                            VendorItemCode: ProductArray[1],
                            Quantity: ProductArray[2],
                            UnitPrice: ProductArray[3],
                            Discount: ProductArray[4],
                            SubTotal: ProductArray[5],
                           // PurchaseOrderLineId: getRowIdForUpdateInArray[i],
                            OrderStatus: 0
                        });
                    }
                }
               
            }

            if (getCurrentActiveTabName == "receive") {

                //***************************************************************//
                // These line of code for get purchase Order Item GridTable data.
                var oTablePurchaseOrderReceiveItemGrid = $('#purchaseOrderReceiveItemGridTable').dataTable();
                var purchaseOrderReceiveItemGridTableArray = oTablePurchaseOrderReceiveItemGrid.fnGetData();


                //***************************************************************//
                // These line of code for get Grid row id for Update (this id only for Update time.)
                var idArrayLength = $('#purchaseOrderReceiveItemGridTable > tbody  > tr ');
                var tableBodyForGetId = $('#purchaseOrderReceiveItemGridTable').closest('table').find('tbody');

                var getRowIdForUpdateInArray = [];
                for (var i = 0; i < idArrayLength.length; i++) {
                    getRowIdForUpdateInArray.push(tableBodyForGetId.find('tr:eq(' + i + ')').attr('id'));
                }
                getRowIdForUpdateInArray.sort(function (x, y) {
                    return x - y;
                })


                for (var i = 0; i < purchaseOrderReceiveItemGridTableArray.length; i++) {
                    ProductArray = purchaseOrderReceiveItemGridTableArray[i];
                    var getProIdArray = ProductArray[0];
                    var ProId = getProIdArray.substring(getProIdArray.indexOf('"') + 1, getProIdArray.lastIndexOf('"'));

                    var getLocationIdArray = ProductArray[3];
                    var locationId = getLocationIdArray.substring(getLocationIdArray.indexOf('"') + 1, getLocationIdArray.lastIndexOf('"'));

                    //var tableRowPosition = oTablePurchaseOrderItemGrid.fnGetPosition($("#" + getProductIdInArray[i])[0]);
                    ProductArray.splice(-2, 2)

                    PurchaseOrderReceiveGridModel.push({
                        ProdId: ProId,
                        VendorItemCode: ProductArray[1],
                        Quantity: ProductArray[2],
                        LocationId: locationId,
                        ReceiveDate: ProductArray[4],
                        PurchaseOrderReceiveLineId: getRowIdForUpdateInArray[i],
                         
                    });
                }
            }


            if (getCurrentActiveTabName == "return") {

                //***************************************************************//
                // These line of code for get purchase Order Item GridTable data.
                var oTablePurchaseOrderReturnItemGrid = $('#purchaseOrderItemReturnGridTable').dataTable();
                var purchaseOrderReturnItemGridTableArray = oTablePurchaseOrderReturnItemGrid.fnGetData();

                //***************************************************************//
                // These line of code for get Grid row id for Update (this id only for Update time.)
                var idArrayLength = $('#purchaseOrderItemReturnGridTable > tbody  > tr ');
                var tableBodyForGetId = $('#purchaseOrderItemReturnGridTable').closest('table').find('tbody');

                var getRowIdForUpdateInArray = [];
                for (var i = 0; i < idArrayLength.length; i++) {
                    getRowIdForUpdateInArray.push(tableBodyForGetId.find('tr:eq(' + i + ')').attr('id'));
                }
                getRowIdForUpdateInArray.sort(function (x, y) {
                    return x - y;
                })

                for (var i = 0; i < purchaseOrderReturnItemGridTableArray.length; i++) {
                    ProductArray = purchaseOrderReturnItemGridTableArray[i];
                    var getProIdArray = ProductArray[0];
                    var ProId = getProIdArray.substring(getProIdArray.indexOf('"') + 1, getProIdArray.lastIndexOf('"'));
                   // var tableRowPosition = oTablePurchaseOrderReturnItemGrid.fnGetPosition($("#" + getProductIdInArray[i])[0]);
                    ProductArray.splice(-2, 2)

                    PurchaseOrderReturnGridModel.push({
                        ProdId: ProId,
                        VendorItemCode: ProductArray[1],
                        Quantity: ProductArray[2],
                        ReturnDate: ProductArray[3],
                        UnitPrice: ProductArray[4],
                        Discount: ProductArray[5],
                        SubTotal: ProductArray[6],
                        PurchaseOrderReturnLineId: getRowIdForUpdateInArray[i],

                    });
                }
            }


            if (getCurrentActiveTabName == "unstock") {

                //***************************************************************//
                // These line of code for get purchase Order Item GridTable data.
                var oTablePurchaseOrderUnstockItemGrid = $('#purchaseOrderItemUnstockGridTable').dataTable();
                var purchaseOrderUnstockItemGridTableArray = oTablePurchaseOrderUnstockItemGrid.fnGetData();

                //***************************************************************//
                // These line of code for get Grid row id for Update (this id only for Update time.)
                var idArrayLength = $('#purchaseOrderItemUnstockGridTable > tbody  > tr ');
                var tableBodyForGetId = $('#purchaseOrderItemUnstockGridTable').closest('table').find('tbody');

                var getRowIdForUpdateInArray = [];
                for (var i = 0; i < idArrayLength.length; i++) {
                    getRowIdForUpdateInArray.push(tableBodyForGetId.find('tr:eq(' + i + ')').attr('id'));
                }
                getRowIdForUpdateInArray.sort(function (x, y) {
                    return x - y;
                })
                for (var i = 0; i < purchaseOrderUnstockItemGridTableArray.length; i++) {
                    ProductArray = purchaseOrderUnstockItemGridTableArray[i];
                    var getProIdArray = ProductArray[0];
                    var ProId = getProIdArray.substring(getProIdArray.indexOf('"') + 1, getProIdArray.lastIndexOf('"'));

                    var getLocationIdArray = ProductArray[3];
                    var locationId = getLocationIdArray.substring(getLocationIdArray.indexOf('"') + 1, getLocationIdArray.lastIndexOf('"'));

                   // var tableRowPosition = oTablePurchaseOrderUnstockItemGrid.fnGetPosition($("#" + getProductIdInArray[i])[0]);
                    ProductArray.splice(-2, 2)

                    PurchaseOrderUnStockGridModel.push({
                        ProdId: ProId,                       
                        Quantity: ProductArray[1],
                        UnstockDate:ProductArray[2],
                        LocationId: locationId,
                        PurchaseOrderUnstockLineId: getRowIdForUpdateInArray[i],

                    });
                }
            }

            var FormDataModel = {
                VendorId: VendorId,
                OrderNumber: $("#txt_OrderNumber_PO").val(),
                LocationId: $('#ddl_LocationId_PO option:selected').val(),
                ContactName: $('#txt_ContactName_PO').val(),
                Phone: $('#txt_Phone_PO').val(),
                VendorAddress1: $('#txt_VendorAddress1_PO').val(),
                PaymentTermsId: $('#ddl_PaymentTermsId_PO option:selected').val(),
                VendorOrderNumber: $('#txt_VendorOrderNumber_PO').val(),
                ShipToAddress1: $('#txt_ShipToAddress1_PO').val(),
                OrderDate: $('#txt_OrderDate_datepicker_PO').val(),
                Carrier: $('#txt_Carrier_PO').val(),
                CurrencyId: $('#ddl_CurrencyId_PO option:selected').val(),
                DueDate: $('#txt_DueDate_datepicker_PO').val(),
                TaxingSchemeId: $('#ddl_TaxingSchemeId_PO option:selected').val(),
                RequestShipDate: $('#txt_RequestShipDate_datepicker_PO').val(),
                OrderRemarks: $('#txt_OrderRemarks_PO').val(),
                Freight: $('#txt_Freight_PO').val(),
                AmountPaid: $('#txt_AmountPaid_PO').val(),
                PurchaseOrderId: $('#hdn_PurchaseOrderId_PO').val(),
                OrderSubTotal: $('#txt_CompleteGridItemOrderSubTotal_PO').val(),
                Balance: $('#txt_Balance_PO').val(),
                OrderTotal: $('#txt_OrderTotal_PO').val(),
                ////////////////////////////////////////
                // PuchaseOrderFormId: $('# ').val(),
                ReceiveRemarks: $('#txt_ReceiveRemarks_OP').val(),
                ReturnRemarks: $('#txt_ReturnRemarks_OP').val(),
                UnstockRemarks: $('#txt_UnstockRemarks_OP').val(),

                ReturnSubTotal: $('#txt_CompleteGridItemReturnSubTotal_PO').val(),
                ReturnTotal: $('#txt_ReturnTotal_PO').val(),
                 



                PuchaseOrderFormId: getCurrentActiveTabName,
            };

            var AllFormDataModel = {
                PurchaseOrder: FormDataModel,
                PurchaseOrderItem: purchaseOrderGridModel,
                PurchaseOrderReceiveItem: PurchaseOrderReceiveGridModel,
                PurchaseOrderReturnItem: PurchaseOrderReturnGridModel,
                PurchaseOrderUnstockItem: PurchaseOrderUnStockGridModel,


            };


            $.ajax({
                url: '/PurchaseOrder/AddPurchaseOrder',
                contentType: "application/json; charset=utf-8",
                type: "POST",
                data: JSON.stringify(AllFormDataModel),
                dataType: "json",
                success: function (data) {

                    debugger;

                    if (data.success == true) {
                        // $("#hdnVendorID").val(data.vendorid)
                        //debugger;

                        // $.toaster({ priority: 'success', title: 'Notice', message: 'Your information successfully save.' });
                       // window.location.href = "/PurchaseOrder/PurchaseOrdeList";
                       
                        // window.location.href = "";
                        var getUrlId = GetParameterValues("PurchaseOrderNumber");
                        if (typeof getUrlId !== "undefined")
                        {
                            setTimeout(function () {
                                debugger;
                                PopUp();
                            });
                            setTimeout(function () {
                                debugger;
                                loadPage();

                            }, 3000);
                            
                        }
                        else
                        {
                            setTimeout(function () {
                                 
                                PopUp();
                            },1000);

                            setTimeout(function () {
                                debugger;
                                window.location.href = "/PurchaseOrder/PurchaseOrdeList";

                            }, 2000);
                           
                        }
                                      


                    }
                    else if (data.success == false)
                    {

                        if (data.purchaseOrderFalseId === 'purchaseFalse') {
                            bootbox.alert({
                                message: "You do not add Order Items !",
                                size: 'small'
                            });
                        }

                        else if (data.purchaseOrderFalseId === 'receiveFalse') {
                            bootbox.alert({
                                message: "You do not add Order Receive Items !",
                                size: 'small'
                            });
                        }
                        else if (data.purchaseOrderFalseId === 'returnFalse') {
                            bootbox.alert({
                                message: "You do not add Order Return Items !",
                                size: 'small'
                            });
                        }
                        else if (data.purchaseOrderFalseId === 'unstockFalse')
                        {
                            bootbox.alert({
                                message: "You do not add Order Unstock Items !",
                                size: 'small'
                            });
                        }
                        
                       
                    }
 
                },
            });

        }
        else {

            bootbox.alert({
                message: "Vendor is not selected !",
                size: 'small'
            });
            $('#ddl_VendorId_PO').css('border-color', 'red');
           // alert("Vendor is not Selected.")
            // $("#ddl_VendorId_PO").tooltip('show');
            //$("#txt_OrderNumber_PO").tooltip('show');
        }

    });

    $(".nav > li > a").click(function () {
        debugger;
        var UrlParameterId = GetParameterValues("PurchaseOrderNumber");
         
        if (typeof UrlParameterId !== "undefined") {
             var currentTabName = $(this).attr('name');
        var PurchaseOrderId = $('#hdn_PurchaseOrderId_PO').val();         
       

        var modelData = {
            tabName: currentTabName,
            getUrlid: UrlParameterId,
            PurchaseOrderId: PurchaseOrderId,
        }
       
        $.ajax({
            url: '/PurchaseOrder/loadTabData',
            contentType: "application/json; charset=utf-8",
            type: "POST",
            data: JSON.stringify(modelData),
            dataType: "json",
            success: function (data) {

                debugger;

                if (data.success == true) {
                    // $("#hdnVendorID").val(data.vendorid)
                    //debugger;

                    // $.toaster({ priority: 'success', title: 'Notice', message: 'Your information successfully save.' });
                    // window.location.reload();

                    // window.location.href = "";
                }
            },
        });
        }
       
        
    
         
    });

    function GetParameterValues(param) {
        var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < url.length; i++) {
            var urlparam = url[i].split('=');
            if (urlparam[0] == param) {
                return urlparam[1];
            }
        }
    }

    function PopUp() {
        debugger;
        $.toaster({ priority: 'success', title: 'Notice', message: 'Your information successfully save.' });
        
    }
    function loadPage() {
      
            window.location.reload();
         
    }

    $(".purchasecheckbox").click(function () {
        debugger;
        var chechBoxid = $(this).attr('id');
        var gatDataId = $(this).attr('data-id');
        var popUpid = "myPopup_" + gatDataId;
        var popup = $("#" + popUpid);
        $(".popuptext ").removeClass("show");
        if ($("#" + chechBoxid).is(':checked')) {
            popup.addClass("show");
        }

    });

    $(".purchaseOrdered").click(function () {
        debugger;
        var getChechBoxValue = $(this).attr('value');
     
        if (getChechBoxValue === 'Ordered') {


            var gatVenderItemCode = $(this).attr('data-id');
            var getPurchaseOrderLineId = $(this).attr('data-orderlineid');
            var getPurchaseOrderId = $('#hdn_PurchaseOrderId_PO').val();


               rowFromPurchaseToReceivedDelete = {
                
                VendorItemCode: gatVenderItemCode,
                PurchaseOrderLineId :getPurchaseOrderLineId,
                PurchaseOrderId: getPurchaseOrderId,

            };

            $.ajax({
                url: '/PurchaseOrder/receivedDataRowRemove',
                contentType: "application/json; charset=utf-8",
                type: "POST",
                data: JSON.stringify(rowFromPurchaseToReceivedDelete),
                dataType: "json",
                success: function (data) {

                    debugger;

                    if (data.success == true) {
                        // $("#hdnVendorID").val(data.vendorid)
                        //debugger;

                        // $.toaster({ priority: 'success', title: 'Notice', message: 'Your information successfully save.' });
                        window.location.reload();

                        // window.location.href = "";



                    }
                },
            });
        }
        else if (getChechBoxValue === 'Received') {

            var rowFromPurchaseToReceived;

            var date = new Date();
            var month = date.getMonth() + 1;
            var day = date.getDate();
            var year = date.getFullYear();
            var currentDate = month + "/" + day + "/" + year;
            

            var gatProdId = $(this).attr('data-itemid');
            var gatVenderItemCode = $(this).attr('data-id');
            var gatQuantity = $(this).attr('data-quantityid');
            var gatLocation = $('#ddl_LocationId_PO option:selected').val();
            var getPurchaseOrderLineId = $(this).attr('data-orderlineid');
            var gatReceivedDate = currentDate;
            var getPurchaseOrderId = $('#hdn_PurchaseOrderId_PO').val();

             rowFromPurchaseToReceived = {
                 ProdId: gatProdId,
                 VendorItemCode: gatVenderItemCode,
                 Quantity: gatQuantity,
                 LocationId: gatLocation,
                 ReceiveDate: gatReceivedDate,
                 PurchaseOrderLineId: getPurchaseOrderLineId,
                 PurchaseOrderId: getPurchaseOrderId,
             };

             $.ajax({
                 url: '/PurchaseOrder/addReceivedOrderedProduct',
                 contentType: "application/json; charset=utf-8",
                 type: "POST",
                 data: JSON.stringify(rowFromPurchaseToReceived),
                 dataType: "json",
                 success: function (data) {

                     debugger;

                     if (data.success == true) {
                         // $("#hdnVendorID").val(data.vendorid)
                         //debugger;

                         // $.toaster({ priority: 'success', title: 'Notice', message: 'Your information successfully save.' });
                         window.location.reload();
                         //$(window).load(function() {
                         //    alert("test123");
                         //})

                         // window.location.href = "";



                     }
                 },
             });
        }
         
    });

    $("#autoFillReceiveItems").click(function () {
        debugger;
        var getPurchaseOrderId = $('#hdn_PurchaseOrderId_PO').val();
        var gatLocation = $('#ddl_LocationId_PO option:selected').val();

        var date = new Date();
        var month = date.getMonth() + 1;
        var day = date.getDate();
        var year = date.getFullYear();
        var currentDate = month + "/" + day + "/" + year;

        autofillDataPurchaseToReceived = {   
                       
            LocationId: gatLocation,
            ReceiveDate: currentDate,             
            PurchaseOrderId: getPurchaseOrderId,
        };
        $.ajax({
            url: '/PurchaseOrder/autoFillDataIntoReceiveLine',
            contentType: "application/json; charset=utf-8",
            type: "POST",
            data: JSON.stringify(autofillDataPurchaseToReceived),
            dataType: "json",
            success: function (data) {

                debugger;

                if (data.success == true) {
                

                    // $.toaster({ priority: 'success', title: 'Notice', message: 'Your information successfully save.' });
                    // window.location.href = "/PurchaseOrder/PurchaseOrdeList";
                    window.location.reload();                  



                }
            },
        });
    });
    //$("#test").click(function () {
    //    debugger;
    //    var popup = document.getElementById("myPopup");
    //    popup.classList.toggle("show");

    //})
    $(".fullFilledBtn").click(function () {
        $("#btnFullfilled").toggleClass('hidden');
        $("#btnUnfullfilled").toggleClass('hidden');

        debugger;
        var orderStatus = $("#txt_purchaseOrderStatus_PO").val();

        var fullFillVal = orderStatus.substring(0, orderStatus.lastIndexOf(","));
        var statusPaidVal = orderStatus.substring(orderStatus.indexOf(',') + 1);

        if (fullFillVal === 'UnFulfilled' && statusPaidVal === 'Unpaid')
        {
            $("#txt_purchaseOrderStatus_PO").val("Fulfilled,Unpaid")
        }
        else if (fullFillVal === 'Fulfilled' && statusPaidVal === 'Unpaid')
        {
            $("#txt_purchaseOrderStatus_PO").val("UnFulfilled,Unpaid")
        }
        else if (fullFillVal === 'UnFulfilled' && statusPaidVal === 'Paid') {
            $("#txt_purchaseOrderStatus_PO").val("Fulfilled,Paid")
        }

        else if (fullFillVal === 'Fulfilled' && statusPaidVal === 'Paid') {
            $("#txt_purchaseOrderStatus_PO").val("UnFulfilled,Paid")
        }
    
       // alert(fullFillVal)
    });

    $(".PayBtn").click(function () {
        $("#btnUnpay").toggleClass('hidden');
        $("#btnPay").toggleClass('hidden');

        var orderStatus = $("#txt_purchaseOrderStatus_PO").val();
        debugger;
        var fullFillVal = orderStatus.substring(0, orderStatus.lastIndexOf(","));
        var statusPaidVal = orderStatus.substring(orderStatus.indexOf(',') + 1);
      
        if (fullFillVal === 'UnFulfilled' && statusPaidVal === 'Unpaid') {
            $("#txt_purchaseOrderStatus_PO").val("UnFulfilled,Paid")
        }
        else if (fullFillVal === 'Fulfilled' && statusPaidVal === 'Unpaid') {
            $("#txt_purchaseOrderStatus_PO").val("Fulfilled,Paid")
        }
        else if (fullFillVal === 'UnFulfilled' && statusPaidVal === 'Paid') {
            $("#txt_purchaseOrderStatus_PO").val("UnFulfilled,Unpaid")
        }       
        else if (fullFillVal === 'Fulfilled' && statusPaidVal === 'Paid') {
            $("#txt_purchaseOrderStatus_PO").val("Fulfilled,Unpaid")
        }
    });

    $("#txt_OrderDate_datepicker_PO").datepicker({
        dateFormat: 'dd/mm/yy'
    });
    $("#txt_DueDate_datepicker_PO").datepicker({
        dateFormat: 'dd/mm/yy'
    });
    $("#txt_RequestShipDate_datepicker_PO").datepicker({
        dateFormat: 'dd/mm/yy',

    });
    $("#ReceiveOrder_datepicker").datepicker({
        dateFormat: 'dd/mm/yy',

    });
    $("#ReturnOrder_datepicker").datepicker({
        dateFormat: 'dd/mm/yy',

    });

    $("#UnstockOrder_datepicker").datepicker({
        dateFormat: 'dd/mm/yy',

    });

     

});

$(document).on('change', '.Calculate', function (event) {
    debugger;

    var quantity = $("#GridProductQuantity").val();

    var unitprice = $("#GridProductUnitPrice").val();

    var discount = $("#GridProductDiscount").val();

    if (quantity !== '' && unitprice !== '') {
        var cost = quantity * unitprice;
        //  alert(quantity + "/ / " + unitprice + "/ / " + discount);
        var SubTotal = $("#GridProductSubTotal").val(cost);
        if (discount !== '') {
            var discountAmount = (quantity * unitprice * discount) / 100;

            cost = (quantity * unitprice) - discountAmount;
            SubTotal = $("#GridProductSubTotal").val(cost);

        }
 
    }

    if (quantity === '' || unitprice === '') {
        var SubTotal = $("#GridProductSubTotal").val('');
    }
});  

$(document).on('change', '.calculateReturn', function (event) {
    debugger;

    var quantity = $("#gridReturnProductQuantity").val();

    var unitprice = $("#gridReturnProductUnitPrice").val();

    var discount = $("#gridReturnProductDiscount").val();

    if (quantity !== '' && unitprice !== '') {
        var cost = quantity * unitprice;
        //  alert(quantity + "/ / " + unitprice + "/ / " + discount);
        var SubTotal = $("#gridReturnProductSubTotal").val(cost);
        if (discount !== '') {
            var discountAmount = (quantity * unitprice * discount) / 100;

            cost = (quantity * unitprice) - discountAmount;
            SubTotal = $("#gridReturnProductSubTotal").val(cost);

        }

    }

    if (quantity === '' || unitprice === '') {
        var SubTotal = $("#gridReturnProductSubTotal").val('');
    }
}); 

$(document).on('change', '#txt_Freight_PO', function (event) {
    debugger;

    var gridItemOrderSubTotal = $("#txt_CompleteGridItemOrderSubTotal_PO").val();

    var freight = $("#txt_Freight_PO").val();

    var AmountPaid = $("#txt_AmountPaid_PO").val();
     
  

    if (gridItemOrderSubTotal !== '' && freight !== '') {
        var total = parseFloat(gridItemOrderSubTotal) + parseFloat(freight);

           $("#txt_OrderTotal_PO").val(total);
    }
    else if (gridItemOrderSubTotal !== '' && freight === '')
    {
            $("#txt_OrderTotal_PO").val(parseFloat(gridItemOrderSubTotal))
    }

    var OrderTotal = $("#txt_OrderTotal_PO").val();
    if (AmountPaid !== '' && OrderTotal !== '')
    {
        var balance = parseFloat(OrderTotal) - parseFloat(AmountPaid);

        var SubTotal = $("#txt_Balance_PO").val(balance);
    }

});

$(document).on('change', '#txt_AmountPaid_PO', function (event) {
    debugger;

    var AmountPaid = $("#txt_AmountPaid_PO").val();

    var OrderTotal = $("#txt_OrderTotal_PO").val();

    if (AmountPaid !== '' && OrderTotal !== '') {
        
        var balance = parseFloat(OrderTotal) - parseFloat(AmountPaid);

        var SubTotal = $("#txt_Balance_PO").val(balance);
    }
    else if (OrderTotal !== '' && AmountPaid === '') {
        $("#txt_Balance_PO").val(parseFloat(gridItemOrderSubTotal))
    }
});

//$('#GridProductSubTotal').bind("change keyup", function () {
//    //Do something, probably with $(this).val()

//    alert("test")
//});
//$("#GridProductSubTotal").autocomplete({
//    change: function () {
//        alert('changed');
//    }
//});
//$(document).ready(function () {

//    var ProductSubTotal = $("#GridProductSubTotal").val();
//    $("#CompleteGridItemSubTotal").autocomplete({
//        change: function (event, ui) {
//            alert("Test222")


//        }
//    });
//});


$(document).on('change', '#GridProductSubTotal', function (event) {
    debugger;

    //var ProductSubTotal = $("#GridProductSubTotal").val();
    alert('changed')
    //var SubTotal = $("#CompleteGridItemSubTotal").val(ProductSubTotal);

});




$(document).on('keypress', '.GridDigit', function (event) {
    var inputValue = event.charCode;
    if (!(inputValue >= 47 && inputValue <= 58) && (inputValue != 32 && inputValue != 0)) {
        event.preventDefault();
    }
});
