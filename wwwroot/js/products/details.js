import { getCategoryFromURL, getIdFromURL } from "../utils/commonUtil.js";
import { getStoredData, storeJsonData } from "../utils/localStorageUtil.js";

function renderProductPage() {
  const category = getCategoryFromURL();
  const id = getIdFromURL();
  const products = getStoredData(category);

  createElements(category, id, products);
}

function createElements(category, id, products) {
  const productId = parseInt(id);
  const product = products[productId - 1];
  console.log(product);

  $("#product-img").attr("src", product.imageURL);
  $("#product-name").text(product.name);
  $("#product-details").text(product.description);
  $("#product-price").text(`$${product.price}`);
  $("#ratings-count").text(`${product.ratingCount} ratings`);

  const avgRating = Math.round(product.rating / product.ratingCount);
  renderStarRatings(avgRating);

  const $cartBtn = $("#cart-btn");
  $cartBtn.data("id", productId);
  $cartBtn.data("category", category);

  renderCartButton(product);

  const $favoriteBtn = $("#favorite-btn");
  $favoriteBtn.data("id", productId);
  $favoriteBtn.data("category", category);

  renderFavoriteButton(product);

  $cartBtn.on("click", toggleCart);
  $favoriteBtn.on("click", toggleFavorite);
}

function renderCartButton(product) {
  const $span = $("#cart-icon");
  if (product.isAddedToCart) {
    $span.removeClass("add-to-cart")
         .addClass("remove-from-cart")
         .text("remove_shopping_cart");
    $("#cart-content").text("Remove from Cart");
  } else {
    $span.removeClass("remove-from-cart")
         .addClass("add-to-cart")
         .text("add_shopping_cart");
    $("#cart-content").text("Add to Cart");
  }
}

function renderFavoriteButton(product) {
  const $span = $("#favorite-icon");
  const $container = $("#favorite-container");

  if (product.isFavorite) {
    $container.removeClass("not-favorite-container")
              .addClass("favorite-container");
    $span.addClass("favorite-icon");
    $("#favorite-content").text("Favorite");
  } else {
    $container.removeClass("favorite-container")
              .addClass("not-favorite-container");
    $span.removeClass("favorite-icon");
    $("#favorite-content").text("Add to Favorites");
  }
}

function renderStarRatings(avgRating) {
  for (let i = 1; i <= 5; i++) {
    const $star = $(`#star-${i}`);
    if (i <= avgRating) {
      $star.addClass("filled-star").removeClass("not-filled-star");
    } else {
      $star.removeClass("filled-star").addClass("not-filled-star");
    }
  }
}

function toggleCart(event) {
  const $target = $(event.target).closest("a");
  const id = $target.data("id");
  const category = $target.data("category");

  const products = getStoredData(category);
  const product = products[id - 1];

  product.isAddedToCart = !product.isAddedToCart;

  storeJsonData(category, products);
  renderCartButton(product);
}

function toggleFavorite(event) {
  const $target = $(event.target).closest("a");
  const id = $target.data("id");
  const category = $target.data("category");

  const products = getStoredData(category);
  const product = products[id - 1];

  product.isFavorite = !product.isFavorite;

  storeJsonData(category, products);
  renderFavoriteButton(product);
}

renderProductPage();
