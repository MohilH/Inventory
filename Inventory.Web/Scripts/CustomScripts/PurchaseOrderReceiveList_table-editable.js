var itemList = [];

 

var TableEditable = function () {

    var handleTable = function () {

        function restoreRow(oOrderReceiveGridTable, nRow) {
            debugger;
            var aData = oOrderReceiveGridTable.fnGetData(nRow);
            if (aData !== null) {
                var jqTds = $('>td', nRow);

                for (var i = 0, iLen = jqTds.length; i < iLen; i++) {
                    oOrderReceiveGridTable.fnUpdate(aData[i], nRow, i, false);
                }

                oOrderReceiveGridTable.fnDraw();
            }

        }


        function editRow(oOrderReceiveGridTable, nRow) {

            debugger;
            var aData = oOrderReceiveGridTable.fnGetData(nRow);
            var jqTds = $('>td', nRow);
            jqTds[0].innerHTML = '<input type="hidden"   value="' + aData[1] + '">';
            jqTds[1].innerHTML = '<select  id="SelectItem" class="Itemddl form-control input-small" >  <option value="' + aData[1] + '"> </option>  </select">';

            jqTds[2].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[2] + '">';
            jqTds[3].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[3] + '">';
            jqTds[4].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[4] + '">';
            jqTds[5].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[5] + '">';
            

            //jqTds[6].innerHTML = '<input type="checkbox" name="vehicle"   value="' + aData[0] + '">';
            jqTds[6].innerHTML = '<a class="edit"   href="">Save</a>';
            jqTds[7].innerHTML = '<a class="cancel" href="">Cancel</a>';

        }

        function saveRow(oOrderReceiveGridTable, nRow) {
            debugger;

            var jqInputs = $('input,select,hidden', nRow);

            var itemInfo = $.grep(itemList, function (v) {
                return v.Value === jqInputs[1].value;
            });
            oOrderReceiveGridTable.fnUpdate(jqInputs[1].value, nRow, 0, false);
            oOrderReceiveGridTable.fnUpdate((itemInfo[0]) ? itemInfo[0].Text : "", nRow, 1, false);
            oOrderReceiveGridTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
            oOrderReceiveGridTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
            oOrderReceiveGridTable.fnUpdate(jqInputs[4].value, nRow, 4, false);
            oOrderReceiveGridTable.fnUpdate(jqInputs[5].value, nRow, 5, false);
            
            //oOrderReceiveGridTable.fnUpdate(jqInputs[0].value, nRow, 6, false);
            oOrderReceiveGridTable.fnUpdate('<a class="edit"  href="">Edit</a>', nRow, 6, false);
            oOrderReceiveGridTable.fnUpdate('<a class="delete" href="">Delete</a>', nRow, 7, false);
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
            oOrderReceiveGridTable.fnDraw();
        }


        function cancelEditRow(oOrderReceiveGridTable, nRow) {
            var jqInputs = $('input', nRow);
            oOrderReceiveGridTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
            oOrderReceiveGridTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
            oOrderReceiveGridTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
            oOrderReceiveGridTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
            oOrderReceiveGridTable.fnUpdate(jqInputs[4].value, nRow, 4, false);
            oOrderReceiveGridTable.fnUpdate(jqInputs[5].value, nRow, 5, false);
            oOrderReceiveGridTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 6, false);

            oOrderReceiveGridTable.fnDraw();
        }

         
        var table = $('#sample_editable_2');


        var oOrderReceiveGridTable = table.dataTable({
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
            { // This is use for hidden fiels
                "targets": [ 2 ],
            "visible": false,
            "searchable": false
        }],
            "order": [
                [0, "asc"]
            ] // set first column as a default sort by asc
        });

        var tableWrapper = $("#sample_editable_2_wrapper");

        tableWrapper.find(".dataTables_length select").select2({
            showSearchInput: false //hide search box with special css class
        }); // initialize select2 dropdown

        var nEditing = null;
        var nNew = false;

        $('#PurchaseOrderReceive_addNew').click(function (e) {
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

            

            var aiNew = oOrderReceiveGridTable.fnAddData(['', '', '', '', '', '', '', '' ]);
            var nRow = oOrderReceiveGridTable.fnGetNodes(aiNew[0]);
            editRow(oOrderReceiveGridTable, nRow);
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

            oOrderReceiveGridTable.fnDeleteRow(nRow);
            //alert("Deleted! Do not forget to do some ajax to sync with backend :)");
        });

        table.on('click', '.cancel', function (e) {
            e.preventDefault();

            if (nNew) {
                oOrderReceiveGridTable.fnDeleteRow(nEditing);
                nNew = false;
            } else {
                restoreRow(oOrderReceiveGridTable, nEditing);
                nEditing = null;
            }
        });

        table.on('click', '.', function (e) {
            e.preventDefault();
            debugger;
            /* Get the row as a parent of the link that was clicked on */
            var nRow = $(this).parents('tr')[0];

            if (nEditing !== null && nEditing != nRow) {
                /* Currently editing - but not this row - restore the old before continuing to edit mode */
                restoreRow(oOrderReceiveGridTable, nEditing);
                editRow(oOrderReceiveGridTable, nRow);
                nEditing = nRow;
            } else if (nEditing == nRow && this.innerHTML == "Save") {
                /* Editing this row and want to save it */
                saveRow(oOrderReceiveGridTable, nEditing);
                nEditing = null;
                //alert("Updated! Do not forget to do some ajax to sync with backend :)");
            } else {
                /* No edit in progress - let's start one */
                editRow(oOrderReceiveGridTable, nRow);
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