import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from 'src/app/services/product/product.service';
import { Location } from '@angular/common';
import { Product } from 'src/app/models';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.css']
})
export class ProductPageComponent implements OnInit {

  productId: number = 0;
  product: Product = { productId: 0, name: '', price: 0, quantity: 0, description: '' };

  constructor(
    private route:ActivatedRoute, 
    private productService: ProductService,
    private location:Location  
  ) { }

  ngOnInit(): void {
    this.productId = (this.route.snapshot.paramMap.get('productId') || 0) as number; 
    
    //RETURN to home if data incorrect.
    if(this.productId == null || this.productId == 0){ 
      this.location.go('');  
    }
    else{
      this.getProduct(); 
    }
  }


  getProduct(): void { 
    this.productService.getProductByIdService(this.productId) 
    .subscribe(Product => (Product != null ? this.product = Product : this.location.go('/')) 
    )
  }

}
