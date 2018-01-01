var LoadDropDownController = function (upDrop, downDrop, url) {

   
       
            //Dropdownlist Selectedchange event
            $(upDrop)
                .change(function () {
                    $(downDrop).empty();
                    $.ajax({
                            type: 'POST',
                            url: '/api/' + url + '/' + $(upDrop).val(), // we are calling json method

                            dataType: 'json'


                            // here we are get value of selected country and passing same value as input to json method GetStates.


                        })
                        .done(function(results) {
                                // states contains the JSON formatted list
                                // of states passed from the controller

                                $.each(results,
                                    function(i, result) {
                                        $(downDrop)
                                            .append('<option value="' +
                                                result.value +
                                                '">' +
                                                result.text +
                                                '</option>');
                                        // here we are adding option for States

                                    });
                            })
                            .fail(function (ex) {
                                bootbox.alert('Failed to retrieve data.' + ex);
                            });
                    return false;
                });
   
}

var LoadDropDownController2 = function (upDrop, downDrop, midDrop, url) {



    //Dropdownlist Selectedchange event
    $(upDrop)
        .change(function () {
            $(downDrop).empty();
            var valList = []
            valList[0] = $(upDrop).val();
            valList[1] = $(midDrop).val()
            $.ajax({
                type: 'POST',
                url: '/api/' + url + '/' + valList, // we are calling json method

                dataType: 'json'


                // here we are get value of selected country and passing same value as input to json method GetStates.


            })
                .done(function (results) {
                    // states contains the JSON formatted list
                    // of states passed from the controller

                    $.each(results,
                        function (i, result) {
                            $(downDrop)
                                .append('<option value="' +
                                    result.value +
                                    '">' +
                                    result.text +
                                    '</option>');
                            // here we are adding option for States

                        });
                })
                    .fail(function (ex) {
                        bootbox.alert('Failed to retrieve data.' + ex);
                    });
            return false;
        });

}
var LoadDropDownController3 = function (upDrop, downDrop, midDrop, url) {



    //Dropdownlist Selectedchange event
    $(upDrop)
        .change(function () {
            $(downDrop).empty();
            var valList = []
            valList[0] = $(upDrop).val();
            valList[1] = $(midDrop).val()
            $.post('/api/' + url, { ValuesList: valList })
                .done(function (results) {
                    // states contains the JSON formatted list
                    // of states passed from the controller

                    $.each(results,
                        function (i, result) {
                            $(downDrop)
                                .append('<option value="' +
                                    result.value +
                                    '">' +
                                    result.text +
                                    '</option>');
                            // here we are adding option for States

                        });
                })
                    .fail(function (ex) {
                        bootbox.alert('Failed to retrieve data.' + ex);
                    });
            return false;
        });

}
var GetClassPosition = function(studentId, spanId)
{
    $.ajax({
        type: 'POST',
        url: '/api/GetPosition/' + $(studentId).val(), // we are calling json method

        dataType: 'json'
        
        // here we are get value of selected country and passing same value as input to json method GetStates.
    })
    .done(function (result) {
        $(spanId).empty();
        $(spanId).append(result);
    })
    .fail(function (ex) {
        bootbox.alert('Failed to retrieve data.' + ex);
    });
}
var GetClassPosition2 = function (studentId, classId, yTermId, spanId) {
    var valList = []
    valList[0] = $(studentId).val();
    valList[1] = $(classId).val()
    valList[2] = $(yTermId).val()
    $.post('/api/GetPosition2', { ValuesList: valList })
    .done(function (result) {
        $(spanId).empty();
        $(spanId).append(result);
    })
    .fail(function (ex) {
        bootbox.alert('Failed to retrieve data.' + ex);
    });
}

var GetClassPosition3 = function (classId, yTermId, spanId) {
    var valList = []
    
    valList[0] = $(classId).val()
    valList[1] = $(yTermId).val()
    $.post('/api/GetPosition3', { ValuesList: valList })
    .done(function (result) {
        $(spanId).empty();
        $(spanId).append(result);
    })
    .fail(function (ex) {
        bootbox.alert('Failed to retrieve data.' + ex);
    });
}