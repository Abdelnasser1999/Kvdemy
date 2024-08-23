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
                        url: 'NewTeacher/GetData',
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
                    return '<p>' + (data.status == 1 ? 'غير فعال' : 'مرفوض') + '</p>';
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
                       <a href="/NewTeacher/Accept/' + data.id + '" class="Confirm btn btn-sm btn-clean btn-icon" title="قبول">\
                        <span class="svg-icon svg-icon-warning svg-icon-md">\
                            <svg xmlns="http://www.w3.org/2000/svg" xmlns: xlink="http://www.w3.org/1999/xlink" version="1.1" width="24px" height="24px" viewBox="0 0 256 256" xml: space="preserve">\
                                <defs>\
                                </defs>\
                                <g style="stroke: none; stroke-width: 0; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: none; fill-rule: nonzero; opacity: 1;" transform="translate(1.4065934065934016 1.4065934065934016) scale(2.81 2.81)" >\
                                    <circle cx="45" cy="45" r="45" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(40,201,55); fill-rule: nonzero; opacity: 1;" transform="  matrix(1 0 0 1 0 0) " />\
                                    <path d="M 38.478 64.5 c -0.01 0 -0.02 0 -0.029 0 c -1.3 -0.009 -2.533 -0.579 -3.381 -1.563 L 21.59 47.284 c -1.622 -1.883 -1.41 -4.725 0.474 -6.347 c 1.884 -1.621 4.725 -1.409 6.347 0.474 l 10.112 11.744 L 61.629 27.02 c 1.645 -1.862 4.489 -2.037 6.352 -0.391 c 1.862 1.646 2.037 4.49 0.391 6.352 l -26.521 30 C 40.995 63.947 39.767 64.5 38.478 64.5 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(255,255,255); fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />\
                                </g>\
                            </svg >\
                        </span >\
                    </a>\
                    <a href="/NewTeacher/Reject/' + data.id + '" tname="#kt_datatable" class="Confirm btn btn-sm btn-clean btn-icon" title="رفض">\
                        <span class="svg-icon svg-icon-danger svg-icon-danger svg-icon-md">\
                            <svg xmlns="http://www.w3.org/2000/svg" xmlns: xlink="http://www.w3.org/1999/xlink" version="1.1" width="24px" height="24px" viewBox="0 0 256 256" xml: space="preserve">\
                                <defs>\
                                </defs>\
                                <g style="stroke: none; stroke-width: 0; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: none; fill-rule: nonzero; opacity: 1;" transform="translate(1.4065934065934016 1.4065934065934016) scale(2.81 2.81)" >\
                                    <path d="M 45 90 C 20.187 90 0 69.813 0 45 C 0 20.187 20.187 0 45 0 c 24.813 0 45 20.187 45 45 C 90 69.813 69.813 90 45 90 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(236,0,0); fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />\
                                    <path d="M 28.902 66.098 c -1.28 0 -2.559 -0.488 -3.536 -1.465 c -1.953 -1.952 -1.953 -5.118 0 -7.07 l 32.196 -32.196 c 1.951 -1.952 5.119 -1.952 7.07 0 c 1.953 1.953 1.953 5.119 0 7.071 L 32.438 64.633 C 31.461 65.609 30.182 66.098 28.902 66.098 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(255,255,255); fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />\
                                    <path d="M 61.098 66.098 c -1.279 0 -2.56 -0.488 -3.535 -1.465 L 25.367 32.438 c -1.953 -1.953 -1.953 -5.119 0 -7.071 c 1.953 -1.952 5.118 -1.952 7.071 0 l 32.195 32.196 c 1.953 1.952 1.953 5.118 0 7.07 C 63.657 65.609 62.377 66.098 61.098 66.098 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(255,255,255); fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />\
                                </g>\
                            </svg >\
                        </span >\
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