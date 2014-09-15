$(function () {
    $(document).on("click", ".note-like", function () {
        var $source = $(this),
            id = $source.attr("data-id"),
            $target = $("#" + id);

        $.ajax({
            type: "POST",
            url: likeUrl,
            data: { userId: currentUserId, noteId: id },
            success: function (data) {
                $target.replaceWith(data);
            }
        });
    });

    $(document).on("click", ".note-edit", function () {
        var $source = $(this),
            id = $source.attr("data-id"),
            $content = $("#content-" + id);

        if ($source.hasClass("edit-mode")) {
            $.ajax({
                type: "POST",
                url: saveUrl,
                data: { id: id, content: $content.children().val() },
                success: function (data) {
                    $content.html(data);
                }
            });
        }
        else {
            $.ajax({
                type: "POST",
                url: editUrl,
                data: { id: id },
                success: function (data) {
                    $content.html(data);
                }
            });
        }
        $source.toggleClass("edit-mode");
    });

    $(document).on("click", ".note-delete", function () {
        var $source = $(this),
            id = $source.attr("data-id");

        bootbox.confirm("Are you sure?", function (result) {
            if (result) {
                deleteNote(id);
            }
        });

    });

    function deleteNote(id) {
        $.ajax({
            type: "POST",
            url: deleteUrl,
            data: { id: id },
            success: function (data) {

                $("#" + id).replaceWith(data);
            }
        });
    }
});