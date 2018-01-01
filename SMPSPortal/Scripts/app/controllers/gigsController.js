var GigsController = function (attendanceService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
       

    };
     
    var fail = function () {
        alert("something failed!!");
    };
    var done = function () {
        var text = (button.text() == "Going") ? "Going?" : "Going";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var toggleAttendance = function (e) {

        button = $(e.target);
        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);

        else
            attendanceService.deleteAttendance(gigId, done, fail);

    };

    


    return {
        init: init
    }


}(AttendanceService);