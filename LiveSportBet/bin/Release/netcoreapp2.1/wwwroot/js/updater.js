"use strict";

//create new connection to Hub
var connection = new signalR.HubConnectionBuilder().withUrl("/updateHub").build();

//if we recieve UpdateData from Hub
connection.on("UpdateData", function (matchId, rateId, data) {
    var item = $('*[data-id="' + matchId + '"]').find("td:nth-child(" + (rateId + 1) + ") div"); // Get element using jquey
    if (item.text() > data) { // check if new rate is bigger or smaller and change color
        item.addClass("red");
    } else {
        item.addClass("green");
    }
    item.text(data); // fill element with new data
    setTimeout( // after 3s remove color highlighting
        function () {
            item.removeClass();
        }, 3000);
});

// start connection
connection.start().then(function () {
    
}).catch(function (err) {
    return console.error(err.toString());
});

