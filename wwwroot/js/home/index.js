import { tshirts } from "../data/tShirts.js";
import { shirts } from "../data/shirts.js";
import { belts } from "../data/belts.js";
import { shoes } from "../data/shoes.js";
import { pants } from "../data/pants.js";
import { watches } from "../data/watches.js";

import { getStoredData, storeJsonData, removeAllData } from "../utils/localStorageutil.js";

window.addEventListener("DOMContentLoaded", () => {
  populateAllData();
  // removeAllData();
});

// Store all initial data
function populateAllData() {
  const dataMap = {
    tshirts,
    shirts,
    belts,
    shoes,
    pants,
    watches,
  };

  for (const [key, value] of Object.entries(dataMap)) {
    if (!getStoredData(key)) {
      storeJsonData(key, value);
    }
  }
}
