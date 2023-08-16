$(document).ready(function () {
    $("#togglePassword").click(function () {
        const password = $("#password");
        const type = password.attr("type") === "password" ? "text" : "password";
        password.attr("type", type);

        $(this).toggleClass("bi-eye");
    });
});

document.addEventListener("DOMContentLoaded", function () {
    const loginButton = document.getElementById("loginButton");
    const usernameInput = document.getElementById("username");
    const passwordInput = document.getElementById("password");

    function checkLoginInput(e) {
        const username = usernameInput.value;
        const password = passwordInput.value;

        if (!username || !password) {
            e.preventDefault();
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Please fill in both username and password.'
            });
        }
    }

    loginButton.addEventListener("click", checkLoginInput);
});
