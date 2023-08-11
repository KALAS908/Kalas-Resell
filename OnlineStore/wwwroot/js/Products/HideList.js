function HideList() {
    $(document).ready(function () {

        $("#CategoryList").hide();
        $("#MeasureList").hide();


        $("#genderDropdown, #typeDropdown").change(function () {
            var selectedGender = $("#genderDropdown").val();
            var selectedType = $("#typeDropdown").val();


            if (selectedGender !== '' && selectedType !== '') {

                $("#CategoryList").show();
                $("#MeasureList").show();
            } else {

                $("#CategoryList").hide();
                $("#MeasureList").hide();
            }
        });
    });
}