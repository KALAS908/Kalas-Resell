function RemoveItem(productId, measure) {
    
    $.ajax({
        /*url: `/ShoppingCart/RemoveItem?` + $.param({
            productId: productId, measure: measure
        }),*/
        url: `/ShoppingCart/RemoveItem`,
        type: 'POST',
        data: { productId: productId, measure: measure },
        success: function (result) {
            $(`#item-${productId}-${measure}`).remove();;
        },
        error: function (xhr, status, error) {
            console.error('error removing item:', error);
        }
    });

}
