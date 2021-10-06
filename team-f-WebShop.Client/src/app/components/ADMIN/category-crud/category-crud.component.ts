import { Component, OnInit } from '@angular/core';
import { category } from 'src/app/models';
import { CategoryService } from 'src/app/services/category/category.service';

@Component({
  selector: 'app-category-crud',
  templateUrl: './category-crud.component.html',
  styleUrls: ['./category-crud.component.css']
})
export class CategoryCrudComponent implements OnInit {
  
  categorys: category[] = [];
  category:category = {id:0, categoryName:''};

  constructor(private CategoryService:CategoryService) { }

  ngOnInit(): void {
    this.getCategorys();
  }

  getCategorys():void{
    this.CategoryService.getCategorys()
        .subscribe(a => this.categorys = a);
  }

  edit(category:category):void{
    this.category = category;
  }

  delete(category:category):void{
    if(confirm('Er du sikker på du vil slette?')){
      this.CategoryService.deleteCategory(category.id)
      .subscribe(() => {this.getCategorys()});
    }
  }

  cancel(): void{
     this.category = {id:0, categoryName:''};
  }

  save():void{
    if(this.category.id == 0){
      this.CategoryService.addCategory(this.category)
        .subscribe(a => {
          this.categorys.push(a);
          this.cancel();
        });
    }
    else
    {
      this.CategoryService.updateCategory(this.category.id, this.category)
      .subscribe(() => {this.cancel()})
    }

  }

}
