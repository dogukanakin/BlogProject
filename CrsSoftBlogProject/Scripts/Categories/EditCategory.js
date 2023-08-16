$(document).ready(function () {
    // Update functionality
    $("#Edit").submit(function (e) {
        e.preventDefault();

        var idToEdit = $("#id").val();
        var categoriesName = $("#categoriesName").val();

        $.ajax({
            url: '@Url.Action("CategoriesEdit", "AdminCategories")',
            type: 'POST',
            data: {
                Id: idToEdit,
                CategoriesName: categoriesName,
            },
            success: function (result) {
                $("#successMessage").fadeIn().delay(1000).fadeOut();

                refreshCategoryList();
            },
            error: function (xhr, status, error) {
                alert("An error occurred while editing the category.");
            }
        });
    });

    $("#btnDelete").click(function (e) {
        e.preventDefault();
        // Delete functionality

        var idToDelete = $("#id").val();

        $.ajax({
            url: '@Url.Action("DeleteCategory", "AdminCategories")',
            type: 'POST',
            data: { id: idToDelete },
            success: function (result) {
                $("#successMessage").fadeIn().delay(2000).fadeOut();

                window.location.href = '@Url.Action("CategoriesEdit", "AdminCategories")';
            },
            error: function (xhr, status, error) {
                alert("An error occurred while deleting the tag.");
            }
        });
    });

    function refreshCategoryList() {
    }
});