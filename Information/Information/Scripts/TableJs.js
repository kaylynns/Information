$(document).ready(function () {
    //鼠标悬浮
    $(document).on("mouseover", "#cc", function () {
        $(this).addClass("over");
    })
    //鼠标移出
    $(document).on("mouseout", "#cc", function () {
        $(this).removeClass("over");
    })
})