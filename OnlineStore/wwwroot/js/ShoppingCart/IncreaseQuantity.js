﻿function IncreaseQuantity(productId, measure) {

    $.ajax({
        url: '/ShoppingCart/IncreaseQuantity',
        type: 'POST',
        data: { productId: productId, measure: measure },
        success: function (result) {

            console.log('cart item updated successfully.');
           location.reload();
        },
        error: function (xhr, status, error) {
            console.log('error updating cart item:', error);
        }
    });
}


