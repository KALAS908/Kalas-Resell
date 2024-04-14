function RemoveFromWishList(productId) {

    $.ajax({
        url: `/Product/RemoveFromWishList`,
        type: 'POST',
        data: { productId: productId },
        success: function (result) {

            var notification = $('#notification');
            notification.css('background-color', 'green');
            $(`#item-${productId}`).remove();
            ShowNotification('item removed from wish list');
        },
        error: function (xhr, status, error) {
            var notification = $('#notification');
            notification.css('background-color', 'red');
            ShowNotification('error removing item')
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
