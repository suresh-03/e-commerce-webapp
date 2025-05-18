$(document).on("click", function (event) {
    closeDropDownMenu(event);
});

function closeDropDownMenu(event) {
    const $dropDownMenu = $("#dropdown-menu");

    const isClickInsideDropDownMenu = $dropDownMenu[0].contains(event.target);


    if (!isClickInsideDropDownMenu) {
        $dropDownMenu.addClass("hidden");
    }
}
function toggleDropdown() {
    this.event.stopPropagation(); // Prevent the click event from bubbling up to the document
    $("#dropdown-menu").toggleClass("hidden");
}

function updatePriceDisplay(rangeInput) {
    const min = rangeInput.min;
    const max = rangeInput.value;
    if (max == rangeInput.max) {
        document.getElementById("price-range-display").textContent = `₹${min} - ₹${max}+`;

    }
    else {
        document.getElementById("price-range-display").textContent = `₹${min} - ₹${max}`;
    }
}

function clearPrice() {
    const range = document.getElementById("priceRange");
    range.value = range.max;
    updatePriceDisplay(range);
}

function clearSelection(name) {
    const inputs = document.querySelectorAll(`[name="${name}"]`);
    inputs.forEach(input => {
        if (input.type === "radio" || input.tagName === "SELECT") {
            input.checked = false;
            input.selectedIndex = 0;
        }
    });
}



function clearAll() {
    clearSelection("sort");
    clearSelection("color");
    clearSelection("category");
    clearSelection("brand");
    clearPrice();
}


function handleApplyFilter() {
    var selectedSort = $('input[name="sort"]:checked').val() || '';
    var selectedColor = $('select[name="color"]').val();
    var selectedBrand = $('select[name="brand"]').val();
    var minPrice = parseFloat($('#priceRange').attr('min'));
    var maxPrice = parseFloat($('#priceRange').val()); // current slider value
    const params = new URLSearchParams(window.location.search);
    var category = params.get('category') || '';
    const bodyData = {
        category,
        sort: selectedSort,
        color: selectedColor,
        brand: selectedBrand,
        minPrice,
        maxPrice
    }

    alert(JSON.stringify(bodyData));

    $.ajax({
        url: "/api/product/filter",
        type: "POST",
        data: JSON.stringify(bodyData),
        contentType: "application/json",
        success: function (partialViewResult) {
            $("main").html(partialViewResult);
        },
        error: function (xhr) {
            console.error(xhr.responseText);
        }
    });
}

