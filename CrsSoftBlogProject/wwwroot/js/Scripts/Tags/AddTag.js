$(document).ready(function () {
    $("#addTagForm").submit(function (e) {
        e.preventDefault();

        var data = {
            Name: $("#name").val(),
            DisplayName: $("#displayName").val()
        };

        var actionUrl = $(this).data("action-url");

        $.ajax({
            url: actionUrl,
            type: 'POST',
            data: data,
            success: function (result) {
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
