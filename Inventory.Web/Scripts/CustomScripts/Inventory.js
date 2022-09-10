///Valid the frm_InventoryNewProduct Form/////
$(document).ready(function () {

    ValidateForm("frm_InventoryNewProduct")

});

//$(document).on('click', '#btn_NextProductInfo', function () {
    
//    debugger;
//    $('#li_ExtraInfo').addClass('active');
//    $('#li_ProductInfo').removeClass('active');
//});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/
///Function for Save the Product Info Tab/////
$(document).on('click', '#btn_SaveProductInfo', function () {
    debugger;
    if ($("#frm_InventoryNewProduct,#frm_InventoryExtraInfo").valid()) {
        debugger;
        var data = new FormData();
        $.each($('#image-upload')[0].files, function (i, file) {
            data.append('file-' + i, file);
        });
        data.append('Name', $("#txt_ProductName").val());
        data.append('CategoryId', $("#ddl_Category option:selected").val());
        data.append('ItemType', $("#ddl_ItemType option:selected").val());
        data.append('Barcode', $("#txt_Barcode").val());
        data.append('UnitPrice', $("#txt_ProductCost").val());
        data.append('NormalPrice', $("#txt_NormalPrice").val());
        data.append('ReorderPoint', $("#txt_ReorderPoint").val());
        data.append('ReorderQuantity', $("#txt_ReorderQuantity").val());
        data.append('DefaultLocationId', $("#ddl_Location option:selected").val());
        data.append('LastVendorId', $("#ddl_Vendor option:selected").val());
        data.append('ProductLength', $("#txt_ProductLength").val());
        data.append('ProductWidth', $("#txt_ProductWidth").val());
        data.append('ProductHeight', $("#txt_ProductHeight").val());
        data.append('ProductWeight', $("#txt_ProductWeight").val()); 
        data.append('Remarks', $("#txt_Remarks").val());
      

        var inventoryGridModel = [];
        // These line of code for get purchase Order Item GridTable data.
        var oTableInventoryLocationGrid = $('#inventoryLocationGridTable').dataTable();
        var inventoryLocationTableArray = oTableInventoryLocationGrid.fnGetData();

        // These line of code for get Grid row id for Update (this id only for Update time.)
        var idArrayLength = $('#inventoryLocationGridTable > tbody  > tr ');
        var tableBodyForGetId = $('#inventoryLocationGridTable').closest('table').find('tbody');

        var getProductIdInArray = [];
        for (var i = 0; i < idArrayLength.length; i++) {
            getProductIdInArray.push(tableBodyForGetId.find('tr:eq(' + i + ')').attr('id'));
        }

        for (var i = 0; i < inventoryLocationTableArray.length; i++) {
          var  LocationArray = inventoryLocationTableArray[i];
            var getLocationIdArray = LocationArray[0];
            var LocationId = getLocationIdArray.substring(getLocationIdArray.indexOf('"') + 1, getLocationIdArray.lastIndexOf('"'));
            //var tableRowPosition = oTableInventoryLocationGrid.fnGetPosition($("#" + getProductIdInArray[i])[0]);
            //LocationArray.splice(-2, 2)
            debugger;
            inventoryGridModel.push({
                LocationId: LocationId,
                Quantity: LocationArray[1],
            });
        }
     
        data.append('inventoryGridModel', JSON.stringify(inventoryGridModel))
        $.ajax({
            url: "/InventoryProductInfo/SaveInventoryProductInfo",
            type: 'POST',
            data: data,
            dataType:'json',
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                debugger;
                if (data.success == true) {
                    $("#hdnProdId").val(data.prodid)
                 //   $.toaster({ priority: 'success', title: 'Notice', message: 'Your information successfully save.' });
                    window.location.href = "/InventoryProductInfo/ProductInfoList";
                }
            },
            error: function (err) {
            }
        });
    }
    else {
        $("#txt_ProductName").tooltip('show');
        if ($("#ddl_ItemType").val() == "") {
            $("#ddl_ItemType").tooltip('show');
        }
    }
});


//--------------------------------------------------------------------------------------------------------------------------------------------------------/

