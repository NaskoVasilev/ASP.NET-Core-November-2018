var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

// The Receive Message Client event. This will trigger, when the Back-End calls the ReceiveMessage method
connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = "[" + user + "]: " + msg;

    var messageHtmlElement = $(`<div>${encodedMsg}</div>`);
    var messageList = $("#messagesList");
    messageList.append(messageHtmlElement);
});

//An error handler for connection errors
connection.start().catch(function (err) {
    return console.error(err.toString());
});

// The Send Message DOM event. This will trigger the Back-End SendMessage method
document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = $("#messageInput").val();
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
