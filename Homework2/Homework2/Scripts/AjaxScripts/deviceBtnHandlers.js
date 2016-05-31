function RefreshDevice(id) {
	RefreshBrightness(id);
	RefreshClock(id);
}

function RefreshBrightness(id) {
	var label = $("#" + id + " [interface=\"IBrightable\"] .value");

	if (label.length > 0) {
		$.ajax({
			url: "/api/Brightness/" + id,
			type: "GET",
			success: function (data) {
				label[0].innerHTML = data;
			}
		});
	}
}

function RefreshClock(id) {
	var clock = $("#" + id + "[devtype=\"Clock\"]");

	if (clock.length > 0) {
		$.ajax({
			url: "/api/Clock/" + id,
			type: "GET",
			success: function (data) {
				if (clock.attr("on") == "on") {
					$("#" + id + " .devIcon")[0].innerHTML = "<span>" + (data.hour < 10 ? "0" : "") + data.hour + " " + (data.minute < 10 ? 0 : "") + data.minute + "</span>";
				}
				else {
					$("#" + id + " .devIcon span").remove();
				}
			}
		});
	}
}

//btnDelete
$("#devicePanel").on("click", ".btnDelete", function (event) {
	event.preventDefault();
	var device = $(this).parentsUntil("#devicePanel").last();
	var id = device.prop("id");
	if (confirm("Действительно удалить устройство " + id + "?")) {
		$.ajax({
			url: "/api/Device/" + id,
			type: "DELETE",
			statusCode: {
				200: function () {
					$("#" + id).remove();
				},
				404: function () {
					alert("Устройство " + id + " не найдено");
				}
			}
		});
	}
});

//btnPower
$("#devicePanel").on("click", ".btnPower", function (event) {
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
			switch (data) {
				case "on":
					device.attr("on", "on");
					break;
				default:
					device.removeAttr("on");
					break;
			}
			RefreshDevice(id);
		}
	});
});

//IOpenCloseable
$("#devicePanel").on("click", "[interface=\"IOpenCloseable\"] .btnSwitch", function (event) {
	event.preventDefault();
	var device = $(this).parentsUntil("#devicePanel").last();
	var button = this;
	var id = device.prop("id");
	var nextState;

	if (device.attr("open") == "open") {
		nextState = false;
	}
	else {
		nextState = true;
	}

	$.ajax({
		url: "/api/Door/" + id,
		data: { "": nextState },
		type: "PUT",
		success: function (data) {
			switch (data) {
				case true:
					device.attr("open", "open");
					button.innerHTML = "открыто";
					break;
				default:
					button.innerHTML = "закрыто";
					device.removeAttr("open");
					break;
			}
		}
	});
});

//IHaveThermostat
$("#devicePanel").on("click", "[interface=\"IHaveThermostat\"] .btnArrow", function (event) {
	event.preventDefault();
	var device = $(this).parentsUntil("#devicePanel").last();
	var button = $(this);
	var id = device.prop("id");
	var label = $("#" + id + " [interface=\"IHaveThermostat\"] .value")[0];
	var action;

	if (button.is(".btnDecrease")) {
		action = "-";
	}
	if (button.is(".btnIncrease")) {
		action = "+";
	}

	$.ajax({
		url: "/api/Temperature/" + id,
		data: { "": action },
		type: "PUT",
		success: function (data) {
			label.innerHTML = data;
		}
	});
});

//IBrightable
$("#devicePanel").on("click", "[interface=\"IBrightable\"] .btnArrow", function (event) {
	event.preventDefault();
	var device = $(this).parentsUntil("#devicePanel").last();
	var button = $(this);
	var id = device.prop("id");
	var label = $("#" + id + " [interface=\"IBrightable\"] .value")[0];
	var action;

	if (button.is(".btnDecrease")) {
		action = "-";
	}
	if (button.is(".btnIncrease")) {
		action = "+";
	}

	$.ajax({
		url: "/api/Brightness/" + id,
		data: { "": action },
		type: "PUT",
		success: function (data) {
			label.innerHTML = data;
		}
	});
});