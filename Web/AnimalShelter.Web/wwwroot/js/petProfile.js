var likeStatus = false;

function LikeController(postId, postIsLiked) {

    var inputModel = { PostId: postId, IsLiked: postIsLiked };

    $.ajax({
        url: "/api/PetProfile",
        type: "POST",
        data: JSON.stringify(inputModel),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {

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