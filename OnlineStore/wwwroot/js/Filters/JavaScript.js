$(document).ready(function () {
    var selectedBrands = $('#selectedBrandsHidden').val().split(',');
    $('input[type=checkbox]').each(function () {
        if (selectedBrands.includes(this.value)) {
            this.checked = true;
        }
        console.log(this.value);
    }
    );
});


