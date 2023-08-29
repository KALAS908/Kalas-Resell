function AddToCart() {


    function AddProductToCart(productId, measureId) {
        $.ajax({
            url: '/Product/AddProductToCart',
            type: 'POST',
            data: { productId: productId, measureId: measureId },
            success: function (result) {
                ShowNotification('Product added to cart');
            },
            error: function (xhr, status, error) {
                console.error('error adding product to cart:', error);
            }
        });
    }

    function ShowNotification(message, measureId) {


        var notification = $('#notification');

        if (measureId === '') {
            notification.css('background-color', 'red');
        }
        else {
            notification.css('background-color', 'green');
        }

        notification.text(message);
        notification.removeClass("hidden");
        setTimeout(function () {
            notification.addClass("hidden");
        }, 3000);
    }




    var productId = $('#productIdHidden').val();
    var measureId = $('#productMeasureDropdown').val();
    var isUserAuthenticated = $('#isUserAuthenticatedHidden').val();



    if (isUserAuthenticated === '') {
        window.location.href = '/UserAccount/Login';
    }

    if (measureId === '') {

        ShowNotification('Please select a measure', measureId);
        return;
    }
    else { AddProductToCart(productId, measureId); }

}
