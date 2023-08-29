function BrandCheckBox() {
    $(document).ready(function () {
        $.ajax({
            url: '/Brand/GetBrands',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var brandList = $('#brandList');
                brandList.empty();

                $.each(data, (index, brand) => {
                    var listItem = $('<li></li>');
                    var checkbox = $('<input type="checkbox" d/>').val(brand.id).prop('id', 'brand_' + brand.id).prop('data-brand-id', brand.id);
                    var label = $('<label></label>').text(brand.name).attr('for', 'brand_' + brand.id);

                    listItem.append(checkbox);
                    listItem.append(label);
                    brandList.append(listItem);
                });
            },
            error: function () {
                console.error('Eroare la încărcarea brandurilor.');
            }
        });
    });
}
