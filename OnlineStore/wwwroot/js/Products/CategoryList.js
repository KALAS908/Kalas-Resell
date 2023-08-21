function CategoryList() {
    $(document).ready(function () {
       
        function getCategoryList(genderId, typeId) {
           
            $.ajax({
                url: '/Category/GetCategories',
                type: 'GET',
                data: { genderId: genderId, typeId: typeId },
                success: function (result) {
             
                    var categoryDropdown = $('#categoryDropdown');
                    categoryDropdown.empty();
                    categoryDropdown.append($('<option></option>').val('').text('Select a category'));
                    $.each(result, function (index, category) {
                        categoryDropdown.append($('<option></option>').val(category.id).text(category.name));
                    });
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            });
        }
        $('#genderDropdown, #typeDropdown').change(function () {
            var genderId = $('#genderDropdown').val();
            var typeId = $('#typeDropdown').val();
            getCategoryList(genderId, typeId);
        });
    });

}