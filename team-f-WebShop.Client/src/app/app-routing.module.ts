import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminProductComponent } from './components/ADMIN/admin-product/admin-product.component';

const routes: Routes = [
  {path:'', component: AdminProductComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
