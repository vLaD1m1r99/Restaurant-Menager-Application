import { Products } from './Products.js';
export class Table {
  constructor(id) {
    this.id = id;
    this.container = null;
    this.listOfProducts = [];
    this.listOfQuantities = [];
  }
  DrawTable(host, id) {
    if (!host) throw new Exception("Parent element doesn't exists");
    this.container = document.createElement('button');
    this.container.classList.add('table-container');
    this.container.innerHTML = `Table ${id}`;
    host.appendChild(this.container);
    //Kreiram promenu kada se klikne na sto
    var clickTable = new CustomEvent('clickTable', {
      detail: {
        id: this.id,
      },
    });
    this.container.onclick = () => {
      dispatchEvent(clickTable);
    };
  }
  AddProduct(product) {
    this.listOfProducts.push(product);
  }
}
