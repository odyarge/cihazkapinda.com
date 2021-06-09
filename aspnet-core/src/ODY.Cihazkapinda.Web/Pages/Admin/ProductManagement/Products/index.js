(function ($) {
    var l = abp.localization.getResource('Cihazkapinda');

    var _productsAppService = oDY.cihazkapinda.productManagement.product;

    var _dataTable = null;

    abp.ui.extensions.entityActions.get('products').addContributor(
        function (actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: '<span style="cursor:pointer;" class="form-control border-0">' + l("Edit") + '</span>',
                        displayNameHtml: true,
                        visible: abp.auth.isGranted(
                            'ProductManagement.Products.Edit'
                        ),
                        action: function (data) {
                            location.href = "/Admin/ProductManagement/ProductAdd/?id=" + data.record.id;
                        },
                    },
                    {
                        text: '<span style="cursor:pointer;color:red;" class="form-control border-0">' + l("Delete") + '</span>',
                        displayNameHtml: true,
                        visible: function (data) {
                            return (
                                !data.isStatic &&
                                abp.auth.isGranted(
                                    'ProductManagement.Products.Delete'
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
                            _productsAppService
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

    abp.ui.extensions.tableColumns.get('products').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('products').actions.toArray()
                        }
                    },
                    //{
                    //    title: l('Image'),
                    //    data: 'image',
                    //    render: function (data, type, row) {
                    //        return name = '<img src="' + row.image + '" style="background-color:#e5e5e5;" class="rounded avatar-sm" />';
                    //    }
                    //},
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
                        title: l('Title'),
                        data: 'title',
                    },
                    {
                        title: l('SubTitle'),
                        data: 'subTitle',
                    },
                    {
                        title: l('Price'),
                        data: 'price',
                    },
                    {
                        title: l('DiscountPrice'),
                        data: 'discountPrice',
                    },
                    {
                        title: l('ProductType'),
                        data: "productType",
                        render: function (data) {
                            return l('Enum:ProductType:' + data);
                        }
                    },
                    {
                        title: l('ProductColor'),
                        data: "productColor",
                        render: function (data) {
                            return l('Enum:ProductColorType:' + data);
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
        var _$wrapper = $('#ProductsWrapper');
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
                    _productsAppService.getList
                ),
                columnDefs: abp.ui.extensions.tableColumns.get('products').columns.toArray(),
                drawCallback: function (settings, json) {
                    $('div.dropdown.action-button > ul').attr("class", "dropdown-menu centered");
                    $('div.dropdown.action-button > ul > li').css("float", "left");
                    $('div.dropdown.action-button').attr("class", "dropright");
                }
            })
        );

        _$wrapper.find('button[name=CreateNewProduct]').click(function (e) {
            location.href = "/Admin/ProductManagement/ProductAdd/";
        });
    });
})(jQuery);