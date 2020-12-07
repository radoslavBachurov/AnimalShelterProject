function LikeController(postId, postIsLiked) {

    var token = $("#likesForm input[name=__RequestVerificationToken]").val();

    var inputModel = { PostId: postId, IsLiked: postIsLiked };

    $.ajax({
        url: "/api/PetProfile",
        type: "POST",
        data: JSON.stringify(inputModel),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {

            if (data.likes === -1) {
                data.likes = 0;
            }

            var likeElement = document.getElementById("like");
            var likeIcon = likeElement.firstElementChild;
            likeElement.innerHTML = '';

            likeElement.setAttribute("onClick", `LikeController(${postId},${data.isLiked})`);

            var likeCount = document.getElementById("likesCount");
            likeCount.innerHTML = data.likes;

            if (data.isLiked) {

                likeIcon.classList.remove("material-icons-favorite");
                likeIcon.classList.add("material-icons-thumb_down");
                likeElement.appendChild(likeIcon)
                likeElement.innerHTML += "Dislike";
            }
            else {

                likeIcon.classList.remove("material-icons-thumb_down");
                likeIcon.classList.add("material-icons-favorite");
                likeElement.appendChild(likeIcon)
                likeElement.innerHTML += "Like";
            }
        }
    })
}