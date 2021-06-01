(function ($) {
    var l = abp.localization.getResource('Cihazkapinda');

    var _bannerImageAppService = oDY.cihazkapinda.bannerImages.bannerImage;

    var _editModal = new abp.ModalManager(
        abp.appPath + 'Admin/BannerImages/EditModal'
    );
    var _createModal = new abp.ModalManager(
        abp.appPath + 'Admin/BannerImages/CreateModal'
    );

    var _dataTable = null;

    abp.ui.extensions.entityActions.get('bannerImage').addContributor(
        function (actionList) {
            return actionList.addManyTail(
                [
                    {
                        text:l("Edit"),
                        visible: abp.auth.isGranted(
                            'BannerSettings.BannerImages.Edit'
                        ),
                        action: function (data) {
                            _editModal.open({
                                id: data.record.id,
                            });
                        },
                    },
                    //{
                    //    text: '<span style="cursor:pointer;color:red;" class="form-control border-0">' + l("Delete") + '</span>',
                    //    displayNameHtml: true,
                    //    visible: function (data) {
                    //        return (
                    //            !data.isStatic &&
                    //            abp.auth.isGranted(
                    //                'BannerSettings.BannerImages.Delete'
                    //            )
                    //        ); //TODO: Check permission
                    //    },
                    //    confirmMessage: function (data) {
                    //        return l(
                    //            'RoleDeletionConfirmationMessage',
                    //            data.record.name
                    //        );
                    //    },
                    //    action: function (data) {
                    //        _bannerImageAppService
                    //            .delete(data.record.id)
                    //            .then(function () {
                    //                _dataTable.ajax.reload();
                    //            });
                    //    },
                ]
            );
        }
    );

    abp.ui.extensions.tableColumns.get('bannerImage').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('bannerImage').actions.toArray()
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
                        title: l('Title'),
                        data: 'title',
                    },
                    {
                        title: l('Content'),
                        data: 'content',
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
        var _$wrapper = $('#BannerImageWrapper');
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
                    _bannerImageAppService.getList
                ),
                columnDefs: abp.ui.extensions.tableColumns.get('bannerImage').columns.toArray(),
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

        _$wrapper.find('button[name=CreateNewImage]').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });
})(jQuery);