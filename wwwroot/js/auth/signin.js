import { isValidEmail, isValidPassword } from "../utils/commonUtil.js";
export function handleSignin(api) {

    $("#signin-form").submit(function (event) {
        event.preventDefault();

        const email = $("#email").val();
        const password = $("#password").val();

        const alertMessage = $("#alertMessage");

        alertMessage.text(""); // Clear previous messages

        if (!isValidEmail(email)) {
            alertMessage.text("Please enter a valid email address.").css("color", "red");
            return;
        }

        if (!isValidPassword(password)) {
            alertMessage.text("Password must be at least 8 characters long and contain an uppercase letter, a lowercase letter, a number, and a special character.").css("color", "red");
            return;
        }

        const bodyData = {
            Email: email,
            Password: password
        }

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
                }
                else {
                    alertMessage.text(response.message).css("color", "red");
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

                alertMessage.text(errorMessage).css("color", "red");
            }
        });


    });

}