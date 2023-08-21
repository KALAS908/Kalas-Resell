function AddToCart() {
    function AddProductToCart(productId, measureId) {
        $.ajax({
            url:'/Product/AddProductToCart',
            type: 'POST',
            data: { productId: productId, measureId: measureId },
            success: function (result) {
                
                const notification = document.getElementById('notification');
                notification.classList.remove('hidden');
                setTimeout(() => {
                    notification.classList.add('hidden');
                }, 3000);
            },
            error: function (xhr, status, error) {
                console.error('error adding product to cart:', error);
            }
        });
    }


    var productId = $('#productIdHidden').val();
    var measureId = $('#productMeasureDropdown').val();
    AddProductToCart(productId, measureId);

}
