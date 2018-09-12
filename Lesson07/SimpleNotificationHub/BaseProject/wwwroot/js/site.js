// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();

let issueCount = 0;
const link = $("#notification-count").parent().attr("href");
connection.on("OnIssueCreate", function (issue) {
    issueCount += 1;
    $("#notification-count").text(issueCount);
    $("#notification-count").parent().attr("href", link + issue.id);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

