import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategorypageComponent } from './categorypage/categorypage.component';
import { CategoryComponent } from './Admin/category/category.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { ProductCRUDComponent } from './components/ADMIN/product-crud/product-crud.component';
import { FrontPageComponent } from './components/Pages/front-page/front-page.component';
import { ProductPageComponent } from './components/Pages/product-page/product-page.component';


const routes: Routes = [
  {path:'', component: FrontPageComponent},
  {path:'ADMIN/product-crud', component: ProductCRUDComponent},
  {path:'ADMIN/product-page', component: ProductPageComponent},
  {path:'admin/categorys', component:CategoryComponent},
  {path:'frontpages', component: FrontpageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
