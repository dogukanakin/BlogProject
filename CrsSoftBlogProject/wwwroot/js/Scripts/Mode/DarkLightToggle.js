$(document).ready(function () {
    const darkModeEnabled = localStorage.getItem('darkModeEnabled') === 'true';

    function applyDarkMode(isDarkMode) {
        if (isDarkMode) {
            $("body").addClass("dark-mode");
            $(".fa-moon-o").hide();
            $(".fa-sun-o").show();
        } else {
            $("body").removeClass("dark-mode");
            $(".fa-moon-o").show();
            $(".fa-sun-o").hide();
        }
    }

    applyDarkMode(darkModeEnabled);

    $("#darkModeCheckbox").change(function () {
        const isDarkMode = $(this).is(":checked");
        applyDarkMode(isDarkMode);
        localStorage.setItem('darkModeEnabled', isDarkMode);
    });
});