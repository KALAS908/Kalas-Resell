function BrandFilterApplied() {
    var selectedBrands = [];
    var selectedMeasures = [];

    function getSelectedBrands() {
        selectedBrands = [];
        $(' #brandList input[type=checkbox]').each(function () {
            if (this.checked) {
                selectedBrands.push(this.value);
            }
        });
        return selectedBrands;
    }
    function getSelectedMeasures() {
        selectedMeasures = [];
        $('#measureList input[type=checkbox]').each(function () {
            if (this.checked) {
                selectedMeasures.push(this.value);
            }
        });
        return selectedMeasures;
    }
    function redirectToShoesView(genderId, searchString, page, selectedBrands, maxPrice, selectedMeasures) {

        var url = window.location.pathname;
        page = 1;
        url = url + '?&genderId=' + genderId + '&searchString=' + searchString
            + '&page=' + page + '&selectedBrands=' + selectedBrands
            + '&maxPrice=' + maxPrice + '&selectedMeasures=' + selectedMeasures;
        window.location.href = url;
    }

    $('#applyFilterButton').click(function () {
        var genderId = $('#genderIdHidden').val();
        var searchString = $('#searchStringHidden').val();
        var maxPrice = document.getElementById("id1").value;
        maxPrice = parseInt(maxPrice);
        var page = 1;
        selectedBrands = getSelectedBrands();
        selectedBrands = selectedBrands.join(',');

        selectedMeasures = getSelectedMeasures();
        selectedMeasures = selectedMeasures.join(',');
        redirectToShoesView(genderId, searchString, page, selectedBrands, maxPrice, selectedMeasures);
    });
}






