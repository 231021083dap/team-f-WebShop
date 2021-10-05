import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategorypageComponent } from './categorypage/categorypage.component';
import { CategoryComponent } from './Admin/category/category.component';
import { FrontpageComponent } from './frontpage/frontpage.component';

const routes: Routes = [
  {path:'', component: CategorypageComponent},
  {path:'admin/categorys', component:CategoryComponent},
  {path:'frontpages', component: FrontpageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
