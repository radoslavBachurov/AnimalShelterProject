function modalLoad(ev) {
    var form = document.getElementById("changePic");
    form.action = `/User/ChangeProfilePic/${ev.target.id}`;
    $("#modalChange").modal('toggle');
}