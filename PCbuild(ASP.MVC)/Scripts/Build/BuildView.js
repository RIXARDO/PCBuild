//document.addEventListener("DOMContentLoaded", function () {
//    var cpu = new { value="Intel" };
//    var gpu = new {value="Nvidia"};
//
//    onchangeCPU(cpu);
//    onchangeGPU(gpu);
//});


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
        data: 'value=' + elem.value,
        success: function (result) {
            for (var i = 0; i < result.length; i++) {
                cpus.add(new Option(result[i].name, result[i].value));
            }
        }
    });
}

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
        data: 'value=' + elem.value,
        success: function (result) {
            //alert("result: " + result.length + " gpus:" + gpus.options.length);
            for (var i = 0; i < result.length; i++) {
                gpus.add(new Option(result[i].name, result[i].value));
            }
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

function onchangeCPUid() {
    $("#CPUid").val($("#CPUs").children("option:selected").val());
}

function onchangeGPUid() {
    //alert($("#GPUs").attr("id"));
    $("#GPUid").val($("#GPUs").children("option:selected").val());
}

function onclickSave() {
    onchangeCPUid();
    onchangeGPUid();
}
//$(document).ready(function () {
//    onchangeCPUid();
//    onchangeGPUid();
//});
window.onload = init;
function init() {

    $("#CPUs").addClass("form-control");
    $("#CPUs").add("onclick", onchangeCPUid());
    $("#GPUs").addClass("form-control");
    $("#GPUs").add("onclick", onchangeGPUid());

    onchangeCPU(document.getElementById("CPU"));
    onchangeGPU(document.getElementById("GPU"));
    onchangeCPUid();
    onchangeGPUid();

}