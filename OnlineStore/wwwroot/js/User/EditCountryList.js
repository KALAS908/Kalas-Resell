function EditCountryList(currentCountryId) {
    $(document).ready(function () {
        $.ajax({
            url: '/Country/GetCountries',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var dropdown = $('#countryDropdown');
                dropdown.empty();

                dropdown.append($('<option>').val('').text('Select a country'));

                $.each(data, (index, country) => {
                    dropdown.append($('<option>').val(country.id).text(country.name));
                });
                if (currentCountryId) {
                    dropdown.val(currentCountryId);
                }
            },
            error: function () {
                console.error('Eroare la încărcarea țărilor.');
            }
        });
    });
}
