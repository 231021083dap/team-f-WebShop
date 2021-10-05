import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CategorypageComponent } from './categorypage/categorypage.component';
import { CategoryComponent } from './Admin/category/category.component';
import { FormsModule } from '@angular/forms';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { FooterComponent } from './components/shared/footer/footer.component';
import { NavigationBarComponent } from './components/shared/navigation-bar/navigation-bar.component';
import { FormsModule } from '@angular/forms';
import { ProductCRUDComponent } from './components/ADMIN/product-crud/product-crud.component';
import { FrontPageComponent } from './components/Pages/front-page/front-page.component';
import { ProductPageComponent } from './components/Pages/product-page/product-page.component';
import { ProductCategoryPageComponent } from './components/Pages/product-category-page/product-category-page.component';
import { ProductComponent } from './components/InstanceOf/product/product.component';

@NgModule({
  declarations: [
    AppComponent,
    CategorypageComponent,
    CategoryComponent,
    FrontpageComponent
    HeaderComponent,
    FooterComponent,
    NavigationBarComponent,
    ProductCRUDComponent,
    FrontPageComponent,
    ProductPageComponent,
    ProductCategoryPageComponent,
    ProductComponent
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
