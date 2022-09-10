

var itemList = [];
var AllItemsIds = [];
$("# ").click(function () {

    debugger;

    var VendorId = $("#ddl_Vendor option:selected").val();
    //var LocationId = $("#ddl_LocationId option:selected").val();
    var Model = [];
    if (VendorId !== undefined && VendorId !== "") {


        var oTable = $('#sample_editable_1').dataTable();
        var TableArray = oTable.fnGetData();
        var ProductArray;
        for (var i = 0; i < TableArray.length; i++) {
            ProductArray = TableArray[i];
            ProductArray.splice(-2, 2)
            ProductArray.push(VendorId);

            Model.push({
                ProdId: ProductArray[0],
                VendorItemCode: ProductArray[2],
                Quantity: ProductArray[3],
                UnitPrice: ProductArray[4],
                Discount: ProductArray[5],
                Cost: ProductArray[6],
                VendorId: ProductArray[7]
            });


        }



        $.ajax({
            url: '/PurchaseOrder/AddPurchaseOrderItems',
            contentType: "application/json; charset=utf-8",
            type: "POST",
            dataType: "json",
            data: JSON.stringify(Model),

            success: function () {
                //alert('Success'); 
                // itemList = [];
            },
        });
        //if (confirm("Previose row not saved. Do you want to save it ?")) {

        //    saveRow(oTable, nEditing); // save
        //    $(nEditing).find("td:first").html("Untitled");
        //    nEditing = null;
        //    nNew = false;

        //}
        //else {
        //    oTable.fnDeleteRow(nEditing); // cancel
        //    nEditing = null;
        //    nNew = false;

        //    return;
        //} 

        // alert("Test");
    }
    else {
        alert("Vendor is not Selected.")
    }

});

var TableEditable = function () {

    var handleTable = function () {

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


        function editRow(oTable, nRow) {

            debugger;
            var aData = oTable.fnGetData(nRow);
            var jqTds = $('>td', nRow);
            jqTds[0].innerHTML = '<input type="hidden"   value="' + aData[1] + '">';
            jqTds[1].innerHTML = '<select  id="SelectItem" class="Itemddl form-control input-small" >  <option value="' + aData[1] + '"> </option>  </select">';

            jqTds[2].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[2] + '">';
            jqTds[3].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[3] + '">';
            jqTds[4].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[4] + '">';
           

            //jqTds[6].innerHTML = '<input type="checkbox" name="vehicle"   value="' + aData[0] + '">';
            jqTds[5].innerHTML = '<a class="edit"   href="">Save</a>';
            jqTds[6].innerHTML = '<a class="cancel" href="">Cancel</a>';

        }

        function saveRow(oTable, nRow) {
            debugger;

            var jqInputs = $('input,select,hidden', nRow);

            var itemInfo = $.grep(itemList, function (v) {
                return v.Value === jqInputs[1].value;
            });
            oTable.fnUpdate(jqInputs[1].value, nRow, 0, false);
            oTable.fnUpdate((itemInfo[0]) ? itemInfo[0].Text : "", nRow, 1, false);
            oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
            oTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
            oTable.fnUpdate(jqInputs[4].value, nRow, 4, false);
            
            //oTable.fnUpdate(jqInputs[0].value, nRow, 6, false);
            oTable.fnUpdate('<a class="edit"  href="">Edit</a>', nRow, 5, false);
            oTable.fnUpdate('<a class="delete" href="">Delete</a>', nRow, 6, false);
            //var model = {
            //    VendorId: jqInputs[0].value,
            //    ProdId: jqInputs[1].value,


            //    VendorItemId: jqInputs[2].value,
            //    Name: jqInputs[3].value,
            //    VendorItemCode: jqInputs[4].value,
            //    Cost: jqInputs[5].value,
            //    Cost1: jqInputs[5].value
            //}
            //$.ajax({
            //    url: '/Vendor/SaveVendorProducts',
            //    type: "POST",
            //    data: { vendorProductModel: model },
            //    dataType: "json",
            //    success: function () { alert('Success'); },
            //});
            //$('.edit').on('click', function () {
            //});
            //$(document).on('click', '.delete', function () {

            //});
            oTable.fnDraw();
        }


        function cancelEditRow(oTable, nRow) {
            var jqInputs = $('input', nRow);
            oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
            oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
            oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
            oTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
            oTable.fnUpdate(jqInputs[4].value, nRow, 4, false);
            oTable.fnUpdate(jqInputs[5].value, nRow, 5, false);
            oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 6, false);

            oTable.fnDraw();
        }




        var table = $('#sample_editable_1');


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
                'targets': [0]
            }, {
                "searchable": true,
                "targets": [0]
            }],
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

        $('#sample_editable_1_new').click(function (e) {
            debugger;
            e.preventDefault();


            $.ajax({
                type: "GET",
                url: "/PurchaseOrder/GetAllProductList",
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                traditional: true,
                success: function (data) {
                    debugger;
                    itemList = data;

                    $.each(data, function (i, _State) {

                        $('.Itemddl').append('<option value="' + _State.Value + '">' + _State.Text + '</option>');
                    })




                },


                error: function (data) {
                    alert("Dynamic content load failed.");
                }

            });

            //else {
            //    var optionsString = "";
            //    $.each(itemList, function (key, state) {

            //      optionsString += '<option value="' + state.Value + '">' + state.Text + '</option>';
            //    })

            //    $('.Itemddl').empty().append(optionsString);
            //}

            //    $.ajax({
            //        url: '/PurchaseOrder/GetAllProductList',
            //        type: "Get",
            //        data: { vendorProductModel: model },
            //        dataType: "json",
            //        success: function () { alert('Success'); },
            //    });
            //    if (confirm("Previose row not saved. Do you want to save it ?")) {

            //        saveRow(oTable, nEditing); // save
            //   

            var aiNew = oTable.fnAddData(['', '', '', '', '', '',  '']);
            var nRow = oTable.fnGetNodes(aiNew[0]);
            editRow(oTable, nRow);
            nEditing = nRow;
            nNew = true;
        });

        table.on('click', '.delete', function (e) {
            e.preventDefault();
            debugger;

            ////if (confirm("Are you sure to delete this row ?") == false) {
            ////    return;
            ////}

            var nRow = $(this).parents('tr')[0];

            //var model = {
            //    ProdId: nRow[],
            //    VendorId: nRow,
            //    VendorItemId: nRow,
            //    Name: nRow,
            //    VendorItemCode: nRow,
            //    Cost: nRow
            //}
            //    $.ajax({
            //        url: '/Vendor/deleteVendorbyId',
            //        type: "POST",
            //        data: { vendorProductModel: model },
            //        dataType: "json",
            //        success: function () { alert('Success'); },
            //    });

            oTable.fnDeleteRow(nRow);
            //alert("Deleted! Do not forget to do some ajax to sync with backend :)");
        });

        table.on('click', '.cancel', function (e) {
            e.preventDefault();

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
                editRow(oTable, nRow);
                nEditing = nRow;
            } else if (nEditing == nRow && this.innerHTML == "Save") {
                /* Editing this row and want to save it */
                saveRow(oTable, nEditing);
                nEditing = null;
                //alert("Updated! Do not forget to do some ajax to sync with backend :)");
            } else {
                /* No edit in progress - let's start one */
                editRow(oTable, nRow);
                nEditing = nRow;
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