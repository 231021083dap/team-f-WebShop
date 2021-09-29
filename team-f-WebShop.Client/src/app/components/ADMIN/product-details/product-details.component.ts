import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models';
import { ProductService } from 'src/app/services/product/product.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {

  products: Product[] = [];
  product: Product = { productId: 0, name: '', price: 0, quantity: 0, description: '' };

  constructor(
    private productService: ProductService
  ) { }

  ngOnInit(): void {

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
      // Delete
    }
  }

  cancel(): void {
    this.product = { productId: 0, name: '', price: 0, quantity: 0, description: '' };
  }

  save(): void {
    // save...
  }

}
