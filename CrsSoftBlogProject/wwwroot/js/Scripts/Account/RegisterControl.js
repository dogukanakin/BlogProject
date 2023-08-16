$(document).ready(function () {
    const registerButton = $("#registerButton");
    const usernameInput = $("#username");
    const passwordInput = $("#password");

    registerButton.click(function (e) {
        const username = usernameInput.val();
        const password = passwordInput.val();

        if (!username || !password) {
            e.preventDefault();
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Please fill in both username and password.'
            });
        }
    });

    function isValidEmail(email) {
        const emailRegex = /^([\w-\.]+)@@((\[[0-9]{1, 3}\.[0-9]{1, 3}\.[0-9]{1, 3}\.)|(([\w-]+\.)+))([a-zA-Z]{2, 4}|[0-9]{1, 3})(\]?)$/;
        return emailRegex.test(email);
    }
});
