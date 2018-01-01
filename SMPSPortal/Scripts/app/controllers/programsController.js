var programsController = function() {
    
    $(document)
        .ready(function() {
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
            ".btn-info",
            function (e) {
               
                bootbox.dialog({
                    title: "Add New Program",
                    message: '<div class="row">  ' +
                        '<div class="form-group frm">' +
                        '<label >Name </label>' +
                        '<input type="text" id="txtName" class="form-control" />' +
                        '<p> </p>'+
                        '<label >Description </label>' +
                        '<input type="text" id="txtDescription" class="form-control" />' +
                        '</div>'+
                        ' </div>',
                    buttons: {
                        success: {
                            label: "Save",
                            className: "btn-success",
                            callback: function () {
                                var txtName = $("input[id='txtName']").val();
                                var txtDescription = $("input[id='txtDescription']").val();
                              
                                var valList = [];

                                valList[0] = txtName;
                                valList[1] = txtDescription;

                               
                                $.post("/api/academics", { ValuesList: valList })
                                    .done(bootbox.alert("Changes Submitted!! Refresh page to reflect"));
                                    
                                ;
                               window.location.reload(true);

                            }
                        }
                    }
                }
                );
              
            });

}