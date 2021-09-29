import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from 'src/app/models'
import { Observable } from 'rxjs'


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = "https://localhost:5001/api/Product";


  constructor(
    private http: HttpClient
  ) { }


  getAllProductsService(): Observable<Product[]>{
    return this.http.get<Product[]>(this.apiUrl);
  }


}
