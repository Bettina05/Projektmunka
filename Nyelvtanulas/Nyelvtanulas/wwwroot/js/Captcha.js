var captcha;  // CAPTCHA változó a kód tárolásához

// CAPTCHA kód generálása
function genNewCaptcha() {
    fetch('/Account/GenerateCaptcha')  //Register.cshtml-ből hívjuk meg a GenerateCaptcha metódust
        .then(response => response.json())
        .then(data => {
            captcha = data.captcha;
            // Frissítjük a CAPTCHA szöveget az oldalon
            document.getElementById("captchaTxtArea").value = captcha;
        });
}

// CAPTCHA ellenőrzés
function checkCaptcha() {
    var check = document.getElementById("CaptchaEnter").value;
    if (captcha == check)
    {
        alert("Valid captcha, success");
        document.getElementById("CaptchaEnter").value = "";
    }
    else
    {
        alert("Invalid captcha, try again");
        document.getElementById("CaptchaEnter").value = "";
    }
    genNewCaptcha();
}

// Betöltéskor generálunk egy új CAPTCHA kódot
window.onload = function () {
    genNewCaptcha();
};
