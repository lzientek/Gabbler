window.alert = function (message, type,onOk) {
    if (!type) { type = 'success' }
    $("body").append('<div class="popup"><div class="dialog dialog-' + type + '"><p>' +
        message +
        '</p><div class="popup-btns">' +
        '<button class="btn popup-ok btn-' + type + '">OK</button>' +
        '</div></div></div>');
    $(".popup-ok").click(function () {
        if (typeof (onOk) == "function") { onOk(); }
        $(".popup").fadeOut(400, function () {
            $(".popup").first().detach();
        });
    });
}

function valid(message, type, onValid, onCancel) {
    if (!type) { type = 'success' }
    $("body").append('<div class="popup"><div class="dialog dialog-' + type + '"><p>' +
        message +
        '</p><div class="popup-btns">' +
        '<button class="btn popup-cancel btn-primary">Cancel</button>' +
        '<button class="btn popup-ok btn-' + type + '">OK</button>' +
        '</div></div></div>');
    $(".popup-ok").click(function () {
        if (typeof (onValid) == "function") { onValid(); }
        $(".popup").fadeOut(400, function () {
            $(".popup").first().detach();
        });
    });
    $(".popup-cancel").click(function () {
        if (typeof (onCancel) == "function") { onCancel(); }
        $(".popup").fadeOut(400, function () {
            $(".popup").first().detach();
        });
    });
}