function SingleSelectDropDown(firstListId, secondListId) {

    let firstList = document.getElementById(firstListId);
    let secondList = document.getElementById(secondListId);

    var updateOptions = function () {


        var selectedValue = firstList.value;
        if (selectedValue) {


            fetch(`/CategoryByType ? parentId=${selectedValue}`, { method: "get" })
                .then(response => response.json())
                .then(response => {
                    secondList.innerHTML = '<option value="">Select</option>';
                    for (var i = 0; i < response.length; i++) {
                        var option = document.createElement("option");
                        option.value = response[i].id;
                        option.text = response[i].name;
                        secondList.appendChild(option);
                    }
                })
                .catch(error => console.error(error));
    }
    else {
        secondList.innerHTML = '<option value="">Select</option>';
        }
    };
   
    updateOptions();
    fisrtList.onchange = updateOptions;
}
