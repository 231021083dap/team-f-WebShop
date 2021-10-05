import { Component, OnInit } from '@angular/core';
import { category } from '../models';
import { CategoryService } from '../category.service';

@Component({
  selector: 'app-categorypage',
  templateUrl: './categorypage.component.html',
  styleUrls: ['./categorypage.component.css']
})
export class CategorypageComponent implements OnInit {

  categorys: category[] = [];

  constructor(private categoryService:CategoryService) { }

  ngOnInit(): void {
    this.categoryService.getCategorys()
      .subscribe(a => this.categorys = a);
  }

}
