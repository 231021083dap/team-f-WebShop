import { Component, OnInit } from '@angular/core';
import { category } from '../models';

@Component({
  selector: 'app-categorypage',
  templateUrl: './categorypage.component.html',
  styleUrls: ['./categorypage.component.css']
})
export class CategorypageComponent implements OnInit {

  categorys: category[] = [];

  constructor() { }

  ngOnInit(): void {
    this.categorys.push({ id:1, categoryName:'Computer'} as category);
    this.categorys.push({ id:2, categoryName:'Screen'} as category);
  }

}
