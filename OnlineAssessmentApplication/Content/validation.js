function GeneratePasscode() {
    document.getElementById('Passcode').value = Math.floor(Math.random() * 80000) + 10000;
}
$(function () {
    var dtToday = new Date();

    var month = dtToday.getMonth() + 1;
    var day = dtToday.getDate();
    var year = dtToday.getFullYear();
    if (month < 10)
        month = '0' + month.toString();
    if (day < 10)
        day = '0' + day.toString();

    var maxDate = year + '-' + month + '-' + day;

    $('#TestDate').attr('min', maxDate);
});


function getMin() {
    var v = document.getElementById("TestDate").value + " " + document.getElementById("StartTime").value;
    var d = new Date(v);
    var h = d.getHours();
    var m = d.getMinutes();
    var minTime = h + ":" + m;
    document.getElementById("EndTime").setAttribute("min", minTime);
}