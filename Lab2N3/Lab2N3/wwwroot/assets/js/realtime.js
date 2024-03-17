
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/rtCart")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.on("ReceiveUpdatedCart", function (cart) {
    //
});

connection.start()
    .then(function () {
        console.log("SignalR Connected");
    })
    .catch(function (err) {
        return console.error(err.toString());
    });