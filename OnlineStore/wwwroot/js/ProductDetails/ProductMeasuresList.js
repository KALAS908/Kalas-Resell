function ProductMeasuresList() {
    $(document).ready(function () {

        function getProductMeasures(productId) {
            $.ajax({
                url: '/Measure/GetProductMeasures',
                type: 'GET',
                data: { productId: productId },
                success: function (result) {
                    var measureDropdown = $('#productMeasureDropdown');
                    measureDropdown.empty();

                    if (result.length !== 0) {
                        measureDropdown.append($('<option></option>').val('').text('Select a measure'));
                        $.each(result, function (index, measure) {
                            measureDropdown.append($('<option></option>').val(measure.measureId).text(measure.measureName));
                        });
                    } else {
                        measureDropdown.append($('<option></option>').val('').text('No measures available'));
                    }
                },
                error: function (xhr, status, error) {
                    console.log('error');
                }
            });
        }
        function loadProductMeasures() {
            var productId = $('#productIdHidden').val();
            getProductMeasures(productId);
        }

        loadProductMeasures();
      
    });
}