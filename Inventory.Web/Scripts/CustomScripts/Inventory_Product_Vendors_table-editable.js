var locationList = [];

var TableEditable = function () {

    var handleTable = function () {

        function restoreRow(oTable, nRow) {
            var aData = oTable.fnGetData(nRow);
            var jqTds = $('>td', nRow);

            for (var i = 0, iLen = jqTds.length; i < iLen; i++) {
                oTable.fnUpdate(aData[i], nRow, i, false);
            }

            oTable.fnDraw();
        }

        function editRow(oTable, nRow) {
            var aData = oTable.fnGetData(nRow);
            var jqTds = $('>td', nRow);
            jqTds[0].innerHTML = '<select  id="SelectItem" class="Itemddl form-control input-small" value="' + aData[0] + '"></select">';
            jqTds[1].innerHTML = '<input type="number" id="gridtxt_Quantity" class="form-control input-small" value="' + aData[1] + '">';
            jqTds[2].innerHTML = '<a class="edit" href="">Save</a>';
            jqTds[3].innerHTML = '<a class="cancel" href="">Cancel</a>';
            //var r = $('<a class="hidden" href="" id="addnewLocation">Add New</a>');
            getLocationListForGridDdl();
        }

        function getLocationListForGridDdl() {
            debugger;
            //e.preventDefault();
            $.ajax({
                type: "GET",
                url: "/InventoryProductInfo/GetAllLocationList",
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                traditional: true,
                success: function (data) {
                    debugger;
                    locationList = data;

                    $.each(data, function (i, _Location) {
                        $('.Itemddl').append('<option  value="' + _Location.Value + '">' + _Location.Text + '</option>');
                    })
                },
                error: function (data) {
                    alert("Location List content load failed.");
                }
            });
        }

        function saveRow(oTable, nRow) {
            debugger;
            $("#btn_SaveProductInfo").prop("disabled", false);
            $("#sample_editable_1_new_ProductInfoLocation").prop("disabled", false);
            var jqInputs = $('input,select', nRow);
            var itemInfo = $.grep(locationList, function (v) {

                return v.Value === jqInputs[0].value;
            });
            var selectedValue = jqInputs[0].value;
            var selectedText = itemInfo[0].Text;
            oTable.fnUpdate('<span name ="' + selectedValue + '">' + selectedText + '</span>', nRow, 0, false);
            oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
            //oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
            oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 2, false);
            oTable.fnUpdate('<a class="delete" href="">Delete</a>', nRow, 3, false);
            oTable.fnDraw();
        }

        function cancelEditRow(oTable, nRow) {
            var jqInputs = $('input', nRow);
            oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
            oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
            oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 2, false);
            oTable.fnDraw();
        }

        var table = $('#inventoryLocationGridTable');
        var oTable = table.dataTable({
            "lengthMenu": [
                [5, 10, 15, 20, -1],
                [5, 10, 15, 20, "All"] // change per page values here
            ],
            // set the initial value
            "pageLength": 5,

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

        $('#sample_editable_1_new_ProductInfoLocation').click(function (e) {
            e.preventDefault();
            $("#btn_SaveProductInfo").prop("disabled", true);
            $("#sample_editable_1_new_ProductInfoLocation").prop("disabled", true);
            if (nNew && nEditing) {
                if (bootbox.alert("Previous row not saved.Please save previous row!")) {
                    saveRow(oTable, nEditing); // save
                    $(nEditing).find("td:first").html("Untitled");
                    nEditing = null;
                    nNew = false;

                } else {
                    oTable.fnDeleteRow(nEditing); // cancel
                    nEditing = null;
                    nNew = false;

                    return;
                }
            }

            var aiNew = oTable.fnAddData(['', '', '', '']);
            var nRow = oTable.fnGetNodes(aiNew[0]);
            editRow(oTable, nRow);
            nEditing = nRow;
            nNew = true;
        });

        table.on('click', '.delete', function (e) {
            e.preventDefault();

            //if (confirm("Are you sure to delete this row ?") == false) {
            //    return;
            //}

            var nRow = $(this).parents('tr')[0];
            oTable.fnDeleteRow(nRow);
            //alert("Deleted! Do not forget to do some ajax to sync with backend :)");
        });

        table.on('click', '.cancel', function (e) {
            e.preventDefault();
            $("#btn_SaveProductInfo").prop("disabled", false);
            $("#sample_editable_1_new_ProductInfoLocation").prop("disabled", false);
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
            $("#btn_SaveProductInfo").prop("disabled", true);
            $("#sample_editable_1_new_ProductInfoLocation").prop("disabled", true);
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





///------------------------------Product Vendors Grid Start From Here---------------------------------------------------------------------------------------//

        var productVendorList = [];

        function restoreRowProductVendors(oTableAddNewProductVendors, nRowProductVendors) {
            var aData = oTableAddNewProductVendors.fnGetData(nRowProductVendors);
            var jqTds = $('>td', nRowProductVendors);

            for (var i = 0, iLen = jqTds.length; i < iLen; i++) {
                oTableAddNewProductVendors.fnUpdate(aData[i], nRowProductVendors, i, false);
            }

            oTableAddNewProductVendors.fnDraw();
        }

        function editRowProductVendors(oTableAddNewProductVendors, nRowProductVendors) {
            var aData = oTableAddNewProductVendors.fnGetData(nRowProductVendors);
            var jqTds = $('>td', nRowProductVendors);
            jqTds[0].innerHTML = '<select  id="SelectVendors" class="productVendors form-control input-small" value="' + aData[0] + '"></select">';
            jqTds[1].innerHTML = '<input type="text" id="gridtxt_VendorsPrice" class="form-control input-small" value="' + aData[1] + '">';
            jqTds[2].innerHTML = '<input type="text" id="gridtxt_VendorsProdCode" class="form-control input-small" value="' + aData[2] + '">';
            jqTds[3].innerHTML = '<a class="editProductVendors" id="save_ProductVendors" href="">Save</a>';
            jqTds[4].innerHTML = '<a class="cancelProductVendors" href="">Cancel</a>';
            //var r = $('<a class="hidden" href="" id="addnewLocation">Add New</a>');
            getProductVendorsListForGridDdl();
        }

        function getProductVendorsListForGridDdl() {
            debugger;
            //e.preventDefault();
            $.ajax({
                type: "GET",
                url: "/InventoryProductInfo/GetAllVendorsList",
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                traditional: true,
                success: function (data) {
                    debugger;
                    productVendorList = data;

                    $.each(data, function (i, _productVendors) {
                        $('.productVendors').append('<option  value="' + _productVendors.Value + '">' + _productVendors.Text + '</option>');
                    })
                },
                error: function (data) {
                    alert("Location List content load failed.");
                }
            });
        }

        function saveRowProductVendors(oTableAddNewProductVendors, nRowProductVendors) {
            debugger;
            $("#btn_SaveProductVendorGrid").prop("disabled", false);
            $("#sample_editable_1_new_ProductVendors").prop("disabled", false);
            var jqInputs = $('input,select', nRowProductVendors);
            var itemInfo = $.grep(productVendorList, function (v) {

                return v.Value === jqInputs[0].value;
            });
            var selectedValue = jqInputs[0].value;
            var selectedText = itemInfo[0].Text;
            oTableAddNewProductVendors.fnUpdate('<span name ="' + selectedValue + '">' + selectedText + '</span>', nRowProductVendors, 0, false);
            oTableAddNewProductVendors.fnUpdate(jqInputs[1].value, nRowProductVendors, 1, false);
            oTableAddNewProductVendors.fnUpdate(jqInputs[2].value, nRowProductVendors, 2, false);
            oTableAddNewProductVendors.fnUpdate('<a class="editProductVendors" href="">Edit</a>', nRowProductVendors, 3, false);
            oTableAddNewProductVendors.fnUpdate('<a class="deleteProductVendors" href="">Delete</a>', nRowProductVendors, 4, false);
            oTableAddNewProductVendors.fnDraw();

            nNewProductVendors = false;
        }

        function cancelEditRowProductVendors(oTableAddNewProductVendors, nRowProductVendors) {
            var jqInputs = $('input', nRowProductVendors);
            oTableAddNewProductVendors.fnUpdate(jqInputs[0].value, nRowProductVendors, 0, false);
            oTableAddNewProductVendors.fnUpdate(jqInputs[1].value, nRowProductVendors, 1, false);
            oTableAddNewProductVendors.fnUpdate(jqInputs[1].value, nRowProductVendors, 2, false);
            oTableAddNewProductVendors.fnUpdate('<a class="editProductVendors" href="">Edit</a>', nRowProductVendors, 3, false);
            oTableAddNewProductVendors.fnDraw();
        }

        var tableAddNewProductVendors = $('#inventoryProductVendorsTable');
        var oTableAddNewProductVendors = tableAddNewProductVendors.dataTable({
            "lengthMenu": [
                [5, 10, 15, 20, -1],
                [5, 10, 15, 20, "All"] // change per page values here
            ],
            // set the initial value
            "pageLength": 5,

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

        var nEditingProductVendors = null;
        var nNewProductVendors = false;

        $('#sample_editable_1_new_ProductVendors').click(function (e) {
            e.preventDefault();
            $("#btn_SaveProductVendorGrid").prop("disabled", true);
            $("#sample_editable_1_new_ProductVendors").prop("disabled", true);
            debugger;

            if (nNewProductVendors && nEditingProductVendors) {
                if (bootbox.alert("Previous row not saved.Please save previous row!")) {
                    saveRowProductVendors(oTableAddNewProductVendors, nEditingProductVendors); // save
                    $(nEditingProductVendors).find("td:first").html("Untitled");
                    nEditingProductVendors = null;
                    nNewProductVendors = false;

                } else {
                    oTableAddNewProductVendors.fnDeleteRow(nEditingProductVendors); // cancel
                    nEditingProductVendors = null;
                    nNewProductVendors = false;

                    return;
                }
            }

            var aiNewProductVendors = oTableAddNewProductVendors.fnAddData(['', '', '', '', '']);
            var nRowProductVendors = oTableAddNewProductVendors.fnGetNodes(aiNewProductVendors[0]);
            editRowProductVendors(oTableAddNewProductVendors, nRowProductVendors);
            nEditingProductVendors = nRowProductVendors;
            nNewProductVendors = true;
        });


        tableAddNewProductVendors.on('click', '.deleteProductVendors', function (e) {
            e.preventDefault();

            //if (confirm("Are you sure to delete this row ?") == false) {
            //    return;
            //}

            var nRowProductVendors = $(this).parents('tr')[0];
            oTableAddNewProductVendors.fnDeleteRow(nRowProductVendors);
            //alert("Deleted! Do not forget to do some ajax to sync with backend :)");
        });

        tableAddNewProductVendors.on('click', '.cancelProductVendors', function (e) {
            e.preventDefault();
            $("#btn_SaveProductVendorGrid").prop("disabled", false);
            $("#sample_editable_1_new_ProductVendors").prop("disabled", false);
            debugger;
            if (nNewProductVendors) {
                oTableAddNewProductVendors.fnDeleteRow(nEditingProductVendors);
                nNewProductVendors = false;
            } else {
                restoreRowProductVendors(oTableAddNewProductVendors, nEditingProductVendors);
                nEditingProductVendors = null;
            }
        });

        tableAddNewProductVendors.on('click', '.editProductVendors', function (e) {
            e.preventDefault();
            $("#btn_SaveProductVendorGrid").prop("disabled", true);
            $("#sample_editable_1_new_ProductVendors").prop("disabled", true);
            /* Get the row as a parent of the link that was clicked on */
            var nRowProductVendors = $(this).parents('tr')[0];

            if (nEditingProductVendors !== null && nEditingProductVendors != nRowProductVendors) {
                /* Currently editing - but not this row - restore the old before continuing to edit mode */
                restoreRowProductVendors(oTableAddNewProductVendors, nEditingProductVendors);
                editRowProductVendors(oTableAddNewProductVendors, nRowProductVendors);
                nEditingProductVendors = nRowProductVendors;
            } else if (nEditingProductVendors == nRowProductVendors && this.innerHTML == "Save") {
                /* Editing this row and want to save it */
                saveRowProductVendors(oTableAddNewProductVendors, nEditingProductVendors);
                nEditingProductVendors = null;
                //alert("Updated! Do not forget to do some ajax to sync with backend :)");
            } else {
                /* No edit in progress - let's start one */
                editRowProductVendors(oTableAddNewProductVendors, nRowProductVendors);
                nEditingProductVendors = nRowProductVendors;
            }
        });

///------------------------------Product Vendors Grid End From Here---------------------------------------------------------------------------------------//

///-----------------------------------Bill Of Materials Grid Start From Here---------------------------------------------------------------------------------//
            var billOfMaterialsList = [];

            function restoreRowbillOfMaterials(oTablebillOfMaterials, nRowbillOfMaterials) {
                var aData = oTablebillOfMaterials.fnGetData(nRowbillOfMaterials);
                var jqTds = $('>td', nRowbillOfMaterials);

                for (var i = 0, iLen = jqTds.length; i < iLen; i++) {
                    oTablebillOfMaterials.fnUpdate(aData[i], nRowbillOfMaterials, i, false);
                }

                oTablebillOfMaterials.fnDraw();
            }

            function editRowbillOfMaterials(oTablebillOfMaterials, nRowbillOfMaterials) {
                var aData = oTablebillOfMaterials.fnGetData(nRowbillOfMaterials);
                var jqTds = $('>td', nRowbillOfMaterials);
                jqTds[0].innerHTML = '<select  id="SelectBillOfMaterials" class="billOfMaterials form-control input-small" value="' + aData[0] + '"></select">';
                jqTds[1].innerHTML = '<input type="text" id="gridtxt_Quantity1" class="form-control input-small" value="' + aData[1] + '">';
                jqTds[2].innerHTML = '<input type="text" id="gridtxt_Cost" readonly class="form-control input-small" value="' + aData[2] + '">';
                jqTds[3].innerHTML = '<a class="editBillOfMaterials" id="save_editBillOfMaterials" href="">Save</a>';
                jqTds[4].innerHTML = '<a class="cancelBillOfMaterials" href="">Cancel</a>';
                //var r = $('<a class="hidden" href="" id="addnewLocation">Add New</a>');
                getBillOfMaterialsListForGridDdl();
            }

            function getBillOfMaterialsListForGridDdl() {
                debugger;
                //e.preventDefault();
                $.ajax({
                    type: "GET",
                    url: "/InventoryProductInfo/GetBillOfMaterialsItemList",
                    contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    traditional: true,
                    success: function (data) {
                        debugger;
                        billOfMaterialsList = data;

                        $.each(data, function (i, _billOfMaterials) {
                            $('.billOfMaterials').append('<option  value="' + _billOfMaterials.Value + '">' + _billOfMaterials.Text + '</option>');
                        })
                    },
                    error: function (data) {
                        alert("Location List content load failed.");
                    }
                });
            }

            function saveRowbillOfMaterials(oTablebillOfMaterials, nRowbillOfMaterials) {
                debugger;
                $("#btn_SaveBillOfMaterialsGrid").prop("disabled", false);
                $("#sample_editable_1_new_BillOfMaterials").prop("disabled", false);
                var jqInputs = $('input,select', nRowbillOfMaterials);
                var itemInfo = $.grep(billOfMaterialsList, function (v) {

                    return v.Value === jqInputs[0].value;
                });
                var selectedValue = jqInputs[0].value;
                var selectedText = itemInfo[0].Text;
                oTablebillOfMaterials.fnUpdate('<span name ="' + selectedValue + '">' + selectedText + '</span>', nRowbillOfMaterials, 0, false);
                oTablebillOfMaterials.fnUpdate(jqInputs[1].value, nRowbillOfMaterials, 1, false);
                oTablebillOfMaterials.fnUpdate(jqInputs[2].value, nRowbillOfMaterials, 2, false);
                oTablebillOfMaterials.fnUpdate('<a class="editBillOfMaterials" href="">Edit</a>', nRowbillOfMaterials, 3, false);
                oTablebillOfMaterials.fnUpdate('<a class="deleteBillOfMaterials" href="">Delete</a>', nRowbillOfMaterials, 4, false);
                oTablebillOfMaterials.fnDraw();

                nNewbillOfMaterials = false;
            }

            function cancelEditRowbillOfMaterials(oTablebillOfMaterials, nRowbillOfMaterials) {
                var jqInputs = $('input', nRowbillOfMaterials);
                oTablebillOfMaterials.fnUpdate(jqInputs[0].value, nRowbillOfMaterials, 0, false);
                oTablebillOfMaterials.fnUpdate(jqInputs[1].value, nRowbillOfMaterials, 1, false);
                oTablebillOfMaterials.fnUpdate(jqInputs[1].value, nRowbillOfMaterials, 2, false);
                oTablebillOfMaterials.fnUpdate('<a class="editBillOfMaterials" href="">Edit</a>', nRowbillOfMaterials, 3, false);
                oTablebillOfMaterials.fnDraw();
            }

            var tablebillOfMaterials = $('#inventoryBillOfMaterialsTable');
            var oTablebillOfMaterials = tablebillOfMaterials.dataTable({
                "lengthMenu": [
                    [5, 10, 15, 20, -1],
                    [5, 10, 15, 20, "All"] // change per page values here
                ],
                // set the initial value
                "pageLength": 5,

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

            var nEditingbillOfMaterials = null;
            var nNewbillOfMaterials = false;

            $('#sample_editable_1_new_BillOfMaterials').click(function (e) {
                e.preventDefault();
                $("#btn_SaveBillOfMaterialsGrid").prop("disabled", true);
                $("#sample_editable_1_new_BillOfMaterials").prop("disabled", true);
                debugger;

                if (nNewbillOfMaterials && nEditingbillOfMaterials) {
                    if (bootbox.alert("Previous row not saved.Please save previous row!")) {
                        saveRowbillOfMaterials(oTablebillOfMaterials, nEditingbillOfMaterials); // save
                        $(nEditingbillOfMaterials).find("td:first").html("Untitled");
                        nEditingbillOfMaterials = null;
                        nNewbillOfMaterials = false;

                    } else {
                        oTablebillOfMaterials.fnDeleteRow(nEditingbillOfMaterials); // cancel
                        nEditingbillOfMaterials = null;
                        nNewbillOfMaterials = false;

                        return;
                    }
                }

                var aiNewbillOfMaterials = oTablebillOfMaterials.fnAddData(['', '', '', '', '']);
                var nRowbillOfMaterials = oTablebillOfMaterials.fnGetNodes(aiNewbillOfMaterials[0]);
                editRowbillOfMaterials(oTablebillOfMaterials, nRowbillOfMaterials);
                nEditingbillOfMaterials = nRowbillOfMaterials;
                nNewbillOfMaterials = true;
            });


            tablebillOfMaterials.on('click', '.deleteBillOfMaterials', function (e) {
                e.preventDefault();

                //if (confirm("Are you sure to delete this row ?") == false) {
                //    return;
                //}

                var nRowbillOfMaterials = $(this).parents('tr')[0];
                oTablebillOfMaterials.fnDeleteRow(nRowbillOfMaterials);
                //alert("Deleted! Do not forget to do some ajax to sync with backend :)");
            });

            tablebillOfMaterials.on('click', '.cancelBillOfMaterials', function (e) {
                e.preventDefault();
                $("#btn_SaveBillOfMaterialsGrid").prop("disabled", false);
                $("#sample_editable_1_new_BillOfMaterials").prop("disabled", false);
                debugger;
                if (nNewbillOfMaterials) {
                    oTablebillOfMaterials.fnDeleteRow(nEditingbillOfMaterials);
                    nNewbillOfMaterials = false;
                } else {
                    restoreRowbillOfMaterials(oTablebillOfMaterials, nEditingbillOfMaterials);
                    nEditingbillOfMaterials = null;
                }
            });

            tablebillOfMaterials.on('click', '.editBillOfMaterials', function (e) {
                e.preventDefault();
                $("#btn_SaveBillOfMaterialsGrid").prop("disabled", true);
                $("#sample_editable_1_new_BillOfMaterials").prop("disabled", true);
                /* Get the row as a parent of the link that was clicked on */
                var nRowbillOfMaterials = $(this).parents('tr')[0];

                if (nEditingbillOfMaterials !== null && nEditingbillOfMaterials != nRowbillOfMaterials) {
                    /* Currently editing - but not this row - restore the old before continuing to edit mode */
                    restoreRowbillOfMaterials(oTablebillOfMaterials, nEditingbillOfMaterials);
                    editRowbillOfMaterials(oTablebillOfMaterials, nRowbillOfMaterials);
                    nEditingbillOfMaterials = nRowbillOfMaterials;
                } else if (nEditingbillOfMaterials == nRowbillOfMaterials && this.innerHTML == "Save") {
                    /* Editing this row and want to save it */
                    saveRowbillOfMaterials(oTablebillOfMaterials, nEditingbillOfMaterials);
                    nEditingbillOfMaterials = null;
                    //alert("Updated! Do not forget to do some ajax to sync with backend :)");
                } else {
                    /* No edit in progress - let's start one */
                    editRowbillOfMaterials(oTablebillOfMaterials, nRowbillOfMaterials);
                    nEditingbillOfMaterials = nRowbillOfMaterials;
                }
            });

///-----------------------------------Bill Of Materials Grid End Here---------------------------------------------------------------------------------//
            
    }

    return {

        //main function to initiate the module
        init: function () {
            handleTable();
        }

    };

}();






 