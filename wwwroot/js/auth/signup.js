function handleSignup(api) { 
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

        alertMessage.text(""); // Clear previous messages

        // Validation with early return
        if (!isValidFullName(fullName)) {
            alertMessage.text("Please enter a valid full name (at least 2 characters).").css("color", "red");
            return;
        }

        if (!isValidEmail(email)) {
            alertMessage.text("Please enter a valid email address.").css("color", "red");
            return;
        }

        if (!isValidPassword(password)) {
            alertMessage.text("Password must be at least 8 characters long and contain an uppercase letter, a lowercase letter, a number, and a special character.").css("color", "red");
            return;
        }

        if (!isValidPhone(phoneNo)) {
            alertMessage.text("Please enter a valid 10-digit phone number.").css("color", "red");
            return;
        }

        const bodyData = {
            FullName: fullName,
            Email: email,
            Password: password,
            RoleType: 2, // Assuming 2 is for 'User'
            Phone: phoneNo
        };

        $.ajax({
            type: "POST",
            url: api,
            data: JSON.stringify(bodyData),
            contentType: "application/json",
            success: function (response) {
                console.log(JSON.stringify(response));
                if (response.status === "success") { 
                    window.location.href = response.redirectURL;
                } else {
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

function isValidFullName(fullName) {
    const regex = /^[A-Za-z\s]{2,}$/;
    return regex.test(fullName);
}

function isValidPassword(password) {
    const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$/;
    return regex.test(password);
}

function isValidPhone(phoneNo) {
    const regex = /^\d{10}$/;
    return regex.test(phoneNo);
}

function isValidEmail(email) {
    const regex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return regex.test(email);
}