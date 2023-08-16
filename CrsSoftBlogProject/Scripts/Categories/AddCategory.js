$(document).ready(function () {
    $("#addCategoriesName").submit(function (e) {
        e.preventDefault();

        var data = {{
            CategoriesName: $("#categoriesName").val(),
        };

        $.ajax({
            url: '@Url.Action("AddCategories", "AdminCategories")',
            type: 'POST',
            data: data,
            success: function (result) {
                $("#successMessage").fadeIn().delay(1000).fadeOut();
                $("#categoriesName").val('');

                refreshCategoryList();
            },
            error: function (xhr, status, error) {
                alert("An error occurred while adding the tag.");
            }
        });
    });
    function refreshCategoryList() {
    }
});