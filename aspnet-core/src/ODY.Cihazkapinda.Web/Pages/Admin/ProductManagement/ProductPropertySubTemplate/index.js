(function ($) {
    var l = abp.localization.getResource('Cihazkapinda');

    var _productPropertySubTemplateAppService = oDY.cihazkapinda.productManagement.productPropertySubTemplate;

    var _editModal = new abp.ModalManager(
        abp.appPath + 'Admin/ProductManagement/ProductPropertySubTemplate/EditModal'
    );

    var _dataTable = null;

    abp.ui.extensions.entityActions.get('productPropertySubTemplate').addContributor(
        function (actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: '<span style="cursor:pointer;" class="form-control border-0">' + l("Edit") + '</span>',
                        displayNameHtml: true,
                        visible: abp.auth.isGranted(
                            'ProductManagement.ProductPropertySubTemplate.Edit'
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
                                    'ProductManagement.ProductPropertySubTemplate.Delete'
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
                            _productPropertySubTemplateAppService
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

    abp.ui.extensions.tableColumns.get('productPropertySubTemplate').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('productPropertySubTemplate').actions.toArray()
                        }
                    },
                    {
                        title: l('Title'),
                        data: 'title',
                    },
                    {
                        title: l('Key'),
                        data: 'key',
                    },
                    {
                        title: l('Value'),
                        data: 'value',
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
        var _$wrapper = $('#ProductPropertySubTemplateWrapper');
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
                    _productPropertySubTemplateAppService.getList
                ),
                columnDefs: abp.ui.extensions.tableColumns.get('productPropertySubTemplate').columns.toArray(),
                drawCallback: function (settings, json) {
                    $('div.dropdown.action-button > ul').attr("class", "dropdown-menu centered");
                    $('div.dropdown.action-button > ul > li').css("float", "left");
                    $('div.dropdown.action-button').attr("class", "dropright");
                }
            })
        );


        _editModal.onResult(function () {
            _dataTable.ajax.reload();
        });

    });
})(jQuery);