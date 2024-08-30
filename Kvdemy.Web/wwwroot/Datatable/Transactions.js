"use strict";
// Class definition

var KTDatatableAutoColumnHideDemo = function () {
    // Private functions

    // basic demo
    var demo = function () {
        var FID = getFID();
        console.error("iddddddddddd = ", FID)
        var url = 'Teacher/GetTransactions/' + FID;
        var datatable = $('#kt_datatable').KTDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: {
                    read: {
                        url: url,
                    },
                },
                pageSize: 10,
                saveState: false,
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true,
            },

            layout: {
                scroll: false
            },

            // column sorting
            sortable: true,

            pagination: true,

            search: {
                input: $('#kt_datatable_search_query'),
                key: 'generalSearch'
            },
            // columns definition
            columns: [
                {
                    field: 'id',
                    title: 'رقم',
                    width: 'auto'
                },
                {
                    field: 'transactionType',
                    title: 'نوع الحركة',
                    width: 'auto',
                    template: function (data) {
                        if (data.transactionType == 1) {
                            return '<p style="color: #dbad26; font-weight: bold;">' + 'ايداع' + '</p>';
                        }
                        else if (data.transactionType == 2) {
                            return '<p style="color: #18A804; font-weight: bold;">' + 'شراء' + '</p>';
                        }
                        else if (data.transactionType == 3) {
                            return '<p style="color: #215CA8; font-weight: bold;" >' + 'سحب' + '</p>';
                        }
                    }
                },
                {
                    field: 'valueBeforDiscount',
                    title: 'القيمة قبل الخصم ',
                    width: 'auto'
                }, {
                    field: 'valueAfterDiscount',
                    title: 'القيمة بعد الخصم ',
                    width: 'auto'
                }, {
                    field: 'valueDiscount',
                    title: ' قيمة الخصم',
                    width: 'auto'
                }, {
                    field: 'transactionPaymentType',
                    title: ' طريقة الدفع',
                    width: 'auto',
                    template: function (data) {
                        if (data.transactionPaymentType == 1) {
                            return '<p style="color: #dbad26; font-weight: bold;">' + 'محفظة' + '</p>';
                        }
                        else if (data.transactionPaymentType == 2) {
                            return '<p style="color: #18A804; font-weight: bold;">' + 'حساب بنكي' + '</p>';
                        }
                        else if (data.transactionPaymentType == 3) {
                            return '<p style="color: #215CA8; font-weight: bold;" >' + 'بوابة دفع الكتروني' + '</p>';
                        }
                    }
                }, {
                    field: 'createdAt',
                    title: ' تاريخ الحركة',
                    width: 'auto',
                    template: function (data) {
                        var date = new Date(data.createdAt);
                        var formattedDate = date.getFullYear() + '-' +
                            ('0' + (date.getMonth() + 1)).slice(-2) + '-' +
                            ('0' + date.getDate()).slice(-2) + ' ' +
                            ('0' + date.getHours()).slice(-2) + ':' +
                            ('0' + date.getMinutes()).slice(-2);
                        return formattedDate;
                    }
                }],
        });

        $('#kt_datatable_search_status').on('change', function () {
            datatable.search($(this).val().toLowerCase(), 'Status');
        });

        $('#kt_datatable_search_type').on('change', function () {
            datatable.search($(this).val().toLowerCase(), 'Type');
        });

        $('#kt_datatable_search_status, #kt_datatable_search_type').selectpicker();
    };

    return {
        // public functions
        init: function () {
            demo();
        },
    };
}();

jQuery(document).ready(function () {
    KTDatatableAutoColumnHideDemo.init();
});

$('#excelBtn').on('click', function () {
    $.post('level/ExportToExcel', {
        generalSearch: $('#kt_datatable_search_query').val().toLowerCase()
    },
        function (returnedData) {
            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = today.getFullYear();

            today = mm + '/' + dd + '/' + yyyy;

            var a = document.createElement('a');
            a.href = returnedData;
            a.download = today + '-report.xlsx';
            document.body.append(a);
            a.click();
            a.remove();
        });
});

function getFID() {
    // Retrieve FID from the data attribute
    return $('#userIdInput').data('userid');
}