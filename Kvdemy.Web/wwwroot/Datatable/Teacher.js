"use strict";
// Class definition

var KTDatatableAutoColumnHideDemo = function () {
    // Private functions

    // basic demo
    var demo = function () {
        var datatable = $('#kt_datatable').KTDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: {
                    read: {
                        url: 'Teacher/GetData',
                    },
                },
                pagination: {
                    perPage: 10,
                    page: 1, // You might want to pass the current page here
                    // Other pagination properties if needed
                },
                query: {
                    generalSearch: 'search term' // If you want to send a search term
                },
                PerPage: 10,
                saveState: false,
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true,
            },

            layout: {
                scroll: true
            },

            // column sorting
            sortable: true,

            pagination: true,

            search: {
                input: $('#kt_datatable_search_query'),
                key: 'generalSearch'
            },
            // columns definition
            columns: [{
                field: 'profileImage',
                title: 'الصورة ',

                width: '60',
                template: function (data) {
                    return '<img src="https://localhost:44302/Images/' + data.profileImage + '" alt="Profile Image" width="50" height = "50">';
                }
            }, {
                field: 'firstName',
                title: 'الاسم الاول',
                width: 'auto'
            }, {
                field: 'lastName',
                title: 'الاسم الاخير',
                width: 'auto'
            }, {
                field: 'email',
                title: 'البريد ',
                width: 'auto'
            }, {
                field: 'status',
                title: 'حالة المستخدم ',
                width: 'auto',
                template: function (data) {
                    return '<p>' + (data.status == 1 ? 'غير فعال' : 'فعال') + '</p>';
                }
            }
                , {
                field: 'phoneNumber',
                title: 'رقم الجوال ',
                width: 'auto'
            },
            {
                field: 'Actions',
                title: 'العمليات',
                sortable: false,
                width: 125,
                overflow: 'visible',
                autoHide: false,
                template: function (data) {
                    return '\<a  href ="/Teacher/TeacherInfo/' + data.id + '"  title="تفاصيل المعلم   ' + data.firstName + ' ">\
                            <span class="svg-icon svg-icon-warning svg-icon-md">\
                                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">\
                                <rect x="2.99658" y="3.78906" width="18.0075" height="16.0067" rx="3.6" stroke="#215CA8" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                <path d="M12.2503 11.8C12.2503 11.9381 12.1383 12.0501 12.0002 12.0501C11.8621 12.0501 11.7501 11.9381 11.7501 11.8C11.7501 11.6619 11.8621 11.5499 12.0002 11.5499C12.1383 11.5499 12.2503 11.6619 12.2503 11.8" stroke="#215CA8" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                <path d="M8.24836 11.7922C8.24836 11.9303 8.13638 12.0423 7.99826 12.0423C7.86013 12.0423 7.74815 11.9303 7.74815 11.7922C7.74815 11.6541 7.86013 11.5421 7.99826 11.5421C8.13638 11.5421 8.24836 11.6541 8.24836 11.7922" stroke="#215CA8" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                <path d="M16.2518 11.7824C16.2518 11.9206 16.1398 12.0325 16.0017 12.0325C15.8635 12.0325 15.7516 11.9206 15.7516 11.7824C15.7516 11.6443 15.8635 11.5323 16.0017 11.5323C16.1398 11.5323 16.2518 11.6443 16.2518 11.7824" stroke="#215CA8" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                </svg>\
                            </span >\
                        </a>\
                        <a href ="/Teacher/Delete/' + data.id + '" tname="#kt_datatable" class="Confirm btn btn-sm btn-clean btn-icon" title="حذف">\
                            <span class="svg-icon svg-icon-danger svg-icon-danger svg-icon-md">\
                                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">\
                                <path d="M18 6V18.75C18 19.993 16.973 21 15.731 21H8.231C6.988 21 6 19.993 6 18.75V6" stroke="#F80202" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                <path d="M19.5 6H4.5" stroke="#F80202" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                <path d="M10 3H14" stroke="#F80202" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                <path d="M14 10V17" stroke="#F80202" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                <path d="M10 17V10" stroke="#F80202" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                </svg>\
                            </span>\
                        </a>';
                },
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