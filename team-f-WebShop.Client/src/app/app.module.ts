import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CategorypageComponent } from './categorypage/categorypage.component';
import { CategoryComponent } from './Admin/category/category.component';
import { FormsModule } from '@angular/forms';
import { FrontpageComponent } from './frontpage/frontpage.component';

@NgModule({
  declarations: [
    AppComponent,
    CategorypageComponent,
    CategoryComponent,
    FrontpageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
