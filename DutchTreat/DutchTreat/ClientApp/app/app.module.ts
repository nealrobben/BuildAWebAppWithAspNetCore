import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { ProductList } from './shop/productList.component';
import { DataService } from "./shared/dataService";

@NgModule({
  declarations: [
      AppComponent,
      ProductList
  ],
  imports: [
    BrowserModule
  ],
    providers: [
        DataService
        ],
  bootstrap: [AppComponent]
})
export class AppModule { }
