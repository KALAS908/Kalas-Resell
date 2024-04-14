function MeasureList() {
    $(document).ready(function () {
        function getMeasureList(typeId) {
            $.ajax({
                url: '/Measure/GetMeasures',
                type: 'GET',
                data: { typeId: typeId },
                success: function (result) {
                    var measureDropdown = $('#measureDropdown');
                    measureDropdown.empty();
                    measureDropdown.append($('<option></option>').val('').text('Select a measure'));
                    $.each(result, function (index, measure) {
                        measureDropdown.append($('<option></option>').val(measure.id).text(measure.name));
                    });
                },
                error: function (xhr, status, error) {
                    console.log('error');
                }
            });
        }

        function loadMeasures() {
            var typeId = $('#typeIdHidden').val();
            getMeasureList(typeId);
        }

        loadMeasures();
    });
}
