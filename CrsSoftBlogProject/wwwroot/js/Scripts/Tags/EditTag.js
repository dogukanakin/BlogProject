$(document).ready(function () {
    $("#editTag").submit(function (e) {
        e.preventDefault();

        var data = {
            Id: $("#id").val(),
            Name: $("#name").val(),
            DisplayName: $("#displayName").val()
        };

        $.ajax({
            url: $(this).attr("action"),
            type: 'POST',
            data: data,
            success: function (result) {
                $("#successMessage").fadeIn().delay(1000).fadeOut();
            },
            error: function (xhr, status, error) {
                alert("An error occurred while saving changes.");
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
                    $.post("/AdminTags/Delete", { id: id }, function (response) {
                        if (response.success) {
                            Swal.fire("Tag deleted successfully!", "", "success");
                            window.location.href = '/AdminTags/List';
                        } else {
                            window.location.href = '/AdminTags/List';
                        }
                    });
                }
            });
        });
    });
});