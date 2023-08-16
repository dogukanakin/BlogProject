$(document).ready(function () {
    $("#addContactForm").submit(function (e) {
        e.preventDefault();

        var data = {
            FirstName: $("#firstname").val(),
            LastName: $("#lastname").val(),
            Email: $("#email").val(),
            Subject: $("#subject").val()
        };

        // Güncellenmiş URL'yi burada kullanın
        var actionUrl = $(this).data("action-url");

        $.ajax({
            url: actionUrl,
            type: 'POST',
            data: data,
            success: function (result) {
                $("#successMessage").fadeIn().delay(2000).fadeOut();
                $("#firstname").val('');
                $("#lastname").val('');
                $("#email").val('');
                $("#subject").val('');

                refreshTagList();
            },
            error: function (xhr, status, error) {
                alert("An error occurred while submitting the contact form.");
            }
        });
    });
    // Call refreshTagList function here
    refreshTagList();
});

function refreshTagList() {
}