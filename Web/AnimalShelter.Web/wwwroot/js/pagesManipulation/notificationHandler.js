function clearEvent() {
    var form = document.getElementById("clearFormAFT");
    var token = form.getElementsByTagName("input")[0].value;

    var uri = `/api/ClearNotification`;

    fetch(uri, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'X-CSRF-TOKEN': token
        }
    })
        .then(responce => responce.json())
        .then(data => hideCounter());
}

function hideCounter() {
    var counter = document.getElementById("noti_Counter");

    counter.style.display = "none";
}