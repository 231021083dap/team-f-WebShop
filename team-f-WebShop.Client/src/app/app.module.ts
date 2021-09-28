import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { FooterComponent } from './components/shared/footer/footer.component';
import { NavigationBarComponent } from './components/shared/navigation-bar/navigation-bar.component';
import { AdminProductComponent } from './components/ADMIN/admin-product/admin-product.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    NavigationBarComponent,
    AdminProductComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule   //similar to add.scoped in api
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
