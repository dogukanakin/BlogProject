$(document).ready(function () {
    $("#createPost").submit(function (e) {
        e.preventDefault();

        var data = {
            PageTitle: $("#pageTitle").val(),
            PageContent: $("#PageContent").val(),
            Description: $("#description").val(),
            Author: $("#author").val(),
            FeaturedImageUrl: $("#featuredImageUrl").val(),
            SelectedTag: $("#SelectedTag").val(),
            CategoriesName: $("#CategoriesName").val(),
            FeaturedImageUpload: $("#featuredImageUpload").val(),
        };

        $.ajax({
            url: '@Url.Action("CreatePost", "BlogPost")',
            type: 'POST',
            data: data,
            success: function (result) {
                $("#successMessage").fadeIn().delay(2000).fadeOut();
                clearFormFields();
            },
            error: function (xhr, status, error) {
                alert("An error occurred while creating the blog post.");
            }
        });
    });

    var editor = new FroalaEditor('#PageContent', {
        imageUploadURL: '/api/images'
    });

    const featuredUploadElement = document.getElementById('featuredImageUpload');
    const featuredImageUrlElement = document.getElementById('featuredImageUrl');
    const featuredImageDisplayElement = document.getElementById('featuredImageDisplay');

    featuredUploadElement.addEventListener('change', uploadFeaturedImage);

    async function uploadFeaturedImage(e) {
        let data = new FormData();
        data.append('file', e.target.files[0]);

        try {
            const response = await fetch('/api/images', {
                method: 'POST',
                headers: {
                    'Accept': '*/*',
                },
                body: data
            });

            if (response.ok) {
                const result = await response.json();
                featuredImageUrlElement.value = result.link;
                featuredImageDisplayElement.src = result.link;
                featuredImageDisplayElement.style.display = 'block';
            } else {
                console.error('Error uploading featured image');
            }
        } catch (error) {
            console.error('An error occurred during the upload', error);
        }
    }

    function clearFormFields() {
        $("#pageTitle").val('');
        $("#PageContent").val('');
        $("#description").val('');
        $("#featuredImageUrl").val('');
        $("#SelectedTag").val('');
        $("#CategoriesName").val('');
        $("#featuredImageUpload").val('');
    }
});
