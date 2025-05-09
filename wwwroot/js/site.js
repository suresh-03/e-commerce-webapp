function toggleSidebar() {
  const sidebar = document.getElementById("sidebar");
  const overlay = document.getElementById("overlay");

  sidebar.classList.toggle("active");
  overlay.classList.toggle("active");
}

// Function to close the sidebar if clicked outside
function closeSidebarOnOutsideClick(event) {
  const sidebar = document.getElementById("sidebar");
  const toggleButton = document.getElementById("categories-menu");

  const isClickInsideSidebar = sidebar.contains(event.target);
  const isClickOnToggleButton = toggleButton.contains(event.target);

  // Close sidebar if click is outside both the sidebar and the toggle button
  if (!isClickInsideSidebar && !isClickOnToggleButton) {
    sidebar.classList.remove("active");
    removeOverlay();
  }
}

// Dynamically populate sidebar menu
function populateMenu() {
  const menuItems = document.getElementById("menuItems");
  const menuData = ["T-Shirts","Shoes","Pants","Shirts","Watches","Belts"];

  menuData.forEach(item => {
    const li = document.createElement("li");
    const a = document.createElement("a");
    a.href="#";
    a.textContent = item;
    a.style.color = "#222";
    li.appendChild(a);
    menuItems.appendChild(li);
  });
}

// Function to initialize event listeners
function initializeSidebar() {
  // Add event listener for the toggle button
  document.getElementById("categories-menu").addEventListener("click", (e) => {
    e.stopPropagation(); // Prevent click from reaching the document
    toggleSidebar();
  });

  // Add event listener to close sidebar if clicked outside
  document.addEventListener("click", closeSidebarOnOutsideClick);

  // Add event listener to close sidebar if clicked on overlay
  document.getElementById("overlay").addEventListener("click", () => {
    document.getElementById("sidebar").classList.remove("active");
    removeOverlay();
  });

  // Add event listener to close sidebar using close button inside the sidebar
  document.getElementById("closeSidebar").addEventListener("click", () => {
    document.getElementById("sidebar").classList.remove("active");
    removeOverlay();
  });
}


// remove overlay
function removeOverlay(){
  const overlay = document.getElementById("overlay");
  overlay.classList.remove("active");
}

// Initialize everything
initializeSidebar();
populateMenu();


