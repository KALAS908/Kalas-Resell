var slideCol = document.getElementById("id1");
var y = document.getElementById("f");
y.innerHTML = slideCol.value;

slideCol.oninput = function () {
    y.innerHTML = this.value;
}

