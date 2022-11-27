

var connectionR = new signalR.HubConnectionBuilder().withUrl("/notificationHub?name=erwin").build();

connectionR.on("NotificationR", (value) => {

    console.log(value);

    var newNotificationList = document.getElementById("NotificationList");
    var newNotificationCount = document.getElementById("NotificationCount");
    var notifList = [];

    Object.keys(value).forEach(key => {
        notifList.push("<li class='list-group-item'>" + value[key].ltoMessage + "</li>");
    });
    newNotificationCount.innerHTML = Object.keys(value).length.toString();
    newNotificationList.innerHTML = notifList.join("");
    console.log("NotificationR");
});

function NotificationOnClient() {
    connectionR.invoke("SendMessage","user-1","message from client").then((value) => console.log(value));
}

function connectionRSuccess() {
    console.log("Connection to Hub Successful");
    NotificationOnClient();
}

function connectionRError() {
    console.log("Connection to Hub Failed");
}

connectionR.start().then(connectionRSuccess, connectionRError);