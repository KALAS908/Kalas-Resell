function BrandList() {
    $(document).ready(function () {
        $.ajax({
            url: '/Brand/GetBrands',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var dropdown = $('#brandDropdown');
                dropdown.empty();
                dropdown.append($('<option></option>').val('0').text('Select a brand'));
                $.each(data, (index, brand) => {
                    dropdown.append($('<option></option>').val(brand.id).text(brand.name));
                });
            },
            error: function () {
                console.error('Eroare la încărcarea țărilor.');
            }
        });
    });
}