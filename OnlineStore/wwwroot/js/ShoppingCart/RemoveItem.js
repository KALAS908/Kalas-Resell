function RemoveItem(productId, measure) {

    $.ajax({
        url: `/ShoppingCart/RemoveItem`,
        type: 'POST',
        data: { productId: productId, measure: measure },
        success: function (result) {
         
            $(`#item-${productId}-${measure}`).remove();
            location.reload();
        },
        error: function (xhr, status, error) {

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
