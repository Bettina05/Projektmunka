var captcha, chars;
function genNewCaptcha() {
    chars = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    captcha = chars[Math.floor(Math.random() * chars.length)];
    for (var i = 0; i < 6; i++) {
        captcha = captcha + chars[Math.floor(Math.random() * chars.length)];
    }
    form1.text.value = captcha;
}
function checkCaptcha() {
    var check = document.getElementById("CaptchaEnter").value;
    if (captcha == check) {
        alert("Valid captcha, succes")
        document.getElementById("CaptchaEnter").value = "";
    }
    else {
        alert("Invalid captcha, try again")
        document.getElementById("CaptchaEnter").value = "";
    }
    genNewCaptcha();
}
document.getElementById("checkbtn").addEventListener("click", function () {
    let inputValue = document.getElementById("CaptchaEnter").value;

    fetch("/Account/ValidateCaptcha", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ captchaInput: inputValue })
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert("Sikeres CAPTCHA!");
            } else {
                alert("Hibás CAPTCHA! Próbáld újra.");
            }
        });
});
