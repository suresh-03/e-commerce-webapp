import { tshirts } from "../data/tShirts.js";
import { shirts } from "../data/shirts.js";
import { belts } from "../data/belts.js";
import { shoes } from "../data/shoes.js";
import { pants } from "../data/pants.js";
import { watches } from "../data/watches.js";

import { getStoredData, storeJsonData, removeAllData } from "../utils/localStorageutil.js";

// Use jQuery's document ready
$(document).ready(function () {
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

  $.each(dataMap, function (key, value) {
    if (!getStoredData(key)) {
      storeJsonData(key, value);
    }
  });
}

