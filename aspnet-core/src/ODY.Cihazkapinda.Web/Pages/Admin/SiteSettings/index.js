(function ($) {
    var l = abp.localization.getResource('Cihazkapinda');

    var _siteSettingAppService = oDY.cihazkapinda.siteSettings.siteSetting;

    var _editModal = new abp.ModalManager(
        abp.appPath + 'Admin/SiteSettings/EditModal'
    );
    var _createModal = new abp.ModalManager(
        abp.appPath + 'Admin/SiteSettings/CreateModal'
    );

    var _dataTable = null;

    abp.ui.extensions.entityActions.get('siteSetting').addContributor(
        function (actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: '<span style="cursor:pointer;" class="form-control border-0">' + l("Edit") + '</span>',
                        displayNameHtml: true,
                        visible: abp.auth.isGranted(
                            'SiteSettings.SiteSettings.Edit'
                        ),
                        action: function (data) {
                            _editModal.open({
                                id: data.record.id,
                            });
                        },
                    },
                    {
                        text: '<span style="cursor:pointer; color:red;" class="form-control border-0">' + l("Delete") + '</span>',
                        displayNameHtml: true,
                        visible: function (data) {
                            return (
                                !data.isStatic &&
                                abp.auth.isGranted(
                                    'SiteSettings.SiteSettings.Delete'
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
                            _siteSettingAppService
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

    abp.ui.extensions.tableColumns.get('siteSetting').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('siteSetting').actions.toArray()
                        }
                    },
                    {
                        title: l('SiteActivated'),
                        data: 'sitE_ACTIVATED',
                        render: function (data, type, row) {
                            var name = '';
                            if (row.sitE_ACTIVATED === true) {
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
                        title: l('SiteName'),
                        data: 'sitE_NAME'
                    },
                    {
                        title: l('SiteOperator'),
                        data: 'sitE_OPERATOR'
                    },
                    {
                        title: l('SiteLicense'),
                        data: 'sitE_LICENSE'
                    },
                    {
                        title: l('SiteLicenseFinishTime'),
                        data: 'sitE_LICENSE_FINISH_TIME',
                        render: function (data) {
                            return luxon
                                .DateTime
                                .fromISO(data, {
                                    locale: abp.localization.currentCulture.name
                                }).toLocaleString();
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
        var _$wrapper = $('#SiteSettingWrapper');
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
                    _siteSettingAppService.getList
                ),
                columnDefs: abp.ui.extensions.tableColumns.get('siteSetting').columns.toArray(),
                initComplete: function (settings, json) {
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

        _$wrapper.find('button[name=CreateSite]').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });
})(jQuery);