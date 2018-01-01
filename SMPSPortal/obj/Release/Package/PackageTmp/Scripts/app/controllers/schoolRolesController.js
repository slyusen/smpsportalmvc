var rolesController = function() {
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
                var userOptions = "";
                var userOptions2 = "";
                var roleTitile = button.attr("data-role-title");
                var roleId = button.attr("data-role-id");
                var allUsers = button.attr("data-all-users").split(",");
                var usersInRole = button.attr("data-users-in-role").split(",");
                //allUsers.forEach(CreateOptions);
                for (i = 1; i < allUsers.length; i++) {
                    userOptions = userOptions + "<option>" + allUsers[i] + "</option>";
                }
                for (i = 1; i < usersInRole.length; i++) {
                    userOptions2 = userOptions2 + "<option>" + usersInRole[i] + "</option>";
                }
                bootbox.dialog({
                    title: "Users for " + roleTitile + " role",
                    message: '<div class="row">  ' +
                        '<div class="col-xs-5"> ' +
                        '<p>All Users </p>' +
                        '<select name="from[]" id="multiselect" class="form-control" size="8" multiple="multiple"> ' +
                        userOptions +
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
                        userOptions2 +
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

                                valList[0] = roleId;
                                valList[1] = roleTitile;

                                $('#multiselect_to option').each(function () {
                                    valList.push($(this).val());
                                });
                                $.post("/api/settings", { ValuesList: valList })
                                    .done(bootbox.alert("Changes Submitted"));
                                    
                                ;
                               window.location.reload(true);

                            }
                        }
                    }
                }
                );
                $('#multiselect').multiselect();
            });

}