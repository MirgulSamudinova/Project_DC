function CloseModal() {
    console.log("close");
    $("#partialModal").modal('toggle');
}

function ShowModal(response) {
    $("#partialModal").find(".modal-body").html(response);
    $("#partialModal").modal('show');
}

function SetSumitEvent(eventName) {
    SetVisibleSubmit(true);
    $("#partialModalFooterSubmit").attr("onclick", eventName);
}

function SetHeader(headerText) {
    $('.modal-title').text(headerText);
}

function SetSize(sizeClass) {

    var classes = $('.modal-dialog').attr("class");
    $('.modal-dialog').attr("class", "modal-dialog " + sizeClass);
}

function api_event(request_type, urlAPI, params, success_event) {
    $.ajax({
        type: request_type,
        url: urlAPI,
        data: params,
        success: success_event,
        failure: function (response) {
            console.log('Failure');
            alert(response.responseText);
        },
        error: function (response) {
            console.log('Error');
            alert(response.responseText);
        }
    });
}

function SetVisibleSubmit(stat) {
    if (stat == false) {
        $("#partialModalFooterSubmit").hide();
    }
    else {
        $("#partialModalFooterSubmit").show();
    }

}


function api_json_event(request_type, urlAPI, params, success_event, error_event) {
    $.ajax({
        type: request_type,
        url: urlAPI,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: params,
        success: success_event,
        failure: function (response) {
            console.log('Failure');
            alert(response.responseText);
        },
        error: error_event
    });
}

function SetErrorBox(errors) {
    console.log($('#ValidationSummary'));
    $("#ValidationSummary").html("");
    var errorsHtml = '<ul>';
    $.each(errors, function (key, value) {
        errorsHtml += '<li>' + value[0] + '</li>';
    });
    errorsHtml += '</ul>';
    $('#ValidationSummary').append(errorsHtml);
}

function Add(url) {
    var Obj = {};
    FillObject(url, Obj);

    console.log(JSON.stringify(Obj));
    api_json_event("POST"
        , "/api/" + url + "/"
        , JSON.stringify(Obj)
        , function (result) {
            location.reload();
        }
        , function (errormessage) {
            console.log(errormessage);
            if (errormessage.responseJSON.status == 400) {
                var errorMessages = [];
                var errors = errormessage.responseJSON.errors;
                for (var key in errors) {
                    errorMessages.push(errors[key]);
                }
                SetErrorBox(errorMessages);
            }
            else {
                SetErrorBox(errormessage.responseJSON.detail);
            }
        });
}

function Edit(url, id) {
    var Obj = {};
    FillObject(url, Obj);
    console.log(JSON.stringify(Obj));
    api_json_event("PUT"
        , "/api/" + url + "/" + id
        , JSON.stringify(Obj)
        , function (result) {
            location.reload();
        }
        , function (errormessage) {
            console.log(errormessage.responseJSON);
            if (errormessage.responseJSON.status == 400) {
                var errorMessages = [];
                var errors = errormessage.responseJSON.errors;
                for (var key in errors) {
                    errorMessages.push(errors[key]);
                }
                SetErrorBox(errorMessages);
            }
            else {
                SetErrorBox(errormessage.responseJSON.detail);
            }
        });
}
