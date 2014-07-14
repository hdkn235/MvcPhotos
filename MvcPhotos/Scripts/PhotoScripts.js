$(function () {

    $("input[data-autocomplete-source]").each(function () {
        var target = $(this);
        target.autocomplete({
            source:
                target.attr("data-autocomplete-source")
        });
    });

    $("#btnSearch").click(function () {
        var target = $(this);
        location.href = target.attr("data-search-source")
            + "?searchString="
            + $("#searchString").val();
    });
});