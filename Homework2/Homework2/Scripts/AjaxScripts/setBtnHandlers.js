function RefreshDevice(id)
{
	RefreshBrightness(id);
}

function RefreshBrightness(id)
{
	var label = $("#" + id + " [interface=\"IBrightable\"] .value");
	if (label.length > 0) {
		$.ajax({
			url: "/api/IBrightable/" + id,
			type: "GET",
			success: function (data) {
				label[0].innerHTML = data;
			}
		});
	}
}

//btnDelete
$("#devicePanel").on("click", ".btnDelete", function () {
	event.preventDefault();
	var device = $(this).parentsUntil("#devicePanel").last();
	var id = device.prop("id");
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
$("#devicePanel").on("click", "[interface=\"IOpenCloseable\"] .btnSwitch", function () {
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
		url: "/api/IsOpened/" + id,
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
$("#devicePanel").on("click", "[interface=\"IHaveThermostat\"] .btnArrow", function () {
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
		url: "/api/IHaveThermostat/" + id,
		data: { "": action },
		type: "PUT",
		success: function (data) {
			label.innerHTML = data;
		}
	});
});

//IBrightable
$("#devicePanel").on("click", "[interface=\"IBrightable\"] .btnArrow", function () {
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
		url: "/api/IBrightable/" + id,
		data: { "": action },
		type: "PUT",
		success: function (data) {
			label.innerHTML = data;
		}
	});
});