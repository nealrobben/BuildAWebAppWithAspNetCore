import { Component } from "@angular/core";
import { DataService } from "../shared/dataService";
import { Observable } from "rxjs";
import { Product, Product as IProduct } from "../shared/product";


@Component({
    selector: "product-list",
    templateUrl: "productlist.component.html",
    styleUrls: ["productList.component.css"]
})
export class ProductList implements OnInit {
    public products: Product[];

    constructor(private data: DataService) {
        this.products = data.products;
    }

    ngOnInit() {
        this.data.loadProducts()
            .subscribe(() => this.products = this.data.products);
    }

    addProduct(product: Product) {
        this.data.addToOrder(product);
    }
}