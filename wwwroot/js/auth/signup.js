import { isValidFullName, isValidEmail, isValidPassword, isValidPhone, showAlert } from "../utils/commonUtil.js";
export function handleSignup(api) { 
    $("#phoneNo").on("input", function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

    $("#signup-form").submit(function (event) {
        event.preventDefault();

        const fullName = $("#fullName").val();
        const email = $("#email").val();
        const password = $("#password").val();
        const phoneNo = $("#phoneNo").val();
        const alertMessage = $("#alertMessage");


        // Validation with early return
        if (!isValidFullName(fullName)) {
            showAlert(alertMessage, "Please enter a valid full name (at least 2 characters).", "red");
            return;
        }

        if (!isValidEmail(email)) {
            showAlert(alertMessage, "Please enter a valid email address.", "red");
            return;
        }

        if (!isValidPassword(password)) {
            showAlert(alertMessage, "Password must be at least 8 characters long and contain an uppercase letter, a lowercase letter, a number, and a special character.", "red");
            return;
        }

        if (!isValidPhone(phoneNo)) {
            showAlert(alertMessage, "Please enter a valid 10-digit phone number.", "red");
            return;
        }

        const bodyData = {
            FullName: fullName,
            Email: email,
            Password: password,
            Phone: phoneNo
        };

        var token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            type: "POST",
            url: api,
            data: JSON.stringify(bodyData),
            contentType: "application/json",
            headers: {
                "RequestVerificationToken": token
            },
            success: function (response) {
                console.log(JSON.stringify(response));
                if (response.status === "success") { 
                    window.location.href = response.redirectUrl;
                } else {
                    showAlert(alertMessage, response.message, "red");
                }
            },
            error: function (xhr) {
                let errorMessage = "An error occurred. Please try again.";

                try {
                    // Try to parse JSON response from the server
                    const responseJson = JSON.parse(xhr.responseText);
                    if (responseJson.message) {
                        errorMessage = responseJson.message;
                    }
                } catch (e) {
                    // If responseText is not JSON, fall back to generic
                    console.error("Failed to parse error message:", e);
                }
                showAlert(alertMessage, errorMessage, "red");
            }
        });
    });
}

