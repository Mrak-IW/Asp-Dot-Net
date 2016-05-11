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
    var id = $(this).parentsUntil("#devicePanel").last()[0].id;
    //Some useful code must be here
    alert(id);
});

//btnOpenClose
$("#devicePanel").on("click", "[interface=\"IOpenCloseable\"] .btnSwitch", function () {
    event.preventDefault();
    var id = $(this).parentsUntil("#devicePanel").last()[0].id;
    //Some useful code must be here
    alert(id);
});