var FollowController = function (followService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-follow", toggleFollow);

    };


    var fail = function () {
        alert("something failed!!");
    };

    var done = function () {
        var text = (button.text() == "Following") ? "Follow?" : "Following";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };


    var toggleFollow = function (e) {

        button = $(e.target);
        var followeeid = button.attr("data-artist-id");


        if (button.hasClass("btn-default"))
            followService.createFollow(followeeid, done, fail);
        else
            followService.deleteFollow(followeeid, done, fail);

    };



   
    return {
        init: init
    }


}(FollowService);