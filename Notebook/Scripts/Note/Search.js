$(function () {
    var $minDateInput = $("#mindate"),
        $maxDateInput = $("#maxdate"),
        $searchAuthors = $(".form-authors .search-choice"),
        $searchLikers = $(".form-likes .search-choice"),
        $authorsInput = $(".authors-input"),
        $likersInput = $(".likers-input"),
        $searchForm = $(".search-form"),
        $selectUsers = $(".select-users");
    

    $(document).on("click", ".button-submit", function (e) {
        var $searchAuthors = $(".form-authors .search-choice"),
            $searchLikers = $(".form-likers .search-choice"),
            authors = "", likers = "";

        $searchAuthors.each(function () {
            if (authors != "") {
                authors += ";";
            }
            authors += $(this).text();
        });
        $authorsInput.attr("value", authors);

        $searchLikers.each(function () {
            if (likers != "") {
                likers += ";";
            }
            likers += $(this).text();
        });
        $likersInput.attr("value", likers);

        $searchForm.submit();
    });

    $selectUsers.chosen();

    if (minDateIsNull) {
        $minDateInput.val("");
    }
    else {
        var value = $minDateInput.val();
        value = value.substring(0, value.indexOf(' '));
        $minDateInput.val(value);
    }
    if (maxDateIsNull) {
        $maxDateInput.val("");
    }
    else {
        var value = $maxDateInput.val();
        value = value.substring(0, value.indexOf(' '));
        $maxDateInput.val(value);
    }
});