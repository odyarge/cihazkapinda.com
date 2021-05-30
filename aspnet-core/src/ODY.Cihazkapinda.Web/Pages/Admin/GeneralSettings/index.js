(function ($) {
    var l = abp.localization.getResource('Cihazkapinda');

    var _generalSettingAppService = oDY.cihazkapinda.generalSettings.generalSetting;


    var tabMenus = document.getElementById("SettingsTabs");
    var tabContents = document.getElementById("SettingsTabsContent");

    var PARTIAL_MODELS = [
        "General"
    ];

    for (var i = 0; i < PARTIAL_MODELS.length; i++) {
        var activeTab = i === 0 ? "active show" : null;
        var activetedTab = i === 0 ? true : false;

        var tabMenu = `<li class="nav-item"><a class="nav-link ` + activeTab + `" id="nav-` + PARTIAL_MODELS[i] + `-tab" data-toggle="tab" href="#nav-` + PARTIAL_MODELS[i] + `" role="tab" aria-controls="nav-` + PARTIAL_MODELS[i] + `" aria-selected=` + activetedTab + `>` + PARTIAL_MODELS[i] + `</a></li>`;
        var tab = `
                <div class="tab-pane fade ` + activeTab + `" id="nav-` + PARTIAL_MODELS[i] + `" role="tabpanel" aria-labelledby="nav-` + PARTIAL_MODELS[i] + `-tab">
                    <div class="spinner-border text-primary m-1" role="status">
                        <span class="sr-only">`+ l("Loading") + `</span>
                    </div>
                </div>`;

        tabMenus.innerHTML += tabMenu;
        tabContents.innerHTML += tab;

        if (i === 0) {
            render(PARTIAL_MODELS[i]);
        }
    }

    $('#SettingsTabs > li').click(function (e) {
        let target_id = e.target.id.toString();
        let id = target_id.substring(target_id.indexOf("-") + 1, (target_id.length - target_id.indexOf("-")) - 1);

        render(id);

    });

    function render(id) {
        abp.ajax({
            type: 'GET',
            url: "SubSettings/" + id,
            dataType: 'html',
        }).then(function (result) {
            $('#nav-' + id).html(result)
        }).catch(function () {
            alert("request failed :(");
        });
    }
})(jQuery);