document.addEventListener("DOMContentLoaded", function () {
    
    const minPriceInput = document.querySelector(".min-price");
    const maxPriceInput = document.querySelector(".max-price");
    const applyFilterButton = document.querySelector("#applyFilterButton");

   
    const productsContainer = document.querySelector(".products");

   
    applyFilterButton.addEventListener("click", function () {
        const minPrice = parseInt(minPriceInput.value);
        const maxPrice = parseInt(maxPriceInput.value);

        
        const products = document.querySelectorAll(".product-card");
        products.forEach(function (product) {
            const price = parseInt(product.getAttribute("data-price"));
            if (price >= minPrice && price <= maxPrice) {
                product.style.display = "block";
            } else {
                product.style.display = "none";
            }
        });
    });
});
