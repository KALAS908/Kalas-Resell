function GenderList() {
    $(document).ready(function () {
        $.ajax({
            url: '/Gender/GetGenders',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var dropdown = $('#genderDropdown');
                dropdown.empty();
                dropdown.append($('<option></option>').val('').text('Selectează un gen'));
                $.each(data, (index, gender) => {
                    dropdown.append($('<option></option>').val(gender.id).text(gender.name));
                });
            },
            error: function () {
                console.error('Erorr');
            }
        });
    });
}