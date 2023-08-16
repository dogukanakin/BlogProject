    $(document).ready(function () {
        // Update functionality
        $("#Edit").submit(function (e) {
            e.preventDefault();

            var idToEdit = $("#id").val();
            var pageTitle = $("#pageTitle").val();
            var pageContent = $("#PageContent").val();
            var description = $("#description").val();
            var featuredImageUrl = $("#featuredImageUrl").val();

            $.ajax({
                url: '@Url.Action("EditPost", "BlogPost")',
                type: 'POST',
                data: {
                    Id: idToEdit,
                    PageTitle: pageTitle,
                    PageContent: pageContent,
                    Description: description,
                    FeaturedImageUrl: featuredImageUrl
                },
                success: function (result) {
                    $("#successMessage").fadeIn().delay(1000).fadeOut();

                    refreshTagList();
                },
                error: function (xhr, status, error) {
                    alert("An error occurred while editing the blog post.");
                }
            });
        });

    // Delete functionality
    $("#btnDelete").click(function (e) {
        e.preventDefault();

    var idToDelete = $("#id").val();

    $.ajax({
        url: '@Url.Action("DeletePost", "BlogPost")',
    type: 'POST',
    data: {id: idToDelete },
    success: function (result) {
        $("#successMessage").fadeIn().delay(1000).fadeOut();
    window.location.href = '@Url.Action("BlogList", "BlogPost")';
                },
    error: function (xhr, status, error) {
        alert("An error occurred while deleting the blog post.");
                }
            });
        });

     function refreshTagList() {
         }
});
    