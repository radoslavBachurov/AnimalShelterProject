function replyAction(replyId, parentId, userNickName) {
    $("#CommentForm textarea[name='Text']").val(`<<${userNickName}>>  `);
    $("#CommentForm input[name='AnswerTo']").val(userNickName);

    var atachTo;
    if (parentId) {
        atachTo = parentId;
    }
    else {
        atachTo = replyId;
    }

    $("#CommentForm input[name='ParentId']").val(atachTo);

    $([document.documentElement, document.body]).animate({
        scrollTop: $("#CommentForm").offset().top - 200
    }, 1000);
}