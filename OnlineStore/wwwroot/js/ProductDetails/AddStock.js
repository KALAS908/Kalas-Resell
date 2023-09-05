function AddStock() {
    var quantity = document.querySelector('input[name="quantity"]').value;
    var measureId = document.getElementById("measureDropdown").value;
    var productId = document.getElementById("productIdHidden").value;

    $.ajax({
        url: '/Product/AddStock',
        type: 'POST',
        data: { quantity: quantity, measureId: measureId, productId: productId },
        success: function (result) {
            ProductMeasuresList();
            var notification = $('#notification');
            notification.css('background-color', 'green');
            ShowNotification('Product added');
        },
        error: function (xhr, status, error) {
            var notification = $('#notification');
            notification.css('background-color', 'red');
            ShowNotification('Something went wrong!');
        }

    });
    function ShowNotification(message) {


        var notification = $('#notification');
        notification.text(message);
        notification.removeClass("hidden");
        setTimeout(function () {
            notification.addClass("hidden");
        }, 3000);
    }
    closeForm();

}