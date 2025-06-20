window.SweetAlertHelper = {
    showAlert: function (title, text, icon) {
        Swal.fire({
            title: title,
            text, text,
            icon: icon,
            confirmationButtonText: 'Ok'
        });
    },
    showConfirmation: async function (title, text) {
        const response = await Swal.fire({
            title: title,
            text: text,
            icon: 'warning',
            showCancelButton: true,
            confirmationButtonText: 'Yes',
            cancelButtonText: 'No'
        });

        return response.isConfirmed;
    }
}