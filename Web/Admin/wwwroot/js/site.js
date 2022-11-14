window.ShowToastr = (type, msg) => {
    switch (type) {
        case 'success':
            toastr.success(msg, 'Success', { timeOut: 5000 });
            break;
        case 'error':
            toastr.error(msg, 'Error', { timeOut: 5000 });
            break;
        case 'warning':
            toastr.warning(msg, 'Warning', { timeOut: 5000 });
            break;
        default:
            toastr.success(msg, 'Operation', { timeOut: 5000 });
            break;
    }
}

window.ShowSwal = (title, type, msg) => {
    Swal.fire(
        title,
        msg,
        type
    )
}

function ShowDeleteConfirmationModal() {
    $("#confirmation-modal").modal('show');
}

function HideDeleteConfirmationModal() {
    $("#confirmation-modal").modal('hide');
}