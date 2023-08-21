function DecreaseQuantity(productId, measure) {

    $.ajax({
        url: '/ShoppingCart/DecreaseQuantity',
        type: 'POST',
        data: { productId: productId, measure: measure },
        success: function (result) {

            location.reload();
        },
        error: function (xhr, status, error) {
            console.error('error updating cart item:', error);
        }
    });
}


