$(document).ready(function () {
    $("#addCategoriesForm").submit(function (e) {
        e.preventDefault();

        var data = {
            CategoriesName: $("#categoriesName").val(),
        };

        var actionUrl = $(this).data("action-url");

        $.ajax({
            url: actionUrl,
            type: 'POST',
            data: data,
            success: function (result) {
                $("#successMessage").fadeIn().delay(1000).fadeOut();
                $("#categoriesName").val('');

                refreshCategoryList();
            },
            error: function (xhr, status, error) {
                $("#error").html(xhr.responseText);
            }
        });
    });

    function refreshCategoryList() {
        // code to refresh the category list
    }
});
