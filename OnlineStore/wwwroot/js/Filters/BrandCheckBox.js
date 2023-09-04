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
                    var checkbox = $('<input type="checkbox" />').val(brand.id).prop('id', 'brand_' + brand.id).prop('data-brand-id', brand.id);
                    var label = $('<label></label>').text(brand.name).attr('for', 'brand_' + brand.id);

                    listItem.append(checkbox);
                    listItem.append(label);
                    brandList.append(listItem);
                });
      
                selectBrands();
            },
            error: function () {
                console.error('Error loading brands.');
            }
        });
    });

    function selectBrands() {
        var selectedBrands = $('#selectedBrandsHidden').val();
        var selectedBrandsArray = selectedBrands.split(',');
        selectedBrandsArray.forEach(function (brandId) {
            var id = parseInt(brandId);
            $('#brand_' + id).prop('checked', true);
        });
    }
}
