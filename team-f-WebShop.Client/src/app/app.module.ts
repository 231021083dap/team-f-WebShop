import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';

import { HeaderComponent } from './components/shared/header/header.component';
import { FooterComponent } from './components/shared/footer/footer.component';
import { NavigationBarComponent } from './components/shared/navigation-bar/navigation-bar.component';
import { ProductCRUDComponent } from './components/ADMIN/product-crud/product-crud.component';
import { FrontPageComponent } from './components/Pages/front-page/front-page.component';
import { CategoryCrudComponent } from './components/ADMIN/category-crud/category-crud.component';
import { ProductPageComponent } from './components/Pages/product-page/product-page.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    NavigationBarComponent,
    ProductCRUDComponent,
    FrontPageComponent,
    CategoryCrudComponent,
    ProductPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,   //similar to add.scoped in api
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
