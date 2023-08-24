let minValue = document.getElementById("min-value");
let maxValue = document.getElementById("max-value");

function validateRange(minPrice, maxPrice) {
    if (minPrice > maxPrice) {

        let tempValue = maxPrice;
        maxPrice = minPrice;
        minPrice = tempValue;
    }

    minValue.innerHTML = "$" + minPrice;
    maxValue.innerHTML = "$" + maxPrice;
}

const inputElements = document.querySelectorAll("input");

inputElements.forEach((element) => {
    element.addEventListener("change", (e) => {
        let minPrice = parseInt(inputElements[0].value);
        let maxPrice = parseInt(inputElements[1].value);

        validateRange(minPrice, maxPrice);
    });
});

validateRange(inputElements[0].value, inputElements[1].value);

//let minValue = document.getElementById("min-value");
//let maxValue = document.getElementById("max-value");

//function validateRange(minPrice, maxPrice) {
//    if (minPrice > maxPrice) {
//        let tempValue = maxPrice;
//        maxPrice = minPrice;
//        minPrice = tempValue;
//    }

//    minValue.innerHTML = "$" + minPrice;
//    maxValue.innerHTML = "$" + maxPrice;
//}

//const inputElements = document.querySelectorAll(".change"); 

//inputElements.forEach((element) => {
//    element.addEventListener("input", (e) => { 
//        let minPrice = parseInt(inputElements[0].value);
//        let maxPrice = parseInt(inputElements[1].value);

//        validateRange(minPrice, maxPrice);
//    });
//});

