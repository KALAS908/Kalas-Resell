function MeasureCheckBox() {
    $(document).ready(function () {
        $.ajax({
            url: '/Measure/GetAllMeasures',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var brandList = $('#measureList');
                brandList.empty();

                $.each(data, (index, measure) => {
                    var listItem = $('<li></li>');
                    var checkbox = $('<input type="checkbox" />').val(measure.id).prop('id', 'measure_' + measure.id).prop('data-measure-id', measure.id);
                    var label = $('<label></label>').text(measure.name).attr('for', 'measure_' + measure.id);

                    listItem.append(checkbox);
                    listItem.append(label);
                    brandList.append(listItem);
                });
                selectMeasures();
            },
            error: function () {
                console.error('Error loading measures.');
            }
        });
    });

    function selectMeasures() {
        var selectMeasures = $('#selectedMeasuresHidden').val();
        var selectMeasuresArray = selectMeasures.split(',');
        selectMeasuresArray.forEach(function (measureId) {
            var id = parseInt(measureId);
            $('#measure_' + id).prop('checked', true);
        });
    }
}