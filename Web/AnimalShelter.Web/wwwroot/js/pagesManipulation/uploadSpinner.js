function showSpinner(formId) {
    var formToAtach = document.getElementById(formId);

    let divSpinner = document.createElement('div');
    divSpinner.classList.add("fa-5x");
    divSpinner.classList.add("text-center");
    divSpinner.setAttribute("id", "spinner");

    let spinnerEl = document.createElement('i');
    spinnerEl.classList.add("fas");
    spinnerEl.classList.add("fa-spinner");
    spinnerEl.classList.add("fa-spin");

    divSpinner.appendChild(spinnerEl);
    formToAtach.appendChild(divSpinner);
}

function removeSpinner() {
    console.log()
    var spinner = document.getElementById("spinner");
    spinner.remove();
}