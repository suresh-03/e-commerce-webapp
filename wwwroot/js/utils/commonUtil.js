export function getCategoryFromURL() {
  const urlParams = new URLSearchParams(window.location.search);
  const category = urlParams.get('category');
  console.log(category);
  return category;
}

export function getIdFromURL() {
  const urlParams = new URLSearchParams(window.location.search);
  const id = urlParams.get('id');
  console.log(id);
  return id;
}