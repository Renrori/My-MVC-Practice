function readURL(file, image) {

    if (file.files && file.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            image.attr("src", e.target.result)

        };
        reader.readAsDataURL(file.files[0]);
    }

}