
//Event for RadioButton
function onchangeCPU(elem) {
    var cpus = document.getElementById('CPUs');
    while (cpus.firstChild) {
        cpus.removeChild(cpus.firstChild);
    }

    $.ajax({
        url: "/Build/DropDownListCPU",
        method: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: 'value=' + elem.val(),
        success: function (result) {
            for (var i = 0; i < result.length; i++) {
                cpus.add(new Option(result[i].name, result[i].value));
            }
            $("#CPUs").children("[value=" + $("CPUGuid").val() + "]").val("selected");
        }
    });
}

//Event for RadioButton
function onchangeGPU(elem) {
    var gpus = document.getElementById('GPUs'); //@* $('#GPUs');*@

    while (gpus.firstChild) {

        gpus.removeChild(gpus.firstChild);
    }

    $.ajax({
        url: "/Build/DropDownListGPU",
        method: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: 'value=' + elem.val(),
        success: function (result) {
            //alert("result: " + result.length + " gpus:" + gpus.options.length);
            for (var i = 0; i < result.length; i++) {
                gpus.add(new Option(result[i].name, result[i].value));
            }
            $("#GPUs").children("[value=" + $("GPUGuid").val() + "]").val("selected");
        }
    });
}

//$(document).ready(function () {
//    $("select#CPUs").change(function () {
//        $("#CPUid").val = $(this).children("option:selected").val();
//    });

//    $(document).ready(function () {
//        $("select#GPUs").change(function () {
//            $("#GPUid").val = $(this).children("option:selected").val();
//        });

//Event for hidden input(CPU)
function onchangeCPUid() {
    $("#CPUGuid").val($("#CPUs").children("option:selected").val());
}
//Event for hidden input(GPU)
function onchangeGPUid() {
    //alert($("#GPUs").attr("id"));
    $("#GPUGuid").val($("#GPUs").children("option:selected").val());
}

//Function to update hidden id
function onclickSave() {
    onchangeCPUid();
    onchangeGPUid();
}

window.onload = init;
//$(document).ready(function () {
//    init();
//});

function init() {
    
    $("#CPUs").addClass("form-control");
    $("#CPUs").change(onchangeCPUid);
    $("#GPUs").addClass("form-control");
    $("#GPUs").change(onchangeGPUid);

    //console.log("init");
    //console.log(document.getElementById("CPU").val());
    onchangeCPU($("input[name=CPU]:checked"));
    onchangeGPU($("input[name=GPU]:checked"));
    onchangeCPUid();
    onchangeGPUid();
}