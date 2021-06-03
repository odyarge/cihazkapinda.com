(function ($) {
    var l = abp.localization.getResource('Cihazkapinda');

    var _categoryAppService = oDY.cihazkapinda.categories.category;

    var _editModal = new abp.ModalManager(
        abp.appPath + 'Admin/Categories/EditModal'
    );
    var _createModal = new abp.ModalManager(
        abp.appPath + 'Admin/Categories/CreateModal'
    );

    var _dataTable = null;

    abp.ui.extensions.entityActions.get('category').addContributor(
        function (actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: '<span style="cursor:pointer;" class="form-control border-0">' + l("Edit") + '</span>',
                        displayNameHtml: true,
                        visible: abp.auth.isGranted(
                            'Categories.Categories.Edit'
                        ),
                        action: function (data) {
                            _editModal.open({
                                id: data.record.id,
                            });
                        },
                    },
                    {
                        text: '<span style="cursor:pointer;color:red;" class="form-control border-0">' + l("Delete") + '</span>',
                        displayNameHtml: true,
                        visible: function (data) {
                            return (
                                !data.isStatic &&
                                abp.auth.isGranted(
                                    'Categories.Categories.Delete'
                                )
                            ); //TODO: Check permission
                        },
                        confirmMessage: function (data) {
                            return l(
                                'RoleDeletionConfirmationMessage',
                                data.record.name
                            );
                        },
                        action: function (data) {
                            _categoryAppService
                                .delete(data.record.id)
                                .then(function () {
                                    _dataTable.ajax.reload();
                                });
                        },
                    }
                ]
            );
        }
    );

    abp.ui.extensions.tableColumns.get('category').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('category').actions.toArray()
                        }
                    },
                    {
                        title: l('Name'),
                        data: 'name',
                    },
                    {
                        title: l('OwnerCategory'),
                        data: 'subCategory',
                        render: function (data, type, row) {
                            var name = '';
                            if (row.subCategory === null || row.subCategory === undefined) {
                                name =
                                    '<span class="badge badge-pill badge-danger ml-1">' +
                                    l('No') +
                                    '</span>';
                            }
                            else {
                                name =
                                    '<span class="badge badge-pill badge-success ml-1">' +
                                    l('Yes') +
                                    '</span>';
                            }
                            return name;
                        }
                    },
                    {
                        title: l('Active'),
                        data: 'active',
                        render: function (data, type, row) {
                            var name = '';
                            if (row.active === true) {
                                name =
                                    '<span class="badge badge-pill badge-success ml-1">' +
                                    l('Yes') +
                                    '</span>';
                            }
                            else {
                                name =
                                    '<span class="badge badge-pill badge-danger ml-1">' +
                                    l('No') +
                                    '</span>';
                            }
                            return name;
                        }
                    },
                    {
                        title: l('CreationTime'),
                        data: 'creationTime',
                        render: function (data) {
                            return luxon
                                .DateTime
                                .fromISO(data, {
                                    locale: abp.localization.currentCulture.name
                                }).toLocaleString();
                        }
                    },
                ]
            );
        },
        0 //adds as the first contributor
    );

    $(function () {
        var _$wrapper = $('#CategoriesWrapper');
        var _$table = _$wrapper.find('table');

        _dataTable = _$table.DataTable(
            abp.libs.datatables.normalizeConfiguration({
                order: [[4, 'desc']],
                searching: false,
                processing: true,
                serverSide: true,
                scrollX: true,
                paging: true,
                ajax: abp.libs.datatables.createAjax(
                    _categoryAppService.getList
                ),
                columnDefs: abp.ui.extensions.tableColumns.get('category').columns.toArray(),
                drawCallback: function (settings, json) {
                    $('div.dropdown.action-button > ul').attr("class", "dropdown-menu centered");
                    $('div.dropdown.action-button > ul > li').css("float", "left");
                    $('div.dropdown.action-button').attr("class", "dropright");
                }
            })
        );

        _createModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _editModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _$wrapper.find('button[name=CreateCategory]').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });
})(jQuery);