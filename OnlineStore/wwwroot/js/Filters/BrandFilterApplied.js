$(document).ready(function () {

    $('#getSelectedBrands').on('click', function () {
        var selectedBrands = [];
        $('input[type=checkbox]').each(function () {

            if (this.checked) {
                selectedBrands.push(this.value);
            }

        });

        const products = document.querySelectorAll(".product-card");
        products.forEach(function (product) {

            if (selectedBrands.length == 0) {
                product.style.display = "block";
            }
            else {
                const brandId = parseInt(product.getAttribute("data-brand-id"));
                if (selectedBrands.includes(brandId.toString())  ) {
                    product.style.display = "block";
                } else {
                    product.style.display = "none";
                }
            }

        });
    });
});
