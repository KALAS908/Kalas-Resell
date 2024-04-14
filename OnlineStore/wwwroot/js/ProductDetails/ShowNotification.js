function ShowNotification(message) {
    var notification = $('#notification');
    notification.text(message);
    notification.removeClass("hidden");
    setTimeout(function () {
        notification.addClass("hidden");
    }, 3000);
}