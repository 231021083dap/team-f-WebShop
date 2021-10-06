import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryCrudComponent } from './components/ADMIN/category-crud/category-crud.component';
import { ProductCRUDComponent } from './components/ADMIN/product-crud/product-crud.component';
import { FrontPageComponent } from './components/Pages/front-page/front-page.component';
import { ProductPageComponent } from './components/Pages/product-page/product-page.component';


const routes: Routes = [

  {path:'', component: FrontPageComponent},     //FRONT PAGE

  {path:'ADMIN/product-crud', component: ProductCRUDComponent},    //PRODUCT ADMIN PAGE

  {path:'ADMIN/category-crud', component: CategoryCrudComponent},  //CATEGORY ADMIN PAGE

  {path:'product/:productId', component: ProductPageComponent}  //SPECIFICK PRODUCT PAGE

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }