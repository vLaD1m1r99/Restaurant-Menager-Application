import { Table } from './Table.js';
import { Products } from './Products.js';
import { Quantity } from './Quantity.js';
export class Restaurant {
  constructor(id, name, m, n) {
    this.id = id;
    this.name = name;
    this.m = m;
    this.n = n;
    this.container = null;
    this.tables = [];
    this.products = [];
    addEventListener('clickTable', (e) => {
      this.HandleClickTable(e.detail.id);
    });
  }
  AddTable(table) {
    this.tables.push(table);
  }
  AddProduct(product) {
    this.products.push(product);
  }
  //Crtam Restoran sa svim elementima
  DrawRestaurant(host) {
    if (!host) throw new Exception("Parent element doesn't exists");
    this.container = document.createElement('div');
    this.container.classList.add('project-container');
    host.appendChild(this.container);
    this.DrawNav(this.container);
    const main = document.createElement('main');
    this.container.appendChild(main);
    const window = document.createElement('div');
    window.classList.add('main-window');
    main.appendChild(window);
    this.AddProducts();
    this.DrawMessage(main);
    this.DrawForm(main);
    this.DrawTables(window);
  }
  //Kreiram prost nav bar
  DrawNav(host) {
    if (!host) throw new Exception("Parent element doesn't exists");
    const nav = document.createElement('nav');
    host.appendChild(nav);
    const logo = document.createElement('div');
    logo.classList.add('logo');
    logo.innerHTML = this.name;
    logo.onclick = () => setTimeout(console.log(this.tables[1]), 1000);
    nav.appendChild(logo);
  }
  //Crtam prozor sa Stolovima
  DrawTables(host) {
    if (!host) throw new Exception("Parent element doesn't exists");
    //Preuzimamo stolove iz baze
    fetch(`https://localhost:5001/Restaurant/GetTables/${this.id}`)
      .then((response) => response.json())
      .then((data) => {
        data.forEach((table) => {
          const t = new Table(table.id);
          this.products.forEach((product) => t.listOfProducts.push(product));
          fetch(
            //Preuzimamo produkte po stolovima iz baze
            `https://localhost:5001/Restaurant/GetQuantitiesFromTable/${table.id}`
          )
            .then((response) => response.json())
            .then((data) => {
              data.forEach((quantity) => {
                const quantity1 = new Quantity(
                  quantity.table.id,
                  quantity.product.name,
                  quantity.quantities
                );
                t.listOfQuantities.push(quantity1);
                if (quantity.quantities != 0) {
                  t.container.style.backgroundColor = 'orangered';
                  t.container.style.opacity = '0.85';
                  t.container.style.color = 'black';
                }
              });
            });

          this.AddTable(t);
        });
        for (let i = 0; i < this.m; i++) {
          const row = document.createElement('div');
          row.classList.add('row');
          for (let j = 0; j < this.n; j++) {
            this.tables[i * (this.n - 1) + i + j].DrawTable(
              row,
              i + j + i * (this.n - 1)
            );
          }
          host.appendChild(row);
        }
      });
  }
  DrawForm(host) {
    if (!host) throw new Exception("Parent element doesn't exists");
    const form = document.createElement('form');
    const tableID = document.createElement('div');
    const selectLabel = document.createElement('label');
    const selectProduct = document.createElement('select');
    const productList = document.createElement('div');
    const productSum = document.createElement('div');
    const btnCheckOut = document.createElement('button');
    const btnAddProduct = document.createElement('button');
    btnCheckOut.type = 'button';
    btnAddProduct.type = 'button';
    tableID.classList.add('table-ID');
    productList.classList.add('product-list');
    productSum.classList.add('product-sum');
    selectProduct.classList.add('select-product');
    form.classList.add('table-form', 'display-none');
    selectLabel.classList.add('select-label');
    selectLabel.innerHTML = 'Select Product: ';
    btnCheckOut.classList.add('form-btn', 'check-out');
    btnAddProduct.classList.add('form-btn', 'add-product');
    btnAddProduct.innerHTML = 'Add Product';
    btnCheckOut.innerHTML = 'Check Out';
    form.appendChild(tableID);
    form.appendChild(selectLabel);
    form.appendChild(selectProduct);
    form.appendChild(btnAddProduct);
    form.appendChild(productList);
    form.appendChild(productSum);
    form.appendChild(btnCheckOut);
    host.appendChild(form);
  }
  DrawMessage(host) {
    const container = document.createElement('div');
    container.classList.add('message-box');
    host.appendChild(container);
  }
  HandleAddProduct(id) {
    const linebreak = document.createElement('br');
    const form = document.querySelector('.table-form');
    const message = document.querySelector('.message-box');
    const productList = form.children[4];
    const select = form.children[2];
    const productSum = form.children[5];
    const name = select.options[select.selectedIndex].text;
    //Povecavamo kvantitet u bazi
    fetch(
      `https://localhost:5001/Restaurant/AddProductToTable/${id}, ${name}`,
      {
        method: 'Post',
        headers: {
          'Content-Type': 'application/json',
        },
        body: null,
      }
    );

    //Povecavamo kvantitet lokalno
    this.tables
      .find((e) => e.id == id)
      .listOfQuantities.find((p) => (p.tableId == id) & (p.name == name))
      .quantity++;
    //Sto nije prazan
    this.tables.find((e) => e.id == id).container.style.backgroundColor =
      'orangered';
    this.tables.find((e) => e.id == id).container.style.opacity = '0.85';
    this.tables.find((e) => e.id == id).container.style.color = 'black';
    // Dodajemo poruku
    this.tables.find((e) => e.id == id).listOfProducts.push();
    message.innerHTML = 'Product Added';
    message.style.display = 'block';
    setTimeout(() => {
      message.style.display = 'none';
    }, 2000);
  }
  HandleCheckOut(id) {
    //Brisemo u bazi
    fetch(`https://localhost:5001/Restaurant/ClearTableOfProducts/${id}`, {
      method: 'Delete',
      headers: {
        'Content-Type': 'application/json',
      },
      body: null,
    });
    //Brisemo lokalno
    this.tables
      .find((e) => e.id == id)
      .listOfQuantities.forEach((q) => {
        q.quantity = 0;
      });
    //Menjamo izgled forme
    const productList = document.querySelector('.product-list');
    productList.innerHTML = ``;
    const productSum = document.querySelector('.product-sum');
    productSum.innerHTML = ``;
    //Sto je prazan
    this.tables.find((e) => e.id == id).container.style.backgroundColor =
      'black';
    this.tables.find((e) => e.id == id).container.style.opacity = '0.8';
    this.tables.find((e) => e.id == id).container.style.color = 'orangered';
    // Dodajemo poruku
    const message = document.querySelector('.message-box');
    message.innerHTML = 'Checked Out';
    message.style.display = 'block';
    setTimeout(() => {
      message.style.display = 'none';
    }, 2000);
  }
  HandleClickTable(id) {
    //Iscrtavamo formu na ekranu
    const form = document.querySelector('.table-form');
    const main = document.querySelector('main');
    const window = document.querySelector('.main-window');
    form.classList.toggle('table-form-activated');
    main.classList.toggle('main-form');
    window.classList.toggle('main-window-form');
    form.firstChild.innerHTML = `Table ${this.tables.indexOf(
      this.tables.find((e) => e.id == id)
    )}`;
    // Ovde prikazujemo sta je na stolu
    this.ShowTable(id);
    //Kreiram promenu kada se klikne na AddProduct
    const addProductBtn = document.querySelector('.add-product');
    addProductBtn.onclick = () => {
      this.HandleAddProduct(id);
      this.ShowTable(id);
    };
    //Kreiram promenu kada se klikne na CheckOut
    const checkOutBtn = document.querySelector('.check-out');
    checkOutBtn.onclick = () => {
      this.HandleCheckOut(id);
      this.ShowTable(id);
    };
  }
  AddProducts() {
    //Preuzimamo produkte iz baze
    fetch(`https://localhost:5001/Restaurant/GetProducts/${this.id}`)
      .then((response) => response.json())
      .then((data) => {
        const selectProduct = document.querySelector('.select-product');
        data.forEach((product, index) => {
          const p = new Products(product.name, product.price);
          this.AddProduct(p);
          const option = document.createElement('option');
          option.setAttribute('value', index);
          option.innerHTML = `${product.name}`;
          selectProduct.appendChild(option);
        });
      });
  }
  ShowTable(id) {
    const linebreak = document.createElement('br');
    const productList = document.querySelector('.product-list');
    const productSum = document.querySelector('.product-sum');
    productList.innerHTML = ``;
    productSum.innerHTML = ``;
    if (this.tables.find((e) => e.id == id).listOfProducts.length != 0) {
      let sum = 0;
      this.tables
        .find((e) => e.id == id)
        .listOfProducts.forEach((element) => {
          const quantity = this.tables
            .find((e) => e.id == id)
            .listOfQuantities.find(
              (p) => (p.tableId == id) & (p.name == element.name)
            );
          if (quantity.quantity != 0) {
            productList.innerHTML += `${element.name} ${quantity.quantity}x ${element.price}`;
            productList.appendChild(linebreak);
            sum += quantity.quantity * element.price;
            productSum.innerHTML = sum;
          }
        });
    } else productSum.innerHTML = ``;
  }
}
