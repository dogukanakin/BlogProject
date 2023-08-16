$(document).ready(function () {
    $("#addTagForm").submit(function (e) {
        e.preventDefault();

        var data = {
            Name: $("#name").val(),
            DisplayName: $("#displayName").val()
        };

        $.ajax({
            url: '@Url.Action("Add", "AdminTags")',
            type: 'POST',
            data: data,
            success: function (result) {
                // Show the success message
                $("#successMessage").fadeIn().delay(1000).fadeOut();

                $("#name").val('');
                $("#displayName").val('');

                refreshTagList();
            },
            error: function (xhr, status, error) {
                alert("An error occurred while adding the tag.");
            }
        });
    });
    function refreshTagList() {
    }
});