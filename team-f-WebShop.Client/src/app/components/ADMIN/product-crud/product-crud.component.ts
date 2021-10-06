import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models';
import { ProductService } from 'src/app/services/product/product.service';

@Component({
  selector: 'app-product-crud',
  templateUrl: './product-crud.component.html',
  styleUrls: ['./product-crud.component.css']
})
export class ProductCRUDComponent implements OnInit {

  products: Product[] = [];
  product: Product = { productId: 0, name: '', price: 0, quantity: 0, description: '' };

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
  


  editProduct(product: Product): void {
    this.product = product;
  }


  deleteProduct(product: Product): void {
    if (confirm('Are you sure about that?')) {
      this.productService.deleteProduct(product.productId)
        .subscribe(() => {this.getAllProducts()});
    }
  }


  cancel(): void {
    this.product = { productId: 0, name: '', price: 0, quantity: 0, description: '' };
  }


  save(): void {
    console.log(this.product)
    if (this.product.productId == 0) {
      this.productService.addProductService(this.product)
        .subscribe(a => {
          this.products.push(a)
          this.cancel();
        });
    } else {
      this.productService.updateProductService(this.product.productId, this.product)
        .subscribe(() => {this.cancel()})
    }
  }
}
