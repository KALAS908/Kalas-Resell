function MakeUser(UserId) {

    $.ajax({
        url: `/UserAccount/MakeUser`,
        type: 'POST',
        data: { UserId: UserId },
        success: function (result) {

            location.reload();
        },
        error: function (xhr, status, error) {
            console.error('error removing item:', error);
        }
    });

}
