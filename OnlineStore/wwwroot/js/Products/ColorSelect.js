function ColorSelect() {
    $(document).ready(function () {
        $.ajax({
            url: '/Color/GetColors',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var dropdown = $('#colorMultiSelectDropdown');
                dropdown.empty();
                dropdown.append($('<option></option>').val('').text('Select a color'));
                $.each(data, (index, color) => {
                    dropdown.append($('<option></option>').val(color.id).text(color.name));
                });
            },
            error: function () {
                console.error('Eroare');
            }
        });
    });
}