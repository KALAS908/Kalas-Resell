function AddToWishList() {


    function AddProductToWishList(productId) {
        $.ajax({
            url: '/Product/AddProductToWishList',
            type: 'POST',
            data: { productId: productId },
            success: function (result) {
                var notification = $('#notification');
                notification.css('background-color', 'green');
                ShowNotification('Product added to wish list');
            },
            error: function (xhr, status, error) {
                var notification = $('#notification');
                notification.css('background-color', 'red');
                ShowNotification('Something went wrong!');
                console.error('error adding product to cart:', error);
            }
        });
    }

    function ShowNotification(message) {


        var notification = $('#notification');
        notification.text(message);
        notification.removeClass("hidden");
        setTimeout(function () {
            notification.addClass("hidden");
        }, 3000);
    }

    var productId = $('#productIdHidden').val();
    var isUserAuthenticated = $('#isUserAuthenticatedHidden').val();

    if (isUserAuthenticated === '') {
        window.location.href = '/UserAccount/Login';
    }
    else { AddProductToWishList(productId); }

}
