function MakeAdmin(UserId) {

    $.ajax({
        url: `/UserAccount/MakeAdmin`,
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
