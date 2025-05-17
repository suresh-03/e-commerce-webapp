function toggleSidebar() {
  $("#sidebar").toggleClass("active");
  $("#overlay").toggleClass("active");
}

// Function to close the sidebar if clicked outside
function closeSidebarOnOutsideClick(event) {
  const $sidebar = $("#sidebar");
  const $toggleButton = $("#categories-menu");

  const isClickInsideSidebar = $sidebar[0].contains(event.target);
  const isClickOnToggleButton = $toggleButton[0].contains(event.target);

  if (!isClickInsideSidebar && !isClickOnToggleButton) {
    $sidebar.removeClass("active");
    removeOverlay();
  }
}

// Function to initialize event listeners
function initializeSidebar() {
  // Toggle sidebar on menu button click
  $("#categories-menu").on("click", function (e) {
    e.stopPropagation(); // Prevent event from bubbling to document
    toggleSidebar();
  });

  // Close sidebar when clicking outside
  $(document).on("click", function (e) {
    closeSidebarOnOutsideClick(e);
  });

  // Close sidebar on overlay click
  $("#overlay").on("click", function () {
    $("#sidebar").removeClass("active");
    removeOverlay();
  });

  // Close sidebar on close button click
  $("#closeSidebar").on("click", function () {
    $("#sidebar").removeClass("active");
    removeOverlay();
  });
}

// Remove overlay
function removeOverlay() {
  $("#overlay").removeClass("active");
}

// Initialize everything
initializeSidebar();


function handleSearch(api) {
    $("#search-form").submit(function (event) {
        event.preventDefault(); // Prevent the default form submission

        
        var rawQuery = $("#search-bar").val(); // Get the search input value
        if (rawQuery.length > 300) {
            alert("Search Query is too long");
            return;
        }

        query = filterQuery(rawQuery); // Filter the query using the filterQuery function

        $.ajax({
            url: api,
            type: "GET",
            data: { query }, // Send the search query to the server
            success: function (partialViewResult) {
                $("main").html(partialViewResult); // Replace the body with the partial view result
                $("#search-result-feedback").css("display", "block");
                $("#search-result-feedback-content").text("Search results for \"" + rawQuery +"\"");
            },
            error: function (xhr, status, error) {
                // Handle any errors here
                errorMessage = xhr.responseText;
                alert(errorMessage);
                console.error("Search request failed:", error);
            }
        });
    });
}

function filterQuery(query) {
    if (!query || typeof query !== 'string') return "";

    const stopwords = new Set([
        "a", "an", "and", "are", "as", "at", "be", "but", "by", "for", "if", "in", "into",
        "is", "it", "no", "not", "of", "on", "or", "such", "that", "the", "their", "then",
        "there", "these", "they", "this", "to", "was", "will", "with", "you", "your", "from",
        "up", "down", "can", "could", "should", "would", "have", "has", "had", "do", "does",
        "did", "just", "so", "than", "too", "very", "also", "about", "after", "again", "before",
        "because", "been", "being", "both", "during", "each", "few", "he", "her", "here", "him",
        "his", "how", "i", "me", "my", "our", "ours", "she", "some", "we", "what", "when", "where",
        "which", "who", "whom", "why", "all", "any", "more", "most", "other", "over", "under",
        "once", "new", "trendy", "latest", "stylish", "fashionable", "classic", "elegant", "cute", "cool",
        "perfect", "pretty", "beautiful", "casual", "formal", "designer", "comfy", "comfortable", "simple",
        "modern", "hot", "musthave", "premium", "basic", "chic",
        "men", "women", "mens", "womens", "boys", "girls", "unisex", "kids", "ladies", "gentleman",
        "clothes", "clothing", "wear", "apparel", "outfit", "dress", "top", "bottoms", "accessory",
        "item", "stuff", "piece", "thing",
        "brand", "branded", "fashion", "collection", "wear", "edition", "line", "original", "pack", "set", "size", "packof",
        "best", "offer", "sale", "discount", "top", "hot", "trending", "deal", "limited", "exclusive", "genuine","color"
    ]);


    // 1. Lowercase
    query = query.toLowerCase();

    // 2. Remove numbers
    query = query.replace(/\d+/g, "");

    // 3. Remove special characters
    query = query.replace(/[^a-z\s]/g, "");

    // 4. Reduce repeated characters (e.g., "coooool" -> "cool")
    query = query.replace(/(\w)\1{2,}/g, "$1");

    // 5. Split into words and filter stopwords
    const words = query.split(/\s+/).filter(word => word && !stopwords.has(word));

    // 6. Remove repeated words (keep only the first occurrence)
    const seen = new Set();
    const uniqueWords = words.filter(word => {
        if (seen.has(word)) return false;
        seen.add(word);
        return true;
    });

    return uniqueWords.join(" ");
}


function renderCartCount(api) {

    $.ajax({
        url: api,
        type: "GET",
        success: function (data) {
            $("#cart-count").text(data.cartItemsCount); // Update the cart count in the UI
            console.log(data);
        },
        error: function (xhr, status, error) {
            console.error("Error fetching cart count:", error);
        }
    });
}



