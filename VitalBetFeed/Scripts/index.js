$(function () {
    var hub = $.connection.matchesHub;

    hub.client.getMatches = function (matches) {
        $("#lastUpdate").val(new Date().toJSON());

        $("#table-body").html('');
        matches.forEach(function (element) {
            var tr = $('<tr>');
            tr.append($('<td>').text(element.Name));
            tr.append($('<td>').text(element.StartDate));
            $("#table-body").append(tr);
        });
    };

    $.connection.hub.start().done(function () {
        hub.server.start();
    });
});