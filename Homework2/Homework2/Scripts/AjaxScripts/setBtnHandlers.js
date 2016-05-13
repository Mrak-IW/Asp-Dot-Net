//btnDelete
$("#devicePanel").on("click", ".btnDelete", function () {
    event.preventDefault();
    var id = $(this).parentsUntil("#devicePanel").last()[0].id;
    //Some useful code must be here
    alert(id);
});

//btnPower
$("#devicePanel").on("click", ".btnPower", function () {
    event.preventDefault();
    var device = $(this).parentsUntil("#devicePanel").last();
    var id = device.prop("id");
    var nextState;

    if (device.attr("on") == "on") {
        nextState = "off";
    }
    else {
        nextState = "on";
    }

    $.ajax({
        url: "/api/PowerState/" + id,
        data: { "": nextState },
        type: "PUT",
        success: function (data) {
            switch (data)
            {
                case "on":
                    device.attr("on", "on");
                    break;
                default:
                    device.removeAttr("on");
                    break;
            }
        }
    });
});

//btnOpenClose
$("#devicePanel").on("click", "[interface=\"IOpenCloseable\"] .btnSwitch", function () {
    event.preventDefault();
    var id = $(this).parentsUntil("#devicePanel").last()[0].id;
    //Some useful code must be here
    alert(id);
});