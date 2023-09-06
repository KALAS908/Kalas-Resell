function RemoveComment(commentId) {

    $.ajax({
        url: `/Product/RemoveComment`,
        type: 'POST',
        data: { commentId: commentId },
        success: function (result) {

            $(`#comment-${commentId}`).remove();
            var notification = $('#notification');
            notification.text('comment removed');
            notification.css('color', 'green');
            notification.css('text-color', 'white')
            ShowNotification('comment removed');
        },
        error: function (xhr, status, error) {
            var notification = $('#notification');
            notification.text('error removing comment');
            notification.css('color', 'red');
            ShowNotification('error removing comment');
            console.error('error removing item:', error);
        }
    });


    function ShowNotification(message) {


        var notification = $('#notification');
        notification.text(message);
        notification.removeClass("hidden");
        setTimeout(function () {
            notification.addClass("hidden");
        }, 3000);
    }
}
