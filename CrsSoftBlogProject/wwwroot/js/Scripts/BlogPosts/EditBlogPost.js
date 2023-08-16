$(document).ready(function () {
    // Update functionality
    $("#edit").submit(function (e) {
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
                    $.post("/BlogPost/DeletePost", { id: id }, function (response) {
                        if (response.success) {
                            Swal.fire("Tag deleted successfully!", "", "success");
                            window.location.href = '/BlogPost/BlogList';
                        } else {
                            window.location.href = '/BlogPost/BlogList';
                        }
                    });
                }
            });
        });
    });
});




var editor = new FroalaEditor('#PageContent', {
    imageUploadURL: '/api/images'
});

$(document).ready(function () {
    const featuredUploadElement = $('#featuredImageUpload');
    const featuredImageUrlElement = $('#featuredImageUrl');
    const featuredImageDisplayElement = $('#featuredImageDisplay');

    featuredUploadElement.on('change', function (e) {
        console.log(e.target.files[0]);

        let data = new FormData();
        data.append('file', e.target.files[0]);

        $.ajax({
            url: '/api/images',
            type: 'POST',
            data: data,
            processData: false,
            contentType: false,
            success: function (result) {
                featuredImageUrlElement.val(result.link);
                featuredImageDisplayElement.attr('src', result.link);
                featuredImageDisplayElement.css('display', 'block');
            },
            error: function () {
                console.error('An error occurred while uploading the image.');
            }
        });
    });
});;