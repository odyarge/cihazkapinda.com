(function ($) {
    var l = abp.localization.getResource('Cihazkapinda');

    var _productInfoTemplateAppService = oDY.cihazkapinda.productManagement.productInfoTemplate;

    var _editModal = new abp.ModalManager(
        abp.appPath + 'Admin/ProductManagement/ProductInfoTemplate/EditModal'
    );
    var _createModal = new abp.ModalManager(
        abp.appPath + 'Admin/ProductManagement/ProductInfoTemplate/CreateModal'
    );

    var _dataTable = null;

    abp.ui.extensions.entityActions.get('productInfoTemplate').addContributor(
        function (actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: '<span style="cursor:pointer;" class="form-control border-0">' + l("Edit") + '</span>',
                        displayNameHtml: true,
                        visible: abp.auth.isGranted(
                            'ProductManagement.ProductInfoTemplate.Edit'
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
                                    'ProductManagement.ProductInfoTemplate.Delete'
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
                            _productInfoTemplateAppService
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

    abp.ui.extensions.tableColumns.get('productInfoTemplate').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('productInfoTemplate').actions.toArray()
                        }
                    },
                    {
                        title: l('Title'),
                        data: 'title',
                    },
                    {
                        title: l('Description'),
                        data: 'description',
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
        var _$wrapper = $('#ProductInfoTemplateWrapper');
        var _$table = _$wrapper.find('table');

        _dataTable = _$table.DataTable(
            abp.libs.datatables.normalizeConfiguration({
                order: [[2, 'desc']],
                searching: false,
                processing: true,
                serverSide: true,
                scrollX: true,
                paging: true,
                ajax: abp.libs.datatables.createAjax(
                    _productInfoTemplateAppService.getList
                ),
                columnDefs: abp.ui.extensions.tableColumns.get('productInfoTemplate').columns.toArray(),
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

        _$wrapper.find('button[name=CreateProductInfoTemplate]').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });
})(jQuery);