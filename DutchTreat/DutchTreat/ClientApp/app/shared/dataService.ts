
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";
import { Observable } from "rxjs"
import "rxjs/add/operator/map";

@Injectable()
export class DataService {

    constructor(private http: HttpClient) {
    }

    public products = [];

    loadProducts(): Observable<boolean> {
        this.http.get("/api/products")
            .map((data: any[]) => {
                this.products = data;
                return true;
            });
    }

}