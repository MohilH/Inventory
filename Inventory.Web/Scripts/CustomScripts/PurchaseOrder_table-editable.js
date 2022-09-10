//$(document).ready(function () {
//    debugger;
//    ValidateForm("frm_AddPurchaseOrder");
//});
var itemList = [];

var locationList = [];

var editDDlVariable = false;

var editDDlReceiveVariable = false;

var editDDlReturnVariable = false;

var editDDlUnstockVariable = false;

 


var TableEditable = function () {


    var handleTable = function () {


        function restoreRow(oTableAddNewPurchaseOrder, nRow) {
            debugger;
            var aData = oTableAddNewPurchaseOrder.fnGetData(nRow);
            if (aData !== null) {
                var jqTds = $('>td', nRow);

                for (var i = 0, iLen = jqTds.length; i < iLen; i++) {
                    oTableAddNewPurchaseOrder.fnUpdate(aData[i], nRow, i, false);
                }

                oTableAddNewPurchaseOrder.fnDraw();
            }

        }

        var tableAddNewPurchaseOrder = $('#purchaseOrderAddNewItemGridTable');


        var oTableAddNewPurchaseOrder = tableAddNewPurchaseOrder.dataTable({
            "lengthMenu": [
                [5, 10, 15, 20, -1],
                [5, 10, 15, 20, "All"] // change per page values here
            ],
            // set the initial value
            "pageLength": 10,

            "language": {
                "lengthMenu": " _MENU_ records"
            },
            "columnDefs": [{ // set default column settings
                'orderable': true,
                //"bVisible": false,
                'targets': [0]
            }, {
                "searchable": true,
                "targets": [0]
            },
          
            ],
           

            "order": [
                [0, "asc"]
            ] // set first column as a default sort by asc
        });

        var tableWrapper = $("#sample_editable_1_wrapper");

        tableWrapper.find(".dataTables_length select").select2({
            showSearchInput: false //hide search box with special css class
        }); // initialize select2 dropdown

        var nEditing = null;
        var nNew = false;
        var SelectedproductId = "";
        var selectedLocationId = "";
        function editNewPurchaseOrderRow(oTableAddNewPurchaseOrder, nRow) {

            debugger;
            var aData = oTableAddNewPurchaseOrder.fnGetData(nRow);
            var jqTds = $('>td', nRow);
          
            jqTds[0].innerHTML = '<select  id="SelectItem" class="Itemddl form-control input-small" value="' + aData[0] + '" >  </select">';

            jqTds[1].innerHTML = '<input type="text" id="GridVendorProductCode" class=" form-control input-small required" value="' + aData[1] + '">';
            jqTds[2].innerHTML = '<input type="text" id="GridProductQuantity" class="GridDigit Calculate form-control input-small" value="' + aData[2] + '">';
            jqTds[3].innerHTML = '<input type="text" id="GridProductUnitPrice" class="GridDigit Calculate form-control input-small" value="' + aData[3] + '">';
            jqTds[4].innerHTML = '<input type="text" id="GridProductDiscount"    maxlength=3 class="GridDigit Calculate form-control input-small" value="' + aData[4] + '">';
            jqTds[5].innerHTML = '<input type="text" id="GridProductSubTotal" class="GridDigit form-control input-small" disabled="true" value="' + aData[5] + '">';


            jqTds[6].innerHTML = '<a class="edit"   href="">Save</a>';
            jqTds[7].innerHTML = '<a class="cancel" href="">Cancel</a>';
            $("#purchaseOrderItemNewRow").prop("disabled", true);

            var getProduct = '' + aData[0] + '';
            //var getProductIdmatch = null;
            debugger;
            if (getProduct != "") {
                SelectedproductId = getProduct.substring(getProduct.indexOf('"') + 1, getProduct.lastIndexOf('"'));

            }
            if (editDDlVariable == true) {
                purchaseOrderGridAddNewRow(SelectedproductId);
                editDDlVariable = false;
            } 




        }

        function saveNewPurchaseOrderRow(oTableAddNewPurchaseOrder, nRow) {
            debugger;

            var firstName = $('#GridVendorProductCode');

            // Check if there is an entered value
            if (!firstName.val()) {

                debugger;
                // Add errors highlight 
                $('#GridVendorProductCode').css('border-color', 'red');


                // Stop submission of the 
                // var nRow = oTable.fnGetNodes(1);
                //nEditing = nRow;
            }

            else {
                debugger;
                $("#purchaseOrderItemNewRow").prop("disabled", false);

                // Remove the errors highlight
                //  firstName.closest('.form-group').removeClass('has-error').addClass('has-success');


                var jqInputs = $('input,select,hidden', nRow);
                var itemInfo = $.grep(itemList, function (v) {

                    return v.Value === jqInputs[0].value;
                });


                var selectedValue = jqInputs[0].value;
                var selectedText = itemInfo[0].Text;

                 
                oTableAddNewPurchaseOrder.fnUpdate('<span name ="' + selectedValue + '">' + selectedText + '</span>', nRow, 0, false);
                //oTable.fnUpdate(itemInfo[0].Text, nRow, 0, false);

                oTableAddNewPurchaseOrder.fnUpdate(jqInputs[1].value, nRow, 1, false);
                oTableAddNewPurchaseOrder.fnUpdate(jqInputs[2].value, nRow, 2, false);
                oTableAddNewPurchaseOrder.fnUpdate(jqInputs[3].value, nRow, 3, false);
                oTableAddNewPurchaseOrder.fnUpdate(jqInputs[4].value, nRow, 4, false);
                oTableAddNewPurchaseOrder.fnUpdate(jqInputs[5].value, nRow, 5, false);
                oTableAddNewPurchaseOrder.fnUpdate('<a class="edit"  href="">Edit</a>', nRow, 6, false);
                oTableAddNewPurchaseOrder.fnUpdate('<a class="delete" href="">Delete</a>', nRow, 7, false);

                //$('#purchaseOrderItemGridTable').find('td:first').hide();
                oTableAddNewPurchaseOrder.fnDraw();
                nNew = false;

                // window.location.reload();


                var oTablePurchaseOrderItemGrid = $('#purchaseOrderAddNewItemGridTable').dataTable();
                var purchaseOrderItemGridTableArray = oTablePurchaseOrderItemGrid.fnGetData();
                if (purchaseOrderItemGridTableArray.length > 0) {
                    var TableArray = purchaseOrderItemGridTableArray;
                    var gridSingleRowArray;
                    var gridSubtotalSum = 0;
                    for (var i = 0; i < TableArray.length; i++) {
                        gridSingleRowArray = TableArray[i];
                        var getRowSubtotal = parseFloat(gridSingleRowArray[5]);
                        gridSubtotalSum = gridSubtotalSum + getRowSubtotal;
                    }


                    $("#txt_CompleteGridItemOrderSubTotal_PO").val(gridSubtotalSum.toFixed(2));

                    var freightAmount = $("#txt_Freight_PO").val();

                    if (freightAmount !== '') {
                        var orderTotalWithFreight = parseFloat(freightAmount) + gridSubtotalSum;
                        $("#txt_OrderTotal_PO").val(orderTotalWithFreight);
                    }
                    else {
                        $("#txt_OrderTotal_PO").val(gridSubtotalSum);
                    }

                    var currentOrderTotal = $("#txt_OrderTotal_PO").val();
                    var amountPaid = $("#txt_AmountPaid_PO").val();

                    if (amountPaid !== '' && currentOrderTotal !== '') {
                        var balanceAmount = parseFloat(currentOrderTotal) - parseFloat(amountPaid);
                        $("#txt_Balance_PO").val(balanceAmount.toFixed(2));
                    }
                    else {
                        $("#txt_Balance_PO").val(currentOrderTotal.toFixed(2));
                    }

                }

            }

        }


        function cancelEditNewPurchaseOrderRow(oTableAddNewPurchaseOrder, nRow) {
            debugger;
            var jqInputs = $('input', nRow);
            oTableAddNewPurchaseOrder.fnUpdate(jqInputs[0].value, nRow, 0, false);
            oTableAddNewPurchaseOrder.fnUpdate(jqInputs[1].value, nRow, 1, false);
            oTableAddNewPurchaseOrder.fnUpdate(jqInputs[2].value, nRow, 2, false);
            oTableAddNewPurchaseOrder.fnUpdate(jqInputs[3].value, nRow, 3, false);
            oTableAddNewPurchaseOrder.fnUpdate(jqInputs[4].value, nRow, 4, false);
            oTableAddNewPurchaseOrder.fnUpdate(jqInputs[5].value, nRow, 5, false);
            oTableAddNewPurchaseOrder.fnUpdate(jqInputs[6].value, nRow, 6, false);
           
            oTableAddNewPurchaseOrder.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 7, false);

            oTableAddNewPurchaseOrder.fnDraw();
        }

        function purchaseOrderGridAddNewRow(SelectedproductId) {
            debugger;
            $("#purchaseOrderItemNewRow").prop("disabled", true);

            $.ajax({
                type: "GET",
                url: "/PurchaseOrder/GetAllProductList",
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                traditional: true,
                success: function (data) {
                    debugger;
                    itemList = data;



                    $.each(data, function (i, _Product) {
                        if (_Product.Value == SelectedproductId) {

                            $('.Itemddl').append('<option  selected="selected"  value="' + _Product.Value + '">' + _Product.Text + '</option>');
                        }

                        else {
                            $('.Itemddl').append('<option  value="' + _Product.Value + '">' + _Product.Text + '</option>');
                        }



                    })


                },


                error: function (data) {
                    alert("Product List content load failed.");
                }

            });

        }


        $('#purchaseOrderAddNewItemNewRow').click(function (e) {
            debugger;
            e.preventDefault();
            editDDlVariable = true;


            var aiNew = oTableAddNewPurchaseOrder.fnAddData(['', '', '', '', '', '', '', '']);
            var test = aiNew[0];
            var nRow = oTableAddNewPurchaseOrder.fnGetNodes(aiNew[0]);
            editNewPurchaseOrderRow(oTableAddNewPurchaseOrder, nRow);
            nEditing = nRow;
            nNew = true;


        });

        tableAddNewPurchaseOrder.on('click', '.delete', function (e) {
            e.preventDefault();
            debugger;

            var nRow = $(this).parents('tr')[0];
            oTableAddNewPurchaseOrder.fnDeleteRow(nRow);

            var oTablePurchaseOrderItemGrid = $('#purchaseOrderAddNewItemGridTable').dataTable();
            var purchaseOrderItemGridTableArray = oTablePurchaseOrderItemGrid.fnGetData();
            if (purchaseOrderItemGridTableArray.length > 0) {
                var TableArray = purchaseOrderItemGridTableArray;
                var gridSingleRowArray;
                var gridSubtotalSum = 0;
                for (var i = 0; i < TableArray.length; i++) {
                    gridSingleRowArray = TableArray[i];
                    var getRowSubtotal = parseFloat(gridSingleRowArray[5]);
                    gridSubtotalSum = gridSubtotalSum + getRowSubtotal;
                }
                $("#txt_CompleteGridItemOrderSubTotal_PO").val(gridSubtotalSum.toFixed(2));

                var freightAmount = $("#txt_Freight_PO").val()

                if (freightAmount !== '') {
                    var orderTotalWithFreight = parseFloat(freightAmount) + gridSubtotalSum;
                    $("#txt_OrderTotal_PO").val(orderTotalWithFreight.toFixed(2));
                }
                else {
                    $("#txt_OrderTotal_PO").val(gridSubtotalSum.toFixed(2));
                }

                var currentOrderTotal = $("#txt_OrderTotal_PO").val();
                var amountPaid = $("#txt_AmountPaid_PO").val();

                if (amountPaid !== '' && currentOrderTotal !== '') {
                    var balanceAmount = parseFloat(currentOrderTotal) - parseFloat(amountPaid);
                    $("#txt_Balance_PO").val(balanceAmount.toFixed(2));
                }
                else {
                    $("#txt_Balance_PO").val(currentOrderTotal.toFixed(2));
                }


            }
            else {
                $("#txt_CompleteGridItemOrderSubTotal_PO").val('');
                $("#txt_OrderTotal_PO").val('');
                $("#txt_Balance_PO").val('');
            }

        });

        tableAddNewPurchaseOrder.on('click', '.cancel', function (e) {

            debugger;
            e.preventDefault();
            $("#purchaseOrderItemNewRow").prop("disabled", false);

            if (nNew) {
                oTableAddNewPurchaseOrder.fnDeleteRow(nEditing);
                nNew = false;
            } else {
                restoreRow(oTableAddNewPurchaseOrder, nEditing);
                nEditing = null;
            }
        });

        tableAddNewPurchaseOrder.on('click', '.edit', function (e) {
            e.preventDefault();
            debugger;
            /* Get the row as a parent of the link that was clicked on */
            var nRow = $(this).parents('tr')[0];

            if (nEditing !== null && nEditing != nRow) {
                /* Currently editing - but not this row - restore the old before continuing to edit mode */
                restoreRow(oTableAddNewPurchaseOrder, nEditing);
                editDDlVariable = true;
                editNewPurchaseOrderRow(oTableAddNewPurchaseOrder, nRow);
                nEditing = nRow;

            } else if (nEditing == nRow && this.innerHTML == "Save") {
                /* Editing this row and want to save it */

                saveNewPurchaseOrderRow(oTableAddNewPurchaseOrder, nEditing);
                nEditing = null;
                //alert("Updated! Do not forget to do some ajax to sync with backend :)");
            } else {
                /* No edit in progress - let's start one */
                editDDlVariable = true;
                editNewPurchaseOrderRow(oTableAddNewPurchaseOrder, nRow);
                nEditing = nRow;
            }
        });






        //***************************************************************************//
        //**************************************************************************//
        function restoreRow(oTable, nRow) {
            debugger;
            var aData = oTable.fnGetData(nRow);
            if (aData !== null) {
                var jqTds = $('>td', nRow);

                for (var i = 0, iLen = jqTds.length; i < iLen; i++) {
                    oTable.fnUpdate(aData[i], nRow, i, false);
                }

                oTable.fnDraw();
            }

        }
         
        var table = $('#purchaseOrderItemGridTable');
     

        var oTable = table.dataTable({
            "lengthMenu": [
                [5, 10, 15, 20, -1],
                [5, 10, 15, 20, "All"] // change per page values here
            ],
            // set the initial value
            "pageLength": 10,

            "language": {
                "lengthMenu": " _MENU_ records"
            },
            "columnDefs": [{ // set default column settings
                'orderable': true,
                //"bVisible": false,
                'targets': [0]
            }, {
                "searchable": true,
                "targets": [0]
            },
            //{
            //    "sClass": "my_class", "aTargets": [0]
            //},
            //{
            //    data: "active",
            //    render: function (data, type, row) {
            //        if (type === 'display') {
            //            return '<input type="checkbox" class="editor-active">';
            //        }
            //        return data;
            //    },
            //    className: "dt-body-center"
            //}
            ],
            //select: {
            //    style: 'os',
            //    selector: 'td:first-child'
            //},

            "order": [
                [0, "asc"]
            ] // set first column as a default sort by asc
        });

        var tableWrapper = $("#sample_editable_1_wrapper");

        tableWrapper.find(".dataTables_length select").select2({
            showSearchInput: false //hide search box with special css class
        }); // initialize select2 dropdown

        var nEditing = null;
        var nNew = false;
        var SelectedproductId = "";
        var selectedLocationId = "";
        function editRow(oTable, nRow) {
          
            debugger;
            var aData = oTable.fnGetData(nRow);
            var jqTds = $('>td', nRow); 
            jqTds[0].innerHTML = '<input type="hidden" id="checkbox" class="  input-small" value="">';
            jqTds[1].innerHTML = '<select  id="SelectItem" class="Itemddl form-control input-small" value="' + aData[1] + '" >  </select">';
           
            jqTds[2].innerHTML = '<input type="text" id="GridVendorProductCode" class=" form-control input-small required" value="' + aData[2] + '">';
            jqTds[3].innerHTML = '<input type="text" id="GridProductQuantity" class="GridDigit Calculate form-control input-small" value="' + aData[3] + '">';
            jqTds[4].innerHTML = '<input type="text" id="GridProductUnitPrice" class="GridDigit Calculate form-control input-small" value="' + aData[4] + '">';
            jqTds[5].innerHTML = '<input type="text" id="GridProductDiscount"    maxlength=3 class="GridDigit Calculate form-control input-small" value="' + aData[5] + '">';
            jqTds[6].innerHTML = '<input type="text" id="GridProductSubTotal" class="GridDigit form-control input-small" disabled="true" value="' + aData[6] + '">';
            

            jqTds[7].innerHTML = '<a class="edit"   href="">Save</a>';
            jqTds[8].innerHTML = '<a class="cancel" href="">Cancel</a>';
            $("#purchaseOrderItemNewRow").prop("disabled", true);

            var getProduct = '' + aData[1] + '';
            //var getProductIdmatch = null;
            debugger;
            if (getProduct != "")
            {
                SelectedproductId = getProduct.substring(getProduct.indexOf('"') + 1, getProduct.lastIndexOf('"'));
                 
            } 
            if (editDDlVariable == true) {
                purchaseOrderGridAddNewRow(SelectedproductId);
                editDDlVariable = false;
            }
           


            
        }
       
        function saveRow(oTable, nRow) {
            debugger;
                      
            var firstName = $('#GridVendorProductCode');

            // Check if there is an entered value
            if (!firstName.val()) {

                debugger;
                // Add errors highlight 
                $('#GridVendorProductCode').css('border-color', 'red');
                   

                // Stop submission of the 
                   // var nRow = oTable.fnGetNodes(1);
                   //nEditing = nRow;
            }

            else {
                debugger;
                $("#purchaseOrderItemNewRow").prop("disabled", false);

                // Remove the errors highlight
              //  firstName.closest('.form-group').removeClass('has-error').addClass('has-success');


                var jqInputs = $('input,select,hidden', nRow);
                var itemInfo = $.grep(itemList, function (v) {
                    
                    return v.Value === jqInputs[1].value;
                });
 
            
                var selectedValue = jqInputs[1].value;
                var selectedText = itemInfo[0].Text;

                oTable.fnUpdate('<input type="radio" class="purchasecheckbox" name="PO" value="' + selectedText + '"  checked="checked"/>', nRow, 0, false);
                oTable.fnUpdate('<span name ="'+ selectedValue + '">' + selectedText + '</span>', nRow, 1, false);
                //oTable.fnUpdate(itemInfo[0].Text, nRow, 0, false);
               
                oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
                oTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
                oTable.fnUpdate(jqInputs[4].value, nRow, 4, false);
                oTable.fnUpdate(jqInputs[5].value, nRow, 5, false);
                oTable.fnUpdate(jqInputs[6].value, nRow, 6, false);
                oTable.fnUpdate('<a class="edit"  href="">Edit</a>', nRow, 7, false);
                oTable.fnUpdate('<a class="delete" href="">Delete</a>', nRow, 8, false);

                //$('#purchaseOrderItemGridTable').find('td:first').hide();
                oTable.fnDraw();
                nNew = false;

               // window.location.reload();


                var oTablePurchaseOrderItemGrid = $('#purchaseOrderItemGridTable').dataTable();
                var purchaseOrderItemGridTableArray = oTablePurchaseOrderItemGrid.fnGetData();
                if (purchaseOrderItemGridTableArray.length > 0) {                     
                    var TableArray = purchaseOrderItemGridTableArray;
                    var gridSingleRowArray;
                    var gridSubtotalSum = 0;
                    for (var i = 0; i < TableArray.length; i++) {
                        gridSingleRowArray = TableArray[i];
                        var getRowSubtotal = parseFloat(gridSingleRowArray[6]);
                        gridSubtotalSum = gridSubtotalSum + getRowSubtotal;
                    }

                   
                    $("#txt_CompleteGridItemOrderSubTotal_PO").val(gridSubtotalSum.toFixed(2));

                    var freightAmount = $("#txt_Freight_PO").val();

                    if (freightAmount !== '')
                    {
                        var orderTotalWithFreight = parseFloat(freightAmount) + gridSubtotalSum;
                          $("#txt_OrderTotal_PO").val(orderTotalWithFreight);
                    }
                    else
                    {
                           $("#txt_OrderTotal_PO").val(gridSubtotalSum);
                    }
                    
                    var currentOrderTotal = $("#txt_OrderTotal_PO").val();
                    var amountPaid = $("#txt_AmountPaid_PO").val();

                    if (amountPaid !== '' && currentOrderTotal !== '')
                    {
                        var balanceAmount = parseFloat(currentOrderTotal) - parseFloat(amountPaid);
                        $("#txt_Balance_PO").val(balanceAmount.toFixed(2));
                    }
                    else
                    {
                        $("#txt_Balance_PO").val(currentOrderTotal.toFixed(2));
                    }
                   
                }
                  
            }
             
        }

    
        function cancelEditRow(oTable, nRow) {
            debugger;
            var jqInputs = $('input', nRow);
            oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
            oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
            oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
            oTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
            oTable.fnUpdate(jqInputs[4].value, nRow, 4, false);
            oTable.fnUpdate(jqInputs[5].value, nRow, 5, false);
            oTable.fnUpdate(jqInputs[6].value, nRow, 6, false);
            oTable.fnUpdate(jqInputs[7].value, nRow, 7, false);
            oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 8, false);

            oTable.fnDraw();
        }

        function purchaseOrderGridAddNewRow(SelectedproductId)
        {
            debugger;
            $("#purchaseOrderItemNewRow").prop("disabled", true);

            $.ajax({
                type: "GET",
                url: "/PurchaseOrder/GetAllProductList",
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                traditional: true,
                success: function (data) {
                    debugger;
                    itemList = data;

             

                    $.each(data, function (i, _Product) {
                        if (_Product.Value == SelectedproductId) {
                            
                            $('.Itemddl').append('<option  selected="selected"  value="' + _Product.Value + '">' + _Product.Text + '</option>');
                        }

                        else
                        {
                            $('.Itemddl').append('<option  value="' + _Product.Value + '">' + _Product.Text + '</option>');
                        }
                       
                       
                       
                    })
  
                   
                },


                error: function (data) {
                    alert("Product List content load failed.");
                }

            });
             
        }


        $('#purchaseOrderItemNewRow').click(function (e) {
            debugger;
            e.preventDefault();
            editDDlVariable = true;


            var aiNew = oTable.fnAddData(['', '', '', '', '', '', '', '', '']);
            var test = aiNew[0];
            var nRow = oTable.fnGetNodes(aiNew[0]);
            editRow(oTable, nRow);
            nEditing = nRow;
            nNew = true;
            
           
        });

        table.on('click', '.delete', function (e) {
            e.preventDefault();
            debugger;
            var nRow = $(this).parents('tr')[0];
            oTable.fnDeleteRow(nRow);

            var oTablePurchaseOrderItemGrid = $('#purchaseOrderItemGridTable').dataTable();
            var purchaseOrderItemGridTableArray = oTablePurchaseOrderItemGrid.fnGetData();
            if (purchaseOrderItemGridTableArray.length > 0) {
                var TableArray = purchaseOrderItemGridTableArray;
                var gridSingleRowArray;
                var gridSubtotalSum = 0;
                for (var i = 0; i < TableArray.length; i++) {
                    gridSingleRowArray = TableArray[i];
                    var getRowSubtotal = parseFloat(gridSingleRowArray[6]);
                    gridSubtotalSum = gridSubtotalSum + getRowSubtotal;
                } 
                $("#txt_CompleteGridItemOrderSubTotal_PO").val(gridSubtotalSum.toFixed(2));

                var freightAmount = $("#txt_Freight_PO").val()

                if (freightAmount !== '') {
                    var orderTotalWithFreight = parseFloat(freightAmount) + gridSubtotalSum;
                    $("#txt_OrderTotal_PO").val(orderTotalWithFreight.toFixed(2));
                }
                else {
                    $("#txt_OrderTotal_PO").val(gridSubtotalSum.toFixed(2));
                }

                var currentOrderTotal = $("#txt_OrderTotal_PO").val();
                var amountPaid = $("#txt_AmountPaid_PO").val();

                if (amountPaid !== '' && currentOrderTotal !== '') {
                    var balanceAmount = parseFloat(currentOrderTotal) - parseFloat(amountPaid);
                    $("#txt_Balance_PO").val(balanceAmount.toFixed(2));
                }
                else {
                    $("#txt_Balance_PO").val(currentOrderTotal.toFixed(2));
                }

               
            }
            else
            {
                $("#txt_CompleteGridItemOrderSubTotal_PO").val('');
                $("#txt_OrderTotal_PO").val('');
                $("#txt_Balance_PO").val('');
            }

        });

        table.on('click', '.cancel', function (e) {

            debugger;
            e.preventDefault();
            $("#purchaseOrderItemNewRow").prop("disabled", false);

            if (nNew) {
                oTable.fnDeleteRow(nEditing);
                nNew = false;
            } else {
                restoreRow(oTable, nEditing);
                nEditing = null;
            }
        });

        table.on('click', '.edit', function (e) {
            e.preventDefault();
            debugger;
            /* Get the row as a parent of the link that was clicked on */
            var nRow = $(this).parents('tr')[0];

            if (nEditing !== null && nEditing != nRow) {
                /* Currently editing - but not this row - restore the old before continuing to edit mode */
                restoreRow(oTable, nEditing);
                editDDlVariable = true;
                editRow(oTable, nRow);
                nEditing = nRow;

            } else if (nEditing == nRow && this.innerHTML == "Save") {
                /* Editing this row and want to save it */
              
                saveRow(oTable, nEditing);
                 nEditing = null;
                //alert("Updated! Do not forget to do some ajax to sync with backend :)");
            } else {
                /* No edit in progress - let's start one */
                editDDlVariable = true;
                editRow(oTable, nRow);
                nEditing = nRow;
            }
        });


        //*******************************************************************************************//
        //******************************************************************************************//
        // This is start of second tab table data. (ReceiveGridTable)


        var OrderReceiveTable = $('#purchaseOrderReceiveItemGridTable');

        var OrderReceiveOTable = $('#purchaseOrderReceiveItemGridTable').dataTable({
            "lengthMenu": [
                [5, 10, 15, 20, -1],
                [5, 10, 15, 20, "All"] // change per page values here
            ],
            // set the initial value
            "pageLength": 10,

            "language": {
                "lengthMenu": " _MENU_ records"
            },
            "columnDefs": [{ // set default column settings
                'orderable': true,
                'targets': [0]
            }, {
                "searchable": true,
                "targets": [0]
            },
             {
                 "sClass": 'ui-datepicker-inline',
                 "targets": [4]
             }
             //,
             //{
             //    "visible": false,
             //    "targets": [5]
            //}
        ],
            "order": [
                [0, "asc"]
            ] // set first column as a default sort by asc
        });

        


        function purchaseOrderReceiveAddNewRow(SelectedproductId) {
            debugger;
            //e.preventDefault();
            $("#PurchaseOrderReceive_addNew").prop("disabled", true);

            $.ajax({
                type: "GET",
                url: "/PurchaseOrder/GetAllProductList",
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                traditional: true,
                success: function (data) {
                    debugger;
                    itemList = data;

                    $.each(data, function (i, _Product) {
                        if (_Product.Value == SelectedproductId) {

                            $('.Itemddl').append('<option  selected="selected"  value="' + _Product.Value + '">' + _Product.Text + '</option>');
                        }

                        else {
                            $('.Itemddl').append('<option  value="' + _Product.Value + '">' + _Product.Text + '</option>');
                        }



                    })




                },


                error: function (data) {
                    alert("Product List content load failed.");
                }

            });


        }

        $('#PurchaseOrderReceive_addNew').click(function (e) {
            debugger;
            e.preventDefault();           
            editDDlReceiveVariable = true;

            var aiNew = OrderReceiveOTable.fnAddData(['', '', '', '', '', '', '', '']);
            var nRow = OrderReceiveOTable.fnGetNodes(aiNew[0]);
            editPurchaseOrderReceiveRow(OrderReceiveOTable, nRow);
            nEditing = nRow;
            nNew = true;
             
        });
        function getLocationListForGridDdl(selectedLocationId) {
            debugger;
            //e.preventDefault();         

            $.ajax({
                type: "GET",
                url: "/PurchaseOrder/GetAllLocationList",
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                traditional: true,
                success: function (data) {
                    debugger;
                    locationList = data;

                    $.each(data, function (i, location) {
                        if (location.Value == selectedLocationId) {

                            $('.LocationDdl').append('<option  selected="selected"  value="' + location.Value + '">' + location.Text + '</option>');
                        }

                        else {
                            $('.LocationDdl').append('<option  value="' + location.Value + '">' + location.Text + '</option>');
                        }
                    })},
                error: function (data) {
                    alert("Location List content load failed.");
                }

            });


        }

        function editPurchaseOrderReceiveRow(OrderReceiveOTable, nRow) {

            debugger;
            // select everything when editing field in focus
            $('#purchaseOrderReceiveItemGridTable tbody td input').live('focus', function (e) {
                $(this).select();
            });

            // attach datepicker on focus and format to return yy-mm-dd	
            $('#purchaseOrderReceiveItemGridTable tbody td.ui-datepicker-inline input').live('focus', function (e) {
                $(this).datepicker({ dateFormat: 'yy-mm-dd' }).datepicker("show");
            });

            // override normal blur function ( needed for date month switching )
            $('#purchaseOrderReceiveItemGridTable tbody td input').live('blur', function (e) {
                return false;
            });

            // set change function to handle sumbit
            //$('#pricing tbody td.ui-datepicker-inline input').live('change', function (e) {
            //    $(this).parents("form").submit();
            //});

            var aData = OrderReceiveOTable.fnGetData(nRow);
            var jqTds = $('>td', nRow);
            //jqTds[0].innerHTML = '<input id="hdnProductid2" type="hidden"  value="' + aData[1] + '">';
               
            jqTds[0].innerHTML = '<select  id="SelectItem2" class="Itemddl form-control input-small" value="' + aData[0] + '">    </select">';

            jqTds[1].innerHTML = '<input type="text" id="gridReceiveProductCode" class="form-control input-small" value="' + aData[1] + '">';
            jqTds[2].innerHTML = '<input type="text" id="gridReceiveProductQuantity" maxlength=4  class="GridDigit form-control input-small" value="' + aData[2] + '">';
            jqTds[3].innerHTML = '<select  id="" class="LocationDdl form-control input-small" value="' + aData[3] + '">    </select">';
            jqTds[4].innerHTML = '<input type="text" id="gridReceiveProductDatepicker"  class="form-control input-small" value="' + aData[4] + '">';
            
            jqTds[5].innerHTML = '<a class="editReceiveOrder"   href="">Save</a>';

            jqTds[6].innerHTML = '<a class="cancelReceiveOrde" href="">Cancel</a>';

            $("#PurchaseOrderReceive_addNew").prop("disabled", true);

            var getProduct = '' + aData[0] + '';
            //var getProductIdmatch = null;
            debugger;
            if (getProduct != "") {
                SelectedproductId = getProduct.substring(getProduct.indexOf('"') + 1, getProduct.lastIndexOf('"'));

            }

            var getLocation = '' + aData[3] + '';

            if (getLocation != "") {
                selectedLocationId = getLocation.substring(getLocation.indexOf('"') + 1, getLocation.lastIndexOf('"'));

            }

            if (editDDlReceiveVariable == true) {              
                purchaseOrderReceiveAddNewRow(SelectedproductId);
                getLocationListForGridDdl(selectedLocationId);
                editDDlReceiveVariable = false;
            }
        }

        function saveReceiveOrderRow(OrderReceiveOTable, nRow) {
            debugger;
            $("#PurchaseOrderReceive_addNew").prop("disabled", false);

            var jqInputs = $('input,select,hidden', nRow);

            var itemInfo = $.grep(itemList, function (v) {
                return v.Value === jqInputs[0].value;
            });

            var selectedValue = jqInputs[0].value;
            var selectedText = itemInfo[0].Text;

            var locationInfo = $.grep(locationList, function (v) {
                return v.Value === jqInputs[3].value;
            });

            var locationSelectedValue = jqInputs[3].value;
            var locationSelectedText = locationInfo[0].Text;
            
            OrderReceiveOTable.fnUpdate('<span name ="' + selectedValue + '">' + selectedText + '</span>', nRow, 0, false);
            OrderReceiveOTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
            OrderReceiveOTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
            OrderReceiveOTable.fnUpdate('<label  id ="' + locationSelectedValue + '">' + locationSelectedText + '</label >', nRow, 3, false);
            OrderReceiveOTable.fnUpdate(jqInputs[4].value, nRow, 4, false);


            OrderReceiveOTable.fnUpdate('<a class="editReceiveOrder"  href="">Edit</a>', nRow, 5, false);
            OrderReceiveOTable.fnUpdate('<a class="deleteReceiveOrder" href="">Delete</a>', nRow, 6, false);


            OrderReceiveOTable.fnDraw();
            nNew = false;
        }

        function canceleditPurchaseOrderReceiveRow(OrderReceiveOTable, nRow) {
            debugger;
            var jqInputs = $('input', nRow);
            OrderReceiveOTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
            OrderReceiveOTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
            OrderReceiveOTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
            OrderReceiveOTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
            OrderReceiveOTable.fnUpdate(jqInputs[4].value, nRow, 4, false);
            OrderReceiveOTable.fnUpdate(jqInputs[5].value, nRow, 5, false);
            OrderReceiveOTable.fnUpdate(jqInputs[6].value, nRow, 6, false);
            
            OrderReceiveOTable.fnUpdate('<a class="editReceiveOrder" href="">Edit</a>', nRow, 7, false);

            OrderReceiveOTable.fnDraw();
        }


        OrderReceiveTable.on('click', '.editReceiveOrder', function (e) {
            e.preventDefault();
            debugger;
            /* Get the row as a parent of the link that was clicked on */
            var nRow = $(this).parents('tr')[0];

            if (nEditing !== null && nEditing != nRow) {
                /* Currently editing - but not this row - restore the old before continuing to edit mode */
                restoreRow(OrderReceiveOTable, nEditing);
                editDDlReceiveVariable = true;
                editPurchaseOrderReceiveRow(OrderReceiveOTable, nRow);
                nEditing = nRow;
            } else if (nEditing == nRow && this.innerHTML == "Save") {
                /* Editing this row and want to save it */
               
                saveReceiveOrderRow(OrderReceiveOTable, nEditing);
                nEditing = null;
                //alert("Updated! Do not forget to do some ajax to sync with backend :)");
            } else {
                /* No edit in progress - let's start one */
                editDDlReceiveVariable = true;
                editPurchaseOrderReceiveRow(OrderReceiveOTable, nRow);
                nEditing = nRow;
            }
        });

        OrderReceiveTable.on('click', '.deleteReceiveOrder', function (e) {
            e.preventDefault();
            debugger;
            var nRow = $(this).parents('tr')[0];
            OrderReceiveOTable.fnDeleteRow(nRow);
        });


        OrderReceiveTable.on('click', '.cancelReceiveOrde', function (e) {

            debugger;
            e.preventDefault();
            $("#PurchaseOrderReceive_addNew").prop("disabled", false);

            if (nNew) {
                OrderReceiveOTable.fnDeleteRow(nEditing);
                nNew = false;
            } else {
                restoreReceiveRow(OrderReceiveOTable, nEditing);
                nEditing = null;
            }
        });

        function restoreReceiveRow(OrderReceiveOTable, nRow) {
            debugger;
            var aData = OrderReceiveOTable.fnGetData(nRow);
            if (aData !== null) {
                var jqTds = $('>td', nRow);

                for (var i = 0, iLen = jqTds.length; i < iLen; i++) {
                    OrderReceiveOTable.fnUpdate(aData[i], nRow, i, false);
                }

                OrderReceiveOTable.fnDraw();
            }

        }

        //*******************************************************************************************//
        //******************************************************************************************//
        // This is start of third tab table data. (ReturnGridTable)

        var OrderReturnTable = $('#purchaseOrderItemReturnGridTable');

        var OrderReturnOTable = $('#purchaseOrderItemReturnGridTable').dataTable({
            "lengthMenu": [
                [5, 10, 15, 20, -1],
                [5, 10, 15, 20, "All"] // change per page values here
            ],
            // set the initial value
            "pageLength": 10,

            "language": {
                "lengthMenu": " _MENU_ records"
            },
            "columnDefs": [{ // set default column settings
                'orderable': true,
                'targets': [0]
            }, {
                "searchable": true,
                "targets": [0]
            }
            ,
             {
                 "sClass": 'ui-datepicker-inline',
                 "targets": [3]
             }
            ],
            "order": [
                [0, "asc"]
            ] // set first column as a default sort by asc
        });

        function purchaseOrderReturnAddNewRow(SelectedproductId) {
            debugger;
            //e.preventDefault();
            $("#PurchaseOrderReturn_addNew").prop("disabled", true);

            $.ajax({
                type: "GET",
                url: "/PurchaseOrder/GetAllProductList",
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                traditional: true,
                success: function (data) {
                    debugger;
                    itemList = data;

                    $.each(data, function (i, _Product) {
                        if (_Product.Value == SelectedproductId) {

                            $('.Itemddl').append('<option  selected="selected"  value="' + _Product.Value + '">' + _Product.Text + '</option>');
                        }

                        else {
                            $('.Itemddl').append('<option  value="' + _Product.Value + '">' + _Product.Text + '</option>');
                        }
                     }); 
                },
                error: function (data) {
                    alert("Product List content load failed.");
                }

            });


        }

        $('#PurchaseOrderReturn_addNew').click(function (e) {
            debugger;
            e.preventDefault();
         
            editDDlReturnVariable = true;

            var aiNew = OrderReturnOTable.fnAddData(['', '', '', '', '', '', '', '' ,'','']);
            var nRow = OrderReturnOTable.fnGetNodes(aiNew[0]);
            editPurchaseOrderReturnRow(OrderReturnOTable, nRow);
            nEditing = nRow;
            nNew = true;

        });


        function editPurchaseOrderReturnRow(OrderReturnOTable, nRow) {

            debugger;
            //************************************************************//
            // This all code for date picker..
            // select everything when editing field in focus
            $('#purchaseOrderItemReturnGridTable tbody td input').live('focus', function (e) {
                $(this).select();
            });

            // attach datepicker on focus and format to return yy-mm-dd	
            $('#purchaseOrderItemReturnGridTable tbody td.ui-datepicker-inline input').live('focus', function (e) {
                $(this).datepicker({ dateFormat: 'yy-mm-dd' }).datepicker("show");
            });

            // override normal blur function ( needed for date month switching )
            $('#purchaseOrderItemReturnGridTable tbody td input').live('blur', function (e) {
                return false;
            });

            // set change function to handle sumbit
            //$('#pricing tbody td.ui-datepicker-inline input').live('change', function (e) {
            //    $(this).parents("form").submit();
            //});


            var aData = OrderReturnOTable.fnGetData(nRow);
            var jqTds = $('>td', nRow);
            
            jqTds[0].innerHTML = '<select  id="SelectItem3" class="Itemddl form-control input-small" value="' + aData[0] + '">   </select">';

            jqTds[1].innerHTML = '<input type="text" id="gridReturnProductCode" class=" form-control input-small" value="' + aData[1] + '">';
            jqTds[2].innerHTML = '<input type="text" id="gridReturnProductQuantity" maxlength="4"  class="calculateReturn GridDigit form-control input-small" value="' + aData[2] + '">';
            jqTds[3].innerHTML = '<input type="text" id="gridReturnProductDatepicker"  class="form-control input-small" value="' + aData[3] + '">';
            jqTds[4].innerHTML = '<input type="text" id="gridReturnProductUnitPrice"  class="calculateReturn GridDigit form-control input-small" value="' + aData[4] + '">';
            jqTds[5].innerHTML = '<input type="text" id="gridReturnProductDiscount" maxlength="2" class="calculateReturn GridDigit form-control input-small" value="' + aData[5] + '">';
            jqTds[6].innerHTML = '<input type="text" id="gridReturnProductSubTotal" disabled="true" class="  form-control input-small" value="' + aData[6] + '">';


            jqTds[7].innerHTML = '<a class="editReturnOrder"   href="">Save</a>';
            jqTds[8].innerHTML = '<a class="cancelReturnOrde" href="">Cancel</a>';
            $("#PurchaseOrderReturn_addNew").prop("disabled", true);


            var getProduct = '' + aData[0] + '';
            //var getProductIdmatch = null;
            debugger;
            if (getProduct != "") {
                SelectedproductId = getProduct.substring(getProduct.indexOf('"') + 1, getProduct.lastIndexOf('"'));

            }


            if( editDDlReturnVariable ==true)
            {
                purchaseOrderReturnAddNewRow(SelectedproductId);
                editDDlReturnVariable = false;
            }
        }

        function saveReturnOrderRow(OrderReturnOTable, nRow) {
            debugger;
            $("#PurchaseOrderReturn_addNew").prop("disabled", false);
            var jqInputs = $('input,select,hidden', nRow);

            var itemInfo = $.grep(itemList, function (v) {
                return v.Value === jqInputs[0].value;
            });

            var selectedValue = jqInputs[0].value;
            var selectedText = itemInfo[0].Text;

            OrderReturnOTable.fnUpdate('<span name ="' + selectedValue + '">' + selectedText + '</span>', nRow, 0, false);
            OrderReturnOTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
            OrderReturnOTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
            OrderReturnOTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
            OrderReturnOTable.fnUpdate(jqInputs[4].value, nRow, 4, false);
            OrderReturnOTable.fnUpdate(jqInputs[5].value, nRow, 5, false);
            OrderReturnOTable.fnUpdate(jqInputs[6].value, nRow, 6, false);
          

            OrderReturnOTable.fnUpdate('<a class="editReturnOrder"  href="">Edit</a>', nRow, 7, false);
            OrderReturnOTable.fnUpdate('<a class="deleteReturnOrder" href="">Delete</a>', nRow, 8, false);


            OrderReturnOTable.fnDraw();

            nNew = false;


            var oTablePurchaseOrderReturnItemGrid = $('#purchaseOrderItemReturnGridTable').dataTable();
            var purchaseOrderReturnItemGridTableArray = oTablePurchaseOrderReturnItemGrid.fnGetData();
            if (purchaseOrderReturnItemGridTableArray.length > 0) {
                var TableArray = purchaseOrderReturnItemGridTableArray;
                var gridSingleRowArray;
                var gridSubtotalSum = 0;
                for (var i = 0; i < TableArray.length; i++) {
                    gridSingleRowArray = TableArray[i];
                    var getRowSubtotal = parseFloat(gridSingleRowArray[6]);
                    gridSubtotalSum = gridSubtotalSum + getRowSubtotal;
                }


                $("#txt_CompleteGridItemReturnSubTotal_PO").val(gridSubtotalSum.toFixed(2));

                var freightAmount = $("#txt_Freight_PO").val();

                if (freightAmount !== '') {
                    var orderTotalWithFreight = parseFloat(freightAmount) + gridSubtotalSum;
                    $("#txt_ReturnTotal_PO").val(orderTotalWithFreight);
                }
                else {
                    $("#txt_ReturnTotal_PO").val(gridSubtotalSum);
                }

                //var currentOrderTotal = $("#txt_OrderTotal_PO").val();
                //var amountPaid = $("#txt_AmountPaid_PO").val();

                //if (amountPaid !== '' && currentOrderTotal !== '') {
                //    var balanceAmount = parseFloat(currentOrderTotal) - parseFloat(amountPaid);
                //    $("#txt_Balance_PO").val(balanceAmount.toFixed(2));
                //}
                //else {
                //    $("#txt_Balance_PO").val(currentOrderTotal.toFixed(2));
                //}

            }
        }

        function canceleditPurchaseOrderReturnRow(OrderReturnOTable, nRow) {
            debugger;
            var jqInputs = $('input', nRow);
            OrderReturnOTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
            OrderReturnOTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
            OrderReturnOTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
            OrderReturnOTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
            OrderReturnOTable.fnUpdate(jqInputs[4].value, nRow, 4, false);
            OrderReturnOTable.fnUpdate(jqInputs[5].value, nRow, 5, false);
            OrderReturnOTable.fnUpdate(jqInputs[6].value, nRow, 6, false);
            OrderReturnOTable.fnUpdate(jqInputs[7].value, nRow, 7, false);
            OrderReturnOTable.fnUpdate(jqInputs[8].value, nRow, 8, false);

            OrderReturnOTable.fnUpdate('<a class="editReturnOrder" href="">Edit</a>', nRow, 9, false);

            OrderReturnOTable.fnDraw();
        }


        OrderReturnTable.on('click', '.editReturnOrder', function (e) {
            e.preventDefault();
            debugger;
            
            /* Get the row as a parent of the link that was clicked on */
            var nRow = $(this).parents('tr')[0];

            if (nEditing !== null && nEditing != nRow) {
                /* Currently editing - but not this row - restore the old before continuing to edit mode */
                restoreRow(OrderReturnOTable, nEditing);
                editDDlReturnVariable = true;
                editPurchaseOrderReturnRow(OrderReturnOTable, nRow);
                nEditing = nRow;
            } else if (nEditing == nRow && this.innerHTML == "Save") {
                /* Editing this row and want to save it */
                saveReturnOrderRow(OrderReturnOTable, nEditing);
                nEditing = null;
                //alert("Updated! Do not forget to do some ajax to sync with backend :)");
            } else {
                /* No edit in progress - let's start one */

                editDDlReturnVariable = true;
                editPurchaseOrderReturnRow(OrderReturnOTable, nRow);
                nEditing = nRow;
            }
        });

        OrderReturnTable.on('click', '.deleteReturnOrder', function (e) {
            e.preventDefault();
            debugger;
            var nRow = $(this).parents('tr')[0];
            OrderReturnOTable.fnDeleteRow(nRow);

            var oTablePurchaseOrderReturnItemGrid = $('#purchaseOrderItemReturnGridTable').dataTable();
            var purchaseOrderReturnItemGridTableArray = oTablePurchaseOrderReturnItemGrid.fnGetData();
            if (purchaseOrderReturnItemGridTableArray.length > 0) {
                var TableArray = purchaseOrderReturnItemGridTableArray;
                var gridSingleRowArray;
                var gridSubtotalSum = 0;
                for (var i = 0; i < TableArray.length; i++) {
                    gridSingleRowArray = TableArray[i];
                    var getRowSubtotal = parseFloat(gridSingleRowArray[6]);
                    gridSubtotalSum = gridSubtotalSum + getRowSubtotal;
                }


                $("#txt_CompleteGridItemReturnSubTotal_PO").val(gridSubtotalSum.toFixed(2));

                var freightAmount = $("#txt_Freight_PO").val();

                if (freightAmount !== '') {
                    var orderTotalWithFreight = parseFloat(freightAmount) + gridSubtotalSum;
                    $("#txt_ReturnTotal_PO").val(orderTotalWithFreight);
                }
                else {
                    $("#txt_ReturnTotal_PO").val(gridSubtotalSum);
                }
            }

            else
            {
                $("#txt_CompleteGridItemReturnSubTotal_PO").val('');
                $("#txt_Freight_PO").val('');
                $("#txt_ReturnTotal_PO").val('');
            }

             

        });


        OrderReturnTable.on('click', '.cancelReturnOrde', function (e) {

            debugger;
            e.preventDefault();
            $("#PurchaseOrderReturn_addNew").prop("disabled", false);

            if (nNew) {
                OrderReturnOTable.fnDeleteRow(nEditing);
                nNew = false;
            } else {
                restoreRow(OrderReturnOTable, nEditing);
                nEditing = null;
            }
        });



        //*******************************************************************************************//
        //******************************************************************************************//
        // This is start of fourth tab table data. (UnstockGridTable)

        var OrderUnstockTable = $('#purchaseOrderItemUnstockGridTable');

        var OrderUnstockOTable = $('#purchaseOrderItemUnstockGridTable').dataTable({
            "lengthMenu": [
                [5, 10, 15, 20, -1],
                [5, 10, 15, 20, "All"] // change per page values here
            ],
            // set the initial value
            "pageLength": 10,

            "language": {
                "lengthMenu": " _MENU_ records"
            },
            "columnDefs": [{ // set default column settings
                'orderable': true,
                'targets': [0]
            }, {
                "searchable": true,
                "targets": [0]
            },
            {
                "sClass": 'ui-datepicker-inline',
                "targets": [2]
            }
            ],
            "order": [
                [0, "asc"]
            ] // set first column as a default sort by asc
        });

        function purchaseOrderUnstockAddNewRow(SelectedproductId) {
            debugger;
            //e.preventDefault();
            $("#PurchaseOrderUnstock_addNew").prop("disabled", true);

            $.ajax({
                type: "GET",
                url: "/PurchaseOrder/GetAllProductList",
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                traditional: true,
                success: function (data) {
                    debugger;
                    itemList = data;

                    $.each(data, function (i, _Product) {
                        if (_Product.Value == SelectedproductId) {

                            $('.Itemddl').append('<option  selected="selected"  value="' + _Product.Value + '">' + _Product.Text + '</option>');
                        }

                        else {
                            $('.Itemddl').append('<option  value="' + _Product.Value + '">' + _Product.Text + '</option>');
                        }



                    });




                },


                error: function (data) {
                    alert("Product List content load failed.");
                }

            });


        }

        $('#PurchaseOrderUnstock_addNew').click(function (e) {
            debugger;
            e.preventDefault();
            editDDlUnstockVariable = true;
          


            var aiNew = OrderUnstockOTable.fnAddData(['', '', '', '', '', '', '']);
            var nRow = OrderUnstockOTable.fnGetNodes(aiNew[0]);
            editPurchaseOrderUnstockRow(OrderUnstockOTable, nRow);
            nEditing = nRow;
            nNew = true;

        });

        function editPurchaseOrderUnstockRow(OrderUnstockOTable, nRow) {

            debugger;
            //************************************************************//
            // This all code for date picker..
            // select everything when editing field in focus
            $('#purchaseOrderItemUnstockGridTable tbody td input').live('focus', function (e) {
                $(this).select();
            });

            // attach datepicker on focus and format to return yy-mm-dd	
            $('#purchaseOrderItemUnstockGridTable tbody td.ui-datepicker-inline input').live('focus', function (e) {
                $(this).datepicker({ dateFormat: 'yy-mm-dd' }).datepicker("show");
            });

            // override normal blur function ( needed for date month switching )
            $('#purchaseOrderItemUnstockGridTable tbody td input').live('blur', function (e) {
                return false;
            });

            // set change function to handle sumbit
            //$('#pricing tbody td.ui-datepicker-inline input').live('change', function (e) {
            //    $(this).parents("form").submit();
            //});
         //   '<label  id ="' + locationSelectedValue + '">' + locationSelectedText + '</label >'

            var aData = OrderUnstockOTable.fnGetData(nRow);
            var jqTds = $('>td', nRow);
          
            jqTds[0].innerHTML = '<select  id="SelectItem4" class="Itemddl form-control input-small" value="' + aData[0] + '">    </select">';

            jqTds[1].innerHTML = '<input type="text" id="gridUnstockProductQuantity" maxlength="4" class="GridDigit form-control input-small" value="' + aData[1] + '">';
            jqTds[2].innerHTML = '<input type="text" id="gridUnstockProductDatepicker"   class="form-control input-small" value="' + aData[2] + '">';
            jqTds[3].innerHTML = '<select  id="" class="LocationDdl form-control input-small" value="' + aData[3] + '">    </select">';
          
            jqTds[4].innerHTML = '<a class="editUnstockOrder"   href="">Save</a>';
            jqTds[5].innerHTML = '<a class="cancelUnstockOrde" href="">Cancel</a>';
            $("#PurchaseOrderUnstock_addNew").prop("disabled", true);

            var getProduct = '' + aData[0] + '';
            
            if (getProduct != "") {
                SelectedproductId = getProduct.substring(getProduct.indexOf('"') + 1, getProduct.lastIndexOf('"'));

            }


            var getLocation = '' + aData[3] + '';
            
            if (getLocation != "") {
                selectedLocationId = getLocation.substring(getLocation.indexOf('"') + 1, getLocation.lastIndexOf('"'));

            }
            if (editDDlUnstockVariable == true)
            {
                purchaseOrderUnstockAddNewRow(SelectedproductId);
                getLocationListForGridDdl(selectedLocationId);
                editDDlUnstockVariable = false;
            }
        }

        function saveUnstockOrderRow(OrderUnstockOTable, nRow) {
            debugger;



            //var firstName = $('#GridVendorProductCode');

            //// Check if there is an entered value
            //if (!firstName.val()) {

            //    debugger;
            //    // Add errors highlight 
            //    $('#GridVendorProductCode').css('border-color', 'red');
            //    //firstName.closest('.form-group').removeClass('has-success').addClass('has-error');

            //    // Stop submission of the 
            //    // var nRow = oTable.fnGetNodes(1);
            //    //nEditing = nRow;
            //}

           // else {
                debugger;
                $("#PurchaseOrderUnstock_addNew").prop("disabled", false);

                // Remove the errors highlight
                //  firstName.closest('.form-group').removeClass('has-error').addClass('has-success');


                var jqInputs = $('input,select,hidden', nRow);
                var itemInfo = $.grep(itemList, function (v) {
                    return v.Value === jqInputs[0].value;
                });

                var selectedValue = jqInputs[0].value;
                var selectedText = itemInfo[0].Text;

                var locationInfo = $.grep(locationList, function (v) {
                    return v.Value === jqInputs[3].value;
                });

                var locationSelectedValue = jqInputs[3].value;
                var locationSelectedText = locationInfo[0].Text;


                OrderUnstockOTable.fnUpdate('<span name ="' + selectedValue + '">' + selectedText + '</span>', nRow, 0, false);
                OrderUnstockOTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
                OrderUnstockOTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
                OrderUnstockOTable.fnUpdate('<label  id ="' + locationSelectedValue + '">' + locationSelectedText + '</label >', nRow, 3, false);
                
              

                OrderUnstockOTable.fnUpdate('<a class="editUnstockOrder"  href="">Edit</a>', nRow, 4, false);
                OrderUnstockOTable.fnUpdate('<a class="deleteUnstockOrder" href="">Delete</a>', nRow, 5, false);

                //$('#purchaseOrderItemGridTable').find('td:first').hide();
                OrderUnstockOTable.fnDraw();
                nNew = false;


           // }





        }


        function canceleditPurchaseOrderUnstockRow(OrderUnstockOTable, nRow) {
            debugger;
            var jqInputs = $('input', nRow);
            OrderUnstockOTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
            OrderUnstockOTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
            OrderUnstockOTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
            OrderUnstockOTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
            OrderUnstockOTable.fnUpdate(jqInputs[4].value, nRow, 4, false);
             
            OrderUnstockOTable.fnUpdate('<a class="editUnstockOrder" href="">Edit</a>', nRow, 6, false);

            OrderUnstockOTable.fnDraw();
        }

        OrderUnstockTable.on('click', '.editUnstockOrder', function (e) {
            e.preventDefault();
            debugger;
         
            /* Get the row as a parent of the link that was clicked on */
            var nRow = $(this).parents('tr')[0];

            if (nEditing !== null && nEditing != nRow) {
                /* Currently editing - but not this row - restore the old before continuing to edit mode */
                restoreRow(OrderUnstockOTable, nEditing);
                editDDlUnstockVariable = true;
                editPurchaseOrderUnstockRow(OrderUnstockOTable, nRow);
                nEditing = nRow;
            } else if (nEditing == nRow && this.innerHTML == "Save") {
                /* Editing this row and want to save it */
                saveUnstockOrderRow(OrderUnstockOTable, nEditing);
                nEditing = null;
                //alert("Updated! Do not forget to do some ajax to sync with backend :)");
            } else {
                /* No edit in progress - let's start one */

                editDDlUnstockVariable = true;
                editPurchaseOrderUnstockRow(OrderUnstockOTable, nRow);
                nEditing = nRow;
            }
        });

        OrderUnstockTable.on('click', '.deleteUnstockOrder', function (e) {
            e.preventDefault();
            debugger;
            var nRow = $(this).parents('tr')[0];
            OrderUnstockOTable.fnDeleteRow(nRow);
        });


        OrderUnstockTable.on('click', '.cancelUnstockOrde', function (e) {

            debugger;
            e.preventDefault();
            $("#PurchaseOrderUnstock_addNew").prop("disabled", false);

            if (nNew) {
                OrderUnstockOTable.fnDeleteRow(nEditing);
                nNew = false;
            } else {
                restoreRow(OrderUnstockOTable, nEditing);
                nEditing = null;
            }
        });
    }

    return {

        //main function to initiate the module
        init: function () {
            handleTable();
        }

    };

}();