$(function () {

    $('#page-topbar').remove();
    $('.vertical-menu').remove();
    $('.main-content').attr("style", "margin:0 !important; padding-left:100px; padding-right:100px;");
    $('.page-content').attr("style", "margin:0 !important; padding:50px !important;");
    $('footer').attr("style", "left:0 !important;");
    var LICENSE = null;
    $('#key_1').on("input", function (e) {
        let target = e.target;
        if (target.value.length === 5) {
            $('#key_2').focus();
        }
        else {
            $('#key_1').attr("class", "form-control text-center font-size-18");
            $('#key_2').attr("class", "form-control text-center font-size-18");
            $('#key_3').attr("class", "form-control text-center font-size-18");
            $('#key_4').attr("class", "form-control text-center font-size-18");
        }
    });
    $('#key_2').on("input", function (e) {
        let target = e.target;
        if (target.value.length === 5) {
            $('#key_3').focus();
        }
        else if (target.value.length === 0) {
            $('#key_1').focus();
        }
    });
    $('#key_3').on("input", function (e) {
        let target = e.target;
        if (target.value.length === 5) {
            $('#key_4').focus();
        }
        else if (target.value.length === 0) {
            $('#key_2').focus();
        }
    });
    $('#key_4').on("input", function (e) {
        let target = e.target;
        if (target.value.length === 5) {
            LICENSE = $('#key_1').val() + "-" + $('#key_2').val() + "-" + $('#key_3').val() + "-" + $('#key_4').val();
            if (LICENSE.length === 23) {
                CheckLisence(LICENSE);
            }
        }
        else if (target.value.length === 0) {
            $('#key_3').focus();
        }
    });

    function CheckLisence(key) {
        abp.ajax({
            type: 'GET',
            url: "/api/app/license/check-license",
            data: { license: key, owner: $('#currentTenant').val() }
        }).then(function (result) {
            if (result === undefined) {
                $('#key_1').attr("class", "form-control text-center font-size-18 border-danger");
                $('#key_2').attr("class", "form-control text-center font-size-18 border-danger");
                $('#key_3').attr("class", "form-control text-center font-size-18 border-danger");
                $('#key_4').attr("class", "form-control text-center font-size-18 border-danger");
            }
            else {
                $('#key_1').attr("disabled", "disabled");
                $('#key_2').attr("disabled", "disabled");
                $('#key_3').attr("disabled", "disabled");
                $('#key_4').attr("disabled", "disabled");

                $('#key_1').attr("class", "form-control text-center font-size-18 border-success");
                $('#key_2').attr("class", "form-control text-center font-size-18 border-success");
                $('#key_3').attr("class", "form-control text-center font-size-18 border-success");
                $('#key_4').attr("class", "form-control text-center font-size-18 border-success");

                document.getElementById("license").value = key;
            }
        }).catch(function () {
            alert("request failed :(");
        });
    }

});