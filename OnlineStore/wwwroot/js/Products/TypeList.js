function TypeList() {
    $(document).ready(function () {
        $.ajax({
            url: '/Type/GetTypes',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var dropdown = $('#typeDropdown');
                dropdown.empty();
                dropdown.append($('<option></option>').val('0').text('Select the type of product'));
                $.each(data, (index, type) => {
                    dropdown.append($('<option></option>').val(type.id).text(type.typeName));
                });
            },
            error: function () {
                console.error('Eroare la incarcarea datelor.');
            }
        });
    });
}