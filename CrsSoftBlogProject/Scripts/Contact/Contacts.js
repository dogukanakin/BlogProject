$(document).ready(function () {
    $("#addContactForm").submit(function (e) {
        e.preventDefault();

        var data = {
            FirstName: $("#fname").val(),
            LastName: $("#lname").val(),
            Email: $("#email").val(),
            Subject: $("#subject").val()
        };

        $.ajax({
            url: '@Url.Action("ContactForm", "Contact")',
            type: 'POST',
            data: data,
            success: function (result) {
                $("#successMessage").fadeIn().delay(2000).fadeOut();
                $("#fname").val('');
                $("#lname").val('');
                $("#email").val('');
                $("#subject").val('');
            },
            error: function (xhr, status, error) {
                alert("An error occurred while submitting the contact form.");
            }
        });
    });
});