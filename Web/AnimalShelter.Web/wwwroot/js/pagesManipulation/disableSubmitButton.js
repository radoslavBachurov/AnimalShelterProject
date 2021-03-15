function checkForErrors() {
    $(document).on('invalid-form.validate', 'form', function () {
        var button = $(this).find(':submit');
        setTimeout(function () {
            button.removeAttr('disabled');
        }, 1);
    });
    $(document).on('submit', 'form', function () {
        var button = $(this).find(':submit');
        setTimeout(function () {
            button.attr('disabled', 'disabled');
        }, 0);
    });

    var errors = document.getElementsByClassName("field-validation-error");

    if (errors.length == 0) {
        removeSpinner();
        showSpinner("uploadInfo");
    }
}