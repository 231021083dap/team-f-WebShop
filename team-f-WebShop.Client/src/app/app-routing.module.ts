import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminProductComponent } from './components/ADMIN/admin-product/admin-product.component';
import { ProductDetailsComponent } from './components/ADMIN/product-details/product-details.component';

const routes: Routes = [
  {path:'', component: AdminProductComponent},
  {path:'ADMIN/product-details', component: ProductDetailsComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
