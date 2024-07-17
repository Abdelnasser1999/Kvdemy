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
                        url: 'Category/GetCategoryData',
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
                }, {
                    field: 'image',
                    title: 'الصورة ',

                    width: '60',
                    template: function (data) {
                        return '<img src="https://localhost:44302/Images/' + data.image + '" alt="Profile Image" width="50" height = "50">';
                    }
                }, {
                    field: 'name',
                    title: 'الاسم ',
                    width: '250'
                },
                {
                    field: 'Actions',
                    title: 'العمليات',
                    sortable: false,
                    width: 125,
                    overflow: 'visible',
                    autoHide: false,
                    template: function (data) {
                        return '\<a  href ="/Category/SubCategories/' + data.id + '"  title="الاقسام الداخلية ' + data.name + ' ">\
                            <span class="svg-icon svg-icon-warning svg-icon-md">\
                                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">\
                                <rect x="2.99658" y="3.78906" width="18.0075" height="16.0067" rx="3.6" stroke="#215CA8" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                <path d="M12.2503 11.8C12.2503 11.9381 12.1383 12.0501 12.0002 12.0501C11.8621 12.0501 11.7501 11.9381 11.7501 11.8C11.7501 11.6619 11.8621 11.5499 12.0002 11.5499C12.1383 11.5499 12.2503 11.6619 12.2503 11.8" stroke="#215CA8" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                <path d="M8.24836 11.7922C8.24836 11.9303 8.13638 12.0423 7.99826 12.0423C7.86013 12.0423 7.74815 11.9303 7.74815 11.7922C7.74815 11.6541 7.86013 11.5421 7.99826 11.5421C8.13638 11.5421 8.24836 11.6541 8.24836 11.7922" stroke="#215CA8" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                <path d="M16.2518 11.7824C16.2518 11.9206 16.1398 12.0325 16.0017 12.0325C15.8635 12.0325 15.7516 11.9206 15.7516 11.7824C15.7516 11.6443 15.8635 11.5323 16.0017 11.5323C16.1398 11.5323 16.2518 11.6443 16.2518 11.7824" stroke="#215CA8" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                </svg>\
                            </span>\
                        </a>\
                        <a  href ="/Category/Update/' + data.id + '" class="PopUp btn btn-sm btn-clean btn-icon mr-2" title="تعديل  بيانات ' + data.name + ' ">\
                            <span class="svg-icon svg-icon-warning svg-icon-md">\
                                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">\
                                <path fill-rule="evenodd" clip-rule="evenodd" d="M17.3816 4.61828V4.61828C18.21 5.44721 18.21 6.7906 17.3816 7.61953L7.39041 17.6107C7.13402 17.8671 6.81278 18.049 6.46102 18.1369L2.99658 19.0033L3.86294 15.5388C3.9509 15.1871 4.13279 14.8658 4.38916 14.6094L14.3813 4.61828C15.2099 3.79 16.553 3.79 17.3816 4.61828Z" stroke="#323232" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                <path d="M15.5017 9.4993L12.5005 6.49805" stroke="#323232" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                <path d="M21.0039 18.0029L19.9094 19.0974C18.7007 20.3058 16.7413 20.3058 15.5326 19.0974V19.0974C14.3224 17.8916 12.365 17.8916 11.1548 19.0974" stroke="#323232" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>\
                                </svg>\
                            </span>\
                        </a>\
                        <a style="margin-right: -13px;" href ="/Category/Delete/' + data.id + '" tname="#kt_datatable" class="Confirm btn btn-sm btn-clean btn-icon" title="حذف">\
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
