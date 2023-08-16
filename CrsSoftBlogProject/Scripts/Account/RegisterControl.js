$("#addContactForm").submit(function (e) {
    e.preventDefault();

    if (firstNameInput.val() === "") {
        alert("Please enter your first name.");
        return;
    }

    if (lastNameInput.val() === "") {
        alert("Please enter your last name.");
        return;
    }

    if (emailInput.val() === "") {
        alert("Please enter your email address.");
        return;
    }

    if (!validateEmail(emailInput.val())) {
        alert("Please enter a valid email address.");
        return;
    }

    if (subjectInput.val() === "") {
        alert("Please enter a subject.");
        return;
    }

    $.ajax({
        url: '@Url.Action("ContactForm", "Contact")',
        type: 'POST',
        data: {
            FirstName: firstNameInput.val(),
            LastName: lastNameInput.val(),
            Email: emailInput.val(),
            Subject: subjectInput.val()
        },
        success: function (result) {
            if (result.success) {
                alert("Your message has been sent successfully.");
            } else {
                alert("An error occurred while sending your message.");
            }
        },
        error: function (xhr, status, error) {
            alert("An error occurred while sending your message.");
        }
    });
});