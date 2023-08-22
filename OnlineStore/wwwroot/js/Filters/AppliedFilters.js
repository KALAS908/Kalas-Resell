document.addEventListener("DOMContentLoaded", function () {

    const minPriceInput = document.querySelector(".min-price");
    const maxPriceInput = document.querySelector(".max-price");
    const applyFilterButton = document.querySelector("#applyFilterButton");



    function getSelectedBrands() {
        selectedBrands = [];
        $('input[type=checkbox]').each(function () {
            if (this.checked) {
                selectedBrands.push(this.value);
            }
        });
        return selectedBrands;
    }



    const productsContainer = document.querySelector(".products");


    applyFilterButton.addEventListener("click", function () {
        const minPrice = parseInt(minPriceInput.value);
        const maxPrice = parseInt(maxPriceInput.value);

        let selectedBrands = getSelectedBrands();

        const products = document.querySelectorAll(".product-card");
        products.forEach(function (product) {
            const price = parseInt(product.getAttribute("data-price"));
            const brandId = parseInt(product.getAttribute("data-brand-id"));
            if (price >= minPrice && price <= maxPrice) {
                if (selectedBrands.includes(brandId.toString()) || selectedBrands.length == 0) {
                    product.style.display = "block";
                }
                else {
                    product.style.display = "none";
                }
            } else {
                product.style.display = "none";
            }
        });
    });
});
