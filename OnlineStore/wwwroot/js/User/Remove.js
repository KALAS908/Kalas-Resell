function RemoveUser(UserId) {

    $.ajax({
        url: `/UserAccount/DeleteByAdmin`,
        type: 'POST',
        data: { UserId: UserId },
        success: function (result) {

            $(`#item-${UserId}`).remove();
        },
        error: function (xhr, status, error) {
            console.error('error removing item:', error);
        }
    });

}
