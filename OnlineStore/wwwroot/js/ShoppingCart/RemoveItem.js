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
            console.log('item removed successfully.');
          
        },
        error: function (xhr, status, error) {
            console.log('error removing item:', error);
        }
    });

}
