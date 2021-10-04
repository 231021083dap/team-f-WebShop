import { Component, OnInit } from '@angular/core';
import { Product } from '../_models/product';
import { ProductPillGeneratorService } from '../_services/product-pill-generator.service';
@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {

  constructor(private productPillGeneratorService: ProductPillGeneratorService) { }

  ngOnInit(): void {

    this.productPillGeneratorService.getProducts('home-page-body', 'getThreeNewestProducts', {});
  }
}


