(function ($) {
    var l = abp.localization.getResource('Cihazkapinda');

    var _productImagesAppService = oDY.cihazkapinda.productManagement.productImage;

    var _editModal = new abp.ModalManager(
        abp.appPath + 'Admin/ProductManagement/ProductImages/EditModal'
    );
    var _createModal = new abp.ModalManager(
        abp.appPath + 'Admin/ProductManagement/ProductImages/CreateModal'
    );

    var _dataTable = null;

    abp.ui.extensions.entityActions.get('productImages').addContributor(
        function (actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: l("Edit"),
                        visible: abp.auth.isGranted(
                            'ProductManagement.ProductImages.Edit'
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
                                    'ProductManagement.ProductImages.Delete'
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
                            _productImagesAppService
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

    abp.ui.extensions.tableColumns.get('productImages').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('productImages').actions.toArray()
                        }
                    },
                    {
                        title: l('Image'),
                        data: 'image',
                        render: function (data, type, row) {
                            return name = '<img src="' + row.image + '" style="background-color:#e5e5e5;" class="rounded avatar-sm" />';
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
        var _$wrapper = $('#ProductImagesWrapper');
        var _$table = _$wrapper.find('table');

        _dataTable = _$table.DataTable(
            abp.libs.datatables.normalizeConfiguration({
                order: [[1, 'asc']],
                searching: false,
                processing: true,
                serverSide: true,
                scrollX: true,
                paging: true,
                ajax: abp.libs.datatables.createAjax(
                    _productImagesAppService.getList
                ),
                columnDefs: abp.ui.extensions.tableColumns.get('productImages').columns.toArray(),
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

        _$wrapper.find('button[name=CreateNewProductImages]').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });
})(jQuery);