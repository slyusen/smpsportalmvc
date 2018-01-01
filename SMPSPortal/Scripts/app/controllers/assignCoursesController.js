var assignCoursesController = function () {
    $(document).ready(function () {
        //Check if the current URL contains '#'
        if (document.URL.indexOf("#") == -1) {
            // Set the URL to whatever it was plus "#".
            url = document.URL + "#";
            location = "#";

            //Reload the page
            location.reload(true);
        }
    });
    $(document)
        .on("click",
            ".list-group-item",
            function (e) {


                var button = $(e.target);
                var leftCourseOptions = "";
                var rightCourseOptions = "";
                var teacherName = button.attr("data-teach-name");
                var teacherId = button.attr("data-teach-id");
                var allCourses = button.attr("data-all-courses").split(",");
                var allCoursesId = button.attr("data-all-courses-id").split(",");
                var assignedCourses = button.attr("data-assigned-courses").split(",");
                var assignedCoursesId = button.attr("data-assigned-courses-id").split(",");
                //allUsers.forEach(CreateOptions);
                for (i = 1; i < allCourses.length; i++) {
                   leftCourseOptions = leftCourseOptions + "<option value='" + allCoursesId[i] + "'>" + allCourses[i] + "</option>";
                }
                for (i = 1; i < assignedCourses.length; i++) {
                    rightCourseOptions = rightCourseOptions + "<option value='" + assignedCoursesId[i] + "'>" + assignedCourses[i] + "</option>";
                }
                bootbox.dialog({
                    title: "Assign Courses for " + teacherName ,
                    message: '<div class="row">  ' +
                        '<div class="col-xs-5"> ' +
                        '<p>All Users </p>' +
                        '<select name="from[]" id="multiselect" class="form-control" size="8" multiple="multiple"> ' +
                        leftCourseOptions +
                        '</select> </div>' +
                        '<div class="col-xs-2">' +
                        ' <button type="button" id="multiselect_rightAll" class="btn btn-block"><i class="glyphicon glyphicon-forward"></i></button>' +
                        '<button type="button" id="multiselect_rightSelected" class="btn btn-block"><i class="glyphicon glyphicon-chevron-right"></i></button>' +
                        '<button type="button" id="multiselect_leftSelected" class="btn btn-block"><i class="glyphicon glyphicon-chevron-left"></i></button>' +
                        '<button type="button" id="multiselect_leftAll" class="btn btn-block"><i class="glyphicon glyphicon-backward"></i></button>' +
                        '</div>' +
                        '<div class="col-xs-5">' +
                        '<p>Users in this Role </p>' +
                        '<select name="to[]" id="multiselect_to" class="form-control to-select" size="8" multiple="multiple">' +
                        rightCourseOptions +
                        '</select>' +
                        '</div>' +
                        ' </div>',
                    buttons: {
                        success: {
                            label: "Save",
                            className: "btn-success",
                            callback: function () {
                                var sel = $("select[id='multiselect_to']");
                                var valList = [];

                                valList[0] = teacherId;
                                valList[1] = teacherName;

                                $('#multiselect_to option').each(function () {
                                    valList.push($(this).val());
                                });
                                $.post("/api/assigncourses", { ValuesList: valList })
                                    .done(bootbox.alert("Changes Submitted"));

                                
                                window.location.reload(true);

                            }
                        }
                    }
                }
                );
                $('#multiselect').multiselect();
            });

}