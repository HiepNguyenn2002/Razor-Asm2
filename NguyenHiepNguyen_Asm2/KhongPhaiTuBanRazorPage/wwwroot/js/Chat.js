

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();


connection.start().then(function () {
    console.log("SignalR Connected.");
}).catch(function (err) {
    return console.error(err.toString());
});


connection.on("ReceiveMessage", function (user, message) {
    addMessage(user, message);
});

document.getElementById("sendButton").addEventListener("click", function () {
    const message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", connection.connectionId, message).catch(function (err) {
        console.error(err.toString());
    });
    document.getElementById("messageInput").value = "";
});

function addMessage(user, message) {
    const encodedMsg = $("<div />").text(message).html();
    const listItem = `<li><strong>${user}</strong>: ${encodedMsg}</li>`;
    $("#messageList").append(listItem);
    $("#messageList").scrollTop($("#messageList")[0].scrollHeight);
}




function openForm() {
    document.getElementById("myForm").style.display = "block";
}

function closeForm() {
    document.getElementById("myForm").style.display = "none";
}