import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models';
import { ProductService } from 'src/app/services/product/product.service';

@Component({
  selector: 'app-front-page',
  templateUrl: './front-page.component.html',
  styleUrls: ['./front-page.component.css']
})
export class FrontPageComponent implements OnInit {

  products: Product[] = [];

  constructor(
    private productService: ProductService
  ) { }

  ngOnInit(): void {
    this.getAllProducts();
  }


  getAllProducts(): void {
    this.productService.getAllProductsService()
      .subscribe(a => this.products = a);
  }

}
