function handleCartExists(api,variantId) {

    $.ajax({
        url: api,
        type: 'GET',
        data: { variantId },
        success: function (response) {
            if (response.itemExists) {
                renderRemoveFromCart();
                return;
            }
            else {
                renderAddToCart();
                return;
            }
        },
        error: function (xhr, status, error) {
            if (xhr.status == 401) {
                window.location.href = "/auth/signin";
            }
            console.error("Failed to check cart:", error);
            alert(xhr.responseText);
        }
    });
}





function handleAddToCart(api) {

    $("#add-cart-btn").click(function (event) {
        event.preventDefault(); // Prevent the default form submission

        button = $(this);
        var variantId = button.data("variant-id"); // Get the variant ID from the button's data attribute)

        $.ajax({
            url: api,
            type: 'GET',
            data: { variantId },
            success: function (response) {
                console.log("Added to cart:", response);
                alert("Product Added to the Cart");
                renderRemoveFromCart();
                renderCartCount('/api/cart/count');
            },
            error: function (xhr, status, error) {
                console.error("Failed to add to cart:", error);
                if (xhr.status == 401) {
                    window.location.href = "/auth/signin";
                }
                alert(xhr.responseText);
            }
        });

    });

}


function handleRemoveFromCart(api) {

    $("#remove-cart-btn").click(function (event) {
        event.preventDefault(); // Prevent the default form submission

        button = $(this);
        var variantId = button.data("variant-id");

        $.ajax({
            url: api,
            type: 'GET',
            data: { variantId },
            success: function (response) {
                console.log("Removed from Cart", response);
                alert("Product Removed from the Cart");
                renderAddToCart();
                renderCartCount('/api/cart/count');
            },
            error: function (xhr, status, error) {
                if (xhr.status == 401) {
                    window.location.href = "/auth/signin";
                }
                console.error("Failed to remove from cart:", error);
                alert(xhr.responseText);
            }
        });

    });
   
}

function renderAddToCart() {
    $("#add-cart-btn").css("display", "block");
    $("#remove-cart-btn").css("display", "none");

}

function renderRemoveFromCart() {
    $("#add-cart-btn").css("display", "none");
    $("#remove-cart-btn").css("display", "block");

}