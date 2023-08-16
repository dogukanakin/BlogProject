$(document).ready(function () {
    $("#editCategory").submit(function (e) {
        e.preventDefault();

        var data = {
            Id: $("#id").val(),
            CategoriesName: $("#categoriesName").val() // Use a colon instead of an equal sign
        };

        $.ajax({
            url: $(this).attr("action"),
            type: 'POST',
            data: data,
            success: function (result) {
                $("#successMessage").fadeIn().delay(1000).fadeOut();

                refreshCategoryList();
            },
            error: function (xhr, status, error) {
                alert("An error occurred while editing the category.");
            }
        });
    });

    $(document).ready(function () {
        $("#btnDelete").click(function (event) {
            event.preventDefault();

            var id = $("#id").val();

            Swal.fire({
                title: "Are you sure you want to delete this tag?",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Yes, delete it!",
                cancelButtonText: "Cancel",
            }).then((result) => {
                if (result.value) {
                    $.post("/AdminCategories/DeleteCategory", { id: id }, function (response) {
                        if (response.success) {
                            Swal.fire("Tag deleted successfully!", "", "success");
                            window.location.href = '/AdminCategories/CategoriesList';
                        } else {
                            window.location.href = '/AdminCategories/CategoriesList';
                        }
                    });
                }
            });
        });
    });

});