export function isValidFullName(fullName) {
    const regex = /^[A-Za-z\s]{2,}$/;
    return regex.test(fullName);
}

export function isValidPassword(password) {
    const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$/;
    return regex.test(password);
}

export function isValidPhone(phoneNo) {
    const regex = /^\d{10}$/;
    return regex.test(phoneNo);
}

export function isValidEmail(email) {
    const regex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return regex.test(email);
}

export function showAlert(alertMessage, message, color = "red") {
    alertMessage.stop(true, true).css({ color: color, display: "block" }).text(message);
    setTimeout(() => {
        alertMessage.fadeOut(300, () => alertMessage.text("").show());
    }, 2500);
}
