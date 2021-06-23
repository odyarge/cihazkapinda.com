var sendUser = "";
const currentTenantName = document.getElementById("currentTenantName").value;

var _createModal = new abp.ModalManager(
    abp.appPath + 'Admin/Customers/CreateModal'
);

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub", {
    headers: { "username": currentTenantName },
    transport: signalR.HttpTransportType.LongPolling
}).build();

connection.start().then(function () {
    return console.log("Successfully");
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceiveMessage", function (data, customers) {
    var clone = $('#messageelementclone').clone('#messageclone');
    clone[0].querySelector('.conversation-name').value = data.sender;
    clone[0].querySelector('.message-body').innerHTML = data.message;

    var date = new Date(data.creationTime);
    clone[0].querySelector('.message-send-time').innerHTML = date.toLocaleTimeString();
    clone[0].style = "display:block;";
    clone[0].className = "";
    $('#messagelist').find('.simplebar-content').append(clone);
    simpleBar.getScrollElement().scrollTo(0, $('#messagelist').find('.simplebar-content').height());

    RenderCustomers(customers);
});

connection.on("OwnMessage", function (data) {
    var clone = $('#messageelementclone').clone('#messageclone');
    clone[0].querySelector('.conversation-name').value = data.sender;
    clone[0].querySelector('.message-body').innerHTML = data.message;
    var date = new Date(data.creationTime);
    clone[0].querySelector('.message-send-time').innerHTML = date.toLocaleTimeString();
    clone[0].style = "display:block;";
    clone[0].className = "right";
    $('#messagelist').find('.simplebar-content').append(clone);
    simpleBar.getScrollElement().scrollTo(0, $('#messagelist').find('.simplebar-content').height());
});

$(document).keypress(function (event) {
    if (event.keyCode == 13) {
        sendMessage();
    }
});

document.getElementById("btn-chat").addEventListener("click", function (event) {
    sendMessage();
});


GetMessageToCustomersData();
$('.customer-element').click(function () {
    var userSelect = $(this).find('input[type="hidden"]').val();
    $('.customer-element').removeAttr("class");
    $(this).attr("class", "customer-element active");
    $(this).find('.customer-name').attr("style","font-weight:normal;");
    GetUserMessages(userSelect);
    sendUser = userSelect;
    document.getElementById("chatcontent").style = "display:block;";
    connection.invoke("MessageRead", sendUser).catch(function (err) {
        return console.error(err.toString());
    });
});
function sendMessage() {
    var message = document.getElementById("btn-input").value;
    connection.invoke("SendMessage", currentTenantName, sendUser, message).catch(function (err) {
        return console.error(err.toString());
    });

    connection.invoke("MessageRead", sendUser).catch(function (err) {
        return console.error(err.toString());
    });

    GetMessageToCustomersData();
    document.getElementById("btn-input").value = "";
    event.preventDefault();
}

//TÜM MESAJLAR İÇERİSİNDEN KULLANICILAR SEÇİLİYOR
function GetMessageToCustomersData() {
    abp.ajax({
        type: 'GET',
        url: "/api/app/messages/message-in-customers",
    }).then(function (result) {
        RenderCustomers(result);
    }).catch(function () {
        alert("request failed :(");
    });
}

//SPESİFİK KULLANICI MESAJLARINI GETİRİYOR
function GetUserMessages(user) {
    abp.ajax({
        type: 'GET',
        url: "/api/app/messages/message-by-user?input=" + user,
    }).then(function (result) {
        RenderMessages(result);
    }).catch(function () {
        alert("request failed :(");
    });
}

$('#closeMessage').click(function () {
    abp.ajax({
        type: 'DELETE',
        url: "/api/app/messages/messages?input=" + sendUser,
    }).then(function () {
        GetMessageToCustomersData();
        connection.invoke("DeleteConnection", sendUser).catch(function (err) {
            return console.error(err.toString());
        });
        document.getElementById("chatcontent").style = "display:none;";
    }).catch(function () {
        alert("request failed :(");
    });
});


$('#saveCustomer').click(function () {
    _createModal.open({
        input: sendUser
    });
});

_createModal.onResult(function () {
    abp.message.success("Müşteri kayıt edildi.", "Başarılı");
});

//MESAJLAR EKRANA BASILIYOR

const $customername = document.getElementById("customer-name");
const simpleBar = new SimpleBar(document.getElementById('messagelist'));
function RenderMessages(result) {
    $('#messagelist').find('.simplebar-content').empty();
    $customername.innerHTML = result[0].receive == currentTenantName ? result[0].sender : result[0].receive;
    $.each(result, function (index, value) {

        var clone = $('#messageelementclone').clone('#messageclone');
        clone[0].querySelector('.conversation-name').value = value.sender;
        clone[0].querySelector('.message-body').innerHTML = value.message;
        var date = new Date(value.creationTime);
        clone[0].querySelector('.message-send-time').innerHTML = date.toLocaleTimeString();
        clone[0].style = "display:block;";
        if (value.sender === currentTenantName) {
            clone[0].className = "right";
        }
        else {
            clone[0].className = "";
        }
        $('#messagelist').find('.simplebar-content').append(clone);
    });
    simpleBar.getScrollElement().scrollTo(0, $('#messagelist').find('.simplebar-content').height());
}
//MÜŞTERİLER EKRANA BASILIYOR

function RenderCustomers(result) {
    $('#chatlist').find('.simplebar-content').empty();
    $.each(result, function (index, value) {
        if (value.sender !== currentTenantName) {
            var clone = $('#customerelementclone').clone('#cloneempty');
            clone[0].querySelector('.customer-value').value = value.sender;
            clone[0].querySelector('.customer-circle').innerHTML = value.sender.substring(value.sender.indexOf("_") + 1, value.sender.indexOf("_") + 2).toUpperCase();
            clone[0].querySelector('.customer-name').innerHTML = value.sender;
            if (value.status === 0) {
                clone[0].querySelector('.customer-name').style = "font-weight:bold;";
            }
            else {
                clone[0].querySelector('.customer-name').style = "font-weight:normal;";
            }
            clone[0].style = "display:block;cursor:pointer;";
            $('#chatlist').find('.simplebar-content').append(clone);
        }
    });
}