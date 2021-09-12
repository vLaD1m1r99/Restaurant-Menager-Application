import { Restaurant } from './Restaurant.js';
import { Table } from './Table.js';
//Ne znam kako da napravim foru za id, ali videcu
fetch('https://localhost:5001/Restaurant/GetRestaurant/8012')
  .then((response) => response.json())
  .then((data) => {
    new Restaurant(
      data.id,
      data.name,
      data.numberOfRows,
      data.numberOfColumns
    ).DrawRestaurant(document.body);
  });
