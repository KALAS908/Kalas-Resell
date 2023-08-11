function PreviewImages() {
    document.querySelector('input[type="file"]').addEventListener('change', function (event) {

        var files = event.target.files;


        document.getElementById('preview-images').innerHTML = '';


        for (var i = 0; i < files.length; i++) {
            var file = files[i];


            var img = document.createElement('img');
            img.src = URL.createObjectURL(file);
            img.style.width = '200px';
            img.style.height = 'auto';


            document.getElementById('preview-images').appendChild(img);
        }
    });
}