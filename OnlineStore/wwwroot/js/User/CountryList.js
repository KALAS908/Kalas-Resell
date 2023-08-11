function CountryList() {
    $(document).ready(function () {
        $.ajax({
            url: '/Country/GetCountries',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var dropdown = $('#countryDropdown');
                dropdown.empty();

                $.each(data, (index, country) => {
                    dropdown.append($('<option>').val(country.id).text(country.name));
                });
            },
            error: function () {
                console.error('Eroare la încărcarea țărilor.');
            }
        });
    });
}