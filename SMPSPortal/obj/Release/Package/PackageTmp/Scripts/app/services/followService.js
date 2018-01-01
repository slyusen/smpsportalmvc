var FollowService = function () {

    var createFollow = function (followeeid, done, fail) {
        $.post("/api/follows", { followeeid: followeeid })
      .done(done)
      .fail(fail);
    };

    var deleteFollow = function (followeeid, done, fail) {
        $.ajax({
            url: "/api/follows/" + followeeid,
            method: "DELETE"

        })
            .done(done)
            .fail(fail);
    };

    return {
        createFollow: createFollow,
        deleteFollow: deleteFollow
    }
}();
