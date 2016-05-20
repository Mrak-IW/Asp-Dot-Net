//btnAddDevice
$("body").on("click", "#btnAddDevice", function (event) {
	event.preventDefault();
	var selectDevType = $("#selectDeviceType");
	var devType = selectDevType[0].value;

	$.ajax({
		url: "/Home/CreateDeviceFormPartial",
		data: { "devType": devType },
		type: "GET",
		success: function (data) {
			if ($(".overlay").length == 0) {
				$("body").append($(data));
			}
		}
	});
});

//frmAddDevice btnClose
$("body").on("click", "#frmAddDevice .btnClose", function (event) {
	event.preventDefault();
	$(this).parentsUntil("body").filter(".overlay").last().remove();
});

//frmAddDevice btnAddDevice
$("body").on("click", "#frmAddDevice [name=\"btnAddDevice\"]", function (event) {
	event.preventDefault();
	var selectDevType = $("#selectDeviceType");
	var devType = selectDevType[0].value;
	var device = [];

	var tbName = $("#frmAddDevice input[name=\"tbName\"]");
	var tbTemperatureMin = $("#frmAddDevice input[name=\"tbTemperatureMin\"]");
	var tbTemperatureMax = $("#frmAddDevice input[name=\"tbTemperatureMax\"]");
	var tbTemperatureStep = $("#frmAddDevice input[name=\"tbTemperatureStep\"]");
	var tbBrightnessMin = $("#frmAddDevice input[name=\"tbBrightnessMin\"]");
	var tbBrightnessMax = $("#frmAddDevice input[name=\"tbBrightnessMax\"]");
	var tbBrightnessStep = $("#frmAddDevice input[name=\"tbBrightnessStep\"]");

	var i = 0;
	device[i] = {};
	device[i].Key = "tbName";
	var devName = tbName[0].value !== "" ? tbName[0].value : tbName[0].placeholder;
	device[i].Value = devName;

	device[++i] = {};
	device[i].Key = "hidDevType";
	device[i].Value = devType;

	if (tbTemperatureMin.length > 0) {
		device[++i] = {};
		device[i].Key = "tbTemperatureMin";
		device[i].Value = tbTemperatureMin[0].value !== "" ? tbTemperatureMin[0].value : tbTemperatureMin[0].placeholder;
		device[++i] = {};
		device[i].Key = "tbTemperatureMax";
		device[i].Value = tbTemperatureMax[0].value !== "" ? tbTemperatureMax[0].value : tbTemperatureMax[0].placeholder;
		device[++i] = {};
		device[i].Key = "tbTemperatureStep";
		device[i].Value = tbTemperatureStep[0].value !== "" ? tbTemperatureStep[0].value : tbTemperatureStep[0].placeholder;
	}
	
	if (tbBrightnessMin.length > 0) {
		device[++i] = {};
		device[i].Key = "tbBrightnessMin";
		device[i].Value = tbBrightnessMin[0].value !== "" ? tbBrightnessMin[0].value : tbBrightnessMin[0].placeholder;
		device[++i] = {};
		device[i].Key = "tbBrightnessMax";
		device[i].Value = tbBrightnessMax[0].value !== "" ? tbBrightnessMax[0].value : tbBrightnessMax[0].placeholder;
		device[++i] = {};
		device[i].Key = "tbBrightnessStep";
		device[i].Value = tbBrightnessStep[0].value !== "" ? tbBrightnessStep[0].value : tbBrightnessStep[0].placeholder;
	}

	var str = JSON.stringify(device);

	$.ajax({
		url: "/api/Device",
		data: { "": str },
		type: "POST",
		success: function (data) {
			$.ajax({
				url: "/Home/GetDeviceView/",
				data: { "id": devName },
				type: "GET",
				success: function (data) {
					$("#devicePanel").append($(data));
				}
			});
		},
		error: function (jqXHR, textStatus, errorThrown) {
			alert("Возникла ошибка: " + JSON.parse(jqXHR.responseText).Message);
		}
	});
	$(this).parentsUntil("body").filter(".overlay").last().remove();
});