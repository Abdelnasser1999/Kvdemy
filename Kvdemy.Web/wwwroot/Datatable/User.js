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
                        url: 'User/GetUserData',
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
                    return '<img src="https://localhost:44373/Images/' + data.profileImage + '" alt="Profile Image" width="50" height = "50">';
                }
            }, {
                field: 'name',
                title: 'الاسم كامل',
                width: '250'
            }, {
                field: 'email',
                title: 'البريد ',
                width: '250'
            }, {
                field: 'status',
                title: 'حالة المستخدم ',
                width: '250',
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
                    return '\<a  href ="/User/FinanceAccount/' + data.id + '"  title="البيانات المالية ل  ' + data.name + ' ">\
                            <span class="svg-icon svg-icon-warning svg-icon-md">\
                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.1" width="256" height="256" viewBox="0 0 256 256" xml:space="preserve">\
                                <defs>\
                                </defs>\
                                <g style="stroke: none; stroke-width: 0; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: none; fill-rule: nonzero; opacity: 1;" transform="translate(1.4065934065934016 1.4065934065934016) scale(2.81 2.81)" >\
	                                <path d="M 80.829 28.501 C 74.406 14.581 60.352 5.588 45.027 5.588 c -18.641 0 -34.29 13.013 -38.367 30.428 l -1.097 -1.803 c -0.862 -1.416 -2.707 -1.866 -4.123 -1.003 c -1.415 0.861 -1.865 2.707 -1.003 4.123 l 5.614 9.227 c 0.011 0.018 0.027 0.031 0.038 0.049 c 0.093 0.145 0.2 0.279 0.316 0.406 c 0.027 0.03 0.049 0.064 0.077 0.093 c 0.148 0.149 0.311 0.282 0.488 0.398 c 0.046 0.03 0.097 0.051 0.145 0.079 c 0.135 0.078 0.272 0.15 0.419 0.207 c 0.07 0.027 0.142 0.045 0.214 0.067 c 0.132 0.04 0.264 0.074 0.403 0.096 c 0.036 0.006 0.07 0.021 0.106 0.025 C 8.377 47.993 8.496 48 8.615 48 c 0.2 0 0.398 -0.021 0.592 -0.06 c 0.118 -0.024 0.228 -0.066 0.341 -0.103 c 0.073 -0.024 0.148 -0.039 0.219 -0.068 c 0.128 -0.053 0.246 -0.124 0.364 -0.194 c 0.049 -0.029 0.103 -0.05 0.151 -0.082 c 0.164 -0.11 0.317 -0.235 0.457 -0.375 l 7.672 -7.672 c 1.172 -1.171 1.172 -3.071 0 -4.242 c -1.171 -1.172 -3.071 -1.172 -4.242 0 l -1.513 1.513 c 3.694 -14.43 16.807 -25.13 32.372 -25.13 c 12.993 0 24.908 7.625 30.354 19.427 c 0.693 1.504 2.477 2.16 3.98 1.467 C 80.867 31.787 81.523 30.005 80.829 28.501 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(61,220,128); fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />\
	                                <path d="M 89.562 52.668 l -5.614 -9.227 c -0.01 -0.017 -0.025 -0.03 -0.036 -0.047 c -0.095 -0.149 -0.205 -0.287 -0.324 -0.417 c -0.024 -0.027 -0.044 -0.058 -0.069 -0.084 c -0.148 -0.15 -0.312 -0.283 -0.489 -0.399 c -0.045 -0.03 -0.095 -0.05 -0.142 -0.077 c -0.135 -0.079 -0.274 -0.151 -0.421 -0.208 c -0.07 -0.027 -0.142 -0.044 -0.214 -0.066 c -0.132 -0.04 -0.264 -0.074 -0.403 -0.096 c -0.036 -0.006 -0.07 -0.021 -0.107 -0.025 c -0.052 -0.006 -0.103 0.003 -0.155 -0.001 C 81.52 42.016 81.455 42 81.385 42 c -0.061 0 -0.117 0.014 -0.177 0.018 c -0.093 0.005 -0.184 0.014 -0.276 0.028 c -0.128 0.019 -0.25 0.049 -0.372 0.084 c -0.08 0.023 -0.159 0.044 -0.236 0.073 c -0.135 0.051 -0.262 0.116 -0.387 0.185 c -0.059 0.033 -0.12 0.059 -0.177 0.096 c -0.18 0.116 -0.349 0.247 -0.5 0.398 l -7.672 7.672 c -1.172 1.171 -1.172 3.071 0 4.242 c 1.172 1.172 3.07 1.172 4.242 0 l 1.513 -1.513 c -3.694 14.43 -16.807 25.13 -32.372 25.13 c -13.459 0 -25.544 -8.011 -30.788 -20.408 c -0.646 -1.526 -2.405 -2.243 -3.932 -1.594 c -1.526 0.646 -2.24 2.405 -1.594 3.932 c 6.185 14.622 20.439 24.07 36.314 24.07 c 18.641 0 34.29 -13.013 38.367 -30.429 l 1.097 1.803 c 0.564 0.928 1.553 1.44 2.565 1.44 c 0.531 0 1.069 -0.141 1.557 -0.437 C 89.974 55.93 90.423 54.084 89.562 52.668 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(61,220,128); fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />\
	                                <path d="M 48.004 42.905 l -4.029 -1.476 c -0.062 -0.022 -0.125 -0.043 -0.188 -0.062 c -1.598 -0.469 -2.847 -1.626 -3.428 -3.174 c -0.491 -1.31 -0.443 -2.733 0.136 -4.006 c 0.579 -1.273 1.619 -2.245 2.929 -2.736 c 1.309 -0.492 2.732 -0.444 4.006 0.136 c 1.273 0.579 2.246 1.619 2.737 2.929 c 0.581 1.551 2.31 2.334 3.862 1.755 c 1.551 -0.582 2.337 -2.311 1.755 -3.863 c -1.343 -3.581 -4.331 -6.071 -7.785 -6.944 v -1.941 c 0 -1.657 -1.343 -3 -3 -3 c -1.657 0 -3 1.343 -3 3 v 2.092 c -0.228 0.069 -0.456 0.133 -0.682 0.217 c -2.811 1.054 -5.042 3.139 -6.285 5.872 c -1.242 2.732 -1.346 5.785 -0.292 8.596 c 1.238 3.299 3.878 5.77 7.255 6.794 l 4.029 1.476 c 0.063 0.023 0.125 0.044 0.188 0.063 c 1.598 0.469 2.847 1.625 3.428 3.175 c 0.491 1.31 0.442 2.732 -0.136 4.006 c -0.579 1.273 -1.619 2.245 -2.93 2.736 c -1.311 0.49 -2.733 0.44 -4.006 -0.136 c -1.273 -0.579 -2.245 -1.619 -2.736 -2.93 c -0.582 -1.552 -2.312 -2.342 -3.862 -1.755 c -1.551 0.581 -2.337 2.311 -1.756 3.862 c 1.054 2.811 3.139 5.042 5.872 6.284 c 0.623 0.283 1.264 0.493 1.914 0.658 v 1.944 c 0 1.657 1.343 3 3 3 c 1.657 0 3 -1.343 3 -3 v -2.088 c 0.228 -0.07 0.456 -0.137 0.682 -0.221 c 2.811 -1.054 5.043 -3.139 6.285 -5.872 c 1.242 -2.732 1.346 -5.785 0.292 -8.595 C 54.022 46.401 51.381 43.93 48.004 42.905 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(134,155,140); fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />\
                                </g>\
                                </svg >\
                            </span >\
                        </a>\
                        <a  href ="/User/Point/' + data.id + '"  title="نقاط الولاء ل  ' + data.name + ' ">\
                            <span class="svg-icon svg-icon-warning svg-icon-md">\
                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.1" width="256" height="256" viewBox="0 0 256 256" xml:space="preserve">\
                                <defs>\
                                </defs>\
                                <g style="stroke: none; stroke-width: 0; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: none; fill-rule: nonzero; opacity: 1;" transform="translate(1.4065934065934016 1.4065934065934016) scale(2.81 2.81)" >\
	                                <path d="M 80.313 77.983 L 68.984 53.691 c 5.113 -5.704 8.23 -13.232 8.23 -21.477 C 77.214 14.451 62.763 0 45 0 S 12.786 14.451 12.786 32.214 c 0 8.245 3.117 15.773 8.23 21.477 L 9.688 77.983 c -0.135 0.289 -0.124 0.625 0.029 0.904 c 0.153 0.279 0.431 0.469 0.747 0.51 l 12.716 1.675 l 9.457 8.665 C 32.822 89.908 33.064 90 33.312 90 c 0.065 0 0.13 -0.006 0.195 -0.02 c 0.312 -0.062 0.576 -0.27 0.711 -0.558 L 45 66.3 l 10.782 23.122 c 0.135 0.288 0.398 0.496 0.711 0.558 c 0.065 0.014 0.131 0.02 0.195 0.02 c 0.248 0 0.489 -0.092 0.676 -0.263 l 9.457 -8.665 l 12.716 -1.675 c 0.315 -0.041 0.593 -0.23 0.746 -0.51 S 80.447 78.272 80.313 77.983 z M 45 2 c 16.66 0 30.214 13.554 30.214 30.214 S 61.66 62.428 45 62.428 S 14.786 48.874 14.786 32.214 S 28.34 2 45 2 z M 32.981 87.342 l -8.683 -7.957 c -0.151 -0.139 -0.342 -0.228 -0.545 -0.254 l -11.676 -1.538 l 10.424 -22.355 c 5.517 5.393 12.955 8.823 21.181 9.156 L 32.981 87.342 z M 66.247 79.131 c -0.203 0.026 -0.394 0.115 -0.545 0.254 l -8.684 7.957 l -10.7 -22.948 c 8.226 -0.333 15.664 -3.764 21.181 -9.156 l 10.425 22.355 L 66.247 79.131 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(15,84,142); fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />\
	                                <path d="M 45 55.087 c -12.612 0 -22.874 -10.261 -22.874 -22.873 c 0 -0.552 0.448 -1 1 -1 s 1 0.448 1 1 c 0 11.509 9.364 20.873 20.874 20.873 c 0.552 0 1 0.447 1 1 S 45.552 55.087 45 55.087 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(15,84,142); fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />\
	                                <path d="M 66.873 33.214 c -0.553 0 -1 -0.448 -1 -1 c 0 -11.51 -9.363 -20.874 -20.873 -20.874 c -0.552 0 -1 -0.448 -1 -1 s 0.448 -1 1 -1 c 12.612 0 22.873 10.261 22.873 22.874 C 67.873 32.766 67.426 33.214 66.873 33.214 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(15,84,142); fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />\
	                                <path d="M 47.946 31.301 h -5.893 c -1.086 0 -1.97 -0.884 -1.97 -1.97 V 24.76 c 0 -1.086 0.884 -1.97 1.97 -1.97 h 5.893 c 1.086 0 1.97 0.884 1.97 1.97 c 0 0.552 0.447 1 1 1 s 1 -0.448 1 -1 c 0 -2.189 -1.781 -3.97 -3.97 -3.97 H 46 v -2.679 c 0 -0.552 -0.448 -1 -1 -1 s -1 0.448 -1 1 v 2.679 h -1.946 c -2.189 0 -3.97 1.781 -3.97 3.97 v 4.571 c 0 2.189 1.781 3.97 3.97 3.97 h 5.893 c 1.086 0 1.97 0.884 1.97 1.97 v 4.571 c 0 1.086 -0.884 1.97 -1.97 1.97 h -5.893 c -1.086 0 -1.97 -0.884 -1.97 -1.97 c 0 -0.552 -0.448 -1 -1 -1 s -1 0.448 -1 1 c 0 2.189 1.781 3.97 3.97 3.97 H 44 v 3.002 c 0 0.553 0.448 1 1 1 s 1 -0.447 1 -1 v -3.002 h 1.946 c 2.188 0 3.97 -1.781 3.97 -3.97 v -4.571 C 51.916 33.082 50.135 31.301 47.946 31.301 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(15,84,142); fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />\
                                </g>\
                                </svg >\
                            </span >\
                        </a>\
                        <a  href ="/User/Update/' + data.id + '" class="PopUp btn btn-sm btn-clean btn-icon mr-2" title="تعديل  بيانات ' + data.name + ' ">\
                            <span class="svg-icon svg-icon-warning svg-icon-md">\
                                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">\
                                <path fill-rule="evenodd" clip-rule="evenodd" d="M17.3816 4.61828V4.61828C18.21 5.44721 18.21 6.7906 17.3816 7.61953L7.39041 17.6107C7.13402 17.8671 6.81278 18.049 6.46102 18.1369L2.99658 19.0033L3.86294 15.5388C3.9509 15.1871 4.13279 14.8658 4.38916 14.6094L14.3813 4.61828C15.2099 3.79 16.553 3.79 17.3816 4.61828Z" stroke="#323232" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                <path d="M15.5017 9.4993L12.5005 6.49805" stroke="#323232" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                <path d="M21.0039 18.0029L19.9094 19.0974C18.7007 20.3058 16.7413 20.3058 15.5326 19.0974V19.0974C14.3224 17.8916 12.365 17.8916 11.1548 19.0974" stroke="#323232" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                </svg>\
                            </span>\
                        </a>\
                        <a style="margin-right: -13px;" href ="/User/Delete/' + data.id + '" tname="#kt_datatable" class="Confirm btn btn-sm btn-clean btn-icon" title="حذف">\
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