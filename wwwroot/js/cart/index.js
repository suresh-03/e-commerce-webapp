// Function to retrieve all localStorage data and send it via a POST request
function sendLocalStorageData(api) {
    // Retrieve all localStorage data
    const localStorageData = {};
    for (let i = 0; i < localStorage.length; i++) {
        const key = localStorage.key(i);
        const value = localStorage.getItem(key);
        try {
            localStorageData[key] = JSON.parse(value);
        }
        catch (e) {
            console.error("Error parsing the data");
        }
    }

    // Send the data using a POST request
    $.ajax({
        url: api, // URL of the controller action
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(localStorageData), // Convert the data to JSON
        success: function (response) {
            console.log(localStorageData);
            console.log("Server response:", response);
        },
        error: function (xhr, status, error) {
            console.error("Error sending localStorage data:", error);
        }
    });
}