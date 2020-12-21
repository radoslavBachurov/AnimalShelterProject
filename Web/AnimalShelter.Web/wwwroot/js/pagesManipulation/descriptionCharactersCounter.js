$(function () {
    $("#Description").keyup(function () {
        var charsLeft = $(this).attr("maxlength") - $(this).val().length;
        $("#charsLeft").text(charsLeft + " characters left");
    });
});