///Function for unique name validation for Item in Inventory Product info/////
$(document).on('blur', '#txt_ProductName', function (e) {
    e.preventDefault();
    $.get("/InventoryProductInfo/IsItemNameExists?ItemName=" + $('#txt_ProductName').val(), function (data) {
        debugger;
        if (data.usercheck == 1) {
            $('#txt_ProductName').css('border-color', 'red');
            bootbox.alert("Item name already exist!");
        }
        else {
            $('#txt_ProductName').css('border-color', 'green');
        }
    })
});


///Function for unique name validation for Category(shown in the PopUp) in Inventory Product info/////
$(document).on('blur', '#txt_Category', function (e) {
    e.preventDefault();
    $.get("/InventoryProductInfo/IsItemNameExists?ItemName=" + $('#txt_Category').val(), function (data) {
        if (data.usercheck == 1) {
            $('#txt_Category').css('border-color', 'red');
            bootbox.alert("Category name already exist!");
        }
        else {
            $('#txt_Category').css('border-color', 'green');
        }
    })
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/

///Function for Save the Category In PopUp/////
$(document).on("click", "#btn_SaveAddCategoryPopup", function () {
    debugger;
    if ($("#frm_AddNewCategory").valid()) {
        var model = {

            Name: $("#txt_Category").val()

        };
        $.ajax({
            url: "/InventoryProductInfo/SaveNewCategoryPopUp",
            type: 'POST',
            data: { CategoryModel: model },
            dataType: "json",
            success: function (data) {
                debugger;
                if (data.success == true) {
                    HidePopupFrame();
                    history.go(0);
                    //$("#hdnProdId").val(data.prodid)
                    $.toaster({ priority: 'success', title: 'Notice', message: 'Your information successfully save.' });
                }
            }, error: function (err) {
            }
        });
    }
    else {
        $("#txt_Category").tooltip('show');
    }
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/
////Function for open the Pop Up in Product Info Tab/////
$(document).on("click", "#addnewcategory", function () {
    debugger;
    $.ajax({
        type: "GET",
        url: '/InventoryProductInfo/AddNewCategoryPopUp',
        datatype: "html",
        success: function (data) {
            debugger;
            ShowPopupFrame(data);
        },
        error: function () {
            alert("Dynamic content load failed.");
        }
    });
});


//--------------------------------------------------------------------------------------------------------------------------------------------------------/

////Function for upload the image in Product Info Tab/////
$(document).ready(function () {
    $.uploadPreview({
        input_field: "#image-upload",   // Default: .image-upload
        preview_box: "#image-preview",  // Default: .image-preview
        label_field: "#image-label",    // Default: .image-label
        label_default: "Choose File",   // Default: Choose File
        label_selected: "Change File",  // Default: Change File
        no_label: false                 // Default: false
    });
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/

///////Function for append the Link Button in the dropdown list//////////
$(document).on("change", "#ddl_Category", function() {
    if ($(this).val() == "00") {
        $("#addnewcategory").click();
        $(this).val('');
    } 
})

//--------------------------------------------------------------------------------------------------------------------------------------------------------/
//////function for only numeric values fill in Barcode Text Box//////
$(document).on('keypress', '#txt_Barcode', function (event) {
    var inputValue = event.charCode;
    if (!(inputValue >= 47 && inputValue <= 58) && (inputValue != 32 && inputValue != 0)) {
        event.preventDefault();
    }
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/
//////function for only numeric values fill in Quantity Text Box Which is open in Product info data grid//////
$(document).on('keypress', '#gridtxt_Quantity', function (event) {
    var inputValue = event.charCode;
    if (!(inputValue >= 47 && inputValue <= 58) && (inputValue != 32 && inputValue != 0)) {
        event.preventDefault();
    }
});
//--------------------------------------------------------------------------------------------------------------------------------------------------------/

//////function for only numeric values fill in Reorder Point Text Box//////
$(document).on('keypress', '#txt_ReorderPoint', function (event) {
    var inputValue = event.charCode;
    if (!(inputValue >= 47 && inputValue <= 58) && (inputValue != 32 && inputValue != 0)) {
        event.preventDefault();
    }
});
//--------------------------------------------------------------------------------------------------------------------------------------------------------/

//////function for only numeric values fill in Reorder Quantity Text Box//////
$(document).on('keypress', '#txt_ReorderQuantity', function (event) {
    var inputValue = event.charCode;
    if (!(inputValue >= 47 && inputValue <= 58) && (inputValue != 32 && inputValue != 0)) {
        event.preventDefault();
    }
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/

//////function for only numeric and decimal values fill in Product Cost Text Box//////
$(document).on('keypress', '#txt_ProductCost', function (event) {
    debugger;
    var $this = $(this);
    if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
       ((event.which < 48 || event.which > 57) &&
       (event.which != 0 && event.which != 8))) {
        event.preventDefault();
    }

    var text = $(this).val();
    if ((event.which == 46) && (text.indexOf('.') == -1)) {
        setTimeout(function () {
            if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
            }
        }, 1);
    }

    if ((text.indexOf('.') != -1) &&
        (text.substring(text.indexOf('.')).length > 2) &&
        (event.which != 0 && event.which != 8) &&
        ($(this)[0].selectionStart >= text.length - 2)) {
        event.preventDefault();
    }
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/

//////function for only numeric and decimal values fill in Normal Price Text Box//////
$(document).on('keypress', '#txt_NormalPrice', function (event) {
    debugger;
    var $this = $(this);
    if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
       ((event.which < 48 || event.which > 57) &&
       (event.which != 0 && event.which != 8))) {
        event.preventDefault();
    }

    var text = $(this).val();
    if ((event.which == 46) && (text.indexOf('.') == -1)) {
        setTimeout(function () {
            if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
            }
        }, 1);
    }

    if ((text.indexOf('.') != -1) &&
        (text.substring(text.indexOf('.')).length > 2) &&
        (event.which != 0 && event.which != 8) &&
        ($(this)[0].selectionStart >= text.length - 2)) {
        event.preventDefault();
    }
});

//$('#txt_ProductWidth').bind("paste", function (e) {
//    var text = e.originalEvent.clipboardData.getData('Text');
//    if ($.isNumeric(text)) {
//        if ((text.substring(text.indexOf('.')).length > 3) && (text.indexOf('.') > -1)) {
//            e.preventDefault();
//            $(this).val(text.substring(0, text.indexOf('.') + 3));
//        }
//    }
//    else {
//        e.preventDefault();
//    }
//});


//--------------------------------------------------------------------------------------------------------------------------------------------------------/
//////function for only numeric and decimal values fill in Vendor Price Text Box(Product Vendors Grid)//////
$(document).on('keypress', '#gridtxt_VendorsPrice', function (event) {
    debugger;
    var $this = $(this);
    if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
       ((event.which < 48 || event.which > 57) &&
       (event.which != 0 && event.which != 8))) {
        event.preventDefault();
    }

    var text = $(this).val();
    if ((event.which == 46) && (text.indexOf('.') == -1)) {
        setTimeout(function () {
            if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
            }
        }, 1);
    }

    if ((text.indexOf('.') != -1) &&
        (text.substring(text.indexOf('.')).length > 2) &&
        (event.which != 0 && event.which != 8) &&
        ($(this)[0].selectionStart >= text.length - 2)) {
        event.preventDefault();
    }
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/

////function for only numeric values fill in Normal Price Text Box//////
$(document).on('keypress', '#txt_ProductWidth', function (event) {
    var inputValue = event.charCode;
    if (!(inputValue >= 47 && inputValue <= 58) && (inputValue != 32 && inputValue != 0)) {
        event.preventDefault();
    }
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/

//////function for only numeric values fill in Length Text Box//////
$(document).on('keypress', '#txt_ProductLength', function (event) {
    var inputValue = event.charCode;
    if (!(inputValue >= 47 && inputValue <= 58) && (inputValue != 32 && inputValue != 0)) {
        event.preventDefault();
    }
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/

//////function for allow cm(centimeter) in Product Height TextBox//////
//$(document).on("blur", "#txt_ProductHeight", function (e) {
//    debugger;

//    if ($("#txt_ProductHeight").val() != "" && $("#txt_ProductHeight").val().indexOf('cm') < 0) {
//        var widthValue = $("#txt_ProductHeight").val() + 'cm';
//        $("#txt_ProductHeight").val(widthValue);
//    }
//});

//////function for only numeric values fill in Height Text Box//////
$(document).on('keypress', '#txt_ProductHeight', function (event) {
    var inputValue = event.charCode;
    if (!(inputValue >= 47 && inputValue <= 58) && (inputValue != 32 && inputValue != 0)) {
        event.preventDefault();
    }
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/
//////function for allow gm(gram) in Product Weight TextBox//////
//$(document).on("blur", "#txt_ProductWeight", function (e) {
//    debugger;

//    if ($("#txt_ProductWeight").val() != "" && $("#txt_ProductWeight").val().indexOf('gm') < 0) {
//        var widthValue = $("#txt_ProductWeight").val() + 'gm';
//        $("#txt_ProductWeight").val(widthValue);
//    }
//});

//////function for only numeric values fill in Weight Text Box//////
$(document).on('keypress', '#txt_ProductWeight', function (event) {
    var inputValue = event.charCode;
    if (!(inputValue >= 47 && inputValue <= 58) && (inputValue != 32 && inputValue != 0)) {
        event.preventDefault();
    }
});
//--------------------------------------------------------------------------------------------------------------------------------------------------------/
//////function for Item Type Dropdown list//////
$(document).on('change', '#ddl_ItemType', function () {
    debugger;
    if ($("#ddl_ItemType option:selected").val() == 1)
    {
        $('#li_MovementHistory,#li_BillMaterials,#grid_Inventory,#div_StorageInfo,#div_Measurement').show();
    }
    else if ($("#ddl_ItemType option:selected").val() == 2) {
        $('#li_MovementHistory,#li_BillMaterials,#grid_Inventory,#div_StorageInfo,#div_Measurement').show();
    }
    else if ($("#ddl_ItemType option:selected").val() == 3) {
        $('#li_MovementHistory,#li_BillMaterials,#grid_Inventory,#div_StorageInfo').hide();
        $('#div_Measurement').show();
    }
    else if ($("#ddl_ItemType option:selected").val() == 4) {
        $('#li_MovementHistory,#li_BillMaterials,#grid_Inventory,#div_StorageInfo,#div_Measurement').hide();
    }
    else {
      
    }
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/
//////function for save the Product Vendors(grid) tab data//////
$(document).on('click', '#btn_SaveProductVendorGrid', function () {
    debugger;

    var productVendorsGridModel = [];
    // These line of code for get purchase Order Item GridTable data.
    var oTableInventoryProdVendorsGrid = $('#inventoryProductVendorsTable').dataTable();
    var inventoryProdVendorsTableArray = oTableInventoryProdVendorsGrid.fnGetData();

    // These line of code for get Grid row id for Update (this id only for Update time.)
    var idArrayLength = $('#inventoryProductVendorsTable > tbody  > tr ');
    var tableBodyForGetId = $('#inventoryProductVendorsTable').closest('table').find('tbody');

    var getProductIdInArray = [];
    for (var i = 0; i < idArrayLength.length; i++) {
        getProductIdInArray.push(tableBodyForGetId.find('tr:eq(' + i + ')').attr('id'));
    }

    for (var i = 0; i < inventoryProdVendorsTableArray.length; i++) {
        var ProdVendorsArray = inventoryProdVendorsTableArray[i];
        var getProdVendorsIdArray = ProdVendorsArray[0];
        var ProdVendorsId = getProdVendorsIdArray.substring(getProdVendorsIdArray.indexOf('"') + 1, getProdVendorsIdArray.lastIndexOf('"'));

        debugger;
        productVendorsGridModel.push({
            Cost: ProdVendorsArray[1],
            VendorId: parseInt(ProdVendorsId),
            VendorItemCode: ProdVendorsArray[2]
        });
    }

    $.ajax({
        url: "/InventoryProductInfo/SaveProductVendorsGrid",
        type: 'POST',
        data: { 'VendorModel': productVendorsGridModel },
        dataType: 'json',
        success: function (data) {
            debugger;
            if (data.success == true) {
                //$("#hdnProdId").val(data.prodid)
                $.toaster({ priority: 'success', title: 'Notice', message: 'Your information successfully save.' });
            }
        }, error: function (err) {
            debugger;
        }
    });

});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/
//////function for save the Bill of Materials(grid) tab data//////
$(document).on('click', '#btn_SaveBillOfMaterialsGrid', function () {
    debugger;

    var billOfMaterialsGridModel = [];
    // These line of code for get purchase Order Item GridTable data.
    var oTableBillOfMaterialsGrid = $('#inventoryBillOfMaterialsTable').dataTable();
    var billOfMaterialsTableArray = oTableBillOfMaterialsGrid.fnGetData();

    // These line of code for get Grid row id for Update (this id only for Update time.)
    var idArrayLength = $('#inventoryBillOfMaterialsTable > tbody  > tr ');
    var tableBodyForGetId = $('#inventoryBillOfMaterialsTable').closest('table').find('tbody');

    var getBillOfMaterialsIdInArray = [];
    for (var i = 0; i < idArrayLength.length; i++) {
        getBillOfMaterialsIdInArray.push(tableBodyForGetId.find('tr:eq(' + i + ')').attr('id'));
    }

    for (var i = 0; i < billOfMaterialsTableArray.length; i++) {
        var BillOfMaterialsArray = billOfMaterialsTableArray[i];
        var getBillOfMaterialsIdArray = BillOfMaterialsArray[0];
        var BillOfMaterialsId = getBillOfMaterialsIdArray.substring(getBillOfMaterialsIdArray.indexOf('"') + 1, getBillOfMaterialsIdArray.lastIndexOf('"'));

        debugger;
         billOfMaterialsGridModel.push({
            
            ProdId: parseInt(BillOfMaterialsId),
            Quantity: BillOfMaterialsArray[1],
            UnitPrice: BillOfMaterialsArray[2]
        });
    }

    $.ajax({
        url: "/InventoryProductInfo/SaveBillOfMaterials",
        type: 'POST',
        data: { 'InventoryBillModel': billOfMaterialsGridModel },
        dataType: 'json',
        success: function (data) {
            debugger;
            if (data.success == true) {
                //$("#hdnProdId").val(data.prodid)
                $.toaster({ priority: 'success', title: 'Notice', message: 'Your information successfully save.' });
            }
        }, error: function (err) {
            debugger;
        }
    });

});



//--------------------------------------------------------------------------------------------------------------------------------------------------------/

//////function for only numeric values fill in VendorPrice(Prodcut Vendor Grid) Text Box//////
$(document).on('keypress', '#gridtxt_VendorsPrice', function (event) {
    var inputValue = event.charCode;
    if (!(inputValue >= 47 && inputValue <= 58) && (inputValue != 32 && inputValue != 0)) {
        event.preventDefault();
    }
});

//--------------------------------------------------------------------------------------------------------------------------------------------------------/
//////function for calculate the cost in bill of materials grid//////
$(document).on('change', '#SelectBillOfMaterials', function () {
    debugger;
    $.get("/InventoryProductInfo/getQuantityUnitpriceByItem?prodId=" + $('#SelectBillOfMaterials option:selected').val(), function (data) {
        debugger;
        var unitQuantity = data.UnitAndQuantity.split("||");
        $('#gridtxt_Quantity1').val(unitQuantity[1]);
        $('#gridtxt_Cost').val(unitQuantity[0]);
        var TotalPrice = parseFloat($('#gridtxt_Cost').val()) * parseInt($("#gridtxt_Quantity1").val())
      //  $("#txt_TotalCost").val(TotalPrice);
    });

});

//$(document).on('keyup', '#gridtxt_Quantity1', function () {

//    var TotalPrice = parseFloat($('#gridtxt_Cost').val()) * parseFloat($(this).val())
//   $("#txt_TotalCost").val(TotalPrice);
//});

$(document).on('click', '#save_editBillOfMaterials', function (e) {
    debugger;
    e.preventDefault();
    var table, tr, tdQuantity, tdCost, i;
    var previousValue = parseFloat($("#txt_TotalCost").val());
    table = document.getElementById("inventoryBillOfMaterialsTable");
    tr = table.getElementsByTagName("tr");
    for (i = 1; i < tr.length; i++) {
        debugger;
        tdQuantity = tr[i].getElementsByTagName("td")[1];
        tdCost = tr[i].getElementsByTagName("td")[2];

        if (tdQuantity && tdCost) {
            {
                var TotalValue = parseFloat(tdQuantity.innerHTML) * parseFloat(tdCost.innerHTML);
                debugger;
                if ($("#txt_TotalCost").val() != "" && $("#txt_TotalCost").val() != null) {
                    $("#txt_TotalCost").val(previousValue + TotalValue);
                }
                else {
                    $("#txt_TotalCost").val(TotalValue);
                }

            }
        }
    }
});