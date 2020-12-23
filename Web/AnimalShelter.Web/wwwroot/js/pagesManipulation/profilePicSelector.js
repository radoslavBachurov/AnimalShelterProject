function modalLoad(ev) {
    var form = document.getElementById("changePic");
    form.action = `/User/ChangeProfilePic/${ev.target.id}`;
    $("#modalChange").modal('toggle');
}

function modalDelete(ev) {
    var form = document.getElementById("deletePic");
    form.action = `/User/DeletePic/${ev.target.id}`;
    $("#modalDelete").modal('toggle');
}