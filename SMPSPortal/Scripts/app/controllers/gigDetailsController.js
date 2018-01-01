var gigDetails = function() {
    
    $(document)
       .on("click",
           ".gigDetailsBtn",
           function (e) {


               var button = $(e.target);
              
               var gDetails = button.attr("data-gig-details").split(",");
               
               bootbox.dialog({
                   title: "Event Details",
                   message: '<div class="row">  ' +
                       '<div class="col-xs-5"> ' +
                       '<p> <strong> Title </strong> </p>' +
                       '<p>' + gDetails[0] + ' </p>' +
                        '<p><strong> Venue </strong> </p>' +
                       '<p>' + gDetails[1] + ' </p>' +
                       '<p> <strong> Date Created </strong> </p>' +
                       '<p>' + gDetails[2] + ' </p>' +
                       '<p><strong> Created by </strong> </p>' +
                       '<p>' + gDetails[3] + ' </p>' +
                        '<p> <strong> Type of Event </strong> </p>' +
                       '<p>' + gDetails[4] + ' </p>' +
                       '</div>' +
                       ' </div>'
                   
               }
               );
             
           });
}