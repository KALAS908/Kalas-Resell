$(document).ready(function () {
    function loadComments() {
        var productId = $('#productIdHidden').val();
        $.ajax({
            url: '/Product/GetComments',
            type: 'GET',
            data: { productId: productId },
            success: function (data) {
                $('#comments-container').empty();
                $('#comments-container').html(data);
            }
        });
    }
    loadComments();


    $('#add-comment-btn').click(function () {

        var commentText = $('#comment-text').val();
        if (commentText == '') {
            alert("Please enter comment.");
            return false;
        }

        var rating = $('#Rating').val();
        if (rating == 0) {
            alert("Please rate this product.");
            return false;
        }
        var productId = $('#productIdHidden').val();

        rating = parseInt(rating);
        var userId = $('#currentUserIdHidden').val();

        $.ajax({
            url: '/Product/AddComment',
            type: 'POST',
            data: {

                text: commentText,
                productId: productId,
                userId: userId,
                rating: rating
            },
            success: function (data) {
                loadComments();
                $('#comment-text').val('');
                $('#Rating').val('0');
                for (var i = 1; i <= 5; i++) {
                    $("#Rate" + i).attr('class', 'starFade');
                }
                console.log('succes');
            }
        });
    });



});