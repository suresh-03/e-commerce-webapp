import { getStoredData } from "../utils/localStorageUtil.js";
import { getCategoryFromURL } from "../utils/commonUtil.js";

function renderProductsPage() {
  const category = getCategoryFromURL();
  const products = getStoredData(category);

  products.forEach((product) => {
    createElements(product, category);
  });
}

function createElements(product, category) {
  const $img = $("<img>").attr("src", product.imageURL);
  const $productImage = $("<div>").addClass("product-image").append($img);

  // ------------------------------------------------------

  const $h4 = $("<h4>").text(product.name);
  const $p1 = $("<p>").text(product.description);
  const $p2 = $("<p>").addClass("product-price").text(`$${product.price}`);

  const avgRating = Math.round(product.rating / product.ratingCount);
  const $rating = $("<span>").addClass("rating").text(`${avgRating}★`);
  setBackgroundForRating(avgRating, $rating);

  const $ratingCount = $("<span>")
    .addClass("rating-count")
    .text(`${product.ratingCount} ratings`);

  const $ratings = $("<div>")
    .addClass("ratings")
    .append($rating)
    .append($ratingCount);

  const $productInformation = $("<div>")
    .addClass("product-information")
    .append($h4, $p1, $p2, $ratings);

  // -------------------------------------------------------

  const $a = $("<a>")
    .attr("href", `product/details?category=${category}&id=${product.id}`)
    .append($productImage, $productInformation);

  // -------------------------------------------------------

  const $productGrid = $("<div>").addClass("product-grid").append($a);

  // -------------------------------------------------------

  $("#product-inner-container").append($productGrid);
}

function setBackgroundForRating(rating, $ratingElement) {
  switch (rating) {
    case 1:
      $ratingElement.css({ backgroundColor: "tomato", color: "white" });
      break;
    case 2:
      $ratingElement.css({ backgroundColor: "orange", color: "white" });
      break;
    case 3:
      $ratingElement.css({ backgroundColor: "yellow" });
      break;
    case 4:
      $ratingElement.css({ backgroundColor: "blue", color: "white" });
      break;
    case 5:
      $ratingElement.css({ backgroundColor: "green", color: "white" });
      break;
    default:
      $ratingElement.css({ backgroundColor: "green", color: "white" });
      $ratingElement.text("5★");
  }
}

renderProductsPage();
