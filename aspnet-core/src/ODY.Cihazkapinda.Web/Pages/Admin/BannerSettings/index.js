(function ($) {
    var l = abp.localization.getResource('Cihazkapinda');

    var _bannerSettingAppService = oDY.cihazkapinda.bannerSettings.bannerSetting;

    var _editModal = new abp.ModalManager(
        abp.appPath + 'Admin/BannerSettings/EditModal'
    );
    var _createModal = new abp.ModalManager(
        abp.appPath + 'Admin/BannerSettings/CreateModal'
    );

    var _dataTable = null;

    abp.ui.extensions.entityActions.get('bannerSetting').addContributor(
        function (actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: '<span style="cursor:pointer;" class="form-control border-0">' + l("Edit") + '</span>',
                        displayNameHtml: true,
                        visible: abp.auth.isGranted(
                            'BannerSettings.BannerSettings.Edit'
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
                                    'BannerSettings.BannerSettings.Delete'
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
                            _bannerSettingAppService
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

    abp.ui.extensions.tableColumns.get('bannerSetting').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('bannerSetting').actions.toArray()
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
                        title: l('Title'),
                        data: 'title',
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
        var _$wrapper = $('#BannerSettingWrapper');
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
                    _bannerSettingAppService.getList
                ),
                columnDefs: abp.ui.extensions.tableColumns.get('bannerSetting').columns.toArray(),
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

        _$wrapper.find('button[name=CreateNewSlide]').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });
})(jQuery);