var express = require("express");
var app = express();
var server = require("http").Server(app);
var io = require("socket.io")(server);
var port = process.env.PORT || 3000;
server.listen(port);

io.on("connection", function (socket) {
    let id = socket.id;
    console.log("[" + id + "] is connect");
    socket.on("client-print", function (obj) {
        console.log(obj);
        io.sockets.emit("server-print", obj);
    });

    socket.on("disconnect", function () {
        console.log("[" + id + "] was disconnected");
    })

    socket.on("close", function () {
        socket.disconnect(0);
    });
});

const tfsNamepaces = io.of('/tfs');
tfsNamepaces.on("connection", function (socket) {
    let id = socket.id;
    let namespace = socket.nsp;
    console.log("[" + id + "] is connect");

    socket.on("client-print", function (obj) {
        console.log(obj);
        io.sockets.emit("server-print", obj);
    });
});