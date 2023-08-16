function AddToCart() {
    function AddProductToCart(productId, measureId) {
        //$.post("/Product/AddProductToCart", (data) => { console.log(data); debugger;; })
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

                console.log('product added to cart successfully.');
            },
            error: function (xhr, status, error) {
                console.log('error adding product to cart:', error);
            }
        });
    }


    var productId = $('#productIdHidden').val();
    var measureId = $('#productMeasureDropdown').val();
    AddProductToCart(productId, measureId);

}
