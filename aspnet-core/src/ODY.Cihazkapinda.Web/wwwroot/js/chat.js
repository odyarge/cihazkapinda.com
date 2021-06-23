"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub", {
    transport: signalR.HttpTransportType.LongPolling
}).build();

document.getElementById("btn-chat").disabled = true;
document.getElementById("btn-input").disabled = true;

var $usernameAndPhone = null;
var $userName = null;
var $phone = null;
const tenantName = document.getElementById("tenantName").value;

checkCookie();
$('#startMessage').click(function () {
    $userName = $('#username').val();
    $phone = $('#phone').val();
    $usernameAndPhone = $phone + "_" + $userName;
    if ($userName != null && $userName != "" && $phone != null && $phone != "") {
        $('#inputForm').css("display", "none");

        connection.connection.options.headers = { "username": $usernameAndPhone };

        connection.start().then(function () {
            document.getElementById("btn-chat").disabled = false;
            document.getElementById("btn-input").disabled = false;
            setCookie("c_username", $usernameAndPhone, 1);
        }).catch(function (err) {
            return console.error(err.toString());
        });
    }
    else {
        $('#usernameValid').html("Zorunlu alan");
        $('#phoneValid').html("Zorunlu alan");
    }
});

connection.on("DeleteConnectionMessage", function () {
    $usernameAndPhone = null;
    $userName = null;
    $phone = null;
    document.getElementById("messageTitle").innerHTML = "Oturumunuz sonlandırılmıştır.";
    document.getElementById("messageTitle").style = "color:red;";
    $('#inputForm').css("display", "block");
});

connection.on("ReceiveMessage", function (data) {
    var clone = $('#messageContentAdd').clone('#messageBody');

    var date = new Date(data.creationTime);
    clone.find(".sendTime").html(date.toLocaleTimeString());
    clone.find(".userMessage").html(data.message);
    clone.find(".userName").html(data.sender);
    clone.find("#messageBody").attr("class", "chatbox__body__message chatbox__body__message--right");
    clone[0].style = "display:block;";

    $('#messagesList').append(clone);
});

connection.on("OwnMessage", function (data) {
    var clone = $('#messageContentAdd').clone('#messageBody');

    var date = new Date(data.creationTime);
    clone.find(".sendTime").html(date.toLocaleTimeString());
    clone.find(".userMessage").html(data.message);
    clone.find(".userName").html(data.sender.substring(data.sender.indexOf("_") + 1, data.sender.indexOf("_") + data.sender.length));
    clone.find("#messageBody").attr("class", "chatbox__body__message chatbox__body__message--left");
    clone[0].style = "display:block;";

    console.log(data);
    $('#messagesList').append(clone);
});

$(document).keypress(function (event) {
    if (event.keyCode == 13) {
        sendMessage();
    }
});

document.getElementById("btn-chat").addEventListener("click", function (event) {
    sendMessage();
});

function GetUserMessages(user) {
    abp.ajax({
        type: 'GET',
        url: "/api/app/messages/message-by-user?input=" + user,
    }).then(function (result) {
        if (result.length > 0) {
            RenderMessages(result);

            $('#inputForm').css("display", "none");

            connection.connection.options.headers = { "username": $usernameAndPhone };

            connection.start().then(function () {
                document.getElementById("btn-chat").disabled = false;
                document.getElementById("btn-input").disabled = false;
            }).catch(function (err) {
                return console.error(err.toString());
            });
        }
        else {

        }
    }).catch(function () {
        alert("request failed :(");
    });
}

function RenderMessages(result) {
    $.each(result, function (index, value) {
        var clone = $('#messageContentAdd').clone('#messageBody');

        var date = new Date(value.creationTime);
        clone.find(".sendTime").html(date.toLocaleTimeString());
        clone.find(".userMessage").html(value.message);
        clone.find(".userName").html(value.sender.substring(value.sender.indexOf("_") + 1, value.sender.indexOf("_") + value.sender.length));
        if (value.sender === tenantName) {
            clone.find("#messageBody").attr("class", "chatbox__body__message chatbox__body__message--right");
        }
        else {
            clone.find("#messageBody").attr("class", "chatbox__body__message chatbox__body__message--left");
        }
        clone[0].style = "display:block;";

        $('#messagesList').append(clone);
    });
}

function sendMessage() {
    if ($usernameAndPhone.length > 0) {
        var message = document.getElementById("btn-input").value;
        connection.invoke("SendMessage", $usernameAndPhone, tenantName, message).catch(function (err) {
            return console.error(err.toString());
        });
        document.getElementById("btn-input").value = "";
        event.preventDefault();
    }
}
function setCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
function checkCookie() {
    let username = getCookie("c_username");
    if (username != "") {
        $usernameAndPhone = username;
        GetUserMessages(getCookie("c_username"));
    } else {
        //COOKIE YOK
    }
}