function CountryForEdit() {


    $(document).ready(function () {
        $.ajax({
            url: '/Country/GetCountries',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                const countryId = $("#countryIdHidden").val();
                console.log(countryId);
                var dropdown = $('#countryDropdown');
                dropdown.empty();

                $.each(data, (index, country) => {
                    dropdown.append($('<option>').val(country.id).text(country.name));

                    if (country.id == countryId) {
                        dropdown.val(country.id);
                    }
                });
            },
            error: function () {
                console.error('Eroare la încărcarea țărilor.');
            }
        });
    });
}
