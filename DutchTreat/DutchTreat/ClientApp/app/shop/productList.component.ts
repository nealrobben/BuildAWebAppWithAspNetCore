import { Component } from "@angular/core";
import { DataService } from "../shared/dataService";
import { Observable } from "rxjs";
import { Product } from "../shared/product";


@Component({
    selector: "product-list",
    templateUrl: "productlist.component.html",
    styleUrls: ["productList.component.css"]
})
export class ProductList implements OnInit {

    constructor(private data: DataService) {
        this.products = data.products;
    }

    public products: Product[] = [];

    ngOnInit(): void {
        this.data.loadProducts().subscribe(success => {
            if (success) {
                this.products = this.data.products;
            }
        });
    }
}