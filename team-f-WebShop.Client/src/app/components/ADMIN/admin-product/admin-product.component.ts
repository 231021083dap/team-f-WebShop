import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models';
import { ProductService } from 'src/app/services/product/product.service';


@Component({
  selector: 'app-admin-product',
  templateUrl: './admin-product.component.html',
  styleUrls: ['./admin-product.component.css']
})
export class AdminProductComponent implements OnInit {

  products: Product[] = []; 

  constructor(
    private productService: ProductService
   ) { }

  ngOnInit(): void {
    this.productService.getAllProducts()
      .subscribe(a => this.products = a);
    // push method with object of 
    // similar to "new product = ..." 
    //this.products.push({ ProductId: 1, Name: 'PC monitor', Price: 15400, Quantity: 2, Desciption: 'A gaming PC Monitor' } as Product);
    //this.products.push({ ProductId: 2, Name: 'Desktop PC i5', Price: 18300, Quantity: 6, Desciption: 'Prequildt gaming PC' } as Product);

  }
}
