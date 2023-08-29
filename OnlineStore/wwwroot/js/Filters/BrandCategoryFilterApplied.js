function BrandCategoryFilterApplied() {
    var selectedBrands = [];
    function getSelectedBrands() {
        selectedBrands = [];
        $('input[type=checkbox]').each(function () {
            if (this.checked) {
                selectedBrands.push(this.value);
            }
        });
        return selectedBrands;
    }
    function redirectToShoesView(categoryId, searchString, page, selectedBrands) {

        var url = window.location.pathname;
        url = url + '?&categoryId=' + categoryId + '&searchString=' + searchString + '&page=' + page + '&selectedBrands=' + selectedBrands;
        window.location.href = url;
    }

    $('#applyFilterButton').click(function () {
        var categoryId = $('#categoryIdHidden').val();
        var searchString = $('#searchStringHidden').val();
        var page = 1;
        selectedBrands = getSelectedBrands();
        selectedBrands = selectedBrands.join(',');
        redirectToShoesView(categoryId, searchString, page, selectedBrands);
    });
}