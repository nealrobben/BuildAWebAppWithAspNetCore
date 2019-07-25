import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";

import { AppComponent } from './app.component';
import { ProductList } from './shop/productList.component';
import { Cart } from "./shop/cart.component";
import { DataService } from "./shared/dataService";

import { RouterModule } from "@angular/router";
import { Shop } from "./shop/shop.component";
import { Checkout } from "./checkout/checkout.component";
import { Login } from "./login/login.component";

let routes = [
    { path: "", component: Shop },
    { path: "checkout", component: Checkout},
    { path: "login", component: Login}
    ];

@
NgModule({
    declarations: [
        AppComponent,
        ProductList,
        Cart,
        Shop,
        Checkout,
        Login
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        RouterModule.forRoot(routes,
            {
                useHash: true,
                enableTracing: false //For debugging of routes
            })
  ],
    providers: [
        DataService
        ],
  bootstrap: [AppComponent]
})
export class AppModule { }
