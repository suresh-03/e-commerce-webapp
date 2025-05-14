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